using LADXRandomizer.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace LADXRandomizer.IO
{
    static class RandomizerIO
    {
        public static void WriteRom(WarpData warpData, uint seed, RandomizerSettings settings, string filename)
        {
            if (!Directory.Exists("Output"))
                Directory.CreateDirectory("Output");

            string file = "Output\\LADX " + filename + ".gbc";

            int selectedROM = settings["SelectedROM"].Index;

            byte[] rom = Resources.romJ10;
            if (selectedROM == (int)Rom.U10)
                rom = Resources.romU10;
            else if (selectedROM == (int)Rom.U12)
                rom = Resources.romU12;

            var warps = new List<Warp>();
            warps.AddRange(warpData.Overworld1);
            warps.AddRange(warpData.Overworld2);

            using (var output = new FileStream(file, FileMode.OpenOrCreate))
            {
                output.Write(rom, 0, 1048576);

                //write the updated warps
                foreach (var warp in warps)
                {
                    var warpValue = warp.WarpValue;
                    if (warpValue == 0)
                        warpValue = 0xE000000000;

                    var data = new List<byte>(BitConverter.GetBytes(warpValue).Reverse());
                    data.RemoveRange(0, 3);

                    output.Seek(warp.Address, SeekOrigin.Begin);
                    output.Write(data.ToArray(), 0, 5);

                    if (warp.Address2 != 0)
                    {
                        output.Seek(warp.Address2, SeekOrigin.Begin);
                        output.Write(data.ToArray(), 0, 5);
                    }
                }

                //cover tal tal heights pit warp
                if (settings["CoverPitWarp"].Enabled)
                {
                    output.Seek(0x24D74, SeekOrigin.Begin);
                    output.Write(BitConverter.GetBytes(0x03), 0, 1);
                }

                //debug mode
                if (settings["DebugMode"].Enabled)
                {
                    output.Seek(0x03, SeekOrigin.Begin);
                    output.Write(BitConverter.GetBytes(0x01), 0, 1);
                }

                //write header
                byte checksum = 0xAD;
                if (selectedROM == (int)Rom.U10)
                    checksum = 0xAC;
                else if (selectedROM == (int)Rom.U12)
                    checksum = 0xAA;

                output.Seek(0x134, SeekOrigin.Begin);
                output.Write(GetHeader(seed, ref checksum), 0, 11);
                output.Seek(0x14D, SeekOrigin.Begin);
                output.Write(new[] { checksum }, 0, 1);
            }
        }

        public static void SaveLog(RandomizerLog log, string filename)
        {
            if (!Directory.Exists("Output"))
                Directory.CreateDirectory("Output");

            string file = "Output\\Log " + filename + ".txt";

            using (var output = new StreamWriter(file))
                output.Write(log.FullLog);
        }

        private static byte[] GetHeader(uint seed, ref byte checksum)
        {
            string str = "V" + Assembly.GetExecutingAssembly().GetName().Version.ToString(1) + " " + seed.ToString("X8");
            byte[] header = Encoding.UTF8.GetBytes(str);
            
            foreach (byte i in header)
            {
                if (checksum - i >= 0)
                    checksum -= i;
                else
                    checksum = (byte)(checksum - i + 0x100);
            }
            
            return header;
        }
    }
}

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
        public static void WriteRom(Randomizer randomizer, RandomizerSettings options, string filename)
        {
            if (!Directory.Exists("Output"))
                Directory.CreateDirectory("Output");

            string file = "Output\\LADX " + filename + ".gbc";

            int selectedROM = options["SelectedROM"].Index;

            byte[] rom = Resources.romJ10;
            if (selectedROM == (int)Rom.U10)
                rom = Resources.romU10;
            else if (selectedROM == (int)Rom.U12)
                rom = Resources.romU12;

            var warps = new List<Warp>();
            warps.AddRange(randomizer.warpData.Overworld1);
            warps.AddRange(randomizer.warpData.Overworld2);

            using (var output = new FileStream(file, FileMode.OpenOrCreate))
            {
                output.Write(rom, 0, 1048576);

                //write the updated warps
                foreach (var warp in warps)
                {
                    var data = new List<byte>(BitConverter.GetBytes(warp.Destination).Reverse());
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
                if (options["CoverPitWarp"].Enabled)
                {
                    output.Seek(0x24D74, SeekOrigin.Begin);
                    output.Write(BitConverter.GetBytes(0x03), 0, 1);
                }

                if (options["DebugMode"].Enabled)
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
                output.Write(GetHeader(randomizer.Seed, ref checksum), 0, 11);
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

        private static byte[] GetHeader(string seed, ref byte checksum)
        {
            string str = "V" + Assembly.GetExecutingAssembly().GetName().Version.ToString(1) + " " + seed;
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

        private static XmlElement ToElement<T>(XmlDocument document, String name, T value)
        {
            var element = document.CreateElement(name);
            element.InnerText = value.ToString();
            return element;
        }
    }
}

using LADXRandomizer.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private static readonly ReadOnlyCollection<Map> FixedMapEdits = new List<Map>
        {
            //OW
            new Map(0x1F, "3A 3A E1 3A 3A 3A 3F E1 3F E5 03 09 03 03 DD 3A 3F 03 38 E5 48 E0 49 2F 4E 3A 3F 03 38 E5 03 03 DD 3A 3F E1 3B 03 38 E5 2F 2F 4E E1 3B 03 03 03 38 E5 3A 3A 3B 03 03 03 03 03 38 E5 2F 2F 2F 2F 2F 2F 2F 2F 4E E5 3A 3A 3A 3A 3A 3A 3A 3A 3F E5", "0B 03 C8 09 E5 86 00 3A 02 E1 E1 0A 82 70 7C C3 06 3F 07 E1 E1 0A 8C 60 7C 08 3F 84 10 03 11 09 14 38 C2 15 3A C5 18 38 84 20 2F 20 48 21 E0 22 49 24 4E 82 30 03 32 38 C2 33 3A 34 3F 35 E1 E1 0A 87 60 7C 36 3B 82 40 2F 42 4E 43 E1 E1 1F FB 50 7C 44 3B 82 50 3A 52 3B 88 60 2F 68 4E 88 70 3A 78 3F 14 DD 32 DD FE"),
            new Map(0x86, "27 28 38 3A 3A 3A 3A 3A 3A 3A 2F 2F 4E 0A 0A 5C 5C 5C 04 04 3A 3A 3F 0A 0A F9 F6 E8 04 04 3A E1 3B 0A E8 F7 0B 0B 0B 0B 26 0A 0A 0A 0B 0B E8 04 E8 04 28 0A 0A 0A E8 44 FD 0A F6 04 26 0A 0A FF 0B 0A E8 F7 FC 25 28 0A 0A 0A 0B F8 04 04 FE 27", "03 04 87 03 3A 02 38 82 10 2F 12 4E 82 20 3A 82 30 3A 22 3F 32 3B 82 13 0A 82 23 0A 33 0A 82 43 0A 85 50 0A 57 0A 83 60 0A 65 0A 84 70 0A 69 F5 37 44 55 44 83 15 5C F0 F5 27 E8 34 E8 46 E8 48 E8 54 E8 66 E8 C3 50 04 C2 4F F5 82 41 0A 84 36 0B 82 44 0B C2 64 0B 31 E1 E1 11 F4 40 7C FE"),
            new Map(0x87, "3A 3A E1 3A 3A 3B 27 28 25 26 F7 44 F9 FF FA FD F6 5C 27 28 04 FB FF 44 25 26 44 F6 5C 25 0B 0B 44 0A 27 28 0A 44 5C 27 04 0B 0B 44 0A 0A 44 F8 04 25 04 04 0B FB 44 44 FC 04 04 27 26 04 0B 0B 0B 44 F8 04 25 26 28 04 04 F7 0B F8 04 6E 27 28", "03 04 C2 29 F5 F6 F5 08 F5 85 00 3A 05 3B 24 F5 33 0A 36 0A 82 44 0A 11 44 23 44 26 44 32 44 37 44 43 44 46 44 54 44 55 44 64 44 65 44 17 5C C2 28 5C 6F F5 68 F5 82 30 0B 82 41 0B C2 52 0B 82 63 0B 74 0B 77 6E 02 E1 E1 1F F3 50 7C FE"),
            new Map(0x8D, "3A 3A 3A 3A 3A 3A 3A 3A 3A 3A 3A 3A 3A E1 3A 3A 3A 3A 3A 3A 3A 3A 46 04 04 47 3A 3A 3A 3A 0E 0E 37 04 04 38 0E 0E 0E 0E 0E 0E 37 04 04 32 2C E0 2C 2C 0E 0E 37 04 04 04 04 04 04 04 0E 0E 33 2F 48 E0 49 2F 3C 6E 0E 0E 0E 0E 0E 0E 0E 0E 37 04", "03 0E 8A 00 3A 8A 10 3A 8A 20 3A C4 22 37 C4 23 04 C4 24 04 C2 25 38 45 32 84 46 2C 47 E0 62 33 85 63 2F 64 48 65 E0 66 49 68 3C 78 37 85 55 04 C2 69 04 69 6E 22 DE 25 DD 13 E1 E1 1F AC 50 7C FE"),
            new Map(0xF4, "3A 3A 3F 08 08 08 08 08 08 37 3A E1 3B 08 08 24 08 08 08 37 08 08 08 08 08 08 08 08 08 37 08 08 08 08 C8 08 08 08 24 37 08 24 08 08 08 08 08 08 08 37 1E 1E 1E 1E 1E 1E 1E 1E 1E 37 1F 1F 1F 1F 1F 1F 1F 1F 1F 33 1F 1F 1F 1F 1F 1F 1F 1F 1F 1F", "02 08 8A 50 1E 8A 60 1F 8A 70 1F 82 00 3A 82 10 3A 02 3F 12 3B C6 09 37 69 33 15 24 34 C8 38 24 41 24 11 E1 E1 1F F5 48 7C FE"),

            //UW
            new Map(1, 0xE7, "0C 3D 00 DF 01 25 02 29 03 25 05 21 06 26 07 2A 08 26 C8 09 DF 10 25 11 29 12 25 13 29 16 2A 17 26 C3 18 24 21 25 22 29 27 24 C2 31 23 35 2C 36 22 37 28 44 2C 45 28 46 2C 47 22 48 28 51 27 52 98 53 22 54 28 55 2C 56 28 82 57 DF 63 1B 64 2C 65 28 66 DF 82 72 1B 74 24 75 DF 78 DF 04 A3 E0 00 2F 18 70 FE"),
            new Map(1, 0xEA, "0C 9D 8A 00 DF 83 06 05 82 11 DF 83 15 DF 8A 20 DF 82 25 0D 88 30 21 38 26 39 DF 85 40 1B 45 AE 48 2A 49 26 86 50 22 56 2B C2 59 24 86 60 DF 62 05 63 0D 66 23 82 71 DF 83 73 05 76 27 82 77 22 79 28 68 CB E0 00 2D 58 50 FE"),
            new Map(1, 0xEE, "0C 9D 02 05 03 DF 04 25 84 05 21 09 26 10 05 82 11 DF 13 25 14 29 C7 19 24 20 25 82 21 21 23 29 C3 30 23 35 2C 36 22 37 2B 45 24 C4 46 DF C4 47 23 54 2C 55 28 60 27 82 61 22 63 98 64 28 65 2C 66 2B 82 70 22 72 2B 75 24 76 23 31 CB E0 00 03 48 50 FE"),
            new Map(1, 0xFE, "00 9D 82 00 DF C3 02 23 C2 05 24 C2 06 23 C4 07 23 C6 09 24 11 DF C3 14 05 20 05 25 2A 26 29 C3 28 DF 30 DF 32 27 33 2B 83 40 DF C3 43 23 45 25 46 21 47 29 52 05 C2 55 23 82 57 DF 60 DF 64 DF 68 2C 69 28 72 DF 73 27 74 2B 75 27 78 28 79 DF 76 FD E0 00 13 58 10 FE"),
            new Map(2, 0x7A, "04 95 00 25 88 01 21 09 26 10 29 11 AC 18 AC 19 2A 20 AC 29 AC 21 25 22 21 23 26 26 25 27 21 28 26 29 AC 30 25 31 29 33 24 36 23 38 2A 39 21 C4 40 23 43 2A 82 44 97 46 29 51 2C 52 22 53 2B 61 2A 62 26 63 27 64 98 65 2B 69 2C 72 24 75 27 84 76 22 79 28 32 01 37 B0 82 41 B0 57 AE 68 AE FE"),
            new Map(2, 0x7C, "04 95 C2 00 23 02 2A 03 26 06 2C 82 07 22 09 2B 10 C9 13 2A 14 21 15 26 16 2A 82 17 21 19 29 20 27 21 2B 25 2A 26 21 27 26 82 28 DF 30 DF 31 27 32 2B 37 2A 38 21 39 26 40 0D 41 DF 42 27 43 2B 49 2A 82 50 0D 52 DF 53 27 54 22 55 2B 60 00 82 61 0D 82 63 DF 65 27 84 66 22 67 C8 82 70 00 83 72 0D 85 75 DF 35 AE 36 AF 46 B0 82 47 AE FE"),
            new Map(2, 0x86, "0C 75 03 26 04 DF 05 25 07 26 82 08 DF 13 2A 14 21 15 29 17 2A 18 26 19 DF 20 21 21 26 31 2A 28 2A 29 26 32 21 33 26 43 2A 82 44 21 46 26 82 54 AF 56 2A 57 26 82 60 22 62 2B 84 63 AF 82 64 01 C2 67 24 70 2B 72 23 84 73 01 FE"),
            new Map(2, 0x87, "0C 15 02 27 03 2B C5 04 01 C5 05 01 06 2C 07 28 C3 13 23 C2 16 24 21 A6 28 A6 29 2A 30 29 36 2A 82 37 21 39 26 40 25 82 41 21 43 29 44 B0 83 46 AF 84 55 B0 60 27 82 61 22 63 2B 68 2C 69 28 C2 70 22 72 2B 73 27 78 28 79 DF 75 FD E0 00 1F 58 40 FE"),
            new Map(2, 0x90, "0C 05 49 4A C8 00 00 01 00 82 02 0D 86 04 DF 07 0D 84 11 0D 12 0D 14 25 83 15 21 18 26 19 DF C5 21 0D 22 DF 23 25 24 29 82 25 AE 27 AF 28 2A 29 26 32 25 33 29 34 AE 37 B0 38 AF C2 42 23 43 AF 48 01 51 DF 53 B0 54 AF 57 AF 58 B0 62 27 63 2B 64 B0 82 65 AE 67 B0 68 2C 69 28 82 71 0D 73 27 78 28 79 DF 45 CB E1 0A 96 28 20 FE"),
            new Map(2, 0x95, "0C 95 8A 00 21 C3 10 0D 20 A6 83 21 A7 25 A7 26 A6 27 2C 82 28 22 33 A7 C3 34 A7 C2 37 24 40 2B 82 48 21 47 2A 50 23 60 27 61 2B 82 64 A6 82 67 0D 70 DF 71 27 88 72 22 76 FD E0 00 B8 58 30 FE"),
            new Map(2, 0xED, "04 95 C8 00 DF 01 DF 02 25 86 03 21 09 26 11 25 12 29 13 25 14 21 15 26 19 2A C3 21 23 22 25 23 29 25 2A 26 98 27 26 32 23 37 24 42 27 43 2B 45 2C 46 22 47 28 48 2C 49 22 51 27 52 2B 53 27 54 22 55 28 56 2C 57 22 58 28 59 DF 61 DF 62 27 83 63 22 66 28 83 67 DF 89 71 DF 34 BE E1 0A EC 68 30 FE"),
            new Map(2, 0xF1, "0C 05 82 00 DF 02 25 05 29 06 25 10 DF 11 25 12 29 16 23 82 17 1B 20 25 21 29 24 25 25 98 26 29 82 27 1B 33 25 34 29 84 35 1B 43 23 85 44 1B 51 25 52 21 53 29 85 54 1B 60 29 61 23 87 62 1B 70 0D 71 27 78 48 23 CB E1 0A F3 58 40 FE"),
        }.AsReadOnly();

        private static readonly List<(int World, int Index, int PointerOffset)> alternateMaps = new List<(int, int, int)>
        {
            (0, 0x06, 0x3202),
            (0, 0x0E, 0x31D2),
            (0, 0x1B, 0x3212),
            (0, 0x2B, 0x3222),
            (0, 0x79, 0x31F2),
            (0, 0x8C, 0x31E2),
            (1, 0xF5, 0x31A6),
        };

        public static void WriteRom(WarpData warpData, int[] mapEdits, uint seed, RandomizerSettings settings, string filename)
        {
            if (!Directory.Exists("Output"))
                Directory.CreateDirectory("Output");

            string file = "Output\\LADX " + filename + ".gbc";

            var rom = new ROMStream();

            int selectedROM = settings["SelectedROM"].Index;
            if (selectedROM == (int)Rom.J10)
                rom.AddRange(Resources.romJ10);
            else if (selectedROM == (int)Rom.U10)
                rom.AddRange(Resources.romU10);
            else if (selectedROM == (int)Rom.U12)
                rom.AddRange(Resources.romU12);

            //-- set debug mode --//
            if (settings["DebugMode"].Enabled)
                rom.Write(0x03, 0x01);

            //-- implement map warp --//
            rom.Write(0x5806, "00 FA 03 00 A7 F0 CB FE A0 20 38 3E 0B EA 95 DB CD 7D 0C 3E 01 EA 01 D4 3E 10 EA 02 D4 3E A3 EA 03 D4 3E 50 EA 04 D4 3E 60");

            //-- remove bird key cave pit warping --//
            rom.Write(0x917C, new byte[] { 0x3E, 0x00 });

            //-- remove lanmolas pit warp --//
            rom.Write(0xDCF0, new byte[] { 0x3E, 0x00 });
            rom.Write(0x6422D, 0xC9);

            //-- write ROM header --//
            byte checksum = 0xAD;
            if (selectedROM == (int)Rom.U10)
                checksum = 0xAC;
            else if (selectedROM == (int)Rom.U12)
                checksum = 0xAA;

            rom.Write(0x134, GetHeader(seed, ref checksum));
            rom.Write(0x14D, checksum);
            //------------------

            //-- apply map edits --//
            var mapList = new List<Map>(FixedMapEdits);
            mapList.AddRange(GetRandomizedMapEdits(mapEdits));
            foreach (var map in mapList)
            {
                var collisionOffset = map.GetCollisionOffset(rom);
                var oldLength = rom.NextCollisionOffset(collisionOffset) - collisionOffset;

                if (map.OverlayData != null)
                    rom.Replace(map.OverlayOffset, 0x50, map.OverlayData);

                rom.Replace(collisionOffset, oldLength, map.CollisionData);

                var newLength = map.CollisionData.Count();
                if (map.BufferOffset != 0)
                {
                    if (newLength > oldLength)
                        rom.RemoveRange(map.BufferOffset, newLength - oldLength);
                    else
                        for (int i = 0; i < oldLength - newLength; i++)
                            rom.Insert(map.BufferOffset, 0);
                }
                else
                {
                    var pointer = rom[0x241FF] << 8 | rom[0x241FE];
                    var bufferOffset = rom.NextCollisionOffset(0x64000 + pointer);

                    for (int i = 0; i < oldLength - newLength; i++)
                        rom.Insert(bufferOffset, 0);
                }

                rom.ShiftHeader(map.World, map.Index, newLength - oldLength);
            }
            //---------------

            UpdateColourDungeon();
            void UpdateColourDungeon() //fix colour dungeon pointer and header
            {
                //pointer
                var headerOffset = rom.NextCollisionOffset(0x24000 + (rom[0x281FF] << 8 | rom[0x281FE]));
                var pointer = BitConverter.GetBytes((short)(headerOffset - 0x24000));

                rom[0x318F] = pointer[0];
                rom[0x3190] = pointer[1];

                //header
                var shift = headerOffset - 0x2BB77;
                for (int i = 0; i < 0x20; i++)
                {
                    var currentOffset = headerOffset + i * 2;
                    if (rom[currentOffset] + shift > 0xFF)
                    {
                        rom[currentOffset] += (byte)(shift - 0x100);
                        rom[currentOffset + 1]++;
                    }
                    else if (rom[currentOffset] + shift < 0)
                    {
                        rom[currentOffset] += (byte)(shift + 0x100);
                        rom[currentOffset + 1]--;
                    }
                    else
                        rom[currentOffset] += (byte)shift;
                }
            }

            //-- update pointers for the alternate maps --//
            foreach (var map in alternateMaps)
            {
                var headerOffset = 0x24000 + map.World * 0x4000 + map.Index * 2;

                var baseOffset = 0;
                if (map.World == 0 && map.Index >= 0x80)
                    baseOffset = 0x64000;
                else
                    baseOffset = 0x20000 + map.World * 0x4000;
                
                var collisionOffset = rom.NextCollisionOffset(baseOffset + (rom[headerOffset + 1] << 8 | rom[headerOffset]));
                var pointer = BitConverter.GetBytes((short)(collisionOffset - baseOffset));

                rom[map.PointerOffset] = pointer[0];
                rom[map.PointerOffset + 1] = pointer[1];
            }
            //----------------------------------

            //-- find and update all warps --//
            foreach (var warp in warpData)
            {
                var warpValue = 0xE000000000;
                if (warp.WarpValue != 0)
                    warpValue = warp.WarpValue;

                var warpBytes = BitConverter.GetBytes(warpValue).Reverse().ToList();
                warpBytes.RemoveRange(0, 3);

                var searchBytes = BitConverter.GetBytes(warp.DefaultWarpValue).Reverse().ToList();
                searchBytes.RemoveRange(0, 3);
                searchBytes.RemoveAt(4);

                var headerOffset = 0;
                if (warp.World == 3)
                    headerOffset = 0x24000 + (rom[0x3190] << 8 | rom[0x318F]) + warp.MapIndex * 2;
                else
                    headerOffset = 0x24000 + warp.World * 0x4000 + warp.MapIndex * 2;

                var collisionOffset = 0;
                if (warp.World == 0 && warp.MapIndex >= 0x80)
                    collisionOffset = 0x64000 + (rom[headerOffset + 1] << 8 | rom[headerOffset]);
                else if (warp.World == 3)
                    collisionOffset = 0x24000 + (rom[headerOffset + 1] << 8 | rom[headerOffset]);
                else
                    collisionOffset = 0x20000 + warp.World * 0x4000 + (rom[headerOffset + 1] << 8 | rom[headerOffset]);

                var warpOffset = rom.GetWarpOffset(collisionOffset, searchBytes);

                if (warpOffset != 0)
                    rom.Write(warpOffset, warpBytes);

                if (warp.MapIndex2 != 0)
                {
                    if (warp.World == 3)
                        headerOffset = 0x24000 + (rom[0x3190] << 8 | rom[0x318F]) + warp.MapIndex2 * 2;
                    else
                        headerOffset = 0x24000 + warp.World * 0x4000 + warp.MapIndex2 * 2;

                    if (warp.World == 0 && warp.MapIndex2 >= 0x80)
                        collisionOffset = 0x64000 + (rom[headerOffset + 1] << 8 | rom[headerOffset]);
                    else if (warp.World == 3)
                        collisionOffset = 0x24000 + (rom[headerOffset + 1] << 8 | rom[headerOffset]);
                    else
                        collisionOffset = 0x20000 + warp.World * 0x4000 + (rom[headerOffset + 1] << 8 | rom[headerOffset]);

                    warpOffset = rom.GetWarpOffset(collisionOffset, searchBytes);

                    if (warpOffset != 0)
                        rom.Write(warpOffset, warpBytes);
                }

                if (alternateMaps.Exists(x => x.Index == warp.MapIndex))
                {
                    collisionOffset = rom.NextCollisionOffset(collisionOffset);
                    warpOffset = rom.GetWarpOffset(collisionOffset, searchBytes);

                    if (warpOffset != 0)
                        rom.Write(warpOffset, warpBytes);
                }
            }
            //---------------------------

            using (var stream = new FileStream(file, FileMode.OpenOrCreate))
            {
                stream.Write(rom.ToArray(), 0, 1048576);
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

        private static List<Map> GetRandomizedMapEdits(int[] mapEdits)
        {
            var list = new List<Map>();

            //0x03/0x13
            if (mapEdits[0] == 0 && mapEdits[1] == 0)
            {
                list.Add(new Map(0x03, "80 4D 4D 81 00 80 4D 4D 81 00 37 03 03 4C 4D 4B 03 03 38 00 37 03 03 03 03 03 03 03 38 EF 37 03 03 3D 2F 3C 03 03 38 5D 2E 2F 2F 4E E1 2E 2F 2F 4E 38 39 3A 3A 3B 03 39 3A 3A 3B 38 03 03 03 03 03 03 C8 03 03 7A 2F 2F 2F 2F 2F 2F 2F 2F 2F 4E", "0B 03 00 80 82 01 4D 03 81 04 00 05 80 82 06 4D 08 81 09 00 C3 10 37 87 11 03 87 21 03 87 31 03 13 4C 14 4D 15 4B C3 18 38 40 2E 82 41 2F 43 4E 33 3D 34 2F 35 3C 44 3A 45 2E 82 46 2F 48 4E 50 39 82 51 3A 53 3B 55 39 82 56 3A 58 3B 39 5D C4 49 38 69 7A 78 09 19 00 29 EF 66 C8 8A 70 2F 79 4E 44 E1 E1 1F EE 18 40 FE"));
                list.Add(new Map(0x13, "3A 3A 3A 3A 3A E1 3A 3A 3A 3B C8 03 C8 03 03 03 C8 03 C8 03 03 03 C8 03 C8 03 C8 03 03 C8 03 C8 03 03 03 03 C8 03 03 C8 C8 03 03 C8 03 C8 03 03 C8 03 C8 03 03 03 03 03 03 03 03 C8 03 03 03 03 C8 03 03 C8 09 C8 2F 2F 2F 2F 2F 2F 2F 2F 2F 2F", "0B 03 8A 00 3A 09 3B 12 09 68 09 8A 70 2F 10 C8 C2 12 C8 C3 16 C8 18 C8 24 C8 C2 29 C8 31 C8 C2 40 C8 43 C8 45 C8 48 C8 C2 59 C8 64 C8 67 C8 05 E1 E1 1F FE"));
            }
            else if (mapEdits[0] == 1 && mapEdits[1] == 0)
            {
                list.Add(new Map(0x03, "80 4D 4D 81 00 80 4D 4D 81 00 37 03 03 4C 4D 4B 03 03 38 00 37 03 03 03 03 03 03 03 38 EF 37 03 03 3D 2F 3C 03 03 38 5D 2E 2F 2F 4E E1 2E 2F 2F 4E 38 39 3A 3A 3B 03 39 3A 3A 3B 38 03 03 03 03 03 03 C8 03 03 7A 2F 2F 48 E0 49 2F 2F 2F 2F 4E", "0B 03 00 80 82 01 4D 03 81 04 00 05 80 82 06 4D 08 81 09 00 C3 10 37 87 11 03 87 21 03 87 31 03 13 4C 14 4D 15 4B C3 18 38 40 2E 82 41 2F 43 4E 33 3D 34 2F 35 3C 44 3A 45 2E 82 46 2F 48 4E 50 39 82 51 3A 53 3B 55 39 82 56 3A 58 3B 39 5D C4 49 38 69 7A 78 09 19 00 29 EF 66 C8 8A 70 2F 72 48 73 E0 74 49 79 4E 44 E1 E1 1F EE 18 40 FE"));
                list.Add(new Map(0x13, "3A 3A 3A E0 3A E1 3A 3A 3A 3B C8 03 C8 03 03 03 C8 03 C8 03 03 03 C8 03 C8 03 C8 03 03 C8 03 C8 03 03 03 03 C8 03 03 C8 C8 03 03 C8 03 C8 03 03 C8 03 C8 03 03 03 03 03 03 03 03 C8 03 03 03 03 C8 03 03 C8 09 C8 2F 2F 2F 2F 2F 2F 2F 2F 2F 2F", "0B 03 8A 00 3A 03 E0 09 3B 12 09 68 09 8A 70 2F 10 C8 C2 12 C8 C3 16 C8 18 C8 24 C8 C2 29 C8 31 C8 C2 40 C8 43 C8 45 C8 48 C8 C2 59 C8 64 C8 67 C8 05 E1 E1 1F FE"));
            }
            else if (mapEdits[0] == 0 && mapEdits[1] == 1)
            {
                list.Add(new Map(0x03, "80 4D 4D 81 00 80 4D 4D 81 00 37 03 03 4C 4D 4B 03 03 38 00 37 03 03 03 03 03 03 03 38 EF 37 03 03 3D 2F 3C 03 03 38 5D 2E 2F 2F 4E E1 2E 2F 2F 4E 38 39 3A 3A 3B 03 39 3A 3A 3B 38 03 03 03 03 03 03 C8 03 03 7A 2F 2F 2F 2F 2F 2F 48 E0 49 4E", "0B 03 00 80 82 01 4D 03 81 04 00 05 80 82 06 4D 08 81 09 00 C3 10 37 87 11 03 87 21 03 87 31 03 13 4C 14 4D 15 4B C3 18 38 40 2E 82 41 2F 43 4E 33 3D 34 2F 35 3C 44 3A 45 2E 82 46 2F 48 4E 50 39 82 51 3A 53 3B 55 39 82 56 3A 58 3B 39 5D C4 49 38 69 7A 78 09 19 00 29 EF 66 C8 8A 70 2F 76 48 77 E0 78 49 79 4E 44 E1 E1 1F EE 18 40 FE"));
                list.Add(new Map(0x13, "3A 3A 3A 3A 3A E1 3A E0 3A 3B C8 03 C8 03 03 03 C8 03 C8 03 03 03 C8 03 C8 03 C8 03 03 C8 03 C8 03 03 03 03 C8 03 03 C8 C8 03 03 C8 03 C8 03 03 C8 03 C8 03 03 03 03 03 03 03 03 C8 03 03 03 03 C8 03 03 C8 09 C8 2F 2F 2F 2F 2F 2F 2F 2F 2F 2F", "0B 03 8A 00 3A 07 E0 09 3B 12 09 68 09 8A 70 2F 10 C8 C2 12 C8 C3 16 C8 18 C8 24 C8 C2 29 C8 31 C8 C2 40 C8 43 C8 45 C8 48 C8 C2 59 C8 64 C8 67 C8 05 E1 E1 1F FE"));
            }
            else if (mapEdits[0] == 1 && mapEdits[1] == 1)
            {
                list.Add(new Map(0x03, "80 4D 4D 81 00 80 4D 4D 81 00 37 03 03 4C 4D 4B 03 03 38 00 37 03 03 03 03 03 03 03 38 EF 37 03 03 3D 2F 3C 03 03 38 5D 2E 2F 2F 4E E1 2E 2F 2F 4E 38 39 3A 3A 3B 03 39 3A 3A 3B 38 03 03 03 03 03 03 C8 03 03 7A 2F 2F 48 E0 49 2F 48 E0 49 4E", "0B 03 00 80 82 01 4D 03 81 04 00 05 80 82 06 4D 08 81 09 00 C3 10 37 87 11 03 87 21 03 87 31 03 13 4C 14 4D 15 4B C3 18 38 40 2E 82 41 2F 43 4E 33 3D 34 2F 35 3C 44 3A 45 2E 82 46 2F 48 4E 50 39 82 51 3A 53 3B 55 39 82 56 3A 58 3B 39 5D C4 49 38 69 7A 78 09 19 00 29 EF 66 C8 8A 70 2F 72 48 73 E0 74 49 76 48 77 E0 78 49 79 4E 44 E1 E1 1F EE 18 40 FE"));
                list.Add(new Map(0x13, "3A 3A 3A E0 3A E1 3A E0 3A 3B C8 03 C8 03 03 03 C8 03 C8 03 03 03 C8 03 C8 03 C8 03 03 C8 03 C8 03 03 03 03 C8 03 03 C8 C8 03 03 C8 03 C8 03 03 C8 03 C8 03 03 03 03 03 03 03 03 C8 03 03 03 03 C8 03 03 C8 09 C8 2F 2F 2F 2F 2F 2F 2F 2F 2F 2F", "0B 03 8A 00 3A 03 E0 07 E0 09 3B 12 09 68 09 8A 70 2F 10 C8 C2 12 C8 C3 16 C8 18 C8 24 C8 C2 29 C8 31 C8 C2 40 C8 43 C8 45 C8 48 C8 C2 59 C8 64 C8 67 C8 05 E1 E1 1F FE"));
            }

            //0x07
            if (mapEdits[2] == 0)
            {
                list.Add(new Map(0x07, "00 00 00 00 00 00 00 00 00 00 7C 7D 00 80 4D 4D 4D 4D 4D 81 EF EF EF 37 03 09 03 03 03 38 E5 E5 1D 4B 03 03 03 03 03 7A E5 E5 37 D3 03 3D 2F 2F 2F 4E E5 E5 37 03 03 38 3A 3A 3A 3B E5 E5 37 09 03 38 03 03 03 03 E5 E5 2E 2F 2F 4E E0 3C 03 03", "0B E5 8A 00 00 8A 10 00 85 20 EF 10 7C 11 7D 13 80 85 14 4D 19 81 23 37 85 24 03 25 09 29 38 32 1D 33 4B 85 34 03 39 7A C3 42 37 43 D3 E1 0A EE 78 30 C3 44 03 45 3D 83 46 2F 49 4E 53 03 C2 55 38 83 56 3A 59 3B 63 09 84 66 03 72 2E 82 73 2F 75 4E 76 E0 77 3C 82 78 03 FE"));
            }
            else if (mapEdits[2] == 1)
            {
                list.Add(new Map(0x07, "00 00 00 00 00 00 00 00 00 00 7C 7D 00 80 4D 4D 4D 4D 4D 81 EF EF EF 37 03 09 03 03 03 38 E5 E5 1D 4B 03 03 03 03 03 7A E5 E5 37 D3 03 3D 48 E0 49 4E E5 E5 37 03 03 38 3A E0 3A 3B E5 E5 37 09 03 38 03 03 03 03 E5 E5 2E 2F 2F 4E E0 3C 03 03", "0B E5 8A 00 00 8A 10 00 85 20 EF 10 7C 11 7D 13 80 85 14 4D 19 81 23 37 85 24 03 25 09 29 38 32 1D 33 4B 85 34 03 39 7A C3 42 37 43 D3 E1 0A EE 78 30 C3 44 03 45 3D 46 48 48 49 49 4E 53 03 C2 55 38 83 56 3A C2 47 E0 59 3B 63 09 84 66 03 72 2E 82 73 2F 75 4E 76 E0 77 3C 82 78 03 FE"));
            }

            //0x09/0x19
            if (mapEdits[3] == 0)
            {
                list.Add(new Map(0x09, "00 00 7C 7D 00 00 00 00 00 00 00 7C 1C 1C 7E 7D 00 00 80 4D C8 78 78 EF EF EF EF 78 78 03 79 79 79 7B 7B 7B 7B 79 C8 03 E5 E5 E5 E5 1D 50 50 50 2E 2F 50 50 50 50 4B 03 03 03 3E 3A 03 03 03 03 03 03 09 03 39 3A 2F 2F 2F 3C 03 03 03 03 03 03", "0B 03 8A 00 00 8A 10 00 8A 20 EF 83 20 78 83 30 79 84 33 7B 84 40 E5 44 1D 85 45 50 84 50 50 54 4B 84 70 2F 73 3C 66 09 02 7C 03 7D 11 7C 82 12 1C 14 7E 15 7D 18 80 19 4D 82 27 78 82 37 79 C2 29 03 48 2E 49 2F 58 3E C2 59 3A 68 39 83 70 2F 20 C8 38 C8 FE"));
            }
            else if (mapEdits[3] == 1)
            {
                list.Add(new Map(0x09, "00 00 7C 7D 00 00 00 00 00 00 00 7C 1C 1C 7E 7D 00 00 80 4D C8 78 78 EF EF EF EF 78 78 03 79 79 79 7B 7B 7B 7B 79 C8 03 E5 E5 E5 E5 1D 50 50 50 2E 2F 50 50 50 50 4B 03 03 03 3E 3A 03 03 03 03 03 03 09 03 39 3A 48 E0 49 3C 03 03 03 03 03 03", "0B 03 8A 00 00 8A 10 00 8A 20 EF 83 20 78 83 30 79 84 33 7B 84 40 E5 44 1D 85 45 50 84 50 50 54 4B 84 70 2F 73 3C 66 09 02 7C 03 7D 11 7C 82 12 1C 14 7E 15 7D 18 80 19 4D 82 27 78 82 37 79 C2 29 03 48 2E 49 2F 58 3E C2 59 3A 68 39 70 48 71 E0 72 49 20 C8 38 C8 FE"));
                list.Add(new Map(0x19, "3A E0 3A 37 03 03 03 03 09 03 03 03 03 37 03 03 03 03 03 03 03 03 09 37 03 09 03 3D 2F 2F 03 03 03 2E 2F 2F 2F 4E E1 3A 2F 48 E0 39 3A 3A 3A 3B 1B 1B 1B 1B 1B 1B 1B 1B 1B 1B 1B 1B 1B 3D 2F 2F 2F 2F 2F 2F 2F 2F 2F 4E 3A 3A 3A 3A 3A 3A 3A 3A", "0B 03 83 00 3A 01 E0 C3 03 37 08 09 22 09 25 09 27 3D 82 28 2F 33 2E 83 34 2F 37 4E 38 E1 E1 0A 89 40 7C 39 3A 40 2F 41 48 42 E0 43 39 83 44 3A 47 3B 82 48 1B 8A 50 1B 60 1B 61 3D 88 62 2F 70 2F 71 4E 88 72 3A FE"));
            }

            //0x0E/0x1E
            if (mapEdits[4] == 0 && mapEdits[5] == 0)
            {
                list.Add(new Map(0x0E, "00 00 00 72 74 74 74 73 00 00 7C 7E 7D 72 75 D6 76 73 7C 7D EF EF EF 72 74 74 74 73 EF EF 1D 50 50 72 74 74 74 73 50 50 37 54 20 B6 74 74 74 B7 03 03 37 20 03 03 03 03 09 03 03 09 2E 3C 03 09 03 03 03 03 03 03 3E 2E 2F 2F 2F 2F 2F 2F 2F 2F", "0B 03 25 E1 8A 00 00 8A 10 00 10 7C 11 7E 12 7D 18 7C 19 7D 8A 20 EF 30 1D 89 31 50 C2 40 37 60 2E 61 3C 70 3E 71 2E 88 72 2F 41 54 42 20 51 20 56 09 59 09 63 09 C4 03 72 43 B6 C5 04 74 C5 05 74 C5 06 74 C4 07 73 47 B7 14 75 15 D6 16 76 E1 06 0E 50 7C FE"));
                list.Add(new Map(0x0E, "00 00 00 72 74 74 74 73 00 00 7C 7E 7D 72 75 D6 76 73 7C 7D EF EF EF 72 74 E1 74 73 EF EF 1D 50 50 72 74 77 74 73 50 50 37 54 20 B6 74 77 74 B7 03 03 37 20 03 03 03 03 09 03 03 09 2E 3C 03 09 03 03 03 03 03 03 3E 2E 2F 2F 2F 2F 2F 2F 2F 2F", "0B 03 8A 00 00 8A 10 00 10 7C 11 7E 12 7D 18 7C 19 7D 8A 20 EF 30 1D 89 31 50 C2 40 37 60 2E 61 3C 70 3E 71 2E 88 72 2F 41 54 42 20 51 20 56 09 59 09 63 09 C4 03 72 43 B6 C5 04 74 C5 05 74 C5 06 74 C4 07 73 47 B7 14 75 15 D6 16 76 C2 35 77 25 E1 E1 06 0E 50 7C FE", true));
                list.Add(new Map(0x1E, "3E 3E 3A E1 3A 3A 3A E1 3A 3A 3E 3E DE 03 DD 3A DE 03 03 03 39 3E 2E E0 4E 3A 2E 2F 2F 2F 03 39 3E E0 3F 3A 3E DE 03 03 03 6F 39 E0 3B 03 39 2E 2F 2F 03 03 03 03 03 03 03 39 3A 3A 2F 2F 2F 2F 2F 2F 2F 2F 2F 2F 3A 3A 3A 3A 3A 3A 3A 3A 3A 3A", "0B 03 C2 00 3E 20 39 C3 01 3E 31 39 88 02 3A 12 37 22 2E 32 3E 42 39 03 E1 E1 0A 80 20 7C 13 03 C3 23 E0 14 38 24 4E 34 3F 44 3B 16 37 26 2E 36 3E 46 39 83 27 2F 83 17 03 07 E1 E1 0A 83 80 7C 37 37 47 2E 57 39 82 48 2F 82 58 3A 39 03 8A 60 2F 8A 70 3A C3 15 3A 16 DE 37 DE 12 DE 14 DD 41 6F FE"));
            }
            else if (mapEdits[4] == 1 && mapEdits[5] == 0)
            {
                list.Add(new Map(0x0E, "00 00 00 72 74 74 74 73 00 00 7C 7E 7D 72 75 D6 76 73 7C 7D EF EF EF 72 74 74 74 73 EF EF 1D 50 50 72 74 74 74 73 50 50 37 54 20 B6 74 74 74 B7 03 03 37 20 03 03 03 03 09 03 03 09 2E 3C 03 09 03 03 03 03 03 03 3E 2E 2F 2F 48 E0 49 2F 2F 2F", "0B 03 25 E1 8A 00 00 8A 10 00 10 7C 11 7E 12 7D 18 7C 19 7D 8A 20 EF 30 1D 89 31 50 C2 40 37 60 2E 61 3C 70 3E 71 2E 88 72 2F 74 48 75 E0 76 49 41 54 42 20 51 20 56 09 59 09 63 09 C4 03 72 43 B6 C5 04 74 C5 05 74 C5 06 74 C4 07 73 47 B7 14 75 15 D6 16 76 E1 06 0E 50 7C FE"));
                list.Add(new Map(0x0E, "00 00 00 72 74 74 74 73 00 00 7C 7E 7D 72 75 D6 76 73 7C 7D EF EF EF 72 74 E1 74 73 EF EF 1D 50 50 72 74 77 74 73 50 50 37 54 20 B6 74 77 74 B7 03 03 37 20 03 03 03 03 09 03 03 09 2E 3C 03 09 03 03 03 03 03 03 3E 2E 2F 2F 48 E0 49 2F 2F 2F", "0B 03 8A 00 00 8A 10 00 10 7C 11 7E 12 7D 18 7C 19 7D 8A 20 EF 30 1D 89 31 50 C2 40 37 60 2E 61 3C 70 3E 71 2E 88 72 2F 74 48 75 E0 76 49 41 54 42 20 51 20 56 09 59 09 63 09 C4 03 72 43 B6 C5 04 74 C5 05 74 C5 06 74 C4 07 73 47 B7 14 75 15 D6 16 76 C2 35 77 25 E1 E1 06 0E 50 7C FE", true));
                list.Add(new Map(0x1E, "3E 3E 3A E1 3A E0 3A E1 3A 3A 3E 3E DE 03 DD E0 DE 03 03 03 39 3E 2E E0 4E E0 2E 2F 2F 2F 03 39 3E E0 3F E0 3E DE 03 03 03 6F 39 E0 3B 03 39 2E 2F 2F 03 03 03 03 03 03 03 39 3A 3A 2F 2F 2F 2F 2F 2F 2F 2F 2F 2F 3A 3A 3A 3A 3A 3A 3A 3A 3A 3A", "0B 03 C2 00 3E 20 39 C3 01 3E 31 39 88 02 3A 12 37 22 2E 32 3E 42 39 03 E1 E1 0A 80 20 7C 13 03 C3 23 E0 14 38 24 4E 34 3F 44 3B 16 37 26 2E 36 3E 46 39 83 27 2F 83 17 03 07 E1 E1 0A 83 80 7C 37 37 47 2E 57 39 82 48 2F 82 58 3A 39 03 8A 60 2F 8A 70 3A C4 05 E0 16 DE 37 DE 12 DE 14 DD 41 6F FE"));
            }
            else if (mapEdits[4] == 0 && mapEdits[5] == 1)
            {
                list.Add(new Map(0x0E, "00 00 00 72 74 74 74 73 00 00 7C 7E 7D 72 75 D6 76 73 7C 7D EF EF EF 72 74 74 74 73 EF EF 1D 50 50 72 74 74 74 73 50 50 37 54 20 B6 74 74 74 B7 03 03 37 20 03 03 03 03 09 03 03 09 2E 3C 03 09 03 03 03 03 03 03 3E 2E 2F 2F 2F 2F 2F 48 E0 49", "0B 03 25 E1 8A 00 00 8A 10 00 10 7C 11 7E 12 7D 18 7C 19 7D 8A 20 EF 30 1D 89 31 50 C2 40 37 60 2E 61 3C 70 3E 71 2E 85 72 2F 77 48 78 E0 79 49 41 54 42 20 51 20 56 09 59 09 63 09 C4 03 72 43 B6 C5 04 74 C5 05 74 C5 06 74 C4 07 73 47 B7 14 75 15 D6 16 76 E1 06 0E 50 7C FE"));
                list.Add(new Map(0x0E, "00 00 00 72 74 74 74 73 00 00 7C 7E 7D 72 75 D6 76 73 7C 7D EF EF EF 72 74 E1 74 73 EF EF 1D 50 50 72 74 77 74 73 50 50 37 54 20 B6 74 77 74 B7 03 03 37 20 03 03 03 03 09 03 03 09 2E 3C 03 09 03 03 03 03 03 03 3E 2E 2F 2F 2F 2F 2F 48 E0 49", "0B 03 8A 00 00 8A 10 00 10 7C 11 7E 12 7D 18 7C 19 7D 8A 20 EF 30 1D 89 31 50 C2 40 37 60 2E 61 3C 70 3E 71 2E 85 72 2F 77 48 78 E0 79 49 41 54 42 20 51 20 56 09 59 09 63 09 C4 03 72 43 B6 C5 04 74 C5 05 74 C5 06 74 C4 07 73 47 B7 14 75 15 D6 16 76 C2 35 77 25 E1 E1 06 0E 50 7C FE", true));
                list.Add(new Map(0x1E, "3E 3E 3A E1 3A 3A 3A E1 E0 3A 3E 3E DE 03 DD 3A DE 03 03 03 39 3E 2E E0 4E 3A 2E 2F 2F 2F 03 39 3E E0 3F 3A 3E DE 03 03 03 6F 39 E0 3B 03 39 2E 2F 2F 03 03 03 03 03 03 03 39 3A 3A 2F 2F 2F 2F 2F 2F 2F 2F 2F 2F 3A 3A 3A 3A 3A 3A 3A 3A 3A 3A", "0B 03 C2 00 3E 20 39 C3 01 3E 31 39 88 02 3A 12 37 22 2E 32 3E 42 39 03 E1 E1 0A 80 20 7C 13 03 C3 23 E0 14 38 24 4E 34 3F 44 3B 16 37 26 2E 36 3E 46 39 83 27 2F 83 17 03 07 E1 E1 0A 83 80 7C 37 37 47 2E 57 39 08 E0 82 48 2F 82 58 3A 39 03 8A 60 2F 8A 70 3A C3 15 3A 16 DE 37 DE 12 DE 14 DD 41 6F FE"));
            }
            else if (mapEdits[4] == 1 && mapEdits[5] == 1)
            {
                list.Add(new Map(0x0E, "00 00 00 72 74 74 74 73 00 00 7C 7E 7D 72 75 D6 76 73 7C 7D EF EF EF 72 74 74 74 73 EF EF 1D 50 50 72 74 74 74 73 50 50 37 54 20 B6 74 74 74 B7 03 03 37 20 03 03 03 03 09 03 03 09 2E 3C 03 09 03 03 03 03 03 03 3E 2E 2F 2F 48 E0 49 48 E0 49", "0B 03 25 E1 8A 00 00 8A 10 00 10 7C 11 7E 12 7D 18 7C 19 7D 8A 20 EF 30 1D 89 31 50 C2 40 37 60 2E 61 3C 70 3E 71 2E 82 72 2F 74 48 75 E0 76 49 77 48 78 E0 79 49 41 54 42 20 51 20 56 09 59 09 63 09 C4 03 72 43 B6 C5 04 74 C5 05 74 C5 06 74 C4 07 73 47 B7 14 75 15 D6 16 76 E1 06 0E 50 7C FE"));
                list.Add(new Map(0x0E, "00 00 00 72 74 74 74 73 00 00 7C 7E 7D 72 75 D6 76 73 7C 7D EF EF EF 72 74 E1 74 73 EF EF 1D 50 50 72 74 77 74 73 50 50 37 54 20 B6 74 77 74 B7 03 03 37 20 03 03 03 03 09 03 03 09 2E 3C 03 09 03 03 03 03 03 03 3E 2E 2F 2F 48 E0 49 48 E0 49", "0B 03 8A 00 00 8A 10 00 10 7C 11 7E 12 7D 18 7C 19 7D 8A 20 EF 30 1D 89 31 50 C2 40 37 60 2E 61 3C 70 3E 71 2E 82 72 2F 74 48 75 E0 76 49 77 48 78 E0 79 49 41 54 42 20 51 20 56 09 59 09 63 09 C4 03 72 43 B6 C5 04 74 C5 05 74 C5 06 74 C4 07 73 47 B7 14 75 15 D6 16 76 C2 35 77 25 E1 E1 06 0E 50 7C FE", true));
                list.Add(new Map(0x1E, "3E 3E 3A E1 3A E0 3A E1 E0 3A 3E 3E DE 03 DD E0 DE 03 03 03 39 3E 2E E0 4E E0 2E 2F 2F 2F 03 39 3E E0 3F E0 3E DE 03 03 03 6F 39 E0 3B 03 39 2E 2F 2F 03 03 03 03 03 03 03 39 3A 3A 2F 2F 2F 2F 2F 2F 2F 2F 2F 2F 3A 3A 3A 3A 3A 3A 3A 3A 3A 3A", "0B 03 C2 00 3E 20 39 C3 01 3E 31 39 88 02 3A 12 37 22 2E 32 3E 42 39 03 E1 E1 0A 80 20 7C 13 03 C3 23 E0 14 38 24 4E 34 3F 44 3B 16 37 26 2E 36 3E 46 39 83 27 2F 83 17 03 07 E1 E1 0A 83 80 7C 37 37 47 2E 57 39 08 E0 82 48 2F 82 58 3A 39 03 8A 60 2F 8A 70 3A C4 05 E0 16 DE 37 DE 12 DE 14 DD 41 6F FE"));
            }

            //0x11/0x21
            if (mapEdits[6] == 0 && mapEdits[7] == 0)
            {
                list.Add(new Map(0x11, "C8 C8 C8 39 3A 3A 3A 3A 3A 3A 09 03 03 03 03 25 45 26 03 03 09 09 03 09 03 27 E1 28 03 03 09 09 03 09 03 03 03 03 03 03 2F 2F 3C 03 03 03 3D 2F 2F 2F C8 6F 2E 2F 2F 2F 4E 3A 3A 3A C8 03 39 3A 3A 3A 3B 03 03 03 26 0A 09 0A 0A 3D 2F 2F 2F 3C", "0B 03 03 39 86 04 3A C4 10 09 15 F5 16 F5 16 45 C2 21 09 C2 23 09 26 E1 E1 10 99 50 7C 82 40 2F 42 3C 46 3D 83 47 2F C2 50 C8 51 0A 52 2E 83 53 2F 56 4E 83 57 3A 62 39 84 63 3A 66 3B 7F F5 84 71 0A 72 09 75 3D 83 76 2F 79 3C 51 6F 83 00 C8 FE"));
            }
            else if (mapEdits[6] == 1 && mapEdits[7] == 0)
            {
                list.Add(new Map(0x11, "C8 C8 C8 39 3A 3A 3A 3A 3A 3A 09 03 03 03 03 25 45 26 03 03 09 09 03 09 03 27 E1 28 03 03 09 09 03 09 03 03 03 03 03 03 2F 2F 3C 03 03 03 3D 2F 2F 2F C8 6F 2E 48 E0 49 4E 3A 3A 3A C8 03 39 3A E0 3A 3B 03 03 03 26 0A 09 0A 0A 3D 2F 2F 2F 3C", "0B 03 03 39 86 04 3A C4 10 09 15 F5 16 F5 16 45 C2 21 09 C2 23 09 26 E1 E1 10 99 50 7C 82 40 2F 42 3C 46 3D 83 47 2F C2 50 C8 51 0A 52 2E 53 48 55 49 56 4E 83 57 3A 62 39 84 63 3A C2 54 E0 66 3B 7F F5 84 71 0A 72 09 75 3D 83 76 2F 79 3C 51 6F 83 00 C8 FE"));
            }
            else if (mapEdits[6] == 0 && mapEdits[7] == 1)
            {
                list.Add(new Map(0x11, "C8 C8 C8 39 3A 3A 3A 3A 3A 3A 09 03 03 03 03 25 45 26 03 03 09 09 03 09 03 27 E1 28 03 03 09 09 03 09 03 03 03 03 03 03 2F 2F 3C 03 03 03 3D 2F 2F 2F C8 6F 2E 2F 2F 2F 4E 3A 3A 3A C8 03 39 3A 3A 3A 3B 03 03 03 26 0A 09 0A 0A 3D 48 E0 49 3C", "0B 03 03 39 86 04 3A C4 10 09 15 F5 16 F5 16 45 C2 21 09 C2 23 09 26 E1 E1 10 99 50 7C 82 40 2F 42 3C 46 3D 83 47 2F C2 50 C8 51 0A 52 2E 83 53 2F 56 4E 83 57 3A 62 39 84 63 3A 66 3B 7F F5 84 71 0A 72 09 75 3D 76 48 77 E0 78 49 79 3C 51 6F 83 00 C8 FE"));
                list.Add(new Map(0x21, "28 0A 0A 0A 0A 38 3A E0 3A 37 2F 2F 48 E0 49 4E 3A E0 3A 37 3A E1 3A E0 3A 3B 04 04 F5 37 F7 FA F8 04 5C 5C F5 F9 FF 37 2D 04 04 04 F5 F9 FF FA 0A 37 38 F6 5C F5 FF FF 0A 5C 0A 37 38 FF F9 FF FF 0A 0A 0A 0A 37 38 FB FF FF FF FF FF FF 0A 37", "03 04 FF F5 86 01 0A 05 38 83 06 3A 83 16 3A C2 07 E0 C8 09 37 85 10 2F 12 48 14 49 15 4E 86 20 3A 21 E1 E1 11 AF 50 7C 25 3B 82 34 5C 40 2D C4 48 0A C3 50 38 52 5C 56 0A 57 5C 83 65 0A C2 13 E0 FE"));
            }
            else if (mapEdits[6] == 1 && mapEdits[7] == 1)
            {
                list.Add(new Map(0x11, "C8 C8 C8 39 3A 3A 3A 3A 3A 3A 09 03 03 03 03 25 45 26 03 03 09 09 03 09 03 27 E1 28 03 03 09 09 03 09 03 03 03 03 03 03 2F 2F 3C 03 03 03 3D 2F 2F 2F C8 6F 2E 48 E0 49 4E 3A 3A 3A C8 03 39 3A E0 3A 3B 03 03 03 26 0A 09 0A 0A 3D 48 E0 49 3C", "0B 03 03 39 86 04 3A C4 10 09 15 F5 16 F5 16 45 C2 21 09 C2 23 09 26 E1 E1 10 99 50 7C 82 40 2F 42 3C 46 3D 83 47 2F C2 50 C8 51 0A 52 2E 53 48 55 49 56 4E 83 57 3A 62 39 84 63 3A C2 54 E0 66 3B 7F F5 84 71 0A 72 09 75 3D 76 48 77 E0 78 49 79 3C 51 6F 83 00 C8 FE"));
                list.Add(new Map(0x21, "28 0A 0A 0A 0A 38 3A E0 3A 37 2F 2F 48 E0 49 4E 3A E0 3A 37 3A E1 3A E0 3A 3B 04 04 F5 37 F7 FA F8 04 5C 5C F5 F9 FF 37 2D 04 04 04 F5 F9 FF FA 0A 37 38 F6 5C F5 FF FF 0A 5C 0A 37 38 FF F9 FF FF 0A 0A 0A 0A 37 38 FB FF FF FF FF FF FF 0A 37", "03 04 FF F5 86 01 0A 05 38 83 06 3A 83 16 3A C2 07 E0 C8 09 37 85 10 2F 12 48 14 49 15 4E 86 20 3A 21 E1 E1 11 AF 50 7C 25 3B 82 34 5C 40 2D C4 48 0A C3 50 38 52 5C 56 0A 57 5C 83 65 0A C2 13 E0 FE"));
            }

            //0x1B/0x2B
            if (mapEdits[8] == 0)
            {
                list.Add(new Map(0x1B, "03 03 03 03 03 03 03 38 50 50 03 3D 2F 3C 03 03 03 38 1B 1B 2F 4E E1 2E 2F 2F 2F 4E 03 1B 3A 3F 3A 3E 3A 3A E1 3F 03 03 3A 3B 03 39 3A 3A 3A 3B 03 03 1B 03 09 03 03 09 09 03 03 1B 1B 3D 2F 2F 2F 2F 2F 3C 09 03 2F 4E 3A 3A 3A 3A 3A 2E 2F 2F", "0B 03 82 18 1B 29 1B 59 1B C2 50 1B 87 00 03 87 10 03 20 2F 21 4E 11 3D 12 2F 13 3C 23 2E 83 24 2F 27 4E C2 07 38 82 08 50 22 E9 C2 30 3A 31 3F 32 E9 33 3E 83 34 3A 36 E1 37 3F 41 3B 43 39 83 44 3A 47 3B 8A 70 2F 61 3D 85 62 2F 67 3C 71 4E 85 72 3A 77 2E 52 09 82 55 09 68 09 FE", true));
            }
            else if (mapEdits[8] == 1)
            {
                list.Add(new Map(0x1B, "03 03 03 03 03 03 03 38 50 50 03 3D 2F 3C 03 03 03 38 1B 1B 2F 4E E1 2E 2F 2F 2F 4E 03 1B 3A 3F 3A 3E 3A 3A E1 3F 03 03 3A 3B 03 39 3A 3A 3A 3B 03 03 1B 03 09 03 03 09 09 03 03 1B 1B 3D 48 4A 4A 4A 49 3C 09 03 2F 4E 3A 3A 3A 3A 3A 2E 2F 2F", "0B 03 82 18 1B 29 1B 59 1B C2 50 1B 87 00 03 87 10 03 20 2F 21 4E 11 3D 12 2F 13 3C 23 2E 83 24 2F 27 4E C2 07 38 82 08 50 22 E9 C2 30 3A 31 3F 32 E9 33 3E 83 34 3A 36 E1 37 3F 41 3B 43 39 83 44 3A 47 3B 8A 70 2F 61 3D 62 48 66 49 67 3C 71 4E 83 63 4A 85 72 3A 77 2E 52 09 82 55 09 68 09 FE", true));
                list.Add(new Map(0x2B, "3A 3F 3A D5 D6 D7 3A 3E 3A 3A 3A 3F E1 CD E1 CE 3A 3E 3A 3A 3A 3B 03 09 03 09 C6 39 3A 3A 0E 0E 1B 1B 03 03 09 0E 0E 0E 0E 0E 0E 1B 1B 1B 1B 0E 0E 0E 2C 2C 2C 2C 54 2C 2C 2C 2C 2C 04 04 04 04 04 04 04 04 04 04 2C 2D 2B 2C 2D 2B 2C 2D 2B 2C", "0B 0E 8A 00 3A 8A 10 3A 8A 20 3A C2 01 3F C2 07 3E 21 3B 27 39 8A 50 2C 8A 60 04 8A 70 04 54 54 03 D5 04 D6 05 D7 85 22 03 83 34 03 23 09 25 09 36 09 82 32 1B 84 43 1B 12 E1 E0 00 1B 28 30 13 CD 14 E1 15 CE E1 03 7A 50 7C 70 2C 71 2D 72 2B 73 2C 74 2D 75 2B 76 2C 77 2D 78 2B 79 2C 26 C6 E1 1F E9 28 20 FE", true));
            }

            //0x25/0x26/0x27
            if (mapEdits[9] % 2 == 1)
            {
                list.Add(new Map(0x25, "3A 3A 3A 3A 3A 3A 3A 3A 3A 3E FA F8 04 04 04 04 04 F7 FF 39 26 25 26 20 36 2F 3C 04 F7 F8 28 27 28 20 D4 20 37 04 04 04 F9 FD FD F6 04 04 2E 2F 2F 2F FC 36 3C F7 F9 F6 39 3A 3A 3A FA F9 37 04 F7 FF F9 F6 04 04 26 F7 37 04 04 F7 FA FF F9 F6", "03 04 8A 00 3A 82 2F F5 C2 23 20 35 20 24 36 25 2F 26 3C 36 37 46 2E 83 47 2F 56 39 83 57 3A 7F F5 51 36 C2 62 37 52 3C 34 D4 09 3E 19 39 FE"));
                list.Add(new Map(0x26, "3A 3A 3A 3A 3A E0 3A 3A 3A 3A 3A 3A 3A 3A 3A E0 3A 3A 3A 3A 04 F5 FA FD F8 04 04 09 F7 F9 F5 FC 3D 2F 2F 2F 2F 2F 2F 2F 2F 2F 4E 3A 3A 3A 3A 3A 3A 3A 3A 3A 3B FF FF F9 F8 04 F7 FA F7 FF FF FF FA F8 3D 35 04 04 04 FB FF F8 04 3D 4E F6 04 04", "03 04 82 40 2F 82 50 3A 87 33 2F 87 43 3A 32 3D 42 4E 52 3B 02 09 17 09 75 3D 66 3D 67 35 76 4E 27 09 8A 00 3A 8A 10 3A C2 05 E0 FE"));
                list.Add(new Map(0x27, "3A 3A 3F 3A 3A E0 3A 3A 3A 3B 3A 3A 3B 04 F7 F9 FC 09 04 04 FA F6 04 04 09 F7 FA F6 04 2B 2F 2F 2F 2F 2F 2F 3C FE 09 37 3A 3A 3A 3A 3A 3A 37 FF F6 37 FF FF 0A 0A 5C 5C 37 F7 FC 37 F7 FF FF FF F6 5C 37 04 FB 37 04 FB FF FF FF F6 37 09 FB 37", "0B 04 86 30 2F 86 40 3A C4 46 37 82 52 0A 54 5C 55 5C 65 5C 17 09 24 09 38 09 77 09 01 39 89 00 3A 09 3B 05 E0 02 3F 82 10 3A 12 3B 36 3C 29 2B C5 39 37 FE"));
            }
            else if (mapEdits[9] % 2 == 0)
            {
                if (((mapEdits[9] / 2) & 1) == 0)
                    list.Add(new Map(0x25, "3A 3A 3A 3A 3A 3A 3A 3A 3A 3E FA F8 04 04 04 04 04 F7 FF 39 26 25 26 20 36 2F 3C 04 F7 F8 28 27 28 20 D4 20 37 04 04 04 F9 FD FD F6 04 04 2E 2F 2F 2F FC 36 3C F7 F9 F6 39 3A 3A 3A FA F9 37 04 F7 FF F9 F6 04 04 26 F7 37 04 04 F7 FA FF F9 F6", "03 04 8A 00 3A 82 2F F5 C2 23 20 35 20 24 36 25 2F 26 3C 36 37 46 2E 83 47 2F 56 39 83 57 3A 7F F5 51 36 C2 62 37 52 3C 34 D4 09 3E 19 39 FE"));
                else
                    list.Add(new Map(0x25, "3A 3A 3A 3A 3A 3A 3A 3A 3A 3E FA F8 04 04 04 04 04 F7 FF 39 26 25 26 20 36 2F 3C 04 F7 F8 28 27 28 20 D4 20 37 04 04 04 F9 FD FD F6 04 04 2E 48 E0 49 FC 36 3C F7 F9 F6 39 3A E0 3A FA F9 37 04 F7 FF F9 F6 04 04 26 F7 37 04 04 F7 FA FF F9 F6", "03 04 8A 00 3A 82 2F F5 C2 23 20 35 20 24 36 25 2F 26 3C 36 37 46 2E 47 48 49 49 56 39 83 57 3A C2 48 E0 7F F5 51 36 C2 62 37 52 3C 34 D4 09 3E 19 39 FE"));

                if (((mapEdits[9] / 2) & 2) == 0)
                    list.Add(new Map(0x26, "3A 3A 3A 3A 3A E0 3A 3A 3A 3A 3A 3A 3A 3A 3A E0 3A 3A 3A 3A 04 F5 FA FD F8 04 04 09 F7 F9 F5 FC 3D 2F 2F 2F 2F 2F 2F 2F 2F 2F 4E 3A 3A 3A 3A 3A 3A 3A 3A 3A 3B FF FF F9 F8 04 F7 FA F7 FF FF FF FA F8 3D 35 04 04 04 FB FF F8 04 3D 4E F6 04 04", "03 04 82 40 2F 82 50 3A 87 33 2F 87 43 3A 32 3D 42 4E 52 3B 02 09 17 09 75 3D 66 3D 67 35 76 4E 27 09 8A 00 3A 8A 10 3A C2 05 E0 FE"));
                else
                    list.Add(new Map(0x26, "3A 3A 3A 3A 3A E0 3A 3A 3A 3A 3A 3A 3A 3A 3A E0 3A 3A 3A 3A 04 F5 FA FD F8 04 04 09 F7 F9 F5 FC 3D 2F 48 E0 49 2F 2F 2F 2F 2F 4E 3A 3A E0 3A 3A 3A 3A 3A 3A 3B FF FF F9 F8 04 F7 FA F7 FF FF FF FA F8 3D 35 04 04 04 FB FF F8 04 3D 4E F6 04 04", "03 04 82 40 2F 82 50 3A 87 33 2F 87 43 3A 32 3D 42 4E 52 3B 34 48 C2 35 E0 36 49 02 09 17 09 75 3D 66 3D 67 35 76 4E 27 09 8A 00 3A 8A 10 3A C2 05 E0 FE"));

                if (((mapEdits[9] / 2) & 4) == 0)
                    list.Add(new Map(0x27, "3A 3A 3F 3A 3A E0 3A 3A 3A 3B 3A 3A 3B 04 F7 F9 FC 09 04 04 FA F6 04 04 09 F7 FA F6 04 2B 2F 2F 2F 2F 2F 2F 3C FE 09 37 3A 3A 3A 3A 3A 3A 37 FF F6 37 FF FF 0A 0A 5C 5C 37 F7 FC 37 F7 FF FF FF F6 5C 37 04 FB 37 04 FB FF FF FF F6 37 09 FB 37", "0B 04 86 30 2F 86 40 3A C4 46 37 82 52 0A 54 5C 55 5C 65 5C 17 09 24 09 38 09 77 09 01 39 89 00 3A 09 3B 05 E0 02 3F 82 10 3A 12 3B 36 3C 29 2B C5 39 37 FE"));
                else
                    list.Add(new Map(0x27, "3A 3A 3F 3A 3A E0 3A 3A 3A 3B 3A 3A 3B 04 F7 F9 FC 09 04 04 FA F6 04 04 09 F7 FA F6 04 2B 2F 2F 48 E0 49 2F 3C FE 09 37 3A 3A 3A E0 3A 3A 37 FF F6 37 FF FF 0A 0A 5C 5C 37 F7 FC 37 F7 FF FF FF F6 5C 37 04 FB 37 04 FB FF FF FF F6 37 09 FB 37", "0B 04 86 30 2F 86 40 3A 32 48 C2 33 E0 34 49 C4 46 37 82 52 0A 54 5C 55 5C 65 5C 17 09 24 09 38 09 77 09 01 39 89 00 3A 09 3B 05 E0 02 3F 82 10 3A 12 3B 36 3C 29 2B C5 39 37 FE"));
            }

            //0x8C/0x9C
            if (mapEdits[10] == 0)
            {
                list.Add(new Map(0x8C, "38 3A 3A 3A 3A 3A 3A 3A 3A 3A 38 B9 B9 B9 B9 B9 47 3A 3A 3A 38 B3 B3 B3 B3 B3 38 70 47 3A 38 B3 B3 B3 B3 B3 F2 04 38 0E 38 B9 B8 B9 B8 B9 F3 04 38 0E 38 B6 B9 B9 B9 B6 F4 04 38 0E 38 B7 B9 B9 B9 B7 38 04 38 0E 38 2F 48 E0 49 2F 4E 04 38 0E", "03 04 C8 00 38 89 01 3A 85 11 B9 C6 16 38 83 17 3A 85 21 B3 C6 28 38 29 3A 85 31 B3 C5 39 0E 85 41 B9 42 B8 44 B8 85 51 B9 51 B6 55 B6 85 61 B9 61 B7 65 B7 85 71 2F 72 48 73 E0 74 49 76 4E 36 F2 46 F3 56 F4 27 70 16 DD 28 DD FE"));
                list.Add(new Map(0x8C, "38 B3 B3 B3 B3 B3 3A 3A 3A 3A 38 B3 B3 B3 B3 B3 47 3A 3A 3A 38 AD B1 E7 AD B1 38 70 47 3A 38 AE B2 E3 AE B2 F2 04 38 0E 38 B9 B8 B9 B8 B9 F3 04 38 0E 38 B6 B9 B9 B9 B6 F4 04 38 0E 38 B7 B9 B9 B9 B7 38 04 38 0E 38 2F 48 E0 49 2F 4E 04 38 0E", "03 04 C8 00 38 89 01 3A 85 11 B9 C6 16 38 83 17 3A C6 28 38 29 3A C5 39 0E 85 41 B9 42 B8 44 B8 85 51 B9 51 B6 55 B6 85 61 B9 61 B7 65 B7 85 71 2F 72 48 73 E0 74 49 76 4E 36 F2 46 F3 56 F4 27 70 16 DD 28 DD 85 01 B3 85 11 B3 21 AD 22 B1 23 E7 24 AD 25 B1 31 AE 32 B2 33 E3 34 AE 35 B2 E1 05 D4 50 7C FE", true));
                list.Add(new Map(0x9C, "38 3A 3A E0 3A E1 3B 04 38 0E 38 3A 46 04 04 04 04 04 38 0E 4E 0E 2E 2F 2F 2F 2F 2F 4E 0E 3B 0E 39 3A 3A 3A 3A 3A 3B 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 2C 2C 2C 2C 2C 2C 2C 2C 2C 2C 04 04 04 04 04 04 04 04 04 04 2C 2C 2C 2C 2C 2C 2C 2C 2C 2C", "03 04 C2 00 38 85 01 3A 03 E0 05 E1 E1 1F F0 38 10 06 3B C2 08 38 C4 09 0E 11 3A 12 DE 20 4E C2 21 0E 22 2E 85 23 2F 28 4E 30 3B 32 39 85 33 3A 38 3B 8A 40 0E 8A 50 2C 8A 70 2C FE"));
            }
            else if (mapEdits[10] == 1)
            {
                list.Add(new Map(0x9C, "38 3A 3A E0 3A E1 3B 04 38 0E 38 3A 46 04 04 04 04 04 38 0E 4E 0E 2E 48 E0 49 2F 2F 4E 0E 3B 0E 39 3A E0 3A 3A 3A 3B 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 2C 2C 2C 2C 2C 2C 2C 2C 2C 2C 04 04 04 04 04 04 04 04 04 04 2C 2C 2C 2C 2C 2C 2C 2C 2C 2C", "03 04 C2 00 38 85 01 3A 03 E0 05 E1 E1 1F F0 38 10 06 3B C2 08 38 C4 09 0E 11 3A 12 DE 89 20 4E C2 21 0E 22 2E 23 48 25 49 82 26 2F 30 3B 32 39 85 33 3A C2 24 E0 38 3B 8A 40 0E 8A 50 2C 8A 70 2C FE"));
            }

            return list;
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

        public static byte[] StringToByteArray(string byteString)
        {
            var list = new List<byte>();
            foreach (var subString in byteString.Split())
                list.Add(byte.Parse(subString, System.Globalization.NumberStyles.HexNumber));

            return list.ToArray();
        }
    }

    public class ROMStream : List<byte>
    {
        public ROMStream() { }
        public ROMStream(IEnumerable<byte> bytes) : base(bytes) { }

        public void Write(int offset, byte _byte) => this[offset] = _byte;

        public void Write(int offset, string bytes) => Write(offset, RandomizerIO.StringToByteArray(bytes));

        public void Write(int offset, IEnumerable<byte> bytes)
        {
            for (int i = 0; i < bytes.Count(); i++)
                this[offset + i] = bytes.ElementAt(i);
        }

        public byte[] Read(int offset, int count)
        {
            var array = new byte[count];
            for (int i = 0; i < count; i++)
                array[i] = this[offset + i];

            return array;
        }

        public void Replace(int offset, int count, IEnumerable<byte> bytes)
        {
            this.RemoveRange(offset, count);
            this.InsertRange(offset, bytes);
        }

        public int NextCollisionOffset(int startingOffset)
        {
            var offset = startingOffset + 1;

            while (this[offset - 1] != 0xFE)
                offset++;

            return offset;
        }

        public int GetWarpOffset(int startingOffset, List<byte> searchBytes)
        {
            var offset = startingOffset;
            while (this[++offset] != 0xFE)
            {
                if (this[offset] == searchBytes[0])
                {
                    var readBytes = this.Read(offset, 4);
                    if (searchBytes.SequenceEqual(readBytes))
                        break;
                }
            }

            if (this[offset] == 0xFE)
                offset = 0;

            return offset;
        }

        public void ShiftHeader(int world, int mapIndex, int shift)
        {
            var baseOffset = 0;
            var maxIndex = 0xFF;

            if (world == 0)
            {
                baseOffset = 0x24000;
                if (mapIndex <= 0x7F)
                    maxIndex = 0x7F;
            }
            else if (world == 1)
                baseOffset = 0x28000;
            else if (world == 2)
                baseOffset = 0x2C000;


            var currentIndex = mapIndex;
            while (++currentIndex <= maxIndex)
            {
                var currentOffset = baseOffset + currentIndex * 2;
                if (this[currentOffset] + shift > 0xFF)
                {
                    this[currentOffset] += (byte)(shift - 0x100);
                    this[currentOffset + 1]++;
                }
                else if (this[currentOffset] + shift < 0)
                {
                    this[currentOffset] += (byte)(shift + 0x100);
                    this[currentOffset + 1]--;
                }
                else
                    this[currentOffset] += (byte)shift;
            }
        }
    }

    public class Map
    {
        private int world; //OW = 0, UW = 1/2
        private int index;
        private byte[] overlayData;
        private byte[] collisionData;
        private bool alternate;

        private int headerOffset;
        private int overlayOffset;
        private int bufferOffset;

        public int World => world;
        public int Index => index;
        public byte[] OverlayData => overlayData;
        public byte[] CollisionData => collisionData;
        public int OverlayOffset => overlayOffset;
        public int BufferOffset => bufferOffset;

        public Map(int world, int index, string collision) //UW constructor
        {
            this.world = world;
            this.index = index;
            collisionData = RandomizerIO.StringToByteArray(collision);

            headerOffset = 0x24000 + 0x4000 * world + index * 2;
            bufferOffset = 0x27FF0 + 0x4000 * world;
        }

        public Map(int index, string overlay, string collision, bool alternate = false) //OW constructor
        {
            this.index = index;
            this.alternate = alternate;
            overlayData = RandomizerIO.StringToByteArray(overlay);
            collisionData = RandomizerIO.StringToByteArray(collision);

            headerOffset = 0x24000 + index * 2;

            if (!alternate)
            {
                overlayOffset = 0x98000 + index * 0x50;

                if (index >= 0xCC)
                    overlayOffset += 0x40;
            }
            else
            {
                if (index == 0x06)
                    overlayOffset = 0x9D040;
                else if (index == 0x0E)
                    overlayOffset = 0x9D090;
                else if (index == 0x1B)
                    overlayOffset = 0x9D0E0;
                else if (index == 0x2B)
                    overlayOffset = 0x9D130;
                else if (index == 0x79)
                    overlayOffset = 0x9D180;
                else if (index == 0x8C)
                    overlayOffset = 0x9D1D0;
            }

            if (index < 0x80)
                bufferOffset = 0x26FF0;
            //else
            //    bufferOffset = 0x6BFF0; //i'm unsure of what follows @ 0x69E76
        }

        public int GetCollisionOffset(ROMStream rom)
        {
            int offset = 0;

            if (world == 0) //OW
            {
                if (index < 0x80)
                    offset = 0x20000 + (rom[headerOffset + 1] << 8 | rom[headerOffset]);
                else
                    offset = 0x64000 + (rom[headerOffset + 1] << 8 | rom[headerOffset]);

                if (alternate)
                    offset = rom.NextCollisionOffset(offset);
            }
            else //UW
                offset = 0x20000 + 0x4000 * world + (rom[headerOffset + 1] << 8 | rom[headerOffset]);

            return offset;
        }
    }
}

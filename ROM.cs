﻿using LADXRandomizer.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace LADXRandomizer
{
    public class ROMBuffer : List<byte>
    {
        private static IReadOnlyList<(int World, int Index, int PointerOffset)> alternateScreens = new List<(int, int, int)>
        {
            (0, 0x06, 0x3202),
            (0, 0x0E, 0x31D2),
            (0, 0x1B, 0x3212),
            (0, 0x2B, 0x3222),
            (0, 0x79, 0x31F2),
            (0, 0x8C, 0x31E2),
            (1, 0xF5, 0x31A6),
        };

        private List<ScreenData> screenEdits = new List<ScreenData>();

        public ROMBuffer(IEnumerable<byte> bytes) : base(bytes) { }

        public void Write(int offset, byte value) => this[offset] = value;

        public void Write(int offset, string bytes) => Write(offset, bytes.ToByteArray());

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
            RemoveRange(offset, count);
            InsertRange(offset, bytes);
        }

        public int NextCollisionOffset(int startingOffset)
        {
            var offset = startingOffset + 1;

            while (this[offset - 1] != 0xFE)
                offset++;

            return offset;
        }

        public void ApplySettings(Settings settings)
        {
            //-- set debug mode --//
            if (settings.HasFlag(Settings.DebugMode))
            {
                Write(0x03, 0x01);
                Write(0x4671, "0A 0D 0C 01 02 05 08 03 06 07 09 04 01 FE 01 00 00 01 01 01 01 06"); //patch debug file items
                Write(0x5B4D, 0x18); //patch minimap
            }

            //framework for custom code
            Write(0x0008, "F5 3E 3E EA 00 21 F1 C3 00 40");
            Write(0xF8000, "E5 D5 16 00 1E 40 21 00 40 19 3D 20 FC D1 E9");

            //-- implement safety warp --//
            if (settings.HasFlag(Settings.HouseWarp))
                Write(0x5806, "00 FA 03 00 A7 F0 CB FE A0 20 38 3E 0B EA 95 DB CD 7D 0C 3E 01 EA 01 D4 3E 10 EA 02 D4 3E A3 EA 03 D4 3E 50 EA 04 D4 3E 60");

            //-- fix warp saving for dungeons --//
            if (settings.HasFlag(Settings.PatchWarpSaving))
            {
                Write(0x1993, "C3 F4 3F 7A");
                Write(0x3FF4, "57 3E 3E EA 00 21 C3 00 40");
                Write(0xF8000, "7A FE 0A D2 C2 19 FE 05 28 05 FE 07 C2 97 19 F0 F6 FE B0 28 09 FE 3A 28 05 FE 3D C2 96 19 C3 C2 19");
            }

            //-- prevent signpost maze cave from becoming inaccessible --//
            if (settings.HasFlag(Settings.PatchSignpostMaze))
            {
                Write(0x2061, 0x63);
                Write(0x206B, 0x63);
            }

            //-- patch ghost to allow dungeon entrances --//
            if (settings.HasFlag(Settings.PatchGhost))
                Write(0xB1D5, 0xFF);

            //-- allow entering D0 entrance with a follower --//
            if (settings.HasFlag(Settings.PatchD0Entrance))
                Write(0xB413, 0x30);

            //-- make egg maze unsolvable without reading the book --//
            if (settings.HasFlag(Settings.PatchEggMaze))
            {
                Write(0xBA26, 0xD9);  //shift lookup of maze patterns back 1
                Write(0x57F40, 0x19); //allow bit 0 to be set on egg maze byte
            }

            //-- prevent slime key softlock --//
            if (settings.HasFlag(Settings.PatchSlimeKey))
                Write(0xE022, 0x38);

            //-- allow jumping off waterfalls without the flippers --//
            if (settings.HasFlag(Settings.PatchWaterfalls))
                Write(0xAE93, 0x38);

            //-- allow acquiring powder from trendy even with the mushroom --//
            if (settings.HasFlag(Settings.PatchTrendy))
            {
                Write(0x134F8, 0xAF); //spawn powder sprite
                Write(0x13666, "C3 D0 7F 00 00 00 00 00 00 00 00 00 00"); //jump to modified code at new location
                Write(0x13FD0, "EA 4C DB AF EA 4B DB 16 0C CD 70 3E 3E 0B E0 A5 C9"); //modified code (resets mushroom flag)
            }

            if (true) //(settings.HasFlag(Settings.PatchKeyEvents))
            {
                //inject code to support custom chest placement
                Write(0x3468, "3E 01 CF 00");
                Write(0x3535, "3E 02 CF 00");
                Write(0x9EAE, "F0 D5");
                Write(0x9ECA, "F0 D6");
                Write(0x9EEB, "F0 D3 E0 CD F0 D4 E0 CE");
                Write(0xF8040, "3E 01 E0 DD F8 04 23 3E 0F 77 E1 18 58");
                Write(0xF8080, "E1 F0 DD A7 28 1B AF E0 DD C5 06 08 0E 10 7B E6 F0 E0 D3 81 E0 D6 7B E6 0F CB 37 E0 D4 80 E0 D5 C1 21 11 D7 19 F5 FA A5 DB A7 20 0E F0 F6 FE 80 30 04 3E 09 18 14 3E 1A 18 10 F0 F7 FE 06 38 08 FE 1A 30 04 3E 0B 18 02 3E 0A C3 21 08");
                
                //set key drop events (0x8X) to chest events (0x6X), and set chest prize to small key (0x1A) for those rooms
                var offset = 0x50000;
                while (offset++ < 0x50216)
                {
                    if (offset == 0x50069) //skip D4 key drop
                        continue;

                    if ((this[offset] & 0xF0) == 0x80)
                    {
                        this[offset] ^= 0xE0;
                        this[offset + 0x660] = 0x1A;
                    }
                }

                //edit key drop screens to have chests
                screenEdits.Add(new ScreenData(1, 0x16, "04 0D 30 F6 39 F7 20 C9 50 C9 21 B0 86 12 B0 28 B0 51 AF 86 62 AF 58 AF 29 2A C2 39 0D 59 2C 00 03 09 03 70 03 79 03 01 25 08 26 10 25 11 29 18 2A 19 26 60 27 61 2B 71 27 68 2C 69 28 78 28 22 A1 FE", null, null)); //D1 hardhats
                screenEdits.Add(new ScreenData(1, 0x32, "04 0D 30 F6 39 F7 74 ED C2 39 0D 82 00 03 82 10 03 82 08 03 82 18 03 82 60 03 82 70 03 82 68 03 82 78 03 C2 37 DB 02 25 12 23 22 29 21 21 20 25 50 27 51 22 52 2B 62 23 72 27 07 26 17 24 27 2A 28 21 29 21 59 22 58 22 57 2C 67 24 77 28 03 C7 06 C7 73 C8 76 C8 23 A1 FE", null, null)); //D2 stalfos
                screenEdits.Add(new ScreenData(1, 0x34, "04 4D 76 C8 74 DB 74 DC 39 F7 74 F5 30 F6 C2 30 0D 19 2A 69 2C C4 29 0D 83 00 03 83 10 03 83 60 03 83 70 03 03 25 13 23 83 20 21 23 29 83 50 22 53 2B 63 23 73 27 32 A1 C2 33 A6 82 14 20 82 67 20 24 20 58 20 67 20 73 23 82 74 0D 76 2C FE", null, null)); //D2 mimics
                screenEdits.Add(new ScreenData(1, 0x54, "07 0D 03 C7 06 C7 74 ED 73 C8 76 C8 00 03 09 03 01 25 10 25 11 29 08 26 19 26 18 2A 22 96 23 0F 24 95 25 96 26 0F 27 95 86 32 0F 42 94 43 0F 44 93 45 94 46 A1 47 93 21 20 28 20 FE", null, null)); //D3 north extra key
                screenEdits.Add(new ScreenData(1, 0x55, "07 0D 39 EF 00 03 09 03 01 25 10 25 11 29 08 26 19 26 18 2A 60 27 71 27 69 28 78 28 61 2B 68 2C 70 03 79 03 C6 13 D2 14 AF C6 15 D1 C5 24 01 64 B0 C2 32 D2 20 C9 50 C9 12 A1 FE", null, null)); //D3 west extra key
                screenEdits.Add(new ScreenData(1, 0x58, "07 0D 04 EC 82 00 03 02 25 03 C7 06 C7 07 26 82 08 03 10 25 11 21 12 29 17 2A 18 21 19 26 60 27 61 2B 63 AE 68 2C 69 28 70 03 71 27 73 C8 76 C8 78 28 79 03 C2 31 CF 87 32 D1 33 CF 35 CF 37 CF 42 D0 43 D1 82 44 AE 46 D0 47 D1 C2 48 D0 82 51 D2 53 CF 82 54 20 82 56 D2 83 63 D2 66 D0 27 A1 FE", null, null)); //D3 south extra key
                screenEdits.Add(new ScreenData(1, 0x4D, "04 0D 04 F4 03 29 82 04 0D 06 2A 00 03 01 25 08 26 09 03 10 25 11 29 13 AC 16 AC 18 2A 19 26 31 AC 38 AC 33 2C 82 34 22 35 98 36 2B 43 24 46 23 53 2A 82 54 21 56 29 41 A1 44 BE E1 02 57 48 60 FE", null, null)); //D3 green zols
                screenEdits.Add(new ScreenData(1, 0x48, "04 0D 04 FA 49 F7 08 F4 06 26 82 16 24 26 2A 82 27 21 29 26 18 21 19 0D 50 27 82 51 22 53 2B 83 60 03 63 23 83 70 03 73 27 C2 07 10 09 12 C2 08 0D 82 18 13 21 A1 17 AC 62 AC FE", null, null)); //D3 pairodds + zols
                screenEdits.Add(new ScreenData(1, 0x41, "04 0D 39 42 60 F6 84 24 13 82 34 20 82 44 20 50 27 88 51 22 59 28 8A 60 12 8A 70 13 32 A1 02 C7 07 C7 FE", null, null)); //D3 bombs
                screenEdits.Add(new ScreenData(1, 0x5B, "07 0D 04 F8 39 F7 03 C7 06 C7 11 CF 12 D1 13 C0 84 14 D2 C3 18 CF 83 21 D2 22 CF 24 D0 25 C0 C2 26 D0 C2 27 C0 C3 31 D0 C2 32 C0 33 CF 34 C0 35 D2 36 D0 82 43 D2 45 D0 46 C0 47 CF 48 D1 52 D1 C2 53 D0 82 54 C0 56 CF 57 D1 58 C0 82 61 C0 63 D0 83 64 D1 82 67 C0 31 D2 32 D2 12 A1 FE", null, null)); //D3 before boss
                screenEdits.Add(new ScreenData(1, 0x81, "0C 0D 30 F6 84 23 DD 86 32 DD 86 42 DD 84 53 DD 33 A7 36 A7 44 A6 45 A6 48 DD 66 DD 68 BE E2 04 A8 18 13 11 A1 FE", null, null)); //D5 push blocks
                screenEdits.Add(new ScreenData(1, 0xB4, "04 9D 72 F5 88 11 DB 88 21 DB 88 31 DB 88 41 DB 88 51 DB 88 61 DB 8A 00 A6 C6 10 A6 C7 19 A6 70 11 71 0D 72 10 86 73 A6 82 14 0D 33 0D 36 0D 15 A1 FE", null, null)); //D6 raising tile room
                screenEdits.Add(new ScreenData(1, 0xC3, "04 0D 03 C7 06 C7 07 F4 76 F5 07 29 08 0D 09 24 18 DE 13 DA 16 DA 22 DA 27 DA 31 DA 33 DA 36 DA 38 DA 42 DA 82 44 DA 47 DA 53 DA 56 DA 82 64 DA 11 BE E2 05 DA 18 13 14 A1 FE", null, null)); //D6 flying tile room
                screenEdits.Add(new ScreenData(2, 0x10, "04 6D 05 EC 50 F6 00 AC 01 23 C3 02 0F 03 0D 04 2A 07 26 C6 08 0F 10 21 11 29 C4 17 24 83 30 0F 87 50 22 57 28 89 60 0F 14 A1 FE", null, null)); //D7 like likes
                screenEdits.Add(new ScreenData(2, 0x1B, "04 0D 05 F4 39 F7 05 29 06 0D 07 2A 11 AC 14 1D 18 AC 21 1D 27 1D 39 2A 49 0D 41 1D 47 1D 59 2C 61 AC 64 1D 68 AC 32 A1 E1 06 0D 00 00 FE", null, null)); //D7 hinox
                screenEdits.Add(new ScreenData(2, 0x4C, "04 0D 71 F5 02 C7 07 C7 C2 00 03 01 25 08 26 C2 09 03 11 23 18 24 20 25 21 29 28 2A 29 26 82 14 AE 31 AF 38 AF 41 B0 48 B0 82 54 AF 82 64 B0 22 A1 FE", null, null)); //D8 vire
                screenEdits.Add(new ScreenData(2, 0x5A, "06 0D 04 47 30 F6 39 F7 83 11 AF 21 B0 82 22 01 24 AF 25 A6 32 B0 82 33 01 35 AF 36 A6 43 B0 82 44 01 46 AF 54 B0 82 55 01 57 AF 83 65 B0 68 AE 18 C0 61 C0 20 29 C2 30 0D 50 2B 17 A1 FE", null, null)); //D8 zamboni
                screenEdits.Add(new ScreenData(2, 0x41, "06 0D 39 4A 01 F4 10 F6 71 F5 70 23 71 0D 72 2C 00 23 01 0D 02 2A 03 29 04 06 05 2A 07 29 C8 08 06 09 24 12 10 83 13 06 14 C0 16 11 22 94 23 16 24 06 25 17 26 93 C4 34 0F 85 42 A6 44 0F 77 2B 79 24 10 29 20 0D 30 2B 17 A1 FE", null, null)); //D8 eye statue
                screenEdits.Add(new ScreenData(2, 0x3E, "06 4D 10 F6 74 F1 87 12 DF 87 22 DF 87 32 DF 87 42 DF 82 57 DF 83 61 DF 83 66 DF 00 C0 01 25 10 0D C4 11 23 20 2B 30 23 40 29 50 21 51 29 85 52 A6 57 A1 FE", null, null)); //D8 cracked floor
            }

            //-- disable post-D1 bowwow kids --//
            if (settings.HasFlag(Settings.DisableBowwowKids))
                Write(0x1A0A6, 0xFF);

            //-- disable bird key cave pit warping --//
            if (settings.HasFlag(Settings.DisableBirdKeyPits))
                Write(0x917C, "3E 00");

            //-- disable lanmolas pit warp --//
            if (settings.HasFlag(Settings.DisableLanmolasPit))
            {
                Write(0xDCF0, "3E 00");
                Write(0x6422D, 0xC9);
            }

            //-- cover pit below D7 --//
            if (settings.HasFlag(Settings.CoverD7Pit))
                screenEdits.Add(new ScreenData(0, 0x1E, "0B 03 C2 00 3E 20 39 C3 01 3E 31 39 88 02 3A 12 37 22 2E 32 3E 42 39 03 E1 E1 0A 80 20 7C 13 03 C3 23 E0 14 38 24 4E 34 3F 44 3B 16 37 26 2E 36 3E 46 39 83 27 2F 83 17 03 07 E1 E1 0A 83 80 7C 37 37 47 2E 57 39 82 48 2F 82 58 3A 39 03 8A 60 2F 8A 70 3A C3 15 3A 16 DE 37 DE 12 DE 14 DD 41 6F FE", "3E 3E 3A E1 3A 3A 3A E1 3A 3A 3E 3E DE 03 DD 3A DE 03 03 03 39 3E 2E E0 4E 3A 2E 2F 2F 2F 03 39 3E E0 3F 3A 3E DE 03 03 03 6F 39 E0 3B 03 39 2E 2F 2F 03 03 03 03 03 03 03 39 3A 3A 2F 2F 2F 2F 2F 2F 2F 2F 2F 2F 3A 3A 3A 3A 3A 3A 3A 3A 3A 3A", null));

            //-- prevent softlocking at the waterfall --//
            if (settings.HasFlag(Settings.PreventWaterSoftlocks))
            {
                //screenEdits.Add(new ScreenData(0, 0x2B, "0B 0E 8A 00 3A 8A 10 3A 8A 20 3A C2 01 3F C2 07 3E 21 3B 27 39 8A 50 2C 8A 60 04 8A 70 04 54 54 83 03 E9 83 13 E9 22 0E 84 23 1B 70 2C 71 2D 72 2B 73 2C 74 2D 75 2B 76 2C 77 2D 78 2B 79 2C FE", "3A 3F 3A E9 E9 E9 3A 3E 3A 3A 3A 3F 3A E9 E9 E9 3A 3E 3A 3A 3A 3B 0E 1B 1B 1B 1B 39 3A 3A 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 2C 2C 2C 2C 54 2C 2C 2C 2C 2C 04 04 04 04 04 04 04 04 04 04 2C 2D 2B 2C 2D 2B 2C 2D 2B 2C", null));
                screenEdits.Add(new ScreenData(1, 0xF2, "0C 0E 00 DF 01 25 08 26 09 DF 10 25 11 29 18 2A 19 26 60 27 61 2B 68 2C 69 28 70 DF 71 27 73 2B 74 0E 75 2C 78 28 79 DF E0 00 2E 58 20 34 1B FE", null, null));
            }

            //patch D4 warps
            if (true)
            {
                Write(0x3515, "28 04 FE C6 20 09 F0 F6 FE 0E C8 FE 2B C8 00 F0 F6 FE 80 3E 09 38 02 3E 1A CD 2F 0B C9");

                screenEdits.Add(new ScreenData(0, 0x2B, "0B 0E 14 E1 E1 03 7A 50 7C 26 C6 E1 1F E9 28 20 8A 00 3A 8A 10 3A 8A 20 3A C2 01 3F C2 07 3E 21 3B 27 39 8A 50 2C 8A 60 04 8A 70 04 54 54 83 03 E9 83 13 E9 22 0E 83 23 1B 26 6E 70 2C 71 2D 72 2B 73 2C 74 2D 75 2B 76 2C 77 2D 78 2B 79 2C FE", "3A 3F 3A E9 E9 E9 3A 3E 3A 3A 3A 3F 3A E9 E9 E9 3A 3E 3A 3A 3A 3B 0E 1B 1B 1B 6E 39 3A 3A 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 0E 2C 2C 2C 2C 54 2C 2C 2C 2C 2C 04 04 04 04 04 04 04 04 04 04 2C 2D 2B 2C 2D 2B 2C 2D 2B 2C", null));
            }

            //-- remove opening cutscene --//
            if (settings.HasFlag(Settings.RemoveHouseMarin))
                screenEdits.Add(new ScreenData(2, 0xA3, null, null, "47 3F 61 2D 62 2D FF"));

            //-- remove owls --//
            if (settings.HasFlag(Settings.RemoveOwls))
            {
                screenEdits.Add(new ScreenData(0, 0xF2, null, null, "55 31 32 C5 FF"));   //beach sword
                screenEdits.Add(new ScreenData(0, 0x80, null, null, "FF"));               //woods entrance
                screenEdits.Add(new ScreenData(0, 0x41, null, null, "FF"));               //tail key
                screenEdits.Add(new ScreenData(0, 0xD2, null, null, "43 3D FF"));         //after D1
                screenEdits.Add(new ScreenData(0, 0x36, null, null, "FF"));               //left of camera shop
                screenEdits.Add(new ScreenData(0, 0xB6, null, null, "FF"));               //after D3
                screenEdits.Add(new ScreenData(0, 0xEE, null, null, "FF"));               //after lanmolas
                screenEdits.Add(new ScreenData(0, 0x64, null, null, "52 44 FF"));         //ghost's grave
                screenEdits.Add(new ScreenData(0, 0x9D, null, null, "22 0F 64 BA FF"));   //before armos
                screenEdits.Add(new ScreenData(0, 0x9C, null, null, "FF"));               //after D6
                screenEdits.Add(new ScreenData(0, 0x17, null, null, "FF"));               //climbing tal tal
                screenEdits.Add(new ScreenData(0, 0x08, null, null, "24 C2 FF"));         //rescuing marin
                screenEdits.Add(new ScreenData(0, 0x16, null, null, "FF"));               //before egg
                screenEdits.Add(new ScreenData(0, 0x06, null, null, "00 DE FF"));         //opening egg
            }
        }

        public void ApplyScreenEdits()
        {
            WriteScreenEdits();
            UpdateColourDungeonPointers();
            UpdateAlternateMapPointers();
        }

        public void UpdateWarps(WarpList warpList)
        {
            foreach (var warp in ((IEnumerable<WarpData>)warpList).Reverse()) //reversing the list avoids issues with multi-warp screens
            {
                var warpValue = (warp.WarpValue == 0) ? 0xE000000000 : warp.WarpValue;

                var warpBytes = BitConverter.GetBytes(warpValue).Reverse().ToList();
                warpBytes.RemoveRange(0, 3);

                var searchBytes = BitConverter.GetBytes(warp.DefaultWarpValue).Reverse().ToList();
                searchBytes.RemoveRange(0, 3);
                searchBytes.RemoveAt(4);

                WriteWarp(warp.World, warp.Index, searchBytes, warpBytes);

                if (warp.Index2 != 0)
                    WriteWarp(warp.World, warp.Index2, searchBytes, warpBytes);
            }

            void WriteWarp(int world, int index, List<byte> searchBytes, List<byte> warpBytes)
            {
                var collisionPointerOffset = 0;
                if (world == 3)
                    collisionPointerOffset = 0x24000 + (this[0x3190] << 8 | this[0x318F]) + index * 2;
                else
                    collisionPointerOffset = 0x24000 + world * 0x4000 + index * 2;

                var collisionOffset = 0;
                if (world == 0 && index >= 0x80)
                    collisionOffset = 0x64000 + (this[collisionPointerOffset + 1] << 8 | this[collisionPointerOffset]);
                else if (world == 3)
                    collisionOffset = 0x24000 + (this[collisionPointerOffset + 1] << 8 | this[collisionPointerOffset]);
                else
                    collisionOffset = 0x20000 + world * 0x4000 + (this[collisionPointerOffset + 1] << 8 | this[collisionPointerOffset]);

                var warpOffset = GetWarpOffset(collisionOffset, searchBytes);

                if (warpOffset != 0)
                    Write(warpOffset, warpBytes);

                if (alternateScreens.ToList().Exists(x => x.Index == index))
                {
                    collisionOffset = NextCollisionOffset(collisionOffset);
                    warpOffset = GetWarpOffset(collisionOffset, searchBytes);

                    if (warpOffset != 0)
                        Write(warpOffset, warpBytes);
                }
            }

            int GetWarpOffset(int startingOffset, List<byte> searchBytes)
            {
                var offset = startingOffset;
                while (this[++offset] != 0xFE)
                {
                    if (this[offset] == searchBytes[0])
                    {
                        var readBytes = Read(offset, 4);
                        if (searchBytes.SequenceEqual(readBytes))
                            break;
                    }
                }

                if (this[offset] == 0xFE)
                    offset = 0;

                return offset;
            }
        }

        public void Save(string filename, Settings settings)
        {
            var titleBytes = Encoding.UTF8.GetBytes(filename.Replace('_', ' '));
            Write(0x134, titleBytes);

            var settingsBytes = Encoding.UTF8.GetBytes(Base62.ToBase62((uint)settings));
            Write(0xFF000, settingsBytes);

            byte headerChecksum = 0;
            for (int i = 0x134; i < 0x14D; i++)
                headerChecksum -= (byte)(this[i] + 1);
            Write(0x14D, headerChecksum);

            ushort globalChecksum = 0;
            for (int i = 0; i < 0x14E; i++)
                globalChecksum += this[i];
            for (int i = 0x150; i < Count; i++)
                globalChecksum += this[i];
            Write(0x14E, (byte)(globalChecksum >> 8));
            Write(0x14F, (byte)(globalChecksum & 0xFF));

            string path = "Output\\LADX_" + filename + ".gbc";

            if (!Directory.Exists("Output"))
                Directory.CreateDirectory("Output");

            File.WriteAllBytes(path, ToArray());
        }

        private void ReplaceKeyDropEvents()
        {
            
        }

        private void WriteScreenEdits()
        {
            foreach (var screen in screenEdits)
            {
                var collisionOffset = screen.GetCollisionOffset(this);
                var oldCollisionLength = NextCollisionOffset(collisionOffset) - collisionOffset;
                var newCollisionLength = screen.CollisionData?.Count() ?? 0;

                var spriteOffset = screen.GetSpriteOffset(this);
                var oldSpriteLength = NextSpriteOffset(spriteOffset) - spriteOffset;
                var newSpriteLength = screen.SpriteData?.Count() ?? 0;

                if (screen.OverlayData != null)
                    Replace(screen.OverlayOffset, 0x50, screen.OverlayData);

                if (screen.CollisionData != null)
                {
                    Replace(collisionOffset, oldCollisionLength, screen.CollisionData);
                    UpdateCollisionPointers(screen.World, screen.Index, newCollisionLength - oldCollisionLength);

                    //adjust buffer
                    if (screen.CollisionBufferOffset != 0)
                    {
                        if (newCollisionLength > oldCollisionLength)
                            RemoveRange(screen.CollisionBufferOffset, newCollisionLength - oldCollisionLength);
                        else
                            for (int i = 0; i < oldCollisionLength - newCollisionLength; i++)
                                Insert(screen.CollisionBufferOffset, 0);
                    }
                    else //for 2nd half of OW
                    {
                        var lastScreenPointer = this[0x241FF] << 8 | this[0x241FE];
                        var collisionBufferOffset = NextCollisionOffset(0x64000 + lastScreenPointer);

                        for (int i = 0; i < oldCollisionLength - newCollisionLength; i++)
                            Insert(collisionBufferOffset, 0);
                    }
                }

                if (screen.SpriteData != null)
                {
                    Replace(spriteOffset, oldSpriteLength, screen.SpriteData);
                    UpdateSpritePointers(screen.SpritePointerOffset, newSpriteLength - oldSpriteLength);

                    //adjust buffer
                    if (newSpriteLength > oldSpriteLength)
                        RemoveRange(ScreenData.SpriteBufferOffset, newSpriteLength - oldSpriteLength);
                    else
                        for (int i = 0; i < oldSpriteLength - newSpriteLength; i++)
                            Insert(ScreenData.SpriteBufferOffset, 0);
                }
            }

            int NextSpriteOffset(int startingOffset)
            {
                var offset = startingOffset + 1;

                while (this[offset - 1] != 0xFF)
                    offset++;

                return offset;
            }

            void UpdateCollisionPointers(int world, int mapIndex, int shift)
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

            void UpdateSpritePointers(int startingOffset, int shift)
            {
                var currentOffset = startingOffset + 2;
                while (currentOffset < 0x58640)
                {
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

                    if (currentOffset == 0x585FE)
                        currentOffset = 0x58000;
                    else if (currentOffset == 0x581FE)
                        currentOffset = 0x58600;
                    else
                        currentOffset += 2;
                }
            }
        }

        private void UpdateColourDungeonPointers() //fix colour dungeon pointer and header
        {
            //pointer
            var headerOffset = NextCollisionOffset(0x24000 + (this[0x281FF] << 8 | this[0x281FE]));
            var pointer = BitConverter.GetBytes((short)(headerOffset - 0x24000));

            this[0x318F] = pointer[0];
            this[0x3190] = pointer[1];

            //header
            var shift = headerOffset - 0x2BB77;
            for (int i = 0; i < 0x20; i++)
            {
                var currentOffset = headerOffset + i * 2;
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

        private void UpdateAlternateMapPointers()
        {
            foreach (var map in alternateScreens)
            {
                var collisionPointerOffset = 0x24000 + map.World * 0x4000 + map.Index * 2;

                var baseOffset = 0;
                if (map.World == 0 && map.Index >= 0x80)
                    baseOffset = 0x64000;
                else
                    baseOffset = 0x20000 + map.World * 0x4000;

                var collisionOffset = NextCollisionOffset(baseOffset + (this[collisionPointerOffset + 1] << 8 | this[collisionPointerOffset]));
                var alternatePointer = BitConverter.GetBytes((short)(collisionOffset - baseOffset));

                this[map.PointerOffset] = alternatePointer[0];
                this[map.PointerOffset + 1] = alternatePointer[1];
            }
        }
    }

    public class ScreenData
    {
        public const int SpriteBufferOffset = 0x59800;
        
        private bool alternate;
        private int collisionPointerOffset;

        public int World { get; }
        public int Index { get; }
        public byte[] CollisionData { get; }
        public byte[] OverlayData { get; }
        public byte[] SpriteData { get; }
        public int CollisionBufferOffset { get; }
        public int SpritePointerOffset { get; }
        public int OverlayOffset { get; }

        public ScreenData(int world, int index, string collision, string overlay, string sprites, bool alternate = false)
        {
            World = world;
            Index = index;
            this.alternate = alternate;
            CollisionData = collision.ToByteArray();
            OverlayData = overlay.ToByteArray();
            SpriteData = sprites.ToByteArray();

            if (OverlayData?.Count() != 0x50)
                OverlayData = null;

            collisionPointerOffset = 0x24000 + 0x4000 * world + index * 2;
            SpritePointerOffset = 0x58000 + 0x200 * world + index * 2;

            if (world == 0) //OW
            {
                if (!alternate)
                {
                    OverlayOffset = 0x98000 + index * 0x50;

                    if (index >= 0xCC)
                        OverlayOffset += 0x40;
                }
                else
                {
                    if (index == 0x06)
                        OverlayOffset = 0x9D040;
                    else if (index == 0x0E)
                        OverlayOffset = 0x9D090;
                    else if (index == 0x1B)
                        OverlayOffset = 0x9D0E0;
                    else if (index == 0x2B)
                        OverlayOffset = 0x9D130;
                    else if (index == 0x79)
                        OverlayOffset = 0x9D180;
                    else if (index == 0x8C)
                        OverlayOffset = 0x9D1D0;
                }

                if (index < 0x80)
                    CollisionBufferOffset = 0x26FF0;
            }
            else //UW
            {
                CollisionBufferOffset = 0x27FF0 + 0x4000 * world;
            }
        }

        public int GetCollisionOffset(ROMBuffer rom)
        {
            int offset = 0;

            if (World == 0) //OW
            {
                if (Index < 0x80)
                    offset = 0x20000 + (rom[collisionPointerOffset + 1] << 8 | rom[collisionPointerOffset]);
                else
                    offset = 0x64000 + (rom[collisionPointerOffset + 1] << 8 | rom[collisionPointerOffset]);

                if (alternate)
                    offset = rom.NextCollisionOffset(offset);
            }
            else //UW
                offset = 0x20000 + 0x4000 * World + (rom[collisionPointerOffset + 1] << 8 | rom[collisionPointerOffset]);

            return offset;
        }

        public int GetSpriteOffset(ROMBuffer rom) => 0x54000 + (rom[SpritePointerOffset + 1] << 8 | rom[SpritePointerOffset]);
    }
}

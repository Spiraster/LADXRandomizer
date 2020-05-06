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

        public static void WriteRom(WarpList warpList, uint seed, RandomizerSettings settings, string filename)
        {
            if (!Directory.Exists("Output"))
                Directory.CreateDirectory("Output");

            string file = "Output\\LADX " + filename + ".gbc";

            var screenEdits = new List<ScreenData>();
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

            //-- implement safety warp --//
            if (settings["SafetyWarp"].Enabled)
                rom.Write(0x5806, "00 FA 03 00 A7 F0 CB FE A0 20 38 3E 0B EA 95 DB CD 7D 0C 3E 01 EA 01 D4 3E 10 EA 02 D4 3E A3 EA 03 D4 3E 50 EA 04 D4 3E 60");

            //-- fix warp saving for dungeons --//
            if (settings["PatchWarpSaving"].Enabled)
                rom.Write(0x1994, "08 20");

            //-- prevent signpost maze cave from becoming inaccessible --//
            if (settings["PatchSignpostMaze"].Enabled)
            {
                rom.Write(0x2061, 0x63);
                rom.Write(0x206B, 0x63);
            }

            //-- patch ghost to allow dungeon entrances --//
            if (settings["PatchGhost"].Enabled)
                rom.Write(0xB1D5, 0xFF);

            //-- make egg maze unsolvable without reading the book --//
            if (settings["PatchEggMaze"].Enabled)
            {
                rom.Write(0xBA26, 0xD9);  //shift lookup of maze patterns back 1
                rom.Write(0x57F40, 0x19); //allow bit 0 to be set on egg maze byte
            }

            //-- prevent slime key softlock --//
            if (settings["PatchSlimeKey"].Enabled)
                rom.Write(0xE022, 0x38);

            //-- disable post-D1 bowwow kids --//
            if (settings["DisableBowwowKids"].Enabled)
                rom.Write(0x1A0A6, 0xFF);

            //-- disable bird key cave pit warping --//
            if (settings["DisableBirdKeyPits"].Enabled)
                rom.Write(0x917C, "3E 00");

            //-- disable lanmolas pit warp --//
            if (settings["DisableLanmolasPit"].Enabled)
            {
                rom.Write(0xDCF0, "3E 00");
                rom.Write(0x6422D, 0xC9);
            }

            //-- cover pit below D7 --//
            if (settings["CoverD7Pit"].Enabled)
                screenEdits.Add(new ScreenData(0, 0x1E, "0B 03 C2 00 3E 20 39 C3 01 3E 31 39 88 02 3A 12 37 22 2E 32 3E 42 39 03 E1 E1 0A 80 20 7C 13 03 C3 23 E0 14 38 24 4E 34 3F 44 3B 16 37 26 2E 36 3E 46 39 83 27 2F 83 17 03 07 E1 E1 0A 83 80 7C 37 37 47 2E 57 39 82 48 2F 82 58 3A 39 03 8A 60 2F 8A 70 3A C3 15 3A 16 DE 37 DE 12 DE 14 DD 41 6F FE", "3E 3E 3A E1 3A 3A 3A E1 3A 3A 3E 3E DE 03 DD 3A DE 03 03 03 39 3E 2E E0 4E 3A 2E 2F 2F 2F 03 39 3E E0 3F 3A 3E DE 03 03 03 6F 39 E0 3B 03 39 2E 2F 2F 03 03 03 03 03 03 03 39 3A 3A 2F 2F 2F 2F 2F 2F 2F 2F 2F 2F 3A 3A 3A 3A 3A 3A 3A 3A 3A 3A", null));

            //-- remove opening cutscene --//
            if (settings["RemoveHouseMarin"].Enabled)
                screenEdits.Add(new ScreenData(2, 0xA3, null, null, "47 3F 61 2D 62 2D FF"));

            //-- remove owls --//
            if (settings["RemoveOwls"].Enabled)
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
            foreach (var screen in screenEdits)
            {
                var collisionOffset = screen.GetCollisionOffset(rom);
                var oldCollisionLength = rom.NextCollisionOffset(collisionOffset) - collisionOffset;
                var newCollisionLength = screen.CollisionData?.Count() ?? 0;

                var spriteOffset = screen.GetSpriteOffset(rom);
                var oldSpriteLength = rom.NextSpriteOffset(spriteOffset) - spriteOffset;
                var newSpriteLength = screen.SpriteData?.Count() ?? 0;

                if (screen.OverlayData != null)
                    rom.Replace(screen.OverlayOffset, 0x50, screen.OverlayData);

                if (screen.CollisionData != null)
                {
                    rom.Replace(collisionOffset, oldCollisionLength, screen.CollisionData);
                    rom.UpdateCollisionPointers(screen.World, screen.Index, newCollisionLength - oldCollisionLength);

                    //adjust buffer
                    if (screen.CollisionBufferOffset != 0)
                    {
                        if (newCollisionLength > oldCollisionLength)
                            rom.RemoveRange(screen.CollisionBufferOffset, newCollisionLength - oldCollisionLength);
                        else
                            for (int i = 0; i < oldCollisionLength - newCollisionLength; i++)
                                rom.Insert(screen.CollisionBufferOffset, 0);
                    }
                    else //for 2nd half of OW
                    {
                        var lastScreenPointer = rom[0x241FF] << 8 | rom[0x241FE];
                        var collisionBufferOffset = rom.NextCollisionOffset(0x64000 + lastScreenPointer);

                        for (int i = 0; i < oldCollisionLength - newCollisionLength; i++)
                            rom.Insert(collisionBufferOffset, 0);
                    }
                }

                if (screen.SpriteData != null)
                {
                    rom.Replace(spriteOffset, oldSpriteLength, screen.SpriteData);
                    rom.UpdateSpritePointers(screen.SpritePointerOffset, newSpriteLength - oldSpriteLength);

                    //adjust buffer
                    if (newSpriteLength > oldSpriteLength)
                        rom.RemoveRange(ScreenData.SpriteBufferOffset, newSpriteLength - oldSpriteLength);
                    else
                        for (int i = 0; i < oldSpriteLength - newSpriteLength; i++)
                            rom.Insert(ScreenData.SpriteBufferOffset, 0);
                }
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
                var collisionPointerOffset = 0x24000 + map.World * 0x4000 + map.Index * 2;

                var baseOffset = 0;
                if (map.World == 0 && map.Index >= 0x80)
                    baseOffset = 0x64000;
                else
                    baseOffset = 0x20000 + map.World * 0x4000;
                
                var collisionOffset = rom.NextCollisionOffset(baseOffset + (rom[collisionPointerOffset + 1] << 8 | rom[collisionPointerOffset]));
                var alternatePointer = BitConverter.GetBytes((short)(collisionOffset - baseOffset));

                rom[map.PointerOffset] = alternatePointer[0];
                rom[map.PointerOffset + 1] = alternatePointer[1];
            }
            //----------------------------------

            //-- find and update all warps --//
            foreach (var warp in ((IEnumerable<WarpData>)warpList).Reverse()) //reversing the list avoids issues with multi-warp screens
            {
                var warpValue = (warp.WarpValue == 0) ? 0xE000000000 : warp.WarpValue;

                var warpBytes = BitConverter.GetBytes(warpValue).Reverse().ToList();
                warpBytes.RemoveRange(0, 3);

                var searchBytes = BitConverter.GetBytes(warp.DefaultWarpValue).Reverse().ToList();
                searchBytes.RemoveRange(0, 3);
                searchBytes.RemoveAt(4);

                UpdateWarp(warp.World, warp.Index, searchBytes, warpBytes);

                if (warp.Index2 != 0)
                    UpdateWarp(warp.World, warp.Index2, searchBytes, warpBytes);
            }

            void UpdateWarp(int world, int index, List<byte> searchBytes, List<byte> warpBytes)
            {
                var collisionPointerOffset = 0;
                if (world == 3)
                    collisionPointerOffset = 0x24000 + (rom[0x3190] << 8 | rom[0x318F]) + index * 2;
                else
                    collisionPointerOffset = 0x24000 + world * 0x4000 + index * 2;

                var collisionOffset = 0;
                if (world == 0 && index >= 0x80)
                    collisionOffset = 0x64000 + (rom[collisionPointerOffset + 1] << 8 | rom[collisionPointerOffset]);
                else if (world == 3)
                    collisionOffset = 0x24000 + (rom[collisionPointerOffset + 1] << 8 | rom[collisionPointerOffset]);
                else
                    collisionOffset = 0x20000 + world * 0x4000 + (rom[collisionPointerOffset + 1] << 8 | rom[collisionPointerOffset]);

                var warpOffset = rom.GetWarpOffset(collisionOffset, searchBytes);

                if (warpOffset != 0)
                    rom.Write(warpOffset, warpBytes);

                if (alternateMaps.Exists(x => x.Index == index))
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
            if (byteString == null)
                return null;

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

        public int NextSpriteOffset(int startingOffset)
        {
            var offset = startingOffset + 1;

            while (this[offset - 1] != 0xFF)
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

        public void UpdateCollisionPointers(int world, int mapIndex, int shift)
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

        public void UpdateSpritePointers(int startingOffset, int shift)
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

    public class ScreenData
    {
        public static readonly int SpriteBufferOffset = 0x59800;

        private int world; //OW = 0, UW = 1/2
        private int index;
        private byte[] collisionData;
        private byte[] overlayData;
        private byte[] spriteData;
        private bool alternate;

        private int collisionPointerOffset;
        private int collisionBufferOffset;
        private int spritePointerOffset;
        private int overlayOffset;

        public int World => world;
        public int Index => index;
        public byte[] CollisionData => collisionData;
        public byte[] OverlayData => overlayData;
        public byte[] SpriteData => spriteData;
        public int CollisionBufferOffset => collisionBufferOffset;
        public int SpritePointerOffset => spritePointerOffset;
        public int OverlayOffset => overlayOffset;

        public ScreenData(int world, int index, string collision, string overlay, string sprites, bool alternate = false)
        {
            this.world = world;
            this.index = index;
            this.alternate = alternate;
            collisionData = RandomizerIO.StringToByteArray(collision);
            overlayData = RandomizerIO.StringToByteArray(overlay);
            spriteData = RandomizerIO.StringToByteArray(sprites);

            if (overlayData?.Count() != 0x50)
                overlayData = null;

            collisionPointerOffset = 0x24000 + 0x4000 * world + index * 2;
            spritePointerOffset = 0x58000 + 0x200 * world + index * 2;

            if (world == 0) //OW
            {
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
                    collisionBufferOffset = 0x26FF0;
            }
            else //UW
            {
                collisionBufferOffset = 0x27FF0 + 0x4000 * world;
            }
        }

        public int GetCollisionOffset(ROMStream rom)
        {
            int offset = 0;

            if (world == 0) //OW
            {
                if (index < 0x80)
                    offset = 0x20000 + (rom[collisionPointerOffset + 1] << 8 | rom[collisionPointerOffset]);
                else
                    offset = 0x64000 + (rom[collisionPointerOffset + 1] << 8 | rom[collisionPointerOffset]);

                if (alternate)
                    offset = rom.NextCollisionOffset(offset);
            }
            else //UW
                offset = 0x20000 + 0x4000 * world + (rom[collisionPointerOffset + 1] << 8 | rom[collisionPointerOffset]);

            return offset;
        }

        public int GetSpriteOffset(ROMStream rom) => 0x54000 + (rom[spritePointerOffset + 1] << 8 | rom[spritePointerOffset]);
    }
}

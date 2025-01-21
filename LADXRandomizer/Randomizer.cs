using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LADXRandomizer
{
    public static class Randomizer
    {
        private static IReadOnlyList<(string Code, Enum Item)> requiredWarps = new List<(string, Enum)>
        {
            ("OW2-D3", Items.Feather),      //D1
            ("OW2-24", Items.Bracelet),     //D2
            ("OW2-B5", Items.Boots),        //D3
            ("OW2-2B-1", Items.Flippers),   //D4
            ("OW2-D9-1", Items.Hookshot),   //D5
            ("OW2-8C", Items.L2Bracelet),   //D6
            ("OW2-0E", Items.L2Shield),     //D7
            ("OW2-10", Items.MagicRod),     //D8
            ("OW2-65", Items.Powder),       //Witch's hut
            ("OW2-B3", Items.Powder),       //Trendy
            ("OW2-93", Items.Shovel),       //Shop
            ("OW2-93", Items.Bombs),        //Shop
            ("OW2-93", Items.Bow),          //Shop
            ("OW2-AC", Keys.FaceKey),       //Armos
            ("OW2-0A-2", Keys.BirdKey),     //Bird key cave
        };

        public static (WarpList, bool) ShuffleWarps(MT19937_64 rng, Settings settings, Log log)
        {
            var warpList = new WarpList(settings);

            //-- special case for D7 entrance --//
            var warpD7 = warpList.Where(x => x.Code.Contains("OW2") && x.DeadEnd && !x.Exclude).Random(rng);

            warpList["OW1-0E"].WarpValue = warpD7.LocationValue;
            warpD7.WarpValue = warpList["OW1-0E"].LocationValue;

            warpList["OW1-0E"].Exclude = true;
            warpD7.Exclude = true;
            //------------------------

            //-- initial shuffling of warps --//
            var ow1Warps = warpList.Where(x => x.Code.Contains("OW1") && !x.Exclude).Shuffle(rng);
            var ow2Warps = warpList.Where(x => x.Code.Contains("OW2") && !x.Exclude).Shuffle(rng);

            if (settings.HasFlag(Settings.PairWarps))
            {
                for (int i = 0; i < ow1Warps.Count; i++)
                {
                    ow1Warps[i].WarpValue = ow2Warps[i].LocationValue;
                    ow2Warps[i].WarpValue = ow1Warps[i].LocationValue;
                }
            }
            else
            {
                var remainingWarps = warpList.Where(x => !x.Exclude).ToList();
                var shuffledWarps = ow2Warps.Concat(ow1Warps).ToList();

                //warpList.ForEach(x => x.WarpValue = x.DefaultWarpValue);
                for (int i = 0; i < remainingWarps.Count; i++)
                    remainingWarps[i].WarpValue = shuffledWarps[i].LocationValue;
            }
            //------------------------

            //-- make sure all warps are accessible and seed is solvable --//
            var inventory = new FlagsCollection(new Enum[] { Items.Shield | Items.Sword });

            var reachableWarps = Pathfinding.Map(warpList);
            var solvable = Pathfinding.TrySolve(warpList, ref inventory, out List<WarpData> encounteredWarps, out List<Enum> encounteredConstraints, out string output);

            var count = 0;
            while (reachableWarps.Count < warpList.Count || !solvable)
            {
                if (count++ > 1000)
                {
                    log.Print("ERROR: Generation timed out.");
                    break;
                }

                WarpData warp1 = null;
                WarpData warp2 = null;

                if (reachableWarps.Count < warpList.Count)
                {
                    var unreachableWarps = warpList.Where(x => !reachableWarps.Contains(x) && !x.Exclude).ToList();

                    if (unreachableWarps.Count != 0) //this temporary bullshit is because of OW2-02
                    {
                        warp2 = unreachableWarps.Random(rng);

                        if (warp2.Code.Contains("OW1"))
                            warp1 = reachableWarps.Where(x => x.Code.Contains("OW2") && !x.Exclude).Random(rng);
                        else
                            warp1 = reachableWarps.Where(x => x.Code.Contains("OW1") && !x.Exclude).Random(rng);

                        log.Record(LogMode.Debug, "DEBUG: Shuffling an unreachableWarp");
                    }
                    else
                    {
                        //error log
                        var missingWarps = new StringBuilder();
                        warpList.Where(x => !reachableWarps.Contains(x)).ToList().ForEach(x => missingWarps.Append(x.Code + ", "));
                        log.Record(LogMode.Debug, "ERROR: Unable to make all warps accessible", "\t=> Warps: " + reachableWarps.Count.ToString() + "/" + warpList.Count.ToString(), "\t=> " + missingWarps.ToString());
                    }
                }
                else if (!solvable)
                {
                    var possibleWarps = requiredWarps.Where(x => encounteredConstraints.Contains(x.Item) && !warpList[x.Code].Exclude).Select(x => warpList[x.Code]).ToList(); //too restrictive??

                    if (possibleWarps.Count > 0)
                    {
                        warp1 = encounteredWarps.Where(x => x.Code.Contains("OW1") && !x.Exclude).Random(rng);
                        warp2 = possibleWarps.Random(rng);

                        log.Record(LogMode.Debug, "DEBUG: Shuffling a possibleWarp (" + warp1.Code + ")");
                    }
                    else
                    {
                        warp1 = encounteredWarps.Where(x => !x.Exclude).Random(rng);

                        var unencounteredWarps = warpList.Where(x => !encounteredWarps.Contains(x) && !x.Exclude).ToList();

                        if (unencounteredWarps.Count != 0)
                            possibleWarps = unencounteredWarps;
                        else
                            possibleWarps = warpList;

                        if (warp1.Code.Contains("OW1"))
                            warp2 = possibleWarps.Where(x => x.Code.Contains("OW2") && !x.Exclude).Random(rng);
                        else
                            warp2 = possibleWarps.Where(x => x.Code.Contains("OW1") && !x.Exclude).Random(rng);

                        //error log
                        log.Record(LogMode.Debug, "ERROR: No possible \"required\" warps");
                    }
                }

                if (warp1 != null && warp2 != null)
                {
                    if (settings.HasFlag(Settings.PairWarps))
                    {
                        var warp1Destination = warp1.GetDestinationWarp();
                        var warp2Destination = warp2.GetDestinationWarp();

                        warp1.WarpValue = warp2.LocationValue;
                        warp2.WarpValue = warp1.LocationValue;
                        warp1Destination.WarpValue = warp2Destination.LocationValue;
                        warp2Destination.WarpValue = warp1Destination.LocationValue;
                    }
                    else
                    {
                        warp1.WarpValue = warp2.LocationValue;
                        warp2.GetOriginWarp().WarpValue = warp1.GetDestinationWarp().LocationValue;
                    }
                }
                else
                {
                    //error log
                    log.Record(LogMode.Debug, "ERROR: Failed to create good seed");
                    break;
                }

                inventory = new FlagsCollection(new Enum[] { Items.Shield | Items.Sword });

                reachableWarps = Pathfinding.Map(warpList);
                solvable = Pathfinding.TrySolve(warpList, ref inventory, out encounteredWarps, out encounteredConstraints, out output);
            }
            //------------------------

            inventory = new FlagsCollection(new Enum[] { Items.Shield | Items.Sword });
            var warps = Pathfinding.Map(warpList);
            var success = Pathfinding.TrySolve(warpList, ref inventory, out encounteredWarps, out encounteredConstraints, out output);
            log.Record(LogMode.Debug, "Warps: " + warps.Count.ToString(), "Success: " + success.ToString());
            log.Record(LogMode.Pathfinder, "<l1>", "Pathfinder:", "<l1>", output);

            return (warpList, success);
        }

        public static void ShuffleThemes(MT19937_64 rng, ref ROMBuffer rom)
        {
            var colourPtrBuffer = new List<byte>();
            var tilePtrBuffer = new List<byte>();
            var thirdRowIndexBuffer = new List<byte>();
            var firstRowIndexBuffer = new List<byte>();
            var wallsIndexBuffer = new List<byte>();
            foreach (var (Index, Floors, Statue, Walls) in new ThemeValues(rng))
            {
                var offset = 0x843EF + Index * 2;
                colourPtrBuffer.Add(rom[offset]);
                colourPtrBuffer.Add(rom[offset + 1]);

                offset = 0x6A076 + Index * 2;
                tilePtrBuffer.Add(rom[offset]);
                tilePtrBuffer.Add(rom[offset + 1]);

                offset = 0x80589 + Floors;
                thirdRowIndexBuffer.Add(rom[offset]);

                firstRowIndexBuffer.Add(Statue);

                wallsIndexBuffer.Add(Walls);
            }

            rom.Write(0x843EF, colourPtrBuffer);
            rom.Write(0x6A076, tilePtrBuffer);
            //rom.Write(0x80589, thirdRowIndexBuffer);
            //rom.Write(0x805CA, firstRowIndexBuffer);
            //rom.Write(0x805A9, wallsIndexBuffer);

            //patch overrides for new palettes
                //remove D1, D3 overrides

            //temp (prevent overrides)
            //rom.Write(0x8518B, 0xC9);

            //patch weird stairs in D7 and D8 (0xA8 -> 0xA2)
            rom.ScreenEdits.Add(new ScreenData(2, 0x2E, "04 4D 04 F0 50 F6 29 F7 72 F5 02 29 03 25 C3 13 23 83 30 21 33 29 C2 50 0D 72 2B 73 0D 74 2C 82 34 A6 36 A7 C3 17 A7 01 A2 E2 06 F8 48 50 FE", null, null));
            rom.ScreenEdits.Add(new ScreenData(2, 0x3A, "06 2D 40 F2 29 F7 71 F5 00 21 02 29 03 25 06 26 07 17 08 12 09 16 83 10 0D 13 23 C2 16 24 19 10 20 25 82 21 21 23 29 29 14 36 2A 83 37 21 82 68 06 70 23 71 0D 72 2C 73 2B 74 06 75 2C 77 2B 78 06 79 2C 86 62 20 64 0D 18 A0 05 A2 E0 00 00 48 50 FE", null, null));

            //patch tiles (0x6A076; 0x8C000)
            //D1 (0x4000)
            rom.Write(0x8C018, "02 02 02 02"); //lava
            rom.Write(0x8C038, "07 07 07 07"); //water
            rom.Write(0x8C06C, "07 07 07 07");
            rom.Write(0x8C080, "06 06 06 06"); //pot
            rom.Write(0x8C238, "06 06 06 06"); //pot switch
            rom.Write(0x8C22C, "07 07 07 07"); //solid floor2
            rom.Write(0x8C288, "04 04 04 04 04 04 04 04"); //wall stairs
            rom.Write(0x8C2D2, "00 00"); //entrance
            rom.Write(0x8C2DC, "00 00 04 00 00 00 00 04");

            //D2 (0x4400)
            rom.Write(0x8C418, "02 02 02 02"); //lava
            rom.Write(0x8C4FC, "04 04 04 04"); //bombable wall (north)
            rom.Write(0x8C62C, "07 07 07 07"); //solid floor2
            rom.Write(0x8C638, "06 06 06 06"); //pot switch
            rom.Write(0x8C688, "01 01 01 01 01 01 01 01"); //wall stairs
            rom.Write(0x8C750, "01 01 01 01"); //spikes

            //D3 (0x5400)
            rom.Write(0x8D418, "02 02 02 02"); //lava
            rom.Write(0x8D438, "07 07 07 07"); //water
            rom.Write(0x8D46C, "07 07 07 07");
            rom.Write(0x8D62C, "07 07 07 07"); //solid floor2
            rom.Write(0x8D638, "06 06 06 06"); //pot switch
            rom.Write(0x8D688, "04 04 04 04 04 04 04 04"); //wall stairs
            rom.Write(0x8D6D2, "07 07"); //entrance
            rom.Write(0x8D6DC, "07 07 04 07 07 07 07 04");
            rom.Write(0x8D6EC, "04 07 04 07 07 04 07 04");
            rom.Write(0x8D750, "01 01 01 01"); //spikes

            //D4 (0x4800)
            rom.Write(0x8C818, "02 02 02 02"); //lava
            rom.Write(0x8CA2C, "06 06 06 06"); //solid floor2
            rom.Write(0x8CA88, "01 01 01 01 01 01 01 01"); //wall stairs
            rom.Write(0x8CB2C, "01 01 01 01"); //stairs up
            rom.Write(0x8CB3C, "01 01 01 01 01 01 01 01 01 01 01 01 01 01 01 01"); //conveyors
            rom.Write(0x8CB50, "01 01 01 01"); //spikes

            //D5 (0x5800)
            rom.Write(0x8D818, "02 02 02 02"); //lava
            rom.Write(0x8D914, "04 01 04 01 01 04 01 04"); //spin door
            rom.Write(0x8DA38, "06 06 06 06"); //pot switch
            rom.Write(0x8DA88, "04 04 04 04 04 04 04 04"); //wall stairs
            rom.Write(0x8DAD2, "04 04"); //entrance
            rom.Write(0x8DADC, "04 04 04 04 04 04 04 04");
            rom.Write(0x8DAEC, "04 04 04 04 04 04 04 04");
            rom.Write(0x8DB50, "01 01 01 01"); //spikes

            //D6 (0x4C00)
            rom.Write(0x8CC18, "02 02 02 02"); //lava
            rom.Write(0x8CE2C, "06 06 06 06"); //solid floor2
            rom.Write(0x8CF2C, "01 01 01 01"); //stairs up
            rom.Write(0x8CF50, "01 01 01 01"); //spikes

            //D7 (0x5C00)
            rom.Write(0x8DC18, "02 02 02 02"); //lava
            rom.Write(0x8DE38, "06 06 06 06"); //pot switch

            //D8 (0x5000)
            rom.Write(0x8D038, "07 07 07 07"); //water
            rom.Write(0x8D06C, "07 07 07 07");
            rom.Write(0x8D22C, "06 06 06 06"); //solid floor2
            rom.Write(0x8D350, "01 01 01 01"); //spikes
        }

        public static string GetSeed(string input)
        {
            byte[] inputBytes;
            if (string.IsNullOrWhiteSpace(input))
                inputBytes = BitConverter.GetBytes(Environment.TickCount);
            else
                inputBytes = Encoding.UTF8.GetBytes(input + Program.Version);

            var hashids = new Hashids(Program.Version, 8);
            
            using (var sha256 = new SHA256Managed())
            {
                var hash = sha256.ComputeHash(inputBytes);

                var list = new List<ulong>();
                for (int i = 3; i >= 0; i--)
                    list.Add(BitConverter.ToUInt64(hash.Reverse().ToArray(), i * 8));

                return hashids.Encode(list);
            }
        }
    }

    public class ThemeValues : List<(int Index, int Floors, byte Statue, byte Walls)>
    {
        private const int totalThemes = 8;

        public ThemeValues(MT19937_64 rng)
        {
            foreach (var index in Enumerable.Range(0, totalThemes).Shuffle(rng))
            {
                int floors = Enumerable.Range(0, 4).Select(x => (x * 2) + (index % 2))
                                                   .Where(x => !Exists(y => y.Floors == x))
                                                   .Random(rng);
                byte statue = new byte[] { 0x77, 0x78, 0x79 }.Random(rng);
                byte walls = new byte[] { 0x40, 0x6C, 0x6E, 0x4A }.Random(rng);

                Add((index, floors, statue, walls));
            }
        }
    }

    //public class PaletteInfo : List<PaletteInfo.Dungeon>
    //{
    //    public PaletteInfo()
    //    {
    //        //D1
    //        Add(new Dungeon(
    //                new List<(int, string)>
    //                {
    //                    (0x07, "00 00 00 00"),
    //                    (0x0F, "07 07 07 07"),
    //                    (0x1C, "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00"),
    //                    (0xAE, "00 00 00 00 00 00 00 00 00 00 00 00"),
    //                    (0xB3, "05 01 05 01 01 01 06 06 01 05 01 05 05 01 05 04 06 06 04 06 06 06 06 04 01 05 04 05 04 04 04 04 04 00 04 00 00 04 00 04 04 04 04 04"),
    //                    (0xC0, "00 00 00 00"),
    //                    (0xDD, "06 06 06 06")
    //                },
    //                new List<(int, string)>
    //                {
    //                    (0x5FE0, "FF 47 C4 26 21 15 00 00 FF 47 F3 51 67 28 00 00 FF 47 17 14 08 10 00 00 FF 47 D9 11 CE 10 00 00 7F 5A DF 38 12 0C 00 00 87 7D E6 44 0D 76 00 00 87 7D 16 7E 8D 38 00 00 52 4A CE 39 29 25 00 00"),
    //                    (0x6080, "FF 47 C4 26 21 15 00 00 FF 47 F3 51 67 28 00 00 FF 47 17 14 08 10 00 00 FF 47 D9 11 CE 10 00 00 7F 5A DF 38 12 0C 00 00 87 7D E6 44 0D 76 00 00 FF 47 C4 26 E2 19 E0 0C 52 4A CE 39 29 25 00 00")
    //                }
    //            ));
    //        //D2
    //        Add(new Dungeon(
    //                new List<(int, string)>
    //                {
    //                    (0x07, "05 05 05 05"),
    //                    (0x0F, "07 07 07 07"),
    //                    (0x20, "06 06 06 06"),
    //                    (0xAE, "03 03 03 03 03 03 03 03 03 03 03 03"),
    //                    (0xB3, "05 00 05 00 00 00 04 04 00 05 00 05 05 00 05 04 04 04 04 04 04 04 04 04 00 05 04 05 04 04 04 04 04 04 04 04 04 04 04 04 04 04 04 04"),
    //                    (0xC0, "01 01 01 01"),
    //                    (0xDB, "00 00 00 00 00 00 00 00")
    //                },
    //                new List<(int, string)>
    //                {
    //                    (0x6120, "94 7E CD 7D 65 34 00 00 FF 47 F3 51 67 28 00 00 FF 47 17 14 08 10 00 00 FF 47 D9 11 CE 10 00 00 12 7F E7 7C 00 34 00 00 BA 3A 70 09 C9 08 00 00 57 2E D9 11 CE 10 00 00 18 5B 0A 5E E0 5C 00 00")
    //                }
    //            ));
    //        //D3
    //        Add(new Dungeon(
    //                new List<(int, string)>
    //                {
    //                    (0x0F, "07 07 07 07"),
    //                    (0x20, "06 06 06 06"),
    //                    (0x4F, "00 00 00 00"),
    //                    (0xAE, "04 04 04 04 04 04 04 04 04 04 04 04"),
    //                    (0xB3, "05 03 05 03 03 03 00 00 03 05 03 05 05 03 05 04 00 00 04 00 00 00 00 04 03 05 04 05 04 04 04 04 04 00 04 00 00 04 00 04 04 04 04 04"),
    //                    (0xC0, "01 01 01 01"),
    //                    (0xDB, "03 03 03 03 03 03 03 03")
    //                },
    //                new List<(int, string)>
    //                {
    //                    (0x61D0, "10 16 C4 26 21 15 00 00 FF 47 F3 51 67 28 00 00 FF 47 17 14 08 10 00 00 FF 47 D9 11 CE 10 00 00 36 47 4E 26 46 01 00 00 10 16 29 01 72 1A 00 00 10 16 D9 11 CE 10 00 00 4F 52 67 39 C2 2C 00 00"),
    //                    (0x6270, "FF 47 45 7D A4 3C 62 20 FF 47 F3 51 67 28 00 00 FF 47 17 14 08 10 00 00 FF 47 D9 11 CE 10 00 00 36 47 4E 26 46 01 00 00 10 16 29 01 72 1A 00 00 10 16 D9 11 CE 10 00 00 4F 52 67 39 C2 2C 00 00")
    //                }
    //            ));
    //        //D4
    //        Add(new Dungeon(
    //                new List<(int, string)>
    //                {
    //                    (0x0F, "06 06 06 06"),
    //                    (0x1C, "03 03 03 03 03 03 03 03 03 03 03 03 03 03 03 03"),
    //                    (0x20, "00 00 00 00"),
    //                    (0xAE, "03 03 03 03 03 03 03 03 03 03 03 03"),
    //                    (0xB3, "05 06 05 06 06 06 04 04 06 05 06 05 05 06 05 04 04 04 04 04 04 04 04 04 06 05 04 05 04 04 04 04 04 04 04 04 04 04 04 04 04 04 04 04"),
    //                    (0xC0, "01 01 01 01"),
    //                    (0xDD, "00 00 00 00")
    //                },
    //                new List<(int, string)>
    //                {
    //                    (0x62C0, "52 16 D9 11 CE 10 00 00 FF 47 F3 51 67 28 00 00 FF 47 17 14 08 10 00 00 FF 47 D9 11 CE 10 00 00 DE 73 A0 3A 40 15 00 00 52 16 6B 15 A5 04 00 00 E0 77 00 3A 40 21 00 00 0D 76 45 7D A4 3C 00 00"),
    //                    (0x6360, "52 16 16 7E 6C 60 00 00 FF 47 F3 51 67 28 00 00 FF 47 17 14 08 10 00 00 FF 47 D9 11 CE 10 00 00 DE 73 A0 3A 40 15 00 00 52 16 6B 15 A5 04 00 00 E0 77 00 3A 40 21 00 00 0D 76 45 7D A4 3C 00 00")
    //                }
    //            ));
    //        //D5
    //        Add(new Dungeon(
    //                new List<(int, string)>
    //                {
    //                    (0x0F, "00 00 00 00"),
    //                    (0x20, "06 06 06 06"),
    //                    (0x4F, "00 00 00 00"),
    //                    (0xAE, "03 03 03 03 03 03 03 03 03 03 03 03"),
    //                    (0xB3, "05 01 05 01 01 01 06 06 01 05 01 05 05 01 05 04 06 06 04 06 06 06 06 04 01 05 04 05 04 04 04 04 04 06 04 06 06 04 06 04 04 04 04 04"),
    //                    (0xC0, "01 01 01 01"),
    //                    (0xDD, "06 06 06 06")
    //                },
    //                new List<(int, string)>
    //                {
    //                    (0x63B0, "74 02 C4 26 21 15 00 00 FF 47 F3 51 67 28 00 00 FF 47 17 14 08 10 00 00 FF 47 D9 11 CE 10 00 00 D7 52 91 29 EC 14 00 00 74 02 6B 01 19 23 00 00 74 02 D9 11 CE 10 00 00 0D 76 45 7D A4 3C 00 00"),
    //                    (0x6450, "74 02 C4 26 21 15 00 00 FF 47 F3 51 67 28 00 00 FF 47 17 14 08 10 00 00 FF 47 D9 11 CE 10 00 00 D7 52 91 29 EC 14 00 00 74 02 6B 01 19 23 00 00 FF 47 10 16 29 01 A5 00 0D 76 45 7D A4 3C 00 00"),
    //                    (0x64A0, "74 02 C4 26 21 15 00 00 FF 47 F3 51 67 28 00 00 FF 47 17 14 08 10 00 00 FF 47 D9 11 CE 10 00 00 D7 52 91 29 EC 14 00 00 74 02 6B 01 19 23 00 00 74 02 16 7E 6C 60 00 00 0D 76 45 7D A4 3C 00 00")
    //                }
    //            ));
    //        //D6
    //        Add(new Dungeon(
    //                new List<(int, string)>
    //                {
    //                    (0x07, "05 05 05 05"),
    //                    (0x0F, "06 06 06 06"),
    //                    (0x20, "00 00 00 00"),
    //                    (0xAE, "03 03 03 03 03 03 03 03 03 03 03 03"),
    //                    (0xB3, "05 06 05 06 06 06 04 04 06 05 06 05 05 06 05 04 04 04 04 04 04 04 04 04 06 05 04 05 04 04 04 04 04 04 04 04 04 04 04 04 04 04 04 04"),
    //                    (0xC0, "01 01 01 01"),
    //                    (0xDA, "03 03 03 03"),
    //                    (0xDB, "03 03 03 03 03 03 03 03")
    //                },
    //                new List<(int, string)>
    //                {
    //                    (0x6500, "52 2E D9 11 CE 10 00 00 FF 47 F3 51 67 28 00 00 FF 47 17 14 08 10 00 00 FF 47 D9 11 CE 10 00 00 DE 73 BE 51 14 30 00 00 52 2E AD 01 C6 00 00 00 3F 36 5A 25 AF 14 00 00 0D 76 45 7D A4 3C 00 00")
    //                }
    //            ));
    //        //D7
    //        Add(new Dungeon(
    //                new List<(int, string)>
    //                {
    //                    (0x07, "00 00 00 00"),
    //                    (0x0F, "00 00 00 00"),
    //                    (0x1C, "07 07 07 07 07 07 07 07 07 07 07 07 07 07 07 07"),
    //                    (0x20, "06 06 06 06"),
    //                    (0xAE, "07 07 07 07 07 07 07 07 07 07 07 07"),
    //                    (0xB3, "05 01 05 01 01 01 07 07 01 05 01 05 05 01 05 04 07 07 04 07 07 07 07 04 01 05 04 05 04 04 04 04 04 07 04 07 07 04 07 04 04 04 04 04"),
    //                    (0xC0, "03 03 03 03"),
    //                    (0xDA, "04 04 04 04"),
    //                    (0xDB, "03 03 03 03 03 03 03 03")
    //                },
    //                new List<(int, string)>
    //                {
    //                    (0x6650, "A7 01 EC 71 A4 3C 00 00 FF 47 F3 51 67 28 00 00 FF 47 17 14 08 10 00 00 FF 47 D9 11 CE 10 00 00 39 67 52 4A 29 25 00 00 A7 01 02 01 2B 02 00 00 A7 01 D9 11 CE 10 00 00 FF 47 2B 49 67 28 00 00"),
    //                    (0x65B0, "A7 01 EC 71 A4 3C 00 00 FF 47 F3 51 67 28 00 00 FF 47 17 14 08 10 00 00 FF 47 D9 11 CE 10 00 00 39 67 52 4A 29 25 00 00 A7 01 02 01 2B 02 00 00 A7 01 D9 11 CE 10 00 00 FF 47 D6 29 2E 1D 86 0C")
    //                }
    //            ));
    //        //D8
    //        Add(new Dungeon(
    //                new List<(int, string)>
    //                {
    //                    (0x07, "00 00 00 00"),
    //                    (0x0F, "06 06 06 06"),
    //                    (0x20, "00 00 00 00"),
    //                    (0xAE, "03 03 03 03 03 03 03 03 03 03 03 03"),
    //                    (0xB3, "05 06 05 06 06 06 04 04 06 05 06 05 05 06 05 04 04 04 04 04 04 04 04 04 06 05 04 05 04 04 04 04 04 04 04 04 04 04 04 04 04 04 04 04"),
    //                    (0xC0, "01 01 01 01"),
    //                    (0xDB, "06 06 06 06 06 06 06 06")
    //                },
    //                new List<(int, string)>
    //                {
    //                    (0x66B0, "9F 02 D9 11 CE 10 00 00 FF 47 F3 51 67 28 00 00 FF 47 17 14 08 10 00 00 FF 47 D9 11 CE 10 00 00 1F 31 15 00 0B 00 00 00 9F 02 DF 00 53 00 00 00 FF 47 3F 09 09 00 00 00 FE 63 12 7F E5 7D 00 00")
    //                }
    //            ));
    //        //Egg
    //        Add(new Dungeon(
    //                new List<(int, string)>
    //                {
    //                    (0x0F, "07 07 07 07"),
    //                    (0x1C, "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00")
    //                },
    //                new List<(int, string)>
    //                {
    //                    (0x6820, "FF 47 C4 26 21 15 00 00 FF 47 F3 51 67 28 00 00 FF 47 17 14 08 10 00 00 FF 47 D9 11 CE 10 00 00 96 66 B1 55 A9 38 00 00 67 39 C2 2C 0D 4A 00 00 FF 7F FF 7F FF 7F 00 00 87 7D 7C 2C 0F 00 08 08")
    //                }
    //            ));
    //    }

    //    public class Dungeon
    //    {
    //        public List<(int Value, int[] Details)> Tiles { get; set; }
    //        public List<(int Offset, int[] Colours)> Palettes { get; set; }

    //        public Dungeon(List<(int, string)> tiles, List<(int, string)> palettes)
    //        {
    //            foreach (var tile in tiles)
    //                Tiles.Add((tile.Item1, tile.Item2.ToIntArray()));

    //            foreach (var palette in palettes)
    //                Palettes.Add((palette.Item1, palette.Item2.ToIntArray()));
    //        }
    //    }
    //}
}

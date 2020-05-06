using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LADXRandomizer
{
    public static class Randomizer
    {
        private static readonly ReadOnlyCollection<(string Code, Enum Item)> requiredWarps = new List<(string, Enum)>
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
        }.AsReadOnly();

        public static (WarpList, bool) GenerateData(MT19937 rng, RandomizerSettings settings, RandomizerLog log)
        {
            var warpList = new WarpList(settings);

            //-- take care of special cases first (D6, D7, D8) --//
            ////D8
            //var warpD8b = new Warp();
            //var warpD8c = new Warp();
            //if (rng.Next() % 2 == 0)
            //{
            //    warpD8b = warpData.Where(x => x.Code.Contains("OW1") && x.DeadEnd && !x.Exclude).Random(rng);
            //    warpD8c = warpData.Where(x => x.Code.Contains("OW1") && x.DeadEnd && !x.Exclude && x != warpD8b).Random(rng);
            //}
            //else
            //{
            //    warpD8b = warpData.Where(x => x.Code.Contains("OW1") && x.ZoneConnections.Inward.Count == 0 && x.WarpConnections.Inward.Count == 1 && x.WarpConnections.Outward.Count == 1 && !x.Exclude).Random(rng);
            //    warpD8c = warpData[warpD8b.WarpConnections.Outward.First().Code];
            //}

            //warpData["OW2-00"].WarpValue = warpD8b.LocationValue;
            //warpData["OW2-02"].WarpValue = warpD8c.LocationValue;
            //warpD8b.WarpValue = warpData["OW2-00"].LocationValue;
            //warpD8c.WarpValue = warpData["OW2-02"].LocationValue;

            //warpData["OW2-00"].Exclude = true;
            //warpData["OW2-02"].Exclude = true;
            //warpD8b.Exclude = true;
            //warpD8c.Exclude = true;

            ////D6
            //var warpD6b = warpData.Where(x => x.Code.Contains("OW1") && x.DeadEnd && !x.Exclude).Random(rng);

            //warpData["OW2-6C"].WarpValue = warpD6b.LocationValue;
            //warpD6b.WarpValue = warpData["OW2-6C"].LocationValue;

            //warpData["OW2-6C"].Exclude = true;
            //warpD6b.Exclude = true;

            //D7
            var warpD7 = warpList.Where(x => x.Code.Contains("OW2") && x.DeadEnd && !x.Exclude).Random(rng);

            warpList["OW1-0E"].WarpValue = warpD7.LocationValue;
            warpD7.WarpValue = warpList["OW1-0E"].LocationValue;

            warpList["OW1-0E"].Exclude = true;
            warpD7.Exclude = true;
            //-------------------------------------------------------//

            //-- initial shuffling and pairing of warps --//
            if (settings["RandomizeWarps"].Enabled)
            {
                var ow1Warps = warpList.Where(x => x.Code.Contains("OW1") && !x.Exclude).OrderBy(x => rng.Next()).ToList();
                var ow2Warps = warpList.Where(x => x.Code.Contains("OW2") && !x.Exclude).OrderBy(x => rng.Next()).ToList();

                for (int i = 0; i < ow1Warps.Count; i++)
                {
                    ow1Warps[i].WarpValue = ow2Warps[i].LocationValue;
                    ow2Warps[i].WarpValue = ow1Warps[i].LocationValue;
                }
            }
            else
                warpList.ForEach(x => x.WarpValue = x.DefaultWarpValue);
            //--------------------------------------------//

            //-- make sure all warps are accessible and seed is solvable --//
            var inventory = new FlagsCollection(new Enum[] { Items.Shield | Items.Sword });

            var reachableWarps = Pathfinding.Map(warpList);
            var solvable = Pathfinding.TrySolve(warpList, ref inventory, out List<WarpData> encounteredWarps, out List<Enum> encounteredConstraints, out string output);

            var count = 0;
            while (reachableWarps.Count < warpList.Count || !solvable)
            {
                if (count++ > 1000)
                {
                    log.Write(LogMode.Output, "ERROR: Infinite loop");
                    break;
                }

                WarpData warp1 = null;
                WarpData warp2 = null;

                if (reachableWarps.Count < warpList.Count)
                {
                    var unreachableWarps = warpList.Where(x => !reachableWarps.Contains(x) && !x.Exclude).ToList();

                    if (unreachableWarps.Count != 0) //this temporary bullshit is because of OW2-02
                    {
                        warp1 = unreachableWarps.Random(rng);

                        if (warp1.Code.Contains("OW1"))
                            warp2 = reachableWarps.Where(x => x.Code.Contains("OW2") && !x.Exclude).Random(rng);
                        else
                            warp2 = reachableWarps.Where(x => x.Code.Contains("OW1") && !x.Exclude).Random(rng);

                        log.Write(LogMode.Output, "DEBUG: Shuffling an unreachableWarp");
                    }
                    else
                    {
                        //error log
                        var missingWarps = new StringBuilder();
                        warpList.Where(x => !reachableWarps.Contains(x)).ToList().ForEach(x => missingWarps.Append(x.Code + ", "));
                        log.Write(LogMode.Output, "ERROR: Unable to make all warps accessible", "\t=> Warps: " + reachableWarps.Count.ToString() + "/" + warpList.Count.ToString(), "\t=> " + missingWarps.ToString());
                    }
                }
                else if (!solvable)
                {
                    var possibleWarps = requiredWarps.Where(x => encounteredConstraints.Contains(x.Item) && !warpList[x.Code].Exclude).Select(x => warpList[x.Code]).ToList(); //too restrictive??

                    if (possibleWarps.Count > 0)
                    {
                        warp1 = possibleWarps.Random(rng);
                        warp2 = encounteredWarps.Where(x => x.Code.Contains("OW1") && !x.Exclude).Random(rng);

                        log.Write(LogMode.Output, "DEBUG: Shuffling a possibleWarp (" + warp1.Code + ")");
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
                        log.Write(LogMode.Output, "ERROR: No possible \"required\" warps");
                    }
                }

                if (warp1 != null && warp2 != null)
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
                    //error log
                    log.Write(LogMode.Output, "ERROR: Failed to create good seed");
                    break;
                }

                inventory = new FlagsCollection(new Enum[] { Items.Shield | Items.Sword });

                reachableWarps = Pathfinding.Map(warpList);
                solvable = Pathfinding.TrySolve(warpList, ref inventory, out encounteredWarps, out encounteredConstraints, out output);
            }
            //---------------------------------------//

            inventory = new FlagsCollection(new Enum[] { Items.Shield | Items.Sword });
            var warps = Pathfinding.Map(warpList);
            var success = Pathfinding.TrySolve(warpList, ref inventory, out encounteredWarps, out encounteredConstraints, out output);
            log.Write(LogMode.Output, "warps: " + warps.Count.ToString(), "success: " + success.ToString(), output);

            return (warpList, success);
        }

        public static int[] GenerateMapEdits(MT19937 rng)
        {
            return new int[] 
            {
                rng.Next(2),     //0x03/0x13 (left)
                rng.Next(2),     //0x03/0x13 (right)
                rng.Next(2),     //0x07
                rng.Next(2),     //0x09/0x19
                rng.Next(2),     //0x0E/0x1E (left)
                rng.Next(2),     //0x0E/0x1E (right)
                rng.Next(2),     //0x11/0x21 (left)
                rng.Next(2),     //0x11/0x21 (right)
                rng.Next(2),     //0x1B/0x2B
                rng.Next(1, 15), //0x25/0x26/0x27
                rng.Next(2)      //0x8C/0x9C
            };
        }

        public static uint GetSeed(string input)
        {
            if (input.Length != 8 || !uint.TryParse(input, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out uint seed))
            {
                if (input != "" && input != null)
                    seed = MurmurHash3.Hash(Encoding.UTF8.GetBytes(input));
                else
                    seed = MurmurHash3.Hash(BitConverter.GetBytes(Environment.TickCount));
            }

            return seed;
        }
    }
}

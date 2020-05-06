using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LADXRandomizer
{
    public static class Randomizer2
    {
        //private static readonly string[] dungeons = new string[]
        //{
        //    "OW2-D3",   //D1
        //    "OW2-24",   //D2
        //    "OW2-B5",   //D3
        //    "OW2-2B-1", //D4
        //    "OW2-D9-1", //D5
        //    "OW2-8C",   //D6
        //    "OW2-0E",   //D7
        //    "OW2-10"    //D8
        //};

        private static readonly List<(string Code, Enum Treasure, List<Enum> Constraints)> dungeonInfo = new List<(string, Enum, List<Enum>)>
        {
            ("OW2-D3", Items.Feather, new List<Enum> { Items.None }),                       //D1
            ("OW2-24", Items.Bracelet, new List<Enum> { Items.Powder | Items.Bracelet }),   //D2
            ("OW2-B5", Items.Boots, new List<Enum> { Items.Bracelet }),                     //D3
            ("OW2-2B-1", Items.Flippers, new List<Enum> { Items.Feather | Items.Boots }),   //D4
            ("OW2-D9-1", Items.Hookshot, new List<Enum> { Items.Feather }),                 //D5
            ("OW2-8C", Items.L2Bracelet, new List<Enum> { Items.None }),                    //D6
            //("OW2-0E", Items., new List<Enum> { }),   //D7
            ("OW2-10", Items.MagicRod, new List<Enum> { Items.Feather }),                   //D8
        };

        private static readonly List<string> specialWarps = new List<string> { "OW2-6C", "OW2-00", "OW2-02" };

        public static (WarpData, double, bool) GenerateData(uint seed, RandomizerSettings settings, RandomizerLog log)
        {
            var rng = new MT19937(seed);

            var warpData = new WarpData(settings);
            var inventory = new FlagsCollection();

            //sort out special cases (i.e. D6b/D8b/D8c) before the pathfinding
            foreach (var code in specialWarps)
            {
                var origin = warpData[code];
                var destination = warpData.Overworld1.Where(x => x.DeadEnd).ToList().Random(rng);

                origin.WarpValue = destination.LocationValue;
                destination.WarpValue = origin.LocationValue;
            }

            while (warpData.Exists(x => x.WarpValue == 0))//(!inventory.Contains(Dungeons.Egg))
            {
                (var availableOrigins, var currentConstraints) = Pathfinding.GetUnlinkedWarps(warpData, out inventory);

                if (availableOrigins.Count > 0)
                {
                    var origin = warpData[availableOrigins.Random(rng)];
                    var destination = new Warp();

                    var availableDestinations = new List<Warp>();
                    if (origin.Code.Contains("OW1"))
                        availableDestinations = warpData.Overworld2.Where(x => x.WarpValue == 0).ToList();
                    else
                        availableDestinations = warpData.Overworld1.Where(x => x.WarpValue == 0).ToList();

                    var curatedDestinations = availableDestinations.Where(x => x.Connections.Outward.Exists(y => inventory.Contains(y.Constraints.ToEnumList()))
                                                                               || x.ZoneConnections.Outward.Exists(y => inventory.Contains(y.Constraints.ToEnumList()))).ToList();

                    var availableDungeons = dungeonInfo.Where(x => warpData[x.Code].WarpValue == 0 && currentConstraints.Contains(x.Treasure) && inventory.Contains(x.Constraints)).ToList();

                    if (availableOrigins.Count == currentConstraints.Count + 2 && origin.Code.Contains("OW1") && availableDungeons.Count > 0)
                        destination = warpData[availableDungeons.Random(rng).Code];
                    else if (availableOrigins.Count <= 2 && availableDestinations.Count > 2 && curatedDestinations.Count > 0)
                        destination = curatedDestinations.Random(rng);
                    else
                        destination = availableDestinations.Random(rng);

                    if (destination != null)
                    {
                        origin.WarpValue = destination.LocationValue;
                        destination.WarpValue = origin.LocationValue;
                    }
                }
                else
                {
                    //log.Write(LogMode.Info, "the randomizer screwed up");
                    break;
                }
            }

            var pairedCount = warpData.Where(x => x.WarpValue != 0).Count();
            var totalCount = warpData.Count;
            var percentage = pairedCount * 1.0 / totalCount;

            var success = inventory.Contains(Dungeons.Egg) && (inventory.Contains(Items.Mushroom) || inventory.Contains(Items.Powder)) && inventory.Contains(Items.Bow);

            log.Write(LogMode.Info, "Seed: " + seed.ToString("X8"), "Paired warps: " + pairedCount + "/" + totalCount + " (" + percentage.ToString("p") + ")", "", "Inventory:", inventory.ToString(), "", "Success: " + success.ToString());

            var sb = new StringBuilder();
            foreach (var warp in warpData.Where(x => x.WarpValue == 0))
            {
                sb.Append("[" + warp.Code + "]\t");
            }

            log.Write(LogMode.Info, "", "OW1: " + warpData.Overworld1.Where(x => x.WarpValue == 0).Count(), "OW2: " + warpData.Overworld2.Where(x => x.WarpValue == 0).Count(), sb.ToString());

            return (warpData, percentage, success);
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

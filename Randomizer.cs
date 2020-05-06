using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LADXRandomizer
{
    public class Randomizer
    {
        private readonly uint seed;
        private bool attemptedWarpRandomization = false;

        private Warp D1, D2, D3, D4, D5, D6, Shop, Hideout;
        private List<Connection> D1Paths, D2Paths, D3Paths, D4Paths, D5Paths, D6Paths, ShopPaths, HideoutPaths;

        private RandomizerLog log;
        private RandomizerOptions options;
        private Random random;

        public WarpData warpData { get; set; }

        public string Seed { get { return seed.ToString("x8"); } }

        public Randomizer(string seed, RandomizerLog log, RandomizerOptions options)
        {
            this.log = log;
            this.options = options;
            warpData = new WarpData(options);

            D1 = warpData.Overworld2["OW2-D3"];
            D2 = warpData.Overworld2["OW2-24"];
            D3 = warpData.Overworld2["OW2-B5"];
            D4 = warpData.Overworld2["OW2-2B-1"];
            D5 = warpData.Overworld2["OW2-D9-1"];
            D6 = warpData.Overworld2["OW2-8C"];
            Shop = warpData.Overworld2["OW2-93"];
            Hideout = warpData.Overworld2["OW2-35"];

            //generate 32-bit seed
            var murmur = new MurmurHash2();

            if (seed.Length != 8 || !uint.TryParse(seed, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out this.seed))
                if (seed != "" && seed != null)
                    this.seed = murmur.Hash(Encoding.UTF8.GetBytes(seed));
                else
                    this.seed = murmur.Hash(BitConverter.GetBytes(Environment.TickCount));

            random = new Random((int)this.seed * warpData.Overworld1.Count);
        }

        public void GenerateData()
        {
            log.Write(LogMode.Info, "<l1>", "Seed: " + Seed, "");
            
            while (!RandomizeWarps()) ;
            CheckWarps();
            LogWarps();
        }

        private bool RandomizeWarps()
        {
            if (!attemptedWarpRandomization)
            {
                log.Write(LogMode.Info, "Randomizing warps...");
                log.Write(LogMode.Spoiler, "", "<l2>", "Warps: " + "(" + warpData.AllWarps.Count + " total)", "<l2>");
                attemptedWarpRandomization = true;
            }
            else
            {
                log.Write(LogMode.Debug, "     Randomizing warps... (again)");
                warpData.Overworld2.ForEach(x => x.Destination = 0);
            }

            if (warpData.Overworld1.Count != warpData.Overworld2.Count)
                MessageBox.Show("the warp counts aren't equal :(");

            foreach (var warp1 in warpData.Overworld1)
            {
                Warp warp2;

                int count = 0;
                do
                {
                    if (count == 1000)
                        return false;

                    var remainingWarps = warpData.Overworld2.Where(x => x.Destination == 0);
                    warp2 = remainingWarps.ElementAt(random.Next(0, remainingWarps.Count()));

                    count++;
                }
                while (!ValidPairing(warp1, warp2));

                warp1.Destination = warp2.Location;
                warp2.Destination = warp1.Location;
            }

            return true;
        }

        private bool ValidPairing(Warp warp1, Warp warp2)
        {
            if (options["PreventDefaultWarps"].Enabled)
                if (warp1.Location == warp2.Default)
                    return false;

            if (options["PreventInaccessible"].Enabled)
            {
                if (warp1.Locked)
                    return CheckLocked(warp1, warp2);
                else
                    return FindPath(warp1) || FindPath(warp2); //pairing is only invalid if both warps lead to dead ends
            }

            return true;
        }

        private bool CheckLocked(Warp warp1, Warp warp2)
        {
            if (!warp2.DeadEnd)
                return false;

            if (warp1.Code == "OW1-92" && warp2.Code == "OW2-24") //D2 can't be under the rooster statue
                return false;

            return true;
        }

        private void CheckWarps()
        {
            log.Write(LogMode.Info, "Checking dungeons and paths...");

            UpdatePaths();

            CheckZone2();
            CheckZone1();

            //checks only for constraints blocking dungeons
            CheckD1();
            CheckD2D6();
            CheckD4();
            CheckD5();
            CheckShop();
            //figure out check to make sure Zones 6 and 7 aren't isolated (something with D4 and D5)
            //probably merge isAccessible and ExtendedZoneAccess
            //figure out Mamu's cave (probably extra condition in CheckD5)
            //finish CheckZone8 for Kanalet
            //figure out D4 entrance (CheckD4 might need more than this too)
            //make .Pair a property of the Warp class
            //combine WarpData and WarpList classes
        }

        private void CheckZone1()
        {
            log.Write(LogMode.Debug, " -> CheckZone1()");
            
            //make sure either the feather or bracelet is accessible from Zone 1
            log.Write(LogMode.Debug, "   -> Checking accessibility...");
            while (!isAccessible(D1Paths, 1) && !isAccessible(D2Paths, 1) && !isAccessible(D6Paths, 1))
            {
                int rand = random.Next(0, 3);
                if (rand == 0)
                    CreateNewPair(D1, "D1");
                else if (rand == 1)
                    CreateNewPair(D2, "D2");
                else if (rand == 2)
                    CreateNewPair(D6, "D6");
            }

            //accommodate for dungeons stuck behind swamp flowers
            log.Write(LogMode.Debug, "   -> Checking D2 entrance...");
            if (!D1Paths.Exists(x => !x.Constraints.Contains(Item.BowWow)))
            {
                while (!isAccessible(D2Paths, 1) && !isAccessible(D6Paths, 1))
                {
                    int rand = random.Next(0, 2);
                    if (rand == 0)
                        CreateNewPair(D2, "D2");
                    else if (rand == 1)
                        CreateNewPair(D6, "D6");
                }
            }
            else if (!D2Paths.Exists(x => !x.Constraints.Contains(Item.BowWow)))
            {
                while (!isAccessible(D1Paths, 1) && !isAccessible(D5Paths, 1) && !isAccessible(D6Paths, 1))
                {
                    int rand = random.Next(0, 3);
                    if (rand == 0)
                    {
                        CreateNewPair(D1, "D1");
                        CheckHideout();
                    }
                    else if (rand == 1)
                        CreateNewPair(D5, "D5");
                    else if (rand == 2)
                        CreateNewPair(D6, "D6");
                }
            }
            else if (!D6Paths.Exists(x => !x.Constraints.Contains(Item.BowWow)))
            {
                while (!isAccessible(D1Paths, 1) && !isAccessible(D2Paths, 1) && !isAccessible(D5Paths, 1))
                {
                    int rand = random.Next(0, 3);
                    if (rand == 0)
                    {
                        CreateNewPair(D1, "D1");
                        CheckHideout();
                    }
                    else if (rand == 1)
                        CreateNewPair(D2, "D2");
                    else if (rand == 2)
                        CreateNewPair(D5, "D5");
                }
            }

            //if D1 is in Zone 1, then the bracelet must be accessible from Zones 1, 2 or 3
            if (isAccessible(D1Paths, 1))
            {
                while (!isAccessible(D2Paths, 1, Item.Feather) && !isAccessible(D2Paths, 2, Item.Feather) && !isAccessible(D2Paths, 3, Item.Feather)
                       && !isAccessible(D6Paths, 1, Item.Feather) && !isAccessible(D6Paths, 2, Item.Feather) && !isAccessible(D6Paths, 3, Item.Feather))
                {
                    int rand = random.Next(0, 2);
                    if (rand == 0)
                        CreateNewPair(D2, "D2");
                    else if (rand == 1)
                        CreateNewPair(D6, "D6");
                }
            }
        }

        private void CheckZone2() //there's got to be a better way to do this
        {
            log.Write(LogMode.Debug, " -> CheckZone2()");

            var zoneWarp1 = warpData.Overworld1["OW1-20"];
            var zoneWarp2 = warpData.Overworld1["OW1-21"];
            var zoneWarp3 = warpData.Overworld1["OW1-30"];
            var taltalWarp1 = warpData.Overworld1["OW1-03"];
            var taltalWarp2 = warpData.Overworld1["OW1-04"];
            var taltalWarp3 = warpData.Overworld1["OW1-10"];
            var taltalWarp4 = warpData.Overworld1["OW1-11"];
            var taltalWarp5 = warpData.Overworld1["OW1-13"];
            var taltalWarp6 = warpData.Overworld1["OW1-15"];

            var zoneWarp1_paths = new List<Connection>();
            var zoneWarp2_paths = new List<Connection>();
            var zoneWarp3_paths = new List<Connection>();
            var taltalWarp1_paths = new List<Connection>();
            var taltalWarp2_paths = new List<Connection>();
            var taltalWarp3_paths = new List<Connection>();
            var taltalWarp4_paths = new List<Connection>();
            var taltalWarp5_paths = new List<Connection>();
            var taltalWarp6_paths = new List<Connection>();

            GetPaths(zoneWarp1, ref zoneWarp1_paths);
            GetPaths(zoneWarp2, ref zoneWarp2_paths);
            GetPaths(zoneWarp3, ref zoneWarp3_paths);
            GetPaths(taltalWarp1, ref taltalWarp1_paths);
            GetPaths(taltalWarp2, ref taltalWarp2_paths);
            GetPaths(taltalWarp3, ref taltalWarp3_paths);
            GetPaths(taltalWarp4, ref taltalWarp4_paths);
            GetPaths(taltalWarp5, ref taltalWarp5_paths);
            GetPaths(taltalWarp6, ref taltalWarp6_paths);

            //if D1 is in Zone 2, then Zone 2 itself doesn't need to be accessible if it can be accessed from above
            if (isAccessible(D1Paths, 2)
                && (taltalWarp1_paths.Exists(x => !x.Constraints.Contains(Item.Feather))
                    || taltalWarp2_paths.Exists(x => !x.Constraints.Contains(Item.Feather))
                    || taltalWarp3_paths.Exists(x => !x.Constraints.Contains(Item.Feather))
                    || taltalWarp4_paths.Exists(x => !x.Constraints.Contains(Item.Feather))
                    || taltalWarp5_paths.Exists(x => !x.Constraints.Contains(Item.Feather))
                    || taltalWarp6_paths.Exists(x => !x.Constraints.Contains(Item.Feather))))
            {
                log.Write(LogMode.Debug, "      D1 present and accessible.");
            }
            else
            {
                while (!isAccessible(zoneWarp1_paths, 1) && !isAccessible(zoneWarp2_paths, 1) && !isAccessible(zoneWarp3_paths, 1))
                {
                    int rand = random.Next(0, 3);
                    if (rand == 0)
                        CreateNewPair(zoneWarp1, zoneWarp1.Code);
                    else if (rand == 1)
                        CreateNewPair(zoneWarp2, zoneWarp2.Code);
                    else if (rand == 2)
                        CreateNewPair(zoneWarp3, zoneWarp3.Code);

                    GetPaths(zoneWarp1, ref zoneWarp1_paths);
                    GetPaths(zoneWarp2, ref zoneWarp2_paths);
                    GetPaths(zoneWarp3, ref zoneWarp3_paths);
                    GetPaths(taltalWarp1, ref taltalWarp1_paths);
                    GetPaths(taltalWarp2, ref taltalWarp2_paths);
                    GetPaths(taltalWarp3, ref taltalWarp3_paths);
                    GetPaths(taltalWarp4, ref taltalWarp4_paths);
                    GetPaths(taltalWarp5, ref taltalWarp5_paths);
                    GetPaths(taltalWarp6, ref taltalWarp6_paths);
                }
            }
        }

        private void CheckZone8()
        {
            var exteriorWarp1 = warpData.Overworld1["OW1-49"];
            var exteriorWarp2 = warpData.Overworld1["OW1-69"];
            var interiorWarp1 = warpData.Overworld2["OW2-59-1"];
            var interiorWarp2 = warpData.Overworld2["OW2-69"];

            //var exteriorWarp1_paths = new List<Connection>();
            //var exteriorWarp2_paths = new List<Connection>();

            //GetPaths(exteriorWarp1, ref exteriorWarp1_paths);
            //GetPaths(exteriorWarp2, ref exteriorWarp2_paths);

            //if one interior connects to either exterior, then make sure the other exterior is accessible
            if (warpData.GetPair(exteriorWarp1).Code == interiorWarp1.Code || warpData.GetPair(exteriorWarp1).Code == interiorWarp2.Code)
            {
                while (exteriorWarp2.DeadEnd)
                    CreateNewPair(exteriorWarp2, "OW1-69");
            }
            else if (warpData.GetPair(exteriorWarp2).Code == interiorWarp1.Code || warpData.GetPair(exteriorWarp2).Code == interiorWarp2.Code)
            {
                while (exteriorWarp1.DeadEnd)
                    CreateNewPair(exteriorWarp1, "OW1-49");
            }

            //if both exterior are dead ends and one is a dungeon, then both interiors can't be behind that dungeon constraint
        }

        private void CheckD1()
        {
            log.Write(LogMode.Debug, " -> CheckD1()");

            while (!D1Paths.Exists(x => !x.Constraints.Contains(Item.Feather)) || !D1Paths.Exists(x => x.Zone != 2))
                CreateNewPair(D1, "D1");
        }

        private void CheckD2D6()
        {
            log.Write(LogMode.Debug, " -> CheckD2D6()");

            while (!D2Paths.Exists(x => !x.Constraints.Contains(Item.Bracelet)) 
                   && !D6Paths.Exists(x => !x.Constraints.Contains(Item.Bracelet)))
            {
                int rand = random.Next(0, 2);
                if (rand == 0)
                    CreateNewPair(D2, "D2");
                else if (rand == 1)
                    CreateNewPair(D6, "D6");
            }
        }

        private void CheckD4()
        {
            log.Write(LogMode.Debug, " -> CheckD4()");

            while (!D4Paths.Exists(x => !x.Constraints.Contains(Item.Flippers)))
                CreateNewPair(D4, "D4");
        }

        private void CheckD5()
        {
            log.Write(LogMode.Debug, " -> CheckD5()");

            while (!D5Paths.Exists(x => !x.Constraints.Contains(Item.Hookshot)) 
                   && (!D1Paths.Exists(x => !x.Constraints.Contains(Item.Hookshot)) || !D3Paths.Exists(x=> !x.Constraints.Contains(Item.Hookshot))))
            {
                CreateNewPair(D5, "D5");
            }
        }

        private void CheckShop()
        {
            log.Write(LogMode.Debug, " -> CheckShop()");

            while (!ShopPaths.Exists(x => x.Zone == 1 && !x.Constraints.ToList().Exists(y => y != Item.None)))
                CreateNewPair(Shop, "shop");
        }

        private void CheckHideout()
        {
            log.Write(LogMode.Debug, " -> CheckHideout()");

            while (!isAccessible(HideoutPaths, 1) && !isAccessible(HideoutPaths, 2) && !isAccessible(HideoutPaths, 3))
                CreateNewPair(Hideout, "moblin hideout");
        }

        private void CreateNewPair(Warp warp1, string name) //'warp1' must be a 'special' warp (OW2)
        {
            if (warp1.Code.Contains("OW2"))
            {
                log.Write(LogMode.Debug, "        Shuffling " + name + "...");

                Warp warp2, warp2_pair;
                var warp1_pair = warpData.GetPair(warp1);

                do
                {
                    if (warp1.Code.Contains("OW2"))
                        warp2 = warpData.Overworld1[random.Next(warpData.Overworld1.Count)];
                    else
                        warp2 = warpData.Overworld2[random.Next(warpData.Overworld2.Count)];

                    warp2_pair = warpData.GetPair(warp2);
                }
                while (warp2_pair.Special || !ValidPairing(warp1, warp2) || !ValidPairing(warp1_pair, warp2_pair));

                warp1.Destination = warp2.Location;
                warp2.Destination = warp1.Location;

                warp1_pair.Destination = warp2_pair.Location;
                warp2_pair.Destination = warp1_pair.Location;

                UpdatePaths();
            }
        }

        private bool isAccessible(List<Connection> paths, int zone, Item allowedConstraint = 0)
        {
            if (paths.Exists(x => x.Zone == zone && x.Accessible(allowedConstraint)))
                return true;

            return ExtendedZoneAccess(paths, zone, allowedConstraint);
        }

        private bool ExtendedZoneAccess(List<Connection> paths, int mainZone, Item allowedConstraint = 0)
        {
            var zoneNums = new List<int>();
            var zonePaths = new List<Connection>();

            foreach (var path in paths.Where(x => x.Zone != mainZone && x.Accessible(allowedConstraint)))
                if (!zoneNums.Contains(path.Zone))
                    zoneNums.Add(path.Zone);

            foreach (int num in zoneNums)
            {
                var zone = warpData.Overworld1.Where(x => x.ZoneConnections.ToList().Exists(y => y.Zone == num && y.Constraints.Contains(Item.None)));
                foreach (var warp in zone)
                    GetPaths(warp, ref zonePaths);

                foreach (var connection in WarpData.ZoneConnections[num])
                    zonePaths.Add(connection);
            }

            if (zonePaths.Exists(x => x.Zone == mainZone && x.Accessible(allowedConstraint)))
                return true;

            return false;
        }

        private void UpdatePaths()
        {
            D1Paths = new List<Connection>();
            D2Paths = new List<Connection>();
            D3Paths = new List<Connection>();
            D4Paths = new List<Connection>();
            D5Paths = new List<Connection>();
            D6Paths = new List<Connection>();
            ShopPaths = new List<Connection>();
            HideoutPaths = new List<Connection>();

            GetPaths(D1, ref D1Paths);
            GetPaths(D2, ref D2Paths);
            GetPaths(D3, ref D3Paths);
            GetPaths(D4, ref D4Paths);
            GetPaths(D5, ref D5Paths);
            GetPaths(D6, ref D6Paths);
            GetPaths(Shop, ref ShopPaths);
            GetPaths(Hideout, ref HideoutPaths);
        }

        private void GetPaths(Warp warp, ref List<Connection> pathList, List<string> previousWarps = null, List<Item> constraintList = null) //returns all paths to zones and their constraints
        {
            if (constraintList == null)
                constraintList = new List<Item>();

            if (previousWarps == null)
                previousWarps = new List<string>();
            else if (previousWarps.Contains(warp.Code))
                return;

            warp = warpData.GetPair(warp);

            if (warp.Connections != null)
            {
                foreach (var connection in warp.Connections)
                {
                    if (previousWarps.Contains(connection.Code))
                        return;

                    var nextWarp = warpData.AllWarps[connection.Code];

                    var newConstraints = constraintList;
                    if (connection.Constraints != null)
                        foreach (var constraint in connection.Constraints)
                            newConstraints.Add(constraint);

                    var newPreviousWarps = previousWarps;
                    newPreviousWarps.Add(warp.Code);

                    GetPaths(nextWarp, ref pathList, newPreviousWarps, newConstraints);
                }
            }
            else if (warp.ZoneConnections != null)
            {
                foreach (var connection in warp.ZoneConnections)
                {
                    var pathConstraints = constraintList;

                    if (connection.Constraints != null)
                        foreach (var constraint in connection.Constraints)
                            pathConstraints.Add(constraint);

                    pathList.Add(new Connection(connection.Zone, pathConstraints.ToArray()));
                }
            }
        }

        private bool FindPath(Warp warp, List<string> previousWarps = null) //only tries to path around dead ends
        {
            if (previousWarps == null)
                previousWarps = new List<string>();
            else if (previousWarps.Contains(warp.Code))
                return false;

            if (warp.DeadEnd)
                return false;

            if (warp.Connections != null)
            {
                foreach (var connection in warp.Connections)
                {
                    if (previousWarps.Contains(connection.Code))
                        return false; //a loop back to a previous point in the path is considered a dead end

                    var nextwarp = warpData.GetPair(connection.Code);
                    if (nextwarp != null)
                    {
                        var newPreviousWarps = previousWarps;
                        newPreviousWarps.Add(warp.Code);
                        if (FindPath(nextwarp, newPreviousWarps))
                            return true;
                    }
                    else
                        return true; //an unpaired warp is not a dead end (yet)
                }

                return false; //if all connections are paired AND dead ends, then this warp is also a dead end
            }

            return true; //if a warp has no connections and is not a dead end, then it must have a zone connection (i.e. not a dead end)
        }

        private void LogWarps()
        {
            foreach (var warp1 in warpData.Overworld1)
            {
                var warp2 = warpData.GetPair(warp1);

                string text1 = "[" + warp1.Code + "] " + warp1.Description;
                string text2 = "[" + warp2.Code + "] " + warp2.Description;
                
                log.Write(LogMode.Spoiler, text1 + "\r\n    ^=> " + text2 + "\r\n");
            }
        }
    }
}

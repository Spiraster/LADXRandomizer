using System;
using System.Collections.Generic;
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

        private Warp D1, D2, D6a, D6b;
        private List<Path> D1Paths, D2Paths, D6aPaths, D6bPaths;

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
            CheckDungeons();

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
                log.Write(LogMode.Debug, "     Randomizing warps... (again)");

            if (warpData.Overworld1.Count != warpData.Overworld2.Count)
                MessageBox.Show("the warp counts aren't equal :(");

            var indices = new List<int>();
            for (int i = 0; i < warpData.Overworld1.Count; i++)
                indices.Add(i);
            
            foreach (var warp1 in warpData.Overworld1)
            {
                int randIndex, warpIndex;
                Warp warp2;

                int count = 0;
                do
                {
                    if (count == 1000)
                        return false;

                    randIndex = random.Next(indices.Count);
                    warpIndex = indices[randIndex];
                    warp2 = warpData.Overworld2[warpIndex];

                    count++;
                }
                while (!ValidPairing(warp1, warp2));

                warp1.Destination = warp2.Location;
                warp2.Destination = warp1.Location;

                indices.RemoveAt(randIndex);
            }

            return true;
        }

        private bool ValidPairing(Warp warp1, Warp warp2)
        {
            if (options.List["PreventDefaultWarps"].Enabled)
                if (warp1.Location == warp2.Default)
                    return false;

            if (options.List["PreventInaccessible"].Enabled)
                return FindPath(warp1) || FindPath(warp2); //pairing is only invalid if both warps lead to dead ends

            return true;
        }

        private void CheckDungeons()
        {
            log.Write(LogMode.Info, "Checking dungeons...");

            D1 = warpData.Overworld2["OW2-D3"];
            D2 = warpData.Overworld2["OW2-24"];
            D6a = warpData.Overworld2["OW2-8C"];
            D6b = warpData.Overworld2["OW2-6C"];
            
            D1Paths = D2Paths = D6aPaths = D6bPaths = new List<Path>();

            GetPaths(warpData.GetPair(D1), ref D1Paths);
            GetPaths(warpData.GetPair(D2), ref D2Paths);
            GetPaths(warpData.GetPair(D6a), ref D6aPaths);
            GetPaths(warpData.GetPair(D6b), ref D6bPaths);

            CheckZone1();

            CheckD1();
            CheckD2D6();
        }

        private void CheckZone1()
        {
            log.Write(LogMode.Debug, "  -> CheckZone1()");

            while (!D1Paths.Exists(x => x.Zone == 1 && !x.Constraints.Contains("feather"))
                && !D2Paths.Exists(x => x.Zone == 1 && !x.Constraints.Contains("bracelet"))
                && !D6aPaths.Exists(x => x.Zone == 1 && !x.Constraints.Contains("bracelet"))
                && !D6bPaths.Exists(x => x.Zone == 1 && !x.Constraints.Contains("bracelet")))
            {
                int rand = random.Next(0, 6);
                if (rand <= 1)
                    CreateNewPair(D1, "D1", out D1Paths);
                else if (rand <= 3)
                    CreateNewPair(D2, "D2", out D2Paths);
                else if (rand == 4)
                    CreateNewPair(D6a, "D6", out D6aPaths);
                else if (rand == 5)
                    CreateNewPair(D6b, "D6", out D6bPaths);
            }
        }

        private void CheckD1()
        {
            log.Write(LogMode.Debug, "  -> CheckD1()");

            while (!D1Paths.Exists(x => !x.Constraints.Contains("feather")))
                CreateNewPair(D1, "D1", out D1Paths);
        }

        private void CheckD2D6()
        {
            log.Write(LogMode.Debug, "  -> CheckD2D6()");

            while (!D2Paths.Exists(x => !x.Constraints.Contains("bracelet")) 
                && !D6aPaths.Exists(x => !x.Constraints.Contains("bracelet")) 
                && !D6bPaths.Exists(x => !x.Constraints.Contains("bracelet")))
            {
                int rand = random.Next(0, 4);
                if (rand <= 1)
                    CreateNewPair(D2, "D2", out D2Paths);
                else if (rand == 2)
                    CreateNewPair(D6a, "D6", out D6aPaths);
                else if (rand == 3)
                    CreateNewPair(D6b, "D6", out D6bPaths);
            }
        }

        private void CreateNewPair(Warp warp1, string name, out List<Path> paths) //'warp1' must be a dungeon warp (OW2)
        {
            paths = new List<Path>();

            if (WarpData.Dungeons.Contains(warp1.Code))
            {
                log.Write(LogMode.Debug, "        Shuffling " + name + "...");

                Warp warp2, warp2_pair;

                var warp1_pair = warpData.GetPair(warp1);

                do
                {
                    warp2 = warpData.Overworld1[random.Next(warpData.Overworld1.Count)];
                    warp2_pair = warpData.GetPair(warp2);
                }
                while (WarpData.Dungeons.Contains(warp2_pair.Code) || !ValidPairing(warp1, warp2) || !ValidPairing(warp1_pair, warp2_pair));

                warp1.Destination = warp2.Location;
                warp2.Destination = warp1.Location;

                warp1_pair.Destination = warp2_pair.Location;
                warp2_pair.Destination = warp1_pair.Location;
                
                GetPaths(warpData.GetPair(warp1), ref paths);
            }
        }

        private void GetPaths(Warp warp, ref List<Path> paths, List<string> path = null, List<string> constraints = null) //returns all paths to zones and their constraints
        {
            if (constraints == null)
                constraints = new List<string>();

            if (path == null)
                path = new List<string>();
            else if (path.Contains(warp.Code))
                return;

            if (warp.Connections != null)
            {
                foreach (var connection in warp.Connections)
                {
                    if (path.Contains(connection.Code))
                        return;

                    var nextwarp = warpData.GetPair(connection.Code);

                    var newconstraints = constraints;
                    if (connection.Constraint != null)
                        newconstraints.Add(connection.Constraint);

                    var newpath = path;
                    newpath.Add(warp.Code);

                    GetPaths(nextwarp, ref paths, newpath, newconstraints);
                }
            }
            else if (warp.ZoneConnections != null)
            {
                foreach (var connection in warp.ZoneConnections)
                {
                    var pathconstraints = constraints;

                    if (connection.Constraint != null)
                        pathconstraints.Add(connection.Constraint);

                    paths.Add(new Path(connection.Zone, pathconstraints));
                }
            }
        }

        private bool FindPath(Warp warp, List<string> path = null) //only tries to path around dead ends
        {
            if (path == null)
                path = new List<string>();
            else if (path.Contains(warp.Code))
                return false;

            if (warp.DeadEnd)
                return false;

            if (warp.Connections != null)
            {
                foreach (var connection in warp.Connections)
                {
                    if (path.Contains(connection.Code))
                        return false; //a loop back to a previous point in the path is considered a dead end

                    var nextwarp = warpData.GetPair(connection.Code);
                    if (nextwarp != null)
                    {
                        var newpath = path;
                        newpath.Add(warp.Code);
                        if (FindPath(nextwarp, newpath))
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

    public class Path
    {
        private int zone;
        private List<string> constraints;

        public int Zone { get { return zone; } }
        public List<string> Constraints { get { return constraints; } }

        public Path(int zone, List<string> constraints)
        {
            this.zone = zone;
            this.constraints = constraints;
        }
    }
}

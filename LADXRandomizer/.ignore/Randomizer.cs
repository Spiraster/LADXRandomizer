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

        private Warp D1, D2, D3, D4, D5, D6, Shop, Hideout, Zone2A, Zone2B, Zone2C, LeftTalTalA, LeftTalTalB, LeftTalTalC, LeftTalTalD, LeftTalTalE, LeftTalTalF, RightTalTalA, RightTalTalB,
                     Zone8ExteriorA, Zone8ExteriorB, Zone8InteriorA, Zone8InteriorB, Zone10A, Zone10B;

        private List<Connection> D1Paths, D2Paths, D3Paths, D4Paths, D5Paths, D6Paths, ShopPaths, HideoutPaths, Zone2APaths, Zone2BPaths, Zone2CPaths,
                                 LeftTalTalAPaths, LeftTalTalBPaths, LeftTalTalCPaths, LeftTalTalDPaths, LeftTalTalEPaths, LeftTalTalFPaths, RightTalTalAPaths, RightTalTalBPaths,
                                 Zone8ExteriorAPaths, Zone8ExteriorBPaths, Zone8InteriorAPaths, Zone8InteriorBPaths, SEPaths, Zone10APaths, Zone10BPaths;

        private bool GlobalCheck, D1Check, D2D6Check, D4Check, D5Check, ShopCheck, HideoutCheck, Zone1AccessibilityCheck, Zone1BraceletCheck, SwampD1Check, SwampD2Check, SwampD6Check,
                     Zone2AccessibilityCheck1, Zone2AccessibilityCheck2, Zone8AccessibilityCheck, Zone8D1Check, SEAccessibilityCheck, SED1Check, Zone10Check, WaterfallAccessibilityCheck, WaterfallKeyCheck;

        private RandomizerLog log;
        private RandomizerSettings settings;
        private Random random;

        public WarpData warpData { get; set; }

        public string Seed { get { return seed.ToString("x8"); } }

        public Randomizer(string seed, RandomizerLog log, RandomizerSettings settings)
        {
            this.log = log;
            this.settings = settings;
            warpData = new WarpData(settings);

            D1 = warpData.Overworld2["OW2-D3"];
            D2 = warpData.Overworld2["OW2-24"];
            D3 = warpData.Overworld2["OW2-B5"];
            D4 = warpData.Overworld2["OW2-2B-1"];
            D5 = warpData.Overworld2["OW2-D9-1"];
            D6 = warpData.Overworld2["OW2-8C"];
            Shop = warpData.Overworld2["OW2-93"];
            Hideout = warpData.Overworld2["OW2-35"];

            Zone2A = warpData.Overworld1["OW1-20"];
            Zone2B = warpData.Overworld1["OW1-21"];
            Zone2C = warpData.Overworld1["OW1-30"];
            LeftTalTalA = warpData.Overworld1["OW1-03"];
            LeftTalTalB = warpData.Overworld1["OW1-04"];
            LeftTalTalC = warpData.Overworld1["OW1-10"];
            LeftTalTalD = warpData.Overworld1["OW1-11"];
            LeftTalTalE = warpData.Overworld1["OW1-13"];
            LeftTalTalF = warpData.Overworld1["OW1-15"];
            RightTalTalA = warpData["OW1-18-2"];
            RightTalTalB = warpData["OW1-19"];

            Zone8ExteriorA = warpData.Overworld1["OW1-49"];
            Zone8ExteriorB = warpData.Overworld1["OW1-69"];
            Zone8InteriorA = warpData.Overworld2["OW2-59-1"];
            Zone8InteriorB = warpData.Overworld2["OW2-69"];

            Zone10A = warpData["OW1-2F"];
            Zone10B = warpData["OW1-3F"];

            //generate 32-bit seed
            if (seed.Length != 8 || !uint.TryParse(seed, NumberStyles.HexNumber, CultureInfo.CurrentCulture, out this.seed))
                if (seed != "" && seed != null)
                    this.seed = MurmurHash3.Hash(Encoding.UTF8.GetBytes(seed));
                else
                    this.seed = MurmurHash3.Hash(BitConverter.GetBytes(Environment.TickCount));

            random = new Random((int)this.seed * warpData.Overworld1.Count);
        }

        public void GenerateData()
        {
            log.Write(LogMode.Info, "<l1>", "Seed: " + Seed, "");

            while (!RandomizeWarps()) ;
            if (settings["CheckSolvability"].Enabled)
                CheckWarps();
            LogWarps();

            //var pf = new Pathfinder();
            //string result = pf.IsSolvable(warpData) ? "YES" : "NO";
            //log.Write(LogMode.Info, "", "Is this seed solvable (No WW/OoB)? " + result, "", pf.Result);

            var sb = new StringBuilder();
            var previousWarps = new List<string>();

            foreach (var warpString in new[] { "OW2-B5", "OW1-8C", "OW1-9C", "OW2-2F", "OW2-8F", "OW1-B8-1" })
            {
                sb.AppendLine(warpString + ":");
                var paths = GetPaths(warpData[warpString], ref previousWarps);

                if (paths == null)
                    sb.Append("null");
                else if (paths.Count == 0)
                    sb.Append("not null; count 0");
                else
                {
                    foreach (var path in paths)
                    {
                        sb.Append(path.Zone.ToString());
                        foreach (var constraint in path.Constraints.ToStringList())
                            sb.Append(", " + constraint);
                        sb.Append("\n");
                    }
                }
                sb.Append("\n");
            }

            MessageBox.Show(sb.ToString());
        }

        private bool RandomizeWarps()
        {
            if (!attemptedWarpRandomization)
            {
                log.Write(LogMode.Info, "Randomizing warps...");
                log.Write(LogMode.Spoiler, "", "<l2>", "Warps: " + "(" + warpData.Count + " total)", "<l2>");
                attemptedWarpRandomization = true;
            }
            else
            {
                log.Write(LogMode.Debug, "     Randomizing warps... (again)");
            }

            if (warpData.Overworld1.Count != warpData.Overworld2.Count)
                MessageBox.Show("the warp counts aren't equal :(");

            warpData.ForEach(x => x.WarpValue = (x.Exclude) ? x.DefaultWarpValue : 0);

            if (settings["PairWarps"].Enabled)
            {
                foreach (var warp1 in warpData.Overworld1.Where(x => !x.Exclude))
                {
                    Warp warp2;
                    var remainingWarps = warpData.Overworld2.Where(x => x.WarpValue == 0);

                    int count = 0;
                    do
                    {
                        if (count++ >= 100)
                            return false;

                        warp2 = remainingWarps.ElementAt(random.Next(remainingWarps.Count()));
                    }
                    while (!ValidPairing(warp1, warp2));

                    warp1.WarpValue = warp2.LocationValue;
                    warp2.WarpValue = warp1.LocationValue;
                }
            }
            else
            {
                var destList = new List<string>();

                foreach (var warp1 in warpData.Where(x => !x.Exclude))
                {
                    Warp warp2;
                    var remainingWarps = warpData.Where(x => !destList.Contains(x.Code) && !x.Exclude);

                    int count = 0;
                    do
                    {
                        if (count++ >= 10)
                            return false;

                        warp2 = remainingWarps.ElementAt(random.Next(remainingWarps.Count()));
                    }
                    while (!ValidPairing(warp1, warp2));

                    warp1.WarpValue = warp2.LocationValue;
                    destList.Add(warp2.Code);
                }
            }

            return true;
        }

        private bool ValidPairing(Warp warp1, Warp warp2)
        {
            if (warp1 == warp2)
                return false;

            if (settings["PreventDefaultWarps"].Enabled)
                if (warp1.DefaultWarpValue == warp2.LocationValue)
                    return false;

            if (settings["PreventInaccessible"].Enabled)
            {
                //special conditions for 'locked' warps
                if (warp1.Locked)
                    return CheckLocked(warp1, warp2);
                //otherwise, pairing is only invalid if both warps lead to dead ends
                else
                {
                    var previousWarps = new List<string>();
                    var paths1 = GetPaths(warp1, ref previousWarps);
                    var paths2 = GetPaths(warp2, ref previousWarps);

                    return (paths1 == null || paths1.Count > 0) || (paths2 == null || paths2.Count > 0);
                }
            }
            else if (warp1.Locked)
                return warp2.DeadEnd;

            return true;
        }

        private bool CheckLocked(Warp warp1, Warp warp2)
        {
            //D2 can't be under the rooster statue
            if (warp2.Code == "OW2-24" && warp1.Code == "OW1-92")
                return false;

            var data = warp1.ParentList;

            var D6a_pair = data["OW2-8C"].GetOriginWarp();
            var D6b_pair = data["OW2-6C"].GetOriginWarp();

            var D8a_pair = data["OW2-10"].GetOriginWarp();
            var D8b_pair = data["OW2-00"].GetOriginWarp();
            var D8c_pair = data["OW2-02"].GetOriginWarp();

            //allow D6 if it connects to dead end
            if ((warp2.Code == "OW2-6C" && D6a_pair != null && D6a_pair.DeadEnd)
                || (warp2.Code == "OW2-8C" && D6b_pair != null && D6b_pair.DeadEnd))
                return true;

            //allow D8 if it connects to dead ends
            if ((warp2.Code == "OW2-02" && (D8a_pair != null && D8a_pair.DeadEnd) && (D8b_pair != null && D8b_pair.DeadEnd))
                || (warp2.Code == "OW2-00" && (D8a_pair != null && D8a_pair.DeadEnd) && (D8c_pair != null && D8c_pair.DeadEnd))
                || (warp2.Code == "OW2-10" && (D8b_pair != null && D8b_pair.DeadEnd) && (D8c_pair != null && D8c_pair.DeadEnd)))
                return true;

            //'locked' warps must only connect to dead ends (other than the above exceptions)
            var previousWarps = new List<string>();
            var paths1 = GetPaths(warp1, ref previousWarps);
            var paths2 = GetPaths(warp2, ref previousWarps);

            return (paths1 == null || paths1.Count > 0) && (paths2 != null && paths2.Count == 0);
            //return warp2.DeadEnd;
        }

        private void CheckWarps()
        {
            UpdateConditions();

            int count = 0;
            while (GlobalCheck)
            {
                CheckZone1();
                CheckZone2();
                CheckSE();
                CheckZone8();
                CheckZone10();
                CheckWaterfall();
                CheckDungeons();

                if (count++ >= 10)
                {
                    log.Write(LogMode.Info, "ERROR: a ROM could not be created for this seed.");
                    break;
                }
            }
        }

        private void CheckDungeons()
        {
            log.Write(LogMode.Debug, " -> CheckDungeons()");

            while (D1Check)
                CreateNewPair(D1, "D1");

            while (D2D6Check)
            {
                int rand = random.Next(2);
                if (rand == 0)
                    CreateNewPair(D2, "D2");
                else
                    CreateNewPair(D6, "D6");
            }

            while (D4Check)
                CreateNewPair(D4, "D4");

            while (D5Check)
                CreateNewPair(D5, "D5");

            while (ShopCheck)
                CreateNewPair(Shop, "shop");
        }

        private void CheckZone1()
        {
            log.Write(LogMode.Debug, " -> CheckZone1()");

            //make sure either the feather or bracelet is accessible from Zone 1
            log.Write(LogMode.Debug, "   -> Checking accessibility...");
            while (Zone1AccessibilityCheck)
            {
                //var sb = new StringBuilder();
                //sb.AppendLine("D1Paths:");
                //foreach (var path in D1Paths)
                //{
                //    sb.Append(path.Zone.ToString());
                //    foreach (var constraint in path.Constraints.ToStringList())
                //        sb.Append(", " + constraint);
                //    sb.AppendLine();
                //}
                //sb.AppendLine("\nD2Paths:");
                //foreach (var path in D2Paths)
                //{
                //    sb.Append(path.Zone.ToString());
                //    foreach (var constraint in path.Constraints.ToStringList())
                //        sb.Append(", " + constraint);
                //    sb.AppendLine();
                //}
                //sb.AppendLine("\nD6Paths:");
                //foreach (var path in D6Paths)
                //{
                //    sb.Append(path.Zone.ToString());
                //    foreach (var constraint in path.Constraints.ToStringList())
                //        sb.Append(", " + constraint);
                //    sb.AppendLine();
                //}

                //MessageBox.Show(sb.ToString());

                int rand = random.Next(3);
                if (rand == 0)
                    CreateNewPair(D1, "D1");
                else if (rand == 1)
                    CreateNewPair(D2, "D2");
                else
                    CreateNewPair(D6, "D6");
            }

            //accommodate for dungeons stuck behind swamp flowers
            log.Write(LogMode.Debug, "   -> Checking D2 entrance...");
            while (SwampD1Check)
            {
                int rand = random.Next(2);
                if (rand == 0)
                    CreateNewPair(D2, "D2");
                else
                    CreateNewPair(D6, "D6");
            }

            while (SwampD2Check)
            {
                int rand = random.Next(3);
                if (rand == 0)
                {
                    CreateNewPair(D1, "D1");
                    while (HideoutCheck)
                        CreateNewPair(Hideout, "moblin hideout");
                }
                else if (rand == 1)
                    CreateNewPair(D5, "D5");
                else
                    CreateNewPair(D6, "D6");
            }

            while (SwampD6Check)
            {
                int rand = random.Next(3);
                if (rand == 0)
                {
                    CreateNewPair(D1, "D1");
                    while (HideoutCheck)
                        CreateNewPair(Hideout, "moblin hideout");
                }
                else if (rand == 1)
                    CreateNewPair(D2, "D2");
                else
                    CreateNewPair(D5, "D5");
            }

            //if D1 is in Zone 1, then the bracelet must be accessible from Zones 1, 2 or 3
            while (Zone1BraceletCheck)
            {
                int rand = random.Next(2);
                if (rand == 0)
                    CreateNewPair(D2, "D2");
                else
                    CreateNewPair(D6, "D6");
            }
        }

        private void CheckZone2() //there's got to be a better way to do this
        {
            log.Write(LogMode.Debug, " -> CheckZone2()");

            //if D1 is in Zone 2, then Zone 2 must be accessible either from the mountain or by the other 2 warps
            while (Zone2AccessibilityCheck1)
            {
                int rand = random.Next(4);
                if (rand == 0)
                    CreateNewPair(Zone2A, Zone2A.Code);
                else if (rand == 1)
                    CreateNewPair(Zone2B, Zone2B.Code);
                else if (rand == 2)
                    CreateNewPair(Zone2C, Zone2C.Code);
                else
                    CreateNewPair(D1, "D1");
            }

            //if D1 is not in Zone 2, then Zone 2 must either be inaccessible from the mountain or accessible by the other 2 warps
            while (Zone2AccessibilityCheck2)
            {
                int rand = random.Next(3);
                if (rand == 0)
                    CreateNewPair(Zone2A, Zone2A.Code);
                else if (rand == 1)
                    CreateNewPair(Zone2B, Zone2B.Code);
                else
                    CreateNewPair(Zone2C, Zone2C.Code);
            }
        }

        private void CheckSE() //Zones 6 and 7
        {
            log.Write(LogMode.Debug, " -> CheckSE()");

            //if the south east is isolated, then D4 and D5 can't both be there
            while (SEAccessibilityCheck)
            {
                int rand = random.Next(2);
                if (rand == 0)
                    CreateNewPair(D4, "D4");
                else
                    CreateNewPair(D5, "D5");
            }

            //if there are no 'feather-less' paths to the south east, then D1 cannot be there
            while (SED1Check)
                CreateNewPair(D1, "D1");
        }

        private void CheckZone8()
        {
            log.Write(LogMode.Debug, " -> CheckZone8()");

            //if Kanalet's interior connects to the exterior, both exterior warps can't lead to dead ends
            while (Zone8AccessibilityCheck)
            {
                int rand = random.Next(2);
                if (rand == 0)
                    CreateNewPair(Zone8ExteriorA, "OW1-49");
                else
                    CreateNewPair(Zone8ExteriorB, "OW1-69");
            }

            //if D1 spawns in Zone 8, make sure Zone 8 is accessible w/o the feather (other dungeons shouldn't be a concern)
            while (Zone8D1Check)
            {
                int rand = random.Next(4);
                if (rand == 0)
                    CreateNewPair(Zone8ExteriorA, "OW1-49");
                else if (rand == 1)
                    CreateNewPair(Zone8ExteriorB, "OW1-69");
                else if (rand == 2)
                    CreateNewPair(Zone8InteriorA, "OW2-59-1");
                else
                    CreateNewPair(Zone8InteriorB, "OW2-69");
            }
        }

        private void CheckZone10()
        {
            log.Write(LogMode.Debug, " -> CheckZone10()");

            //make sure D5 isn't trapped in Zone 10
            while (Zone10Check)
                CreateNewPair(D5, "D5");
        }

        private void CheckWaterfall()
        {
            log.Write(LogMode.Debug, " -> CheckWaterfall()");

            //if D4 is behind the waterfall, the angler key must be accessible
            while (WaterfallKeyCheck)
                CreateNewPair(D4, "D4");

            //if D4 is behind the waterfall, it must be accessible from above
            while (WaterfallAccessibilityCheck)
            {
                int rand = random.Next(2);
                if (rand == 0)
                    CreateNewPair(RightTalTalA, "OW1-18-2");
                else
                    CreateNewPair(RightTalTalB, "OW1-19");
            }
        }

        private void UpdateConditions()
        {
            UpdatePaths();

            //conditions for 'special' warps
            D1Check = !D1Paths.Exists(x => !x.Constraints.Contains(Items.Feather));
            D2D6Check = !D2Paths.Exists(x => !x.Constraints.Contains(Items.Bracelet)) || !D6Paths.Exists(x => !x.Constraints.Contains(Items.Bracelet));
            D4Check = !D4Paths.Exists(x => !x.Constraints.Contains(Items.Flippers));
            D5Check = !D5Paths.Exists(x => !x.Constraints.Contains(Items.Hookshot))
                      && (!D1Paths.Exists(x => !x.Constraints.Contains(Items.Hookshot)) || !D3Paths.Exists(x => !x.Constraints.Contains(Items.Hookshot)));
            ShopCheck = !ShopPaths.Exists(x => x.Zone == 1 && x.Constraints.IsEmpty);
            HideoutCheck = !IsAccessible(HideoutPaths, 1) && !IsAccessible(HideoutPaths, 2) && !IsAccessible(HideoutPaths, 3);

            //conditions for Zone 1
            Zone1AccessibilityCheck = !IsAccessible(D1Paths, 1) && !IsAccessible(D2Paths, 1) && !IsAccessible(D6Paths, 1);
            SwampD1Check = !D1Paths.Exists(x => !x.Constraints.Contains(MiscFlags.BowWow)) && !IsAccessible(D2Paths, 1) && !IsAccessible(D6Paths, 1);
            SwampD2Check = !D2Paths.Exists(x => !x.Constraints.Contains(MiscFlags.BowWow)) && !IsAccessible(D1Paths, 1) && !IsAccessible(D5Paths, 1) && !IsAccessible(D6Paths, 1);
            SwampD6Check = !D6Paths.Exists(x => !x.Constraints.Contains(MiscFlags.BowWow)) && !IsAccessible(D1Paths, 1) && !IsAccessible(D2Paths, 1) && !IsAccessible(D5Paths, 1);
            Zone1BraceletCheck = IsAccessible(D1Paths, 1)
                                 && !IsAccessible(D2Paths, 1, "Feather") && !IsAccessible(D2Paths, 2, "Feather") && !IsAccessible(D2Paths, 3, "Feather")
                                 && !IsAccessible(D6Paths, 1, "Feather") && !IsAccessible(D6Paths, 2, "Feather") && !IsAccessible(D6Paths, 3, "Feather");

            //conditions for Zone 2
            Zone2AccessibilityCheck1 = IsAccessible(D1Paths, 2)
                                       && !LeftTalTalAPaths.Exists(x => !x.Constraints.Contains(Items.Feather))
                                       && !LeftTalTalBPaths.Exists(x => !x.Constraints.Contains(Items.Feather))
                                       && !LeftTalTalCPaths.Exists(x => !x.Constraints.Contains(Items.Feather))
                                       && !LeftTalTalDPaths.Exists(x => !x.Constraints.Contains(Items.Feather))
                                       && !LeftTalTalEPaths.Exists(x => !x.Constraints.Contains(Items.Feather))
                                       && !LeftTalTalFPaths.Exists(x => !x.Constraints.Contains(Items.Feather))
                                       && !IsAccessible(Zone2APaths, 1) && !IsAccessible(Zone2BPaths, 1) && !IsAccessible(Zone2CPaths, 1);

            Zone2AccessibilityCheck2 = !IsAccessible(D1Paths, 2)
                                       && (LeftTalTalAPaths.Exists(x => !x.Constraints.Contains(Items.Feather))
                                           || LeftTalTalBPaths.Exists(x => !x.Constraints.Contains(Items.Feather))
                                           || LeftTalTalCPaths.Exists(x => !x.Constraints.Contains(Items.Feather))
                                           || LeftTalTalDPaths.Exists(x => !x.Constraints.Contains(Items.Feather))
                                           || LeftTalTalEPaths.Exists(x => !x.Constraints.Contains(Items.Feather))
                                           || LeftTalTalFPaths.Exists(x => !x.Constraints.Contains(Items.Feather)))
                                       && !IsAccessible(Zone2APaths, 1) && !IsAccessible(Zone2BPaths, 1) && !IsAccessible(Zone2CPaths, 1);

            //conditions for SE area
            SEAccessibilityCheck = !D4Paths.Exists(x => x.Zone != 6 && x.Zone != 7) && !D5Paths.Exists(x => x.Zone != 6 && x.Zone != 7)
                                   && !SEPaths.Exists(x => x.Zone != 6 && x.Zone != 7 && !x.Constraints.Contains(Items.Flippers) && !x.Constraints.Contains(Items.Hookshot));
            SED1Check = !D1Paths.Exists(x => x.Zone != 6 && x.Zone != 7) && !SEPaths.Exists(x => x.Zone != 6 && x.Zone != 7 && !x.Constraints.Contains(Items.Feather));

            //conditions for Zone 8
            Zone8AccessibilityCheck = ((Zone8InteriorAPaths.Count > 0 && !Zone8InteriorAPaths.Exists(x => x.Zone != 8)) || (Zone8InteriorBPaths.Count > 0 && !Zone8InteriorBPaths.Exists(x => x.Zone != 8)))
                                      && Zone8ExteriorAPaths.Count == 0
                                      && Zone8ExteriorBPaths.Count == 0;
            Zone8D1Check = !D1Paths.Exists(x => x.Zone != 8)
                           && !Zone8ExteriorAPaths.Exists(x => !x.Constraints.Contains(Items.Feather)) && !Zone8ExteriorBPaths.Exists(x => !x.Constraints.Contains(Items.Feather))
                           && !Zone8InteriorAPaths.Exists(x => !x.Constraints.Contains(Items.Feather)) && !Zone8InteriorBPaths.Exists(x => !x.Constraints.Contains(Items.Feather));

            //condition for Zone 10
            Zone10Check = !D5Paths.Exists(x => x.Zone != 10) && !Zone10APaths.Exists(x => !x.Constraints.Contains(Items.Hookshot)) && !Zone10BPaths.Exists(x => !x.Constraints.Contains(Items.Hookshot));

            //conditions for the waterfall
            var D4_pair = D4.GetOriginWarp();
            WaterfallKeyCheck = (D4_pair.Code == "OW1-2B-1" || D4_pair.Code == "OW1-2B-2") && !SEPaths.Exists(x => !x.Constraints.Contains(Items.Flippers));
            WaterfallAccessibilityCheck = (D4_pair.Code == "OW1-2B-1" || D4_pair.Code == "OW1-2B-2") && RightTalTalAPaths.Count != 0 && RightTalTalBPaths.Count != 0;

            GlobalCheck = D1Check || D2D6Check || D4Check || D5Check || ShopCheck
                          || Zone1AccessibilityCheck || SwampD1Check || SwampD2Check || SwampD6Check || Zone1BraceletCheck
                          || Zone2AccessibilityCheck1 || Zone2AccessibilityCheck2
                          || Zone8AccessibilityCheck || Zone8D1Check
                          || SEAccessibilityCheck || SED1Check
                          || Zone10Check
                          || WaterfallAccessibilityCheck || WaterfallKeyCheck;
        }

        private void UpdatePaths()
        {            
            var previousWarps = new List<string>();

            D1Paths = GetPaths(D1, ref previousWarps);
            D2Paths = GetPaths(D2, ref previousWarps);
            D3Paths = GetPaths(D3, ref previousWarps);
            D4Paths = GetPaths(D4, ref previousWarps);
            D5Paths = GetPaths(D5, ref previousWarps);
            D6Paths = GetPaths(D6, ref previousWarps);
            ShopPaths = GetPaths(Shop, ref previousWarps);
            HideoutPaths = GetPaths(Hideout, ref previousWarps);
            Zone2APaths = GetPaths(Zone2A, ref previousWarps);
            Zone2BPaths = GetPaths(Zone2B, ref previousWarps);
            Zone2CPaths = GetPaths(Zone2C, ref previousWarps);
            LeftTalTalAPaths = GetPaths(LeftTalTalA, ref previousWarps);
            LeftTalTalBPaths = GetPaths(LeftTalTalB, ref previousWarps);
            LeftTalTalCPaths = GetPaths(LeftTalTalC, ref previousWarps);
            LeftTalTalDPaths = GetPaths(LeftTalTalD, ref previousWarps);
            LeftTalTalEPaths = GetPaths(LeftTalTalE, ref previousWarps);
            LeftTalTalFPaths = GetPaths(LeftTalTalF, ref previousWarps);
            RightTalTalAPaths = GetPaths(RightTalTalA, ref previousWarps);
            RightTalTalBPaths = GetPaths(RightTalTalB, ref previousWarps);
            Zone8ExteriorAPaths = GetPaths(Zone8ExteriorA, ref previousWarps);
            Zone8ExteriorBPaths = GetPaths(Zone8ExteriorB, ref previousWarps);
            Zone8InteriorAPaths = GetPaths(Zone8InteriorA, ref previousWarps);
            Zone8InteriorBPaths = GetPaths(Zone8InteriorB, ref previousWarps);
            Zone10APaths = GetPaths(Zone10A, ref previousWarps);
            Zone10BPaths = GetPaths(Zone10B, ref previousWarps);

            SEPaths = new List<Connection>();
            foreach (var warp in warpData.Overworld1.Where(x => x.ZoneConnections.Outward.Exists(y => (y.Zone == 6 || y.Zone == 7) && y.IsAccessible())))
                SEPaths.AddRange(GetPaths(warp, ref previousWarps));
        }

        private void CreateNewPair(Warp warp1, string name)
        {
            log.Write(LogMode.Debug, "        Shuffling " + name + "...");

            Warp testWarp1, testWarp1_pair, testWarp2, testWarp2_pair;
            //var warp1_pair = warp1.GetOriginWarp();

            do
            {
                var testData = warpData.Copy();

                testWarp1 = testData[warp1.Code];
                testWarp1_pair = testWarp1.GetOriginWarp();

                if (warp1.Code.Contains("OW2"))
                    testWarp2 = testData.Overworld1[random.Next(warpData.Overworld1.Count)];
                else
                    testWarp2 = testData.Overworld2[random.Next(warpData.Overworld2.Count)];

                testWarp2_pair = testWarp2.GetOriginWarp();

                testWarp1.WarpValue = testWarp2.LocationValue;
                testWarp2.WarpValue = testWarp1.LocationValue;
                testWarp1_pair.WarpValue = testWarp2_pair.LocationValue;
                testWarp2_pair.WarpValue = testWarp1_pair.LocationValue;
            }
            while (testWarp2_pair.Special || !ValidPairing(testWarp1, testWarp2) || !ValidPairing(testWarp1_pair, testWarp2_pair));

            var warp1_pair = warp1.GetOriginWarp();
            var warp2 = warpData[testWarp2.Code];
            var warp2_pair = warp2.GetOriginWarp(); 

            warp1.WarpValue = warp2.LocationValue;
            warp2.WarpValue = warp1.LocationValue;

            warp1_pair.WarpValue = warp2_pair.LocationValue;
            warp2_pair.WarpValue = warp1_pair.LocationValue;

            UpdateConditions();
        }

        private bool IsAccessible(List<Connection> paths, int mainZone, string allowedConstraint = null)
        {
            //first check immediate paths
            if (paths.Exists(x => x.Zone == mainZone && x.IsAccessible(allowedConstraint)))
                return true;

            //next follow each path to the next zone
            var zoneNums = new List<int>();
            var zonePaths = new List<Connection>();

            foreach (var path in paths.Where(x => x.Zone != mainZone && x.IsAccessible(allowedConstraint)))
                if (!zoneNums.Contains(path.Zone))
                    zoneNums.Add(path.Zone);

            foreach (int num in zoneNums)
            {
                var zoneWarps = warpData.Overworld1.Where(x => x.ZoneConnections.Inward.Exists(y => y.Zone == num && y.IsAccessible(allowedConstraint)));

                foreach (var warp in zoneWarps)
                {
                    var previousWarps = new List<string>();
                    zonePaths.AddRange(GetPaths(warp, ref previousWarps));
                }

                foreach (var connection in ZoneData.Connections[num])
                    zonePaths.Add(connection);
            }

            if (zonePaths.Exists(x => x.Zone == mainZone && x.IsAccessible(allowedConstraint)))
                return true;

            return false;
        }

        private List<Connection> GetPaths(Warp destination, ref List<string> previousWarps, List<Enum> constraintList = null)
        {
            var list = new List<Connection>();

            if (previousWarps.Contains(destination.Code))
                return list;
            else
                previousWarps.Add(destination.Code);

            if (constraintList == null) //only possible on first iteration
            {
                constraintList = new List<Enum>();
                previousWarps = new List<string>();
            }

            var current = destination.GetOriginWarp();

            if (current == null)
                return null;

            bool nullFlag = false;
            var data = destination.ParentList;

            foreach (var connection in current.Connections.Inward)
            {
                var updatedConstraintList = constraintList;
                if (!connection.Constraints.IsEmpty)
                    updatedConstraintList.AddRange(connection.Constraints.ToEnumList());

                var paths = GetPaths(data[connection.Code], ref previousWarps, updatedConstraintList);

                if (paths == null)
                    nullFlag = true;
                else
                    list.AddRange(paths);
            }
            
            foreach (var zoneConnection in current.ZoneConnections.Inward)
            {
                var updatedConstraintList = constraintList;
                if (!zoneConnection.Constraints.IsEmpty)
                    updatedConstraintList.AddRange(zoneConnection.Constraints.ToEnumList());

                list.Add(new Connection(zoneConnection.Zone, Connection.Inward, updatedConstraintList.ToArray()));
            }

            return (nullFlag && list.Count == 0) ? null : list;
            //return list;
        }

        private void LogWarps()
        {
            if (settings["PairWarps"].Enabled)
            {
                foreach (var warp1 in warpData.Overworld1)
                {
                    var warp2 = warp1.GetDestinationWarp();

                    string text1 = "[" + warp1.Code + "] " + warp1.Description;
                    string text2 = "[" + warp2.Code + "] " + warp2.Description;

                    log.Write(LogMode.Spoiler, text1 + "\r\n    ^=> " + text2 + "\r\n");
                }
            }
            else
            {
                var list = new List<string>();

                foreach (var warp1 in warpData)
                {
                    var warp2 = warp1.GetDestinationWarp();

                    string text1 = "[" + warp1.Code + "] " + warp1.Description;
                    string text2;

                    list.Add(warp1.Code);

                    if (warp2 != null)
                    {
                        text2 = "[" + warp2.Code + "] " + warp2.Description;
                        list.Add(warp2.Code);
                    }
                    else
                        text2 = "null";



                    log.Write(LogMode.Spoiler, text1 + "\r\n    |=> " + text2 + "\r\n");
                }

                foreach (var warp in warpData)
                {
                    if (list.Where(x => x == warp.Code).Count() > 2)
                        MessageBox.Show(warp.Code);
                }
            }
        }
    }
}

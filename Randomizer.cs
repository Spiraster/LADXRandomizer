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
            if (settings["CheckSolvability"].Enabled)
                CheckWarps();
            LogWarps();
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
                warpData.Overworld2.ForEach(x => x.Destination = 0);
            }

            if (warpData.Overworld1.Count != warpData.Overworld2.Count)
                MessageBox.Show("the warp counts aren't equal :(");

            if (settings["PairWarps"].Enabled)
            {
                foreach (var warp1 in warpData.Overworld1)
                {
                    Warp warp2;
                    var remainingWarps = warpData.Overworld2.Where(x => x.Destination == 0);

                    int count = 0;
                    do
                    {
                        if (count++ >= 1000)
                            return false;

                        warp2 = remainingWarps.ElementAt(random.Next(remainingWarps.Count()));
                    }
                    while (!ValidPairing(warp1, warp2));

                    warp1.Destination = warp2.Location;
                    warp2.Destination = warp1.Location;
                }
            }
            else
            {
                var destList = new List<string>();

                foreach (var warp1 in warpData)
                {
                    Warp warp2;
                    var remainingWarps = warpData.Where(x => !destList.Contains(x.Code));

                    int count = 0;
                    do
                    {
                        if (count++ >= 10)
                            return false;
                        
                        warp2 = remainingWarps.ElementAt(random.Next(remainingWarps.Count()));
                    }
                    while (!ValidPairing(warp1, warp2));

                    warp1.Destination = warp2.Location;
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
                if (warp1.Default == warp2.Location)
                    return false;

            if (settings["PreventInaccessible"].Enabled)
            {
                //special conditions for 'locked' warps
                if (warp1.Locked)
                    CheckLocked(warp1, warp2);
                //otherwise, pairing is only invalid if both warps lead to dead ends
                else if (settings["PairWarps"].Enabled)
                    return FindPath(warp1, new List<string> { warp2.Code }) || FindPath(warp2, new List<string> { warp1.Code });
                else
                {

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

            var D6a_pair = warpData["OW2-8C"].GetPair();
            var D6b_pair = warpData["OW2-6C"].GetPair();

            var D8a_pair = warpData["OW2-10"].GetPair();
            var D8b_pair = warpData["OW2-00"].GetPair();
            var D8c_pair = warpData["OW2-02"].GetPair();

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
            return warp2.DeadEnd;
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
            D1Check = !D1Paths.Exists(x => !x.Constraints.Contains(Item.Feather));
            D2D6Check = !D2Paths.Exists(x => !x.Constraints.Contains(Item.Bracelet)) || !D6Paths.Exists(x => !x.Constraints.Contains(Item.Bracelet));
            D4Check = !D4Paths.Exists(x => !x.Constraints.Contains(Item.Flippers));
            D5Check = !D5Paths.Exists(x => !x.Constraints.Contains(Item.Hookshot))
                      && (!D1Paths.Exists(x => !x.Constraints.Contains(Item.Hookshot)) || !D3Paths.Exists(x => !x.Constraints.Contains(Item.Hookshot)));
            ShopCheck = !ShopPaths.Exists(x => x.Zone == 1 && !x.Constraints.ToList().Exists(y => y != Item.None));
            HideoutCheck = !isAccessible(HideoutPaths, 1) && !isAccessible(HideoutPaths, 2) && !isAccessible(HideoutPaths, 3);

            //conditions for Zone 1
            Zone1AccessibilityCheck = !isAccessible(D1Paths, 1) && !isAccessible(D2Paths, 1) && !isAccessible(D6Paths, 1);
            SwampD1Check = !D1Paths.Exists(x => !x.Constraints.Contains(Item.BowWow)) && !isAccessible(D2Paths, 1) && !isAccessible(D6Paths, 1);
            SwampD2Check = !D2Paths.Exists(x => !x.Constraints.Contains(Item.BowWow)) && !isAccessible(D1Paths, 1) && !isAccessible(D5Paths, 1) && !isAccessible(D6Paths, 1);
            SwampD6Check = !D6Paths.Exists(x => !x.Constraints.Contains(Item.BowWow)) && !isAccessible(D1Paths, 1) && !isAccessible(D2Paths, 1) && !isAccessible(D5Paths, 1);
            Zone1BraceletCheck = isAccessible(D1Paths, 1)
                                 && !isAccessible(D2Paths, 1, Item.Feather) && !isAccessible(D2Paths, 2, Item.Feather) && !isAccessible(D2Paths, 3, Item.Feather)
                                 && !isAccessible(D6Paths, 1, Item.Feather) && !isAccessible(D6Paths, 2, Item.Feather) && !isAccessible(D6Paths, 3, Item.Feather);

            //conditions for Zone 2
            Zone2AccessibilityCheck1 = isAccessible(D1Paths, 2)
                                       && !LeftTalTalAPaths.Exists(x => !x.Constraints.Contains(Item.Feather))
                                       && !LeftTalTalBPaths.Exists(x => !x.Constraints.Contains(Item.Feather))
                                       && !LeftTalTalCPaths.Exists(x => !x.Constraints.Contains(Item.Feather))
                                       && !LeftTalTalDPaths.Exists(x => !x.Constraints.Contains(Item.Feather))
                                       && !LeftTalTalEPaths.Exists(x => !x.Constraints.Contains(Item.Feather))
                                       && !LeftTalTalFPaths.Exists(x => !x.Constraints.Contains(Item.Feather))
                                       && !isAccessible(Zone2APaths, 1) && !isAccessible(Zone2BPaths, 1) && !isAccessible(Zone2CPaths, 1);

            Zone2AccessibilityCheck2 = !isAccessible(D1Paths, 2)
                                       && (LeftTalTalAPaths.Exists(x => !x.Constraints.Contains(Item.Feather))
                                           || LeftTalTalBPaths.Exists(x => !x.Constraints.Contains(Item.Feather))
                                           || LeftTalTalCPaths.Exists(x => !x.Constraints.Contains(Item.Feather))
                                           || LeftTalTalDPaths.Exists(x => !x.Constraints.Contains(Item.Feather))
                                           || LeftTalTalEPaths.Exists(x => !x.Constraints.Contains(Item.Feather))
                                           || LeftTalTalFPaths.Exists(x => !x.Constraints.Contains(Item.Feather)))
                                       && !isAccessible(Zone2APaths, 1) && !isAccessible(Zone2BPaths, 1) && !isAccessible(Zone2CPaths, 1);

            //conditions for SE area
            SEAccessibilityCheck = !D4Paths.Exists(x => x.Zone != 6 && x.Zone != 7) && !D5Paths.Exists(x => x.Zone != 6 && x.Zone != 7)
                                   && !SEPaths.Exists(x => x.Zone != 6 && x.Zone != 7 && !x.Constraints.Contains(Item.Flippers) && !x.Constraints.Contains(Item.Hookshot));
            SED1Check = !D1Paths.Exists(x => x.Zone != 6 && x.Zone != 7) && !SEPaths.Exists(x => x.Zone != 6 && x.Zone != 7 && !x.Constraints.Contains(Item.Feather));

            //conditions for Zone 8
            Zone8AccessibilityCheck = ((Zone8InteriorAPaths.Count > 0 && !Zone8InteriorAPaths.Exists(x => x.Zone != 8)) || (Zone8InteriorBPaths.Count > 0 && !Zone8InteriorBPaths.Exists(x => x.Zone != 8)))
                                      && Zone8ExteriorAPaths.Count == 0
                                      && Zone8ExteriorBPaths.Count == 0;
            Zone8D1Check = !D1Paths.Exists(x => x.Zone != 8)
                           && !Zone8ExteriorAPaths.Exists(x => !x.Constraints.Contains(Item.Feather)) && !Zone8ExteriorBPaths.Exists(x => !x.Constraints.Contains(Item.Feather))
                           && !Zone8InteriorAPaths.Exists(x => !x.Constraints.Contains(Item.Feather)) && !Zone8InteriorBPaths.Exists(x => !x.Constraints.Contains(Item.Feather));

            //condition for Zone 10
            Zone10Check = !D5Paths.Exists(x => x.Zone != 10) && !Zone10APaths.Exists(x => !x.Constraints.Contains(Item.Hookshot)) && !Zone10BPaths.Exists(x => !x.Constraints.Contains(Item.Hookshot));

            //conditions for the waterfall
            var D4_pair = D4.GetPair();
            WaterfallKeyCheck = (D4_pair.Code == "OW1-2B-1" || D4_pair.Code == "OW1-2B-2") && !SEPaths.Exists(x => !x.Constraints.Contains(Item.Flippers));
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
            D1Paths = new List<Connection>();
            D2Paths = new List<Connection>();
            D3Paths = new List<Connection>();
            D4Paths = new List<Connection>();
            D5Paths = new List<Connection>();
            D6Paths = new List<Connection>();
            ShopPaths = new List<Connection>();
            HideoutPaths = new List<Connection>();
            Zone2APaths = new List<Connection>();
            Zone2BPaths = new List<Connection>();
            Zone2CPaths = new List<Connection>();
            LeftTalTalAPaths = new List<Connection>();
            LeftTalTalBPaths = new List<Connection>();
            LeftTalTalCPaths = new List<Connection>();
            LeftTalTalDPaths = new List<Connection>();
            LeftTalTalEPaths = new List<Connection>();
            LeftTalTalFPaths = new List<Connection>();
            RightTalTalAPaths = new List<Connection>();
            RightTalTalBPaths = new List<Connection>();
            SEPaths = new List<Connection>();
            Zone8ExteriorAPaths = new List<Connection>();
            Zone8ExteriorBPaths = new List<Connection>();
            Zone8InteriorAPaths = new List<Connection>();
            Zone8InteriorBPaths = new List<Connection>();
            Zone10APaths = new List<Connection>();
            Zone10BPaths = new List<Connection>();

            GetPaths(D1, ref D1Paths);
            GetPaths(D2, ref D2Paths);
            GetPaths(D3, ref D3Paths);
            GetPaths(D4, ref D4Paths);
            GetPaths(D5, ref D5Paths);
            GetPaths(D6, ref D6Paths);
            GetPaths(Shop, ref ShopPaths);
            GetPaths(Hideout, ref HideoutPaths);
            GetPaths(Zone2A, ref Zone2APaths);
            GetPaths(Zone2B, ref Zone2BPaths);
            GetPaths(Zone2C, ref Zone2CPaths);
            GetPaths(LeftTalTalA, ref LeftTalTalAPaths);
            GetPaths(LeftTalTalB, ref LeftTalTalBPaths);
            GetPaths(LeftTalTalC, ref LeftTalTalCPaths);
            GetPaths(LeftTalTalD, ref LeftTalTalDPaths);
            GetPaths(LeftTalTalE, ref LeftTalTalEPaths);
            GetPaths(LeftTalTalF, ref LeftTalTalFPaths);
            GetPaths(RightTalTalA, ref RightTalTalAPaths);
            GetPaths(RightTalTalB, ref RightTalTalBPaths);
            GetPaths(Zone8ExteriorA, ref Zone8ExteriorAPaths);
            GetPaths(Zone8ExteriorB, ref Zone8ExteriorBPaths);
            GetPaths(Zone8InteriorA, ref Zone8InteriorAPaths);
            GetPaths(Zone8InteriorB, ref Zone8InteriorBPaths);
            GetPaths(Zone10A, ref Zone10APaths);
            GetPaths(Zone10B, ref Zone10BPaths);

            foreach (var warp in warpData.Overworld1.Where(x => x.ZoneConnections != null && x.ZoneConnections.ToList().Exists(y => (y.Zone == 6 || y.Zone == 7) && y.Accessible())))
                GetPaths(warp, ref SEPaths);
        }

        private void CreateNewPair(Warp warp1, string name) //'warp1' must be a 'special' warp (OW2)
        {
            log.Write(LogMode.Debug, "        Shuffling " + name + "...");

            Warp warp2, warp2_pair;
            var warp1_pair = warp1.GetPair();

            do
            {
                if (warp1.Code.Contains("OW2"))
                    warp2 = warpData.Overworld1[random.Next(warpData.Overworld1.Count)];
                else
                    warp2 = warpData.Overworld2[random.Next(warpData.Overworld2.Count)];

                warp2_pair = warp2.GetPair();
            }
            while (warp2_pair.Special || !ValidPairing(warp1, warp2) || !ValidPairing(warp1_pair, warp2_pair));

            warp1.Destination = warp2.Location;
            warp2.Destination = warp1.Location;

            warp1_pair.Destination = warp2_pair.Location;
            warp2_pair.Destination = warp1_pair.Location;

            UpdateConditions();
        }

        private bool isAccessible(List<Connection> paths, int mainZone, Item allowedConstraint = 0)
        {
            //first check immediate paths
            if (paths.Exists(x => x.Zone == mainZone && x.Accessible(allowedConstraint)))
                return true;
            
            //next follow each path to the next zone
            var zoneNums = new List<int>();
            var zonePaths = new List<Connection>();

            foreach (var path in paths.Where(x => x.Zone != mainZone && x.Accessible(allowedConstraint)))
                if (!zoneNums.Contains(path.Zone))
                    zoneNums.Add(path.Zone);

            foreach (int num in zoneNums)
            {
                var zone = warpData.Overworld1.Where(x => x.ZoneConnections != null && x.ZoneConnections.ToList().Exists(y => y.Zone == num && y.Constraints.Contains(Item.None)));
                foreach (var warp in zone)
                    GetPaths(warp, ref zonePaths);

                foreach (var connection in ZoneData.Connections[num])
                    zonePaths.Add(connection);
            }

            if (zonePaths.Exists(x => x.Zone == mainZone && x.Accessible(allowedConstraint)))
                return true;

            return false;
        }

        private void GetPaths(Warp warp, ref List<Connection> pathList, List<string> previousWarps = null, List<Item> constraintList = null) //returns all paths to zones and their constraints
        {
            warp = warp.GetOriginWarp();

            if (constraintList == null)
                constraintList = new List<Item>();

            if (previousWarps == null)
                previousWarps = new List<string>();
            else if (previousWarps.Contains(warp.Code))
                return;

            if (warp.Connections != null)
            {
                foreach (var connection in warp.Connections)
                {
                    if (previousWarps.Contains(connection.Code))
                        continue;

                    var nextWarp = warpData[connection.Code];

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
            else if (warp.Locked) //only 4 warps fit this criteria
            {
                pathList.Add(new Connection(0));
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
                        continue; //a loop back to a previous point in the path is considered a dead end

                    var nextwarp = warpData[connection.Code].GetPair();
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

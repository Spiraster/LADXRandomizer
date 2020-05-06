using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADXRandomizer.Pathfinding
{ 
    public class Pathfinder
    {
        WarpData warpData;
        Inventory inventory;

        private static string origin { get; } = "OW2-A2";

        private static string[] DungeonWarps = new string[]
        {
            "OW2-D3",   //D1
            "OW2-24",   //D2
            "OW2-B5",   //D3
            "OW2-2B-1", //D4
            "OW2-D9-1", //D5
            "OW2-8C",   //D6a (entrance)
            "OW2-6C",   //D6b (stairs)
            "OW2-0E",   //D7
            "OW2-10",   //D8a (entrance)
            "OW2-00",   //D8b (stairs [00])
            "OW2-02",   //D8c (stairs [02])
        };

        public Pathfinder(WarpData warpData)
        {
            this.warpData = warpData;
            inventory = new Inventory();
        }

        private void FollowPath(Warp origin, ref List<string> previousWarps)
        {
            if (previousWarps == null)
                previousWarps = new List<string>();
            else if (previousWarps.Contains(origin.Code))
                return;
            else
                previousWarps.Add(origin.Code);

            var current = origin.GetDestinationWarp();

            if (DungeonWarps.Contains(current.Code))
                UpdateInventory(current.Code);

            foreach (var connection in current.Connections)
            {
                if (previousWarps.Contains(connection.Code))
                    continue;

                if (inventory.Contains(connection.Constraints))
                    FollowPath(warpData[connection.Code], ref previousWarps);
            }

            foreach (var connection in current.ZoneConnections)
            {
                foreach (var warp in warpData.Overworld1.Where(x => x.ZoneConnections != null && x.ZoneConnections.ToList().Exists(y => y.Zone == connection.Zone)))
                {
                    if (previousWarps.Contains(warp.Code))
                        continue;

                    FollowPath(warp, ref previousWarps);
                }

                foreach (var zone in ZoneData.Connections[connection.Zone])
                {
                    foreach (var warp in warpData.Overworld1.Where(x => x.ZoneConnections != null && x.ZoneConnections.ToList().Exists(y => y.Zone == zone.Zone)))
                    {
                        if (previousWarps.Contains(warp.Code))
                            continue;

                        FollowPath(warp, ref previousWarps);
                    }
                }
            }
        }

        private void UpdateInventory(string warpCode)
        {
            if (warpCode == DungeonWarps[0]) //D1
            {
                inventory.Add(Dungeons.D1);
                inventory.Add(Items.Feather);
                inventory.Add(Instruments.FullMoonCello);
            }
            else if (warpCode == DungeonWarps[1]) //D2
            {
                inventory.Add(Dungeons.D2);
                if (inventory.Contains(Items.Powder))
                {
                    inventory.Add(Items.Bracelet);
                    inventory.Add(Instruments.ConchHorn);
                }
            }
            else if (warpCode == DungeonWarps[2]) //D3
            {
                inventory.Add(Dungeons.D3);
                if (inventory.Contains(Items.Bracelet))
                    inventory.Add(Items.Boots);
                if (inventory.Contains(Items.Feather | Items.Bracelet))
                    inventory.Add(Instruments.SeaLilysBell);
            }
            else if (warpCode == DungeonWarps[3]) //D4
            {
                inventory.Add(Dungeons.D4);
                if (inventory.Contains(Items.Feather | Items.Boots))
                {
                    inventory.Add(Items.Flippers);
                    inventory.Add(Instruments.SurfHarp);
                }
            }
            else if (warpCode == DungeonWarps[4]) //D5
            {
                inventory.Add(Dungeons.D5);
                if (inventory.Contains(Items.Feather))
                    inventory.Add(Items.Hookshot);
                if (inventory.Contains(Items.Feather | Items.Flippers))
                    inventory.Add(Instruments.WindMarimba);
            }
            else if (warpCode == DungeonWarps[5]) //D6a
            {
                inventory.Add(Dungeons.D6);
                inventory.Add(Items.Bracelet);
                if (inventory.Contains(Items.Feather | Items.L2Bracelet))
                    inventory.Add(Instruments.CoralTriangle);
            }
            //else if (warpCode == DungeonWarps[6]) //D6b
            //{

            //}
            else if (warpCode == DungeonWarps[7]) //D7
            {
                inventory.Add(Dungeons.D7);
                if (inventory.Contains(Items.Feather))
                    inventory.Add(Instruments.OrganOfEveningCalm);
            }
            else if (warpCode == DungeonWarps[8]) //D8a
            {
                inventory.Add(Dungeons.D8);
                if (inventory.Contains(Items.Feather))
                {
                    inventory.Add(Items.MagicRod);
                    inventory.Add(Instruments.ThunderDrum);
                }
            }
            //else if (warpCode == DungeonWarps[9]) //D8b
            //{

            //}
            //else if (warpCode == DungeonWarps[10]) //D8c
            //{

            //}
        }
    }

    public class Inventory
    {
        private Items items;
        private Keys keys;
        private Dungeons dungeons;
        private Instruments instruments;

        //public Items Items { get { return items; } }
        //public Keys Keys { get { return keys; } }
        //public Dungeons Dungeons { get { return dungeons; } }
        //public Instruments Instruments { get { return instruments; } }

        public void Add(Items toAdd)
        {
            if (toAdd == Items.Bracelet && items.HasFlag(Items.Bracelet))
                items = items | Items.L2Bracelet;
            else
                items = items | toAdd;
        }

        public void Add(Keys toAdd)
        {
            keys = keys | toAdd;
        }

        public void Add(Dungeons toAdd)
        {
            dungeons = dungeons | toAdd;
        }

        public void Add(Instruments toAdd)
        {
            instruments = instruments | toAdd;
        }

        public bool Contains(Items toFind)
        {
            return items.HasFlag(toFind);
        }

        public bool Contains(Keys toFind)
        {
            return keys.HasFlag(toFind);
        }

        public bool Contains(Dungeons toFind)
        {
            return dungeons.HasFlag(toFind);
        }

        public bool Contains(Instruments toFind)
        {
            return instruments.HasFlag(toFind);
        }
    }

    [Flags]
    public enum Items
    {
        None,
        Shield      = 1,
        Sword       = 2,
        Powder      = 4,
        Shovel      = 8,
        Bombs       = 16,
        Bow         = 32,
        Feather     = 64,
        Bracelet    = 128,
        Boots       = 256,
        Flippers    = 512,
        Hookshot    = 1024,
        L2Bracelet  = 2048,
        MagicRod    = 4096,
        All         = 8191
    }

    [Flags]
    public enum Keys
    {
        None,
        TailKey     = 1,
        SlimeKey    = 2,
        AnglerKey   = 4,
        FaceKey     = 8,
        BirdKey     = 16,
        All         = 31
    }

    [Flags]
    public enum Dungeons
    {
        None,
        D1  = 1,
        D2  = 2,
        D3  = 4,
        D4  = 8,
        D5  = 16,
        D6  = 32,
        D7  = 64,
        D8  = 128,
        All = 255
    }

    [Flags]
    public enum Instruments
    {
        None,
        FullMoonCello       = 1,
        ConchHorn           = 2,
        SeaLilysBell        = 4,
        SurfHarp            = 8,
        WindMarimba         = 16,
        CoralTriangle       = 32,
        OrganOfEveningCalm  = 64,
        ThunderDrum         = 128,
        All                 = 255
    }

    [Flags]
    public enum MiscFlags
    {
        None,
        BowWow = 1,
        KanaletSwitch = 2,
    }
}

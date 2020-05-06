using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADXRandomizer.Pathfinding
{ 
    public class Pathfinder
    {
        private WarpData warpData;
        private FlagsCollection inventory;
        private bool solvable;
        private int count;

        public string Result { get { return inventory.ToString(); } }
        public int FinalCount { get { return count; } }

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

        public bool IsSolvable(WarpData warpData)
        {
            this.warpData = warpData;
            solvable = false;
            inventory = new FlagsCollection();
            inventory.Add(Items.Shield | Items.Sword);

            List<string> previousWarps;
            List<int> previousZones;
            count = 0;
            while (!solvable)
            {
                if (count++ == 100)
                    break;

                previousWarps = new List<string>();
                previousZones = new List<int>();
                FollowPath(warpData[origin], ref previousWarps, ref previousZones);
            }

            return solvable;
        }

        private void FollowPath(Warp origin, ref List<string> previousWarps, ref List<int> previousZones)
        {
            if (previousWarps.Contains(origin.Code))
                return;
            else
                previousWarps.Add(origin.Code);

            var current = origin.GetDestinationWarp();

            //perform checks on current location
            UpdateWarps(current);

            //continue following any connected warps, if possible
            foreach (var connection in current.Connections.Outward)
                if (inventory.Contains(connection.Constraints.ToEnumList()))
                    FollowPath(warpData[connection.Code], ref previousWarps, ref previousZones);
            
            //explore any connected zones, if possible
            foreach (var zoneConnection in current.ZoneConnections.Outward)
                if (inventory.Contains(zoneConnection.Constraints.ToEnumList()))
                    FollowZone(zoneConnection.Zone, ref previousWarps, ref previousZones);
        }

        private void FollowZone(int zone, ref List<string> previousWarps, ref List<int> previousZones)
        {
            if (previousZones.Contains(zone))
                return;
            else
                previousZones.Add(zone);

            //perform checks on current zone
            UpdateZones(zone);

            //follow warps within this zone, if possible
            foreach (var warp in warpData.Overworld1.Where(x => x.ZoneConnections.Inward.Exists(y => y.Zone == zone)))
                foreach (var zoneConnection in warp.ZoneConnections.Inward.Where(x => x.Zone == zone)) //there could be multiple connection between the same warp and zone
                    if (inventory.Contains(zoneConnection.Constraints.ToEnumList()))
                        FollowPath(warp, ref previousWarps, ref previousZones);

            //continue into any zones that connect to this one
            foreach (var zoneConnection in ZoneData.Connections[zone - 1])
                if (inventory.Contains(zoneConnection.Constraints.ToEnumList()))
                    FollowZone(zoneConnection.Zone, ref previousWarps, ref previousZones);
        }

        private void UpdateWarps(Warp current)
        {
            //---------------
            //warp checks
            //---------------
            if (current.Code == "OW1-50") //log cave exit
            {
                inventory.Add(Items.Mushroom);
            }
            else if (current.Code == "OW2-65") //witch's hut
            {
                if (inventory.Contains(Items.Mushroom))
                    inventory.Add(Items.Powder);
            }
            else if (current.Code == "OW2-B3") //trendy game
            {
                inventory.Add(Items.Powder);
            }
            else if (current.Code == "OW2-93") //shop
            {
                inventory.Add(Items.Shovel);
                inventory.Add(Items.Bombs);
                inventory.Add(Items.Bow);
            }
            else if (current.Code == "OW2-83") //dream shrine
            {
                if (inventory.Contains(Items.Feather) || inventory.Contains(Items.Boots))
                    inventory.Add(Items.Ocarina);
            }
            else if (current.Code == "OW1-CF")
            {
                inventory.Add(Keys.AnglerKey);
            }
            else if (current.Code == "OW2-AC") //southern face shrine
            {
                inventory.Add(Keys.FaceKey);
            }
            else if (current.Code == "OW2-0A-3") //bird key cave
            {
                if (inventory.Contains(Items.Boots | Items.Feather | Items.Hookshot) || inventory.Contains(Items.Boots | Items.Feather | Items.Shovel))
                    inventory.Add(Keys.BirdKey);
            }
            else if (current.Code == "OW2-D4") //Mamu's cave
            {
                if (inventory.Contains(Items.Ocarina))
                    inventory.Add(Songs.Song3);
            }
            else if (current.Code == "OW2-35") //moblin hideout
            {
                if (inventory.Contains(Instruments.FullMoonCello))
                    inventory.Add(MiscFlags.BowWow);
            }
            else if (current.Code == "OW2-92") //rooster's grave
            {
                if (inventory.Contains(Songs.Song3))
                    inventory.Add(MiscFlags.Rooster);
            }
            else if (current.Code == "OW2-59-1" || current.Code == "OW2-69") //kanalet castle
            {
                inventory.Add(MiscFlags.KanaletSwitch);
            }
            else if (current.Code == "OW2-06") //egg
            {
                solvable = true;
            }
                        
            //---------------
            //dungeon checks
            //---------------
            if (current.Code == DungeonWarps[0]) //D1
            {
                inventory.Add(Dungeons.D1);
                inventory.Add(Items.Feather);
                inventory.Add(Instruments.FullMoonCello);
            }
            else if (current.Code == DungeonWarps[1]) //D2
            {
                inventory.Add(Dungeons.D2);
                if (inventory.Contains(Items.Powder))
                {
                    inventory.Add(Items.Bracelet);
                    inventory.Add(Instruments.ConchHorn);
                }
            }
            else if (current.Code == DungeonWarps[2]) //D3
            {
                inventory.Add(Dungeons.D3);
                if (inventory.Contains(Items.Bracelet))
                    inventory.Add(Items.Boots);
                if (inventory.Contains(Items.Feather | Items.Bracelet))
                    inventory.Add(Instruments.SeaLilysBell);
            }
            else if (current.Code == DungeonWarps[3]) //D4
            {
                inventory.Add(Dungeons.D4);
                if (inventory.Contains(Items.Feather | Items.Boots))
                {
                    inventory.Add(Items.Flippers);
                    inventory.Add(Instruments.SurfHarp);
                }
            }
            else if (current.Code == DungeonWarps[4]) //D5
            {
                inventory.Add(Dungeons.D5);
                if (inventory.Contains(Items.Feather))
                    inventory.Add(Items.Hookshot);
                if (inventory.Contains(Items.Feather | Items.Flippers))
                    inventory.Add(Instruments.WindMarimba);
            }
            else if (current.Code == DungeonWarps[5]) //D6a
            {
                inventory.Add(Dungeons.D6);
                inventory.Add(Items.Bracelet);
                if (inventory.Contains(Items.Feather | Items.L2Bracelet))
                    inventory.Add(Instruments.CoralTriangle);
            }
            //else if (current.Code == DungeonWarps[6]) //D6b
            //{

            //}
            else if (current.Code == DungeonWarps[7]) //D7
            {
                inventory.Add(Dungeons.D7);
                if (inventory.Contains(Items.Feather))
                    inventory.Add(Instruments.OrganOfEveningCalm);
            }
            else if (current.Code == DungeonWarps[8]) //D8a
            {
                inventory.Add(Dungeons.D8);
                if (inventory.Contains(Items.Feather))
                {
                    inventory.Add(Items.MagicRod);
                    inventory.Add(Instruments.ThunderDrum);
                }
            }
            //else if (current.Code == DungeonWarps[9]) //D8b
            //{

            //}
            //else if (current.Code == DungeonWarps[10]) //D8c
            //{

            //}
        }

        private void UpdateZones(int zone)
        {
            //---------------
            //zone checks
            //---------------
            inventory.Add((Zones)Enum.Parse(typeof(Zones), "Zone" + zone.ToString()));

            if (zone == 11)
            {
                inventory.Add(Keys.TailKey);
                if (inventory.Contains(Items.Feather))
                    inventory.Add(Items.Mushroom);
            }
            else if (zone == 4)
            {
                inventory.Add(Keys.SlimeKey);
            }
            else if (zone == 6)
            {
                if (inventory.Contains(Items.Bombs))
                    inventory.Add(Keys.AnglerKey);
            }
            else if (zone == 12)
            {
                if (inventory.Contains(Keys.AnglerKey))
                    inventory.Add(MiscFlags.Waterfall);
            }
        }
    }

    public class FlagsCollection
    {
        private Items items;
        private Keys keys;
        private Instruments instruments;
        private Songs songs;

        private Dungeons dungeons;
        private Zones zones;
        private MiscFlags misc;

        public bool IsEmpty { get { return this.ToStringList().Count == 0; } }
        
        public FlagsCollection() { }

        public FlagsCollection(Enum[] flags)
        {
            foreach (var flag in flags)
            {
                if (flag.GetType() == typeof(Items))
                    Add((Items)flag);
                else if (flag.GetType() == typeof(Keys))
                    Add((Keys)flag);
                else if (flag.GetType() == typeof(Instruments))
                    Add((Instruments)flag);
                else if (flag.GetType() == typeof(Songs))
                    Add((Songs)flag);
                else if (flag.GetType() == typeof(Dungeons))
                    Add((Dungeons)flag);
                else if (flag.GetType() == typeof(Zones))
                    Add((Zones)flag);
                else if (flag.GetType() == typeof(MiscFlags))
                    Add((MiscFlags)flag);
            }
        }

        #region Add() methods
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

        public void Add(Instruments toAdd)
        {
            instruments = instruments | toAdd;
        }

        public void Add(Songs toAdd)
        {
            songs = songs | toAdd;
        }

        public void Add(Dungeons toAdd)
        {
            dungeons = dungeons | toAdd;
        }

        public void Add(Zones toAdd)
        {
            zones = zones | toAdd;
        }

        public void Add(MiscFlags toAdd)
        {
            misc = misc | toAdd;
        }
        #endregion

        #region Contains() methods
        public bool Contains(List<Enum> flags)
        {
            int count = 0;
            foreach (var flag in flags)
            {
                if (flag.GetType() == typeof(Items) && Contains((Items)flag)
                    || flag.GetType() == typeof(Keys) && Contains((Keys)flag)
                    || flag.GetType() == typeof(Instruments) && Contains((Instruments)flag)
                    || flag.GetType() == typeof(Songs) && Contains((Songs)flag)
                    || flag.GetType() == typeof(Dungeons) && Contains((Dungeons)flag)
                    || flag.GetType() == typeof(Zones) && Contains((Zones)flag)
                    || flag.GetType() == typeof(MiscFlags) && Contains((MiscFlags)flag))
                        count++;
            }

            if (count == flags.Count())
                return true;

            return false;
        }

        public bool Contains(Items toFind)
        {
            return items.HasFlag(toFind);
        }

        public bool Contains(Keys toFind)
        {
            return keys.HasFlag(toFind);
        }

        public bool Contains(Instruments toFind)
        {
            return instruments.HasFlag(toFind);
        }

        public bool Contains(Songs toFind)
        {
            return songs.HasFlag(toFind);
        }

        public bool Contains(Dungeons toFind)
        {
            return dungeons.HasFlag(toFind);
        }

        public bool Contains(Zones toFind)
        {
            return zones.HasFlag(toFind);
        }

        public bool Contains(MiscFlags toFind)
        {
            return misc.HasFlag(toFind);
        }
        #endregion

        public List<Enum> ToEnumList()
        {
            var list = new List<Enum>();

            foreach (var flag in items.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                if (flag != "None")
                    list.Add((Items)Enum.Parse(typeof(Items), flag));

            foreach (var flag in keys.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                if (flag != "None")
                    list.Add((Keys)Enum.Parse(typeof(Keys), flag));

            foreach (var flag in instruments.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                if (flag != "None")
                    list.Add((Instruments)Enum.Parse(typeof(Instruments), flag));

            foreach (var flag in songs.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                if (flag != "None")
                    list.Add((Songs)Enum.Parse(typeof(Songs), flag));

            foreach (var flag in dungeons.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                if (flag != "None")
                    list.Add((Dungeons)Enum.Parse(typeof(Dungeons), flag));

            foreach (var flag in zones.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                if (flag != "None")
                    list.Add((Zones)Enum.Parse(typeof(Zones), flag));

            foreach (var flag in misc.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                if (flag != "None")
                    list.Add((MiscFlags)Enum.Parse(typeof(MiscFlags), flag));

            return list;
        }

        public List<string> ToStringList()
        {
            var list = new List<string>();

            foreach (var flag in items.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                if (flag != "None")
                    list.Add(flag);

            foreach (var flag in keys.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                if (flag != "None")
                    list.Add(flag);

            foreach (var flag in instruments.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                if (flag != "None")
                    list.Add(flag);

            foreach (var flag in songs.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                if (flag != "None")
                    list.Add(flag);

            foreach (var flag in dungeons.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                if (flag != "None")
                    list.Add(flag);

            foreach (var flag in zones.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                if (flag != "None")
                    list.Add(flag);

            foreach (var flag in misc.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
                if (flag != "None")
                    list.Add(flag);

            return list;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("Items: " + items.ToString());
            sb.AppendLine("Keys: " + keys.ToString());
            sb.AppendLine("Instruments: " + instruments.ToString());
            sb.AppendLine("Songs: " + songs.ToString());
            sb.AppendLine("Dungeons: " + dungeons.ToString());
            sb.AppendLine("Zones: " + zones.ToString());
            sb.AppendLine("Misc: " + misc.ToString());

            return sb.ToString();
        }
    }

    [Flags]
    public enum Items
    {
        None,
        Shield      = 0x1,
        Sword       = 0x2,
        Mushroom    = 0x4,
        Powder      = 0x8,
        Shovel      = 0x10,
        Bombs       = 0x20,
        Bow         = 0x40,
        Feather     = 0x80,
        Bracelet    = 0x100,
        Boots       = 0x200,
        Ocarina     = 0x400,
        Flippers    = 0x800,
        Hookshot    = 0x1000,
        L2Bracelet  = 0x2000,
        //L2Shield    = 0x4000,
        MagicRod    = 0x8000,
        //Boomerang   = 0x10000,
        All         = 0xBFFF
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
    public enum Songs
    {
        None,
        Song1 = 1,
        Song2 = 2,
        Song3 = 4,
        All = 7
    }

    [Flags]
    public enum Dungeons
    {
        None,
        D1 = 1,
        D2 = 2,
        D3 = 4,
        D4 = 8,
        D5 = 16,
        D6 = 32,
        D7 = 64,
        D8 = 128,
        All = 255
    }

    [Flags]
    public enum Zones
    {
        None,
        Zone1  = 0x1,
        Zone2  = 0x2,
        Zone3  = 0x4,
        Zone4  = 0x8,
        Zone5  = 0x10,
        Zone6  = 0x20,
        Zone7  = 0x40,
        Zone8  = 0x80,
        Zone9  = 0x100,
        Zone10 = 0x200,
        Zone11 = 0x400,
        Zone12 = 0x800,
        Zone13 = 0x1000,
        All    = 0x1FFF
    }

    [Flags]
    public enum MiscFlags
    {
        None,
        BowWow = 1,
        Rooster = 2,
        KanaletSwitch = 4,
        Waterfall = 8,
    }
}

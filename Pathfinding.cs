using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LADXRandomizer
{
    public static class Pathfinding
    {
        private const string start = "OW2-A2";

        public static List<WarpData> Map(WarpList warpList)
        {
            var inventory = new FlagsCollection(new Enum[] { Items.AllItems, Keys.AllKeys, Instruments.AllInstruments, Songs.AllSongs, Dungeons.AllDungeons, Zones.AllZones, MiscFlags.AllMisc });
            var encounteredWarps = new List<WarpData>();
            var encounteredConstraints = new List<Enum>();

            TrySolve(warpList, ref inventory, out encounteredWarps, out encounteredConstraints, out string output);

            return encounteredWarps;
        }

        public static bool TrySolve(WarpList warpList, ref FlagsCollection inventory, out List<WarpData> encounteredWarps, out List<Enum> encounteredConstraints, out string output)
        {
            encounteredWarps = new List<WarpData>();
            var encounteredZones = new List<int>();
            encounteredConstraints = new List<Enum>();

            var oldInventory = new FlagsCollection();
            int oldWarpCount = 0;
            int oldZoneCount = 0;
            int oldConstraintCount = 0;
            var oldEncounteredWarps = new List<WarpData>();

            int count = 0;
            var sb = new StringBuilder();

            while (!inventory.Equals(oldInventory)
                   || encounteredWarps.Count != oldWarpCount
                   || encounteredZones.Count != oldZoneCount
                   || encounteredConstraints.Count != oldConstraintCount) //while incremental progress is being made...
            {
                //update output log
                if (count > 0)
                {
                    sb.AppendLine("\r\n\r\nPass #" + count.ToString() + "\r\n");
                    sb.AppendLine("INVENTORY:");
                    foreach (var item in inventory.ToEnumList().Except(oldInventory.ToEnumList()))
                        sb.Append(item.ToString() + ", ");
                    sb.AppendLine("\r\nWARPS:");
                    foreach (var warp in encounteredWarps.Except(oldEncounteredWarps))
                        sb.Append(warp.Code + ", ");
                }

                //save values from last iteration
                oldInventory = new FlagsCollection(inventory.ToEnumList().ToArray());
                oldWarpCount = encounteredWarps.Count;
                oldZoneCount = encounteredZones.Count;
                oldConstraintCount = encounteredConstraints.Count;
                oldEncounteredWarps = new List<WarpData>(encounteredWarps);

                //clear lists before next iteration
                encounteredWarps.Clear();
                encounteredZones.Clear();
                encounteredConstraints.Clear();
                
                //pathfind
                FollowWarp(warpList, warpList[start], ref inventory, ref encounteredWarps, ref encounteredZones, ref encounteredConstraints);

                //remove duplicates from list
                encounteredWarps = encounteredWarps.Distinct().ToList();
                encounteredZones = encounteredZones.Distinct().ToList();
                encounteredConstraints = encounteredConstraints.Distinct().ToList();

                foreach (var constraint in encounteredConstraints.ToList())
                {
                    if (inventory.Contains(constraint) || !(constraint is Items))
                        encounteredConstraints.Remove(constraint);
                }

                count++;
            }

            output = sb.ToString();

            return inventory.Contains(Dungeons.Egg) && (inventory.Contains(Items.Mushroom) || inventory.Contains(Items.Powder)) && inventory.Contains(Items.Bow | Items.Sword);
        }

        private static void FollowWarp(WarpList warpList, WarpData origin, ref FlagsCollection inventory, ref List<WarpData> encounteredWarps, ref List<int> encounteredZones, ref List<Enum> encounteredConstraints)
        {
            if (encounteredWarps.Contains(origin))
                return;
            else
                encounteredWarps.Add(origin);

            var current = origin.GetDestinationWarp();

            encounteredWarps.Add(current);

            //perform checks on current location
            UpdateInventory(current, ref inventory);

            //continue following any connected warps, if possible
            foreach (var connection in current.WarpConnections.Outward)
                if (inventory.Contains(connection.Constraints.ToEnumList()))
                    FollowWarp(warpList, warpList[connection.Code], ref inventory, ref encounteredWarps, ref encounteredZones, ref encounteredConstraints);
                else
                    encounteredConstraints.AddRange(connection.Constraints.ToEnumList());

            //explore any connected zones, if possible
            foreach (var zoneConnection in current.ZoneConnections.Outward)
                if (inventory.Contains(zoneConnection.Constraints.ToEnumList()))
                    FollowZone(warpList, zoneConnection.Zone, ref inventory, ref encounteredWarps, ref encounteredZones, ref encounteredConstraints);
                else
                    encounteredConstraints.AddRange(zoneConnection.Constraints.ToEnumList());
        }

        private static void FollowZone(WarpList warpList, int zone, ref FlagsCollection inventory, ref List<WarpData> encounteredWarps, ref List<int> encounteredZones, ref List<Enum> encounteredConstraints)
        {
            if (encounteredZones.Contains(zone))
                return;
            else
                encounteredZones.Add(zone);

            //perform checks on current zone
            UpdateInventory(zone, ref inventory);

            //follow warps within this zone, if possible
            foreach (var warp in warpList.Overworld1.Where(x => x.ZoneConnections.Inward.Exists(y => y.Zone == zone)))
                foreach (var zoneConnection in warp.ZoneConnections.Inward.Where(x => x.Zone == zone)) //there could be multiple connections between the same warp and zone
                    if (inventory.Contains(zoneConnection.Constraints.ToEnumList()))
                        FollowWarp(warpList, warp, ref inventory, ref encounteredWarps, ref encounteredZones, ref encounteredConstraints);
                    else
                        encounteredConstraints.AddRange(zoneConnection.Constraints.ToEnumList());

            //continue into any zones that connect to this one
            foreach (var zoneConnection in warpList.ZoneList[zone].ZoneConnections)
                if (inventory.Contains(zoneConnection.Constraints.ToEnumList()))
                    FollowZone(warpList, zoneConnection.Zone, ref inventory, ref encounteredWarps, ref encounteredZones, ref encounteredConstraints);
                else
                    encounteredConstraints.AddRange(zoneConnection.Constraints.ToEnumList());
        }

        private static void UpdateInventory(WarpData current, ref FlagsCollection inventory)
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
                if (inventory.Contains(Items.Boots | Items.Sword))
                    inventory.Add(Items.Ocarina);
            }
            else if (current.Code == "OW1-C6") //villa cave exit
            {
                if (inventory.Contains(Items.Shovel) && (inventory.Contains(Items.Sword) || inventory.Contains(Items.MagicRod)))
                    inventory.Add(Keys.SlimeKey);
            }
            else if (current.Code == "OW1-CF") // desert cave (checked in case of bombless access)
            {
                if (inventory.ContainsAnyDamageItem)
                    inventory.Add(Keys.AnglerKey);
            }
            else if (current.Code == "OW2-AC") //southern face shrine
            {
                if (inventory.Contains(Items.Sword) || inventory.Contains(Items.Bow) || inventory.Contains(Items.MagicRod))
                    inventory.Add(Keys.FaceKey);
            }
            else if (current.Code == "OW2-0A-3") //bird key cave
            {
                if (inventory.Contains(MiscFlags.Rooster))
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
                        
            //---------------
            //dungeon checks
            //---------------
            else if (current.Code == "OW2-D3") //D1
            {
                inventory.Add(Dungeons.D1);
                if (inventory.Contains(Items.Shield))
                    inventory.Add(Items.Feather);
                if (inventory.Contains(Items.Sword | Items.Feather))
                    inventory.Add(Instruments.FullMoonCello);
            }
            else if (current.Code == "OW2-24") //D2
            {
                inventory.Add(Dungeons.D2);
                if (inventory.Contains(Items.Powder | Items.Feather) || inventory.Contains(Items.MagicRod | Items.Feather))
                {
                    if (inventory.ContainsAnyDamageItem)
                        if (inventory.BraceletLocation != BraceletLocation.D2) //prevents prematurely upgrading to L2 bracelet on repeated passes
                        {
                            inventory.Add(Items.Bracelet);
                            inventory.BraceletLocation = BraceletLocation.D2;
                        }
                    if (inventory.Contains(Items.Bracelet) && (inventory.Contains(Items.Sword) || inventory.Contains(Items.MagicRod) || inventory.Contains(Items.Bow | Items.Bombs)))
                        inventory.Add(Instruments.ConchHorn);
                }
            }
            else if (current.Code == "OW2-B5") //D3
            {
                inventory.Add(Dungeons.D3);
                if (inventory.Contains(Items.Bracelet))
                {
                    if (inventory.ContainsAnyDamageItem && inventory.Contains(Items.Bombs))
                        inventory.Add(Items.Boots);
                    if (inventory.Contains(Items.Feather | Items.Boots | Items.Sword))
                        inventory.Add(Instruments.SeaLilysBell);
                }
            }
            else if (current.Code == "OW2-2B-1") //D4
            {
                inventory.Add(Dungeons.D4);
                if (inventory.Contains(Items.Boots | Items.Feather | Items.Bombs | Items.Sword))
                    inventory.Add(Items.Flippers);
                if (inventory.Contains(Items.Boots | Items.Feather | Items.Flippers) && (inventory.Contains(Items.Sword) || inventory.Contains(Items.Bow)))
                    inventory.Add(Instruments.SurfHarp);
            }
            else if (current.Code == "OW2-D9-1") //D5
            {
                inventory.Add(Dungeons.D5);
                if (inventory.Contains(Items.Feather | Items.Sword | Items.Bombs))
                    inventory.Add(Items.Hookshot);
                if (inventory.Contains(Items.Flippers | Items.Hookshot))
                    inventory.Add(Instruments.WindMarimba);
            }
            else if (current.Code == "OW2-8C") //D6a (entrance)
            {
                inventory.Add(Dungeons.D6);
                if (inventory.Contains(Items.Bombs) && inventory.ContainsAnyDamageItem)
                    if (inventory.BraceletLocation != BraceletLocation.D6) //prevents prematurely upgrading to L2 bracelet on repeated passes
                    {
                        inventory.Add(Items.Bracelet);
                        inventory.BraceletLocation = BraceletLocation.D6;
                    }
                if (inventory.Contains(Items.L2Bracelet | Items.Bombs | Items.Feather | Items.Hookshot))
                    inventory.Add(Instruments.CoralTriangle);
            }
            //else if (current.Code == "OW2-6C") //D6b (stairs)
            //{

            //}
            else if (current.Code == "OW2-0E") //D7
            {
                inventory.Add(Dungeons.D7);
                if (inventory.Contains(Items.Bracelet))
                {
                    if (inventory.ContainsAnyDamageItem && inventory.Contains(Items.Shield))
                        inventory.Add(Items.L2Shield);
                    if (inventory.Contains(Items.Feather | Items.Bombs | Items.Hookshot | Items.L2Shield))
                        inventory.Add(Instruments.OrganOfEveningCalm);
                }
            }
            else if (current.Code == "OW2-10") //D8a (entrance)
            {
                inventory.Add(Dungeons.D8);
                if (inventory.Contains(Items.Sword | Items.Feather | Items.Bombs | Items.Bow | Items.Hookshot))
                    inventory.Add(Items.MagicRod);
                if (inventory.Contains(Items.Feather | Items.MagicRod) && (inventory.Contains(Items.Bracelet) || inventory.Contains(Items.Bow | Items.Bombs)))
                    inventory.Add(Instruments.ThunderDrum);
            }
            //else if (current.Code == "OW2-00") //D8b (stairs [00])
            //{

            //}
            //else if (current.Code == "OW2-02") //D8c (stairs [02])
            //{

            //}
            else if (current.Code == "OW2-06") //egg
            {
                inventory.Add(Dungeons.Egg);
            }
        }

        private static void UpdateInventory(int zone, ref FlagsCollection inventory)
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
            //else if (zone == 4)
            //{
            //    if (inventory.Contains(Items.Shovel))
            //        inventory.Add(Keys.SlimeKey);
            //}
            else if (zone == 6)
            {
                if (inventory.Contains(Items.Bombs) && inventory.ContainsAnyDamageItem)
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

        public BraceletLocation BraceletLocation { get; set; }

        public bool ContainsAnyDamageItem => Contains(Items.Sword) || Contains(Items.Bow) || Contains(Items.Hookshot) || Contains(Items.MagicRod);
        
        public FlagsCollection() { }

        public FlagsCollection(IEnumerable<Enum> flags)
        {
            foreach (var flag in flags)
            {
                if (flag is Items)
                    Add((Items)flag);
                else if (flag is Keys)
                    Add((Keys)flag);
                else if (flag is Instruments)
                    Add((Instruments)flag);
                else if (flag is Songs)
                    Add((Songs)flag);
                else if (flag is Dungeons)
                    Add((Dungeons)flag);
                else if (flag is Zones)
                    Add((Zones)flag);
                else if (flag is MiscFlags)
                    Add((MiscFlags)flag);
            }
        }

        #region Add() methods
        public void Add(Items flag)
        {
            if (flag == Items.Bracelet && items.HasFlag(Items.Bracelet))
                items |= Items.L2Bracelet;
            else
                items |= flag;
        }

        public void Add(Keys flag)
        {
            keys |= flag;
        }

        public void Add(Instruments flag)
        {
            instruments |= flag;
        }

        public void Add(Songs flag)
        {
            songs |= flag;
        }

        public void Add(Dungeons flag)
        {
            dungeons |= flag;
        }

        public void Add(Zones flag)
        {
            zones |= flag;
        }

        public void Add(MiscFlags flag)
        {
            misc |= flag;
        }
        #endregion

        #region Remove() methods
        public void Remove(Enum flag)
        {
            if (flag is Items)
                Remove((Items)flag);
            else if (flag is Keys)
                Remove((Keys)flag);
            else if (flag is Instruments)
                Remove((Instruments)flag);
            else if (flag is Songs)
                Remove((Songs)flag);
            else if (flag is Dungeons)
                Remove((Dungeons)flag);
            else if (flag is Zones)
                Remove((Zones)flag);
            else if (flag is MiscFlags)
                Remove((MiscFlags)flag);
        }

        public void Remove(Items flag)
        {
            items &= ~flag;
        }

        public void Remove(Keys flag)
        {
            keys &= ~flag;
        }

        public void Remove(Instruments flag)
        {
            instruments &= ~flag;
        }

        public void Remove(Songs flag)
        {
            songs &= ~flag;
        }

        public void Remove(Dungeons flag)
        {
            dungeons &= ~flag;
        }

        public void Remove(Zones flag)
        {
            zones &= ~flag;
        }

        public void Remove(MiscFlags flag)
        {
            misc &= ~flag;
        }
        #endregion

        #region Contains() methods
        public bool Contains(List<Enum> flags)
        {
            int count = 0;
            foreach (var flag in flags)
            {
                if (Contains(flag))
                        count++;
            }

            if (count == flags.Count)
                return true;

            return false;
        }

        public bool Contains(Enum flag)
        {
            return flag is Items && Contains((Items)flag)
                   || flag is Keys && Contains((Keys)flag)
                   || flag is Instruments && Contains((Instruments)flag)
                   || flag is Songs && Contains((Songs)flag)
                   || flag is Dungeons && Contains((Dungeons)flag)
                   || flag is Zones && Contains((Zones)flag)
                   || flag is MiscFlags && Contains((MiscFlags)flag);
        }

        public bool Contains(Items flag)
        {
            return items.HasFlag(flag);
        }

        public bool Contains(Keys flag)
        {
            return keys.HasFlag(flag);
        }

        public bool Contains(Instruments flag)
        {
            return instruments.HasFlag(flag);
        }

        public bool Contains(Songs flag)
        {
            return songs.HasFlag(flag);
        }

        public bool Contains(Dungeons flag)
        {
            return dungeons.HasFlag(flag);
        }

        public bool Contains(Zones flag)
        {
            return zones.HasFlag(flag);
        }

        public bool Contains(MiscFlags flag)
        {
            return misc.HasFlag(flag);
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

        public bool Equals(FlagsCollection fc)
        {
            return items == fc.items
                   && keys == fc.keys
                   && instruments == fc.instruments
                   && songs == fc.songs
                   && dungeons == fc.dungeons
                   && zones == fc.zones
                   && misc == fc.misc;
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
        L2Shield    = 0x4000,
        MagicRod    = 0x8000,
        //Boomerang   = 0x10000,
        AllItems    = 0xFFFF
    }

    [Flags]
    public enum Keys
    {
        None,
        TailKey     = 0x1,
        SlimeKey    = 0x2,
        AnglerKey   = 0x4,
        FaceKey     = 0x8,
        BirdKey     = 0x10,
        AllKeys     = 0x1F
    }

    [Flags]
    public enum Instruments
    {
        None,
        FullMoonCello       = 0x1,
        ConchHorn           = 0x2,
        SeaLilysBell        = 0x4,
        SurfHarp            = 0x8,
        WindMarimba         = 0x10,
        CoralTriangle       = 0x20,
        OrganOfEveningCalm  = 0x40,
        ThunderDrum         = 0x80,
        AllInstruments      = 0xFF
    }

    [Flags]
    public enum Songs
    {
        None,
        Song1    = 0x1,
        Song2    = 0x2,
        Song3    = 0x4,
        AllSongs = 0x7
    }

    [Flags]
    public enum Dungeons
    {
        None,
        D1          = 0x1,
        D2          = 0x2,
        D3          = 0x4,
        D4          = 0x8,
        D5          = 0x10,
        D6          = 0x20,
        D7          = 0x40,
        D8          = 0x80,
        Egg         = 0x100,
        AllDungeons = 0x1FF
    }

    [Flags]
    public enum Zones
    {
        None,
        Zone1    = 0x1,
        Zone2    = 0x2,
        Zone3    = 0x4,
        Zone4    = 0x8,
        Zone5    = 0x10,
        Zone6    = 0x20,
        Zone7    = 0x40,
        Zone8    = 0x80,
        Zone9    = 0x100,
        Zone10   = 0x200,
        Zone11   = 0x400,
        Zone12   = 0x800,
        Zone13   = 0x1000,
        Zone14   = 0x2000,
        Zone15   = 0x4000,
        Zone16   = 0x8000,
        Zone17   = 0x10000,
        Zone18   = 0x20000,
        Zone19   = 0x40000,
        Zone20   = 0x80000,
        AllZones = 0xFFFFF
    }

    [Flags]
    public enum MiscFlags
    {
        None,
        BowWow        = 0x1,
        Rooster       = 0x2,
        KanaletSwitch = 0x4,
        GoldenLeaves  = 0x8,
        Waterfall     = 0x10,
        MermaidScale  = 0x20,
        AllMisc       = 0x3F
    }
    
    public enum BraceletLocation
    {
        None,
        D2,
        D6,
    }
}

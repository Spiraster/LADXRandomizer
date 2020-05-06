using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LADXRandomizer
{
    public static class Pathfinding
    {
        private static readonly string start = "OW2-A2";

        public static (List<string>, List<Enum>) GetUnlinkedWarps(WarpData warpData, out FlagsCollection inventory)
        {
            inventory = new FlagsCollection(new Enum[] { Items.Shield | Items.Sword });
            var inventory_old = new FlagsCollection();

            var unlinkedWarps = new List<string>();
            var unlinkedWarps_old = new List<string>();

            var encounteredConstraints = new List<Enum>();
            
            while (!inventory.Equals(inventory_old) || unlinkedWarps.Count != unlinkedWarps_old.Count)
            {
                inventory_old = new FlagsCollection(inventory.ToEnumList().ToArray());
                unlinkedWarps_old = unlinkedWarps.ToList();

                var previousWarps = new List<string>();
                var previousZones = new List<int>();
                
                (unlinkedWarps, encounteredConstraints) = FollowWarp(warpData, warpData[start], ref inventory, ref previousWarps, ref previousZones);
                unlinkedWarps = unlinkedWarps.Distinct().ToList();
                encounteredConstraints = encounteredConstraints.Distinct().ToList();

                foreach (var constraint in encounteredConstraints.ToList())
                {
                    if (inventory.Contains(constraint) || !(constraint is Items))
                        encounteredConstraints.Remove(constraint);
                }
            }

            return (unlinkedWarps, encounteredConstraints);
        }

        private static (List<string>, List<Enum>) FollowWarp(WarpData warpData, Warp origin, ref FlagsCollection inventory, ref List<string> previousWarps, ref List<int> previousZones)
        {
            var unlinkedWarps = new List<string>();
            var encounteredConstraints = new List<Enum>();

            if (previousWarps.Contains(origin.Code))
                return (unlinkedWarps, encounteredConstraints);
            else
                previousWarps.Add(origin.Code);

            var current = origin.GetDestinationWarp();

            //when a warp is unlinked
            if (current == null)
            {
                unlinkedWarps.Add(origin.Code);
                return (unlinkedWarps, encounteredConstraints);
            }       

            //perform checks on current location
            UpdateInventory(current, ref inventory);

            //continue following any connected warps, if possible
            foreach (var connection in current.Connections.Outward)
                if (inventory.Contains(connection.Constraints.ToEnumList()))
                {
                    var result = FollowWarp(warpData, warpData[connection.Code], ref inventory, ref previousWarps, ref previousZones);
                    unlinkedWarps.AddRange(result.Item1);
                    encounteredConstraints.AddRange(result.Item2);
                }
                else
                    encounteredConstraints.AddRange(connection.Constraints.ToEnumList());

            //explore any connected zones, if possible
            foreach (var zoneConnection in current.ZoneConnections.Outward)
                if (inventory.Contains(zoneConnection.Constraints.ToEnumList()))
                {
                    var result = FollowZone(warpData, zoneConnection.Zone, ref inventory, ref previousWarps, ref previousZones);
                    unlinkedWarps.AddRange(result.Item1);
                    encounteredConstraints.AddRange(result.Item2);
                }
                else
                    encounteredConstraints.AddRange(zoneConnection.Constraints.ToEnumList());

            return (unlinkedWarps, encounteredConstraints);
        }

        private static (List<string>, List<Enum>) FollowZone(WarpData warpData, int zone, ref FlagsCollection inventory, ref List<string> previousWarps, ref List<int> previousZones)
        {
            var unlinkedWarps = new List<string>();
            var encounteredConstraints = new List<Enum>();

            if (previousZones.Contains(zone))
                return (unlinkedWarps, encounteredConstraints);
            else
                previousZones.Add(zone);

            //perform checks on current zone
            UpdateInventory(zone, ref inventory);

            //follow warps within this zone, if possible
            foreach (var warp in warpData.Overworld1.Where(x => x.ZoneConnections.Inward.Exists(y => y.Zone == zone)))
                foreach (var zoneConnection in warp.ZoneConnections.Inward.Where(x => x.Zone == zone)) //there could be multiple connections between the same warp and zone
                    if (inventory.Contains(zoneConnection.Constraints.ToEnumList()))
                    {
                        var result = FollowWarp(warpData, warp, ref inventory, ref previousWarps, ref previousZones);
                        unlinkedWarps.AddRange(result.Item1);
                        encounteredConstraints.AddRange(result.Item2);
                    }
                    else
                        encounteredConstraints.AddRange(zoneConnection.Constraints.ToEnumList());

            //continue into any zones that connect to this one
            foreach (var zoneConnection in ZoneData.Connections[zone - 1])
                if (inventory.Contains(zoneConnection.Constraints.ToEnumList()))
                {
                    var result = FollowZone(warpData, zoneConnection.Zone, ref inventory, ref previousWarps, ref previousZones);
                    unlinkedWarps.AddRange(result.Item1);
                    encounteredConstraints.AddRange(result.Item2);
                }
                else
                    encounteredConstraints.AddRange(zoneConnection.Constraints.ToEnumList());

            return (unlinkedWarps, encounteredConstraints);
        }

        private static void UpdateInventory(Warp current, ref FlagsCollection inventory)
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
                if (inventory.Contains(Items.Feather) || inventory.Contains(Items.Boots | Items.Sword))
                    inventory.Add(Items.Ocarina);
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
                if (inventory.Contains(MiscFlags.Rooster) || inventory.Contains(Items.Boots | Items.Feather | Items.Hookshot) || inventory.Contains(Items.Boots | Items.Feather | Items.Shovel))
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
                if (inventory.Contains(Items.Powder | Items.Feather))
                {
                    if (inventory.Contains(Items.Sword) || inventory.Contains(Items.Bow) || inventory.Contains(Items.Hookshot))
                        inventory.Add(Items.Bracelet);
                    if (inventory.Contains(Items.Bracelet) && (inventory.Contains(Items.Sword) || inventory.Contains(Items.MagicRod)))
                        inventory.Add(Instruments.ConchHorn);
                }
            }
            else if (current.Code == "OW2-B5") //D3
            {
                inventory.Add(Dungeons.D3);
                if (inventory.Contains(Items.Bracelet))
                {
                    if (inventory.ContainsAnyDamageItem && (inventory.Contains(Items.Bombs) || inventory.Contains(Items.Sword | Items.Feather)))
                        inventory.Add(Items.Boots);
                    if (inventory.Contains(Items.Feather | Items.Boots | Items.Sword))
                        inventory.Add(Instruments.SeaLilysBell);
                }
            }
            else if (current.Code == "OW2-2B-1") //D4
            {
                inventory.Add(Dungeons.D4);
                if (inventory.Contains(Items.Boots | Items.Feather) && (inventory.ContainsAnyDamageItem || inventory.Contains(Items.Bombs | Items.Sword)))
                    inventory.Add(Items.Flippers);
                if (inventory.Contains(Items.Boots | Items.Feather | Items.Flippers) && (inventory.Contains(Items.Sword) || inventory.Contains(Items.Bow)))
                    inventory.Add(Instruments.SurfHarp);
            }
            else if (current.Code == "OW2-D9-1") //D5
            {
                inventory.Add(Dungeons.D5);
                if (inventory.Contains(Items.Feather))
                    inventory.Add(Items.Hookshot);
                if (inventory.Contains(Items.Feather | Items.Flippers))
                    inventory.Add(Instruments.WindMarimba);
            }
            else if (current.Code == "OW2-8C") //D6a (entrance)
            {
                inventory.Add(Dungeons.D6);
                inventory.Add(Items.Bracelet);
                if (inventory.Contains(Items.Feather | Items.L2Bracelet))
                    inventory.Add(Instruments.CoralTriangle);
            }
            //else if (current.Code == "OW2-6C") //D6b (stairs)
            //{

            //}
            else if (current.Code == "OW2-0E") //D7
            {
                inventory.Add(Dungeons.D7);
                if (inventory.Contains(Items.Feather))
                    inventory.Add(Instruments.OrganOfEveningCalm);
            }
            else if (current.Code == "OW2-10") //D8a (entrance)
            {
                inventory.Add(Dungeons.D8);
                if (inventory.Contains(Items.Feather))
                {
                    inventory.Add(Items.MagicRod);
                    inventory.Add(Instruments.ThunderDrum);
                }
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
            else if (zone == 4)
            {
                if (inventory.Contains(Items.Shovel))
                    inventory.Add(Keys.SlimeKey);
            }
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

    //public class FlagsCollection
    //{
    //    private Items items;
    //    private Keys keys;
    //    private Instruments instruments;
    //    private Songs songs;

    //    private Dungeons dungeons;
    //    private Zones zones;
    //    private MiscFlags misc;

    //    public BraceletLocation BraceletLocation { get; set; }

    //    public bool ContainsAnyDamageItem => this.Contains(Items.Sword) || this.Contains(Items.Bow) || this.Contains(Items.Hookshot) || this.Contains(Items.MagicRod);
    //    public bool IsEmpty => this.ToStringList().Count == 0;
        
    //    public FlagsCollection() { }

    //    public FlagsCollection(Enum[] flags)
    //    {
    //        foreach (var flag in flags)
    //        {
    //            if (flag is Items)
    //                Add((Items)flag);
    //            else if (flag is Keys)
    //                Add((Keys)flag);
    //            else if (flag is Instruments)
    //                Add((Instruments)flag);
    //            else if (flag is Songs)
    //                Add((Songs)flag);
    //            else if (flag is Dungeons)
    //                Add((Dungeons)flag);
    //            else if (flag is Zones)
    //                Add((Zones)flag);
    //            else if (flag is MiscFlags)
    //                Add((MiscFlags)flag);
    //        }
    //    }

    //    #region Add() methods
    //    public void Add(Items flag)
    //    {
    //        if (flag == Items.Bracelet && items.HasFlag(Items.Bracelet))
    //            items = items | Items.L2Bracelet;
    //        else
    //            items = items | flag;
    //    }

    //    public void Add(Keys flag)
    //    {
    //        keys = keys | flag;
    //    }

    //    public void Add(Instruments flag)
    //    {
    //        instruments = instruments | flag;
    //    }

    //    public void Add(Songs flag)
    //    {
    //        songs = songs | flag;
    //    }

    //    public void Add(Dungeons flag)
    //    {
    //        dungeons = dungeons | flag;
    //    }

    //    public void Add(Zones flag)
    //    {
    //        zones = zones | flag;
    //    }

    //    public void Add(MiscFlags flag)
    //    {
    //        misc = misc | flag;
    //    }
    //    #endregion

    //    #region Contains() methods
    //    public bool Contains(List<Enum> flags)
    //    {
    //        int count = 0;
    //        foreach (var flag in flags)
    //        {
    //            if (Contains(flag))
    //                    count++;
    //        }

    //        if (count == flags.Count)
    //            return true;

    //        return false;
    //    }

    //    public bool Contains(Enum flag)
    //    {
    //        return flag is Items && Contains((Items)flag)
    //               || flag is Keys && Contains((Keys)flag)
    //               || flag is Instruments && Contains((Instruments)flag)
    //               || flag is Songs && Contains((Songs)flag)
    //               || flag is Dungeons && Contains((Dungeons)flag)
    //               || flag is Zones && Contains((Zones)flag)
    //               || flag is MiscFlags && Contains((MiscFlags)flag);
    //    }

    //    public bool Contains(Items flag)
    //    {
    //        return items.HasFlag(flag);
    //    }

    //    public bool Contains(Keys flag)
    //    {
    //        return keys.HasFlag(flag);
    //    }

    //    public bool Contains(Instruments flag)
    //    {
    //        return instruments.HasFlag(flag);
    //    }

    //    public bool Contains(Songs flag)
    //    {
    //        return songs.HasFlag(flag);
    //    }

    //    public bool Contains(Dungeons flag)
    //    {
    //        return dungeons.HasFlag(flag);
    //    }

    //    public bool Contains(Zones flag)
    //    {
    //        return zones.HasFlag(flag);
    //    }

    //    public bool Contains(MiscFlags flag)
    //    {
    //        return misc.HasFlag(flag);
    //    }
    //    #endregion

    //    //public bool ContainsAnyOf(params Enum[] flags)
    //    //{
    //    //    foreach (var flag in flags)
    //    //        if (this.Contains(flag))
    //    //            return true;

    //    //    return false;
    //    //}

    //    public List<Enum> ToEnumList()
    //    {
    //        var list = new List<Enum>();

    //        foreach (var flag in items.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
    //            if (flag != "None")
    //                list.Add((Items)Enum.Parse(typeof(Items), flag));

    //        foreach (var flag in keys.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
    //            if (flag != "None")
    //                list.Add((Keys)Enum.Parse(typeof(Keys), flag));

    //        foreach (var flag in instruments.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
    //            if (flag != "None")
    //                list.Add((Instruments)Enum.Parse(typeof(Instruments), flag));

    //        foreach (var flag in songs.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
    //            if (flag != "None")
    //                list.Add((Songs)Enum.Parse(typeof(Songs), flag));

    //        foreach (var flag in dungeons.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
    //            if (flag != "None")
    //                list.Add((Dungeons)Enum.Parse(typeof(Dungeons), flag));

    //        foreach (var flag in zones.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
    //            if (flag != "None")
    //                list.Add((Zones)Enum.Parse(typeof(Zones), flag));

    //        foreach (var flag in misc.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
    //            if (flag != "None")
    //                list.Add((MiscFlags)Enum.Parse(typeof(MiscFlags), flag));

    //        return list;
    //    }

    //    public List<string> ToStringList()
    //    {
    //        var list = new List<string>();

    //        foreach (var flag in items.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
    //            if (flag != "None")
    //                list.Add(flag);

    //        foreach (var flag in keys.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
    //            if (flag != "None")
    //                list.Add(flag);

    //        foreach (var flag in instruments.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
    //            if (flag != "None")
    //                list.Add(flag);

    //        foreach (var flag in songs.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
    //            if (flag != "None")
    //                list.Add(flag);

    //        foreach (var flag in dungeons.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
    //            if (flag != "None")
    //                list.Add(flag);

    //        foreach (var flag in zones.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
    //            if (flag != "None")
    //                list.Add(flag);

    //        foreach (var flag in misc.ToString().Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries))
    //            if (flag != "None")
    //                list.Add(flag);

    //        return list;
    //    }

    //    public override string ToString()
    //    {
    //        var sb = new StringBuilder();
    //        sb.AppendLine("Items: " + items.ToString());
    //        sb.AppendLine("Keys: " + keys.ToString());
    //        sb.AppendLine("Instruments: " + instruments.ToString());
    //        sb.AppendLine("Songs: " + songs.ToString());
    //        sb.AppendLine("Dungeons: " + dungeons.ToString());
    //        sb.AppendLine("Zones: " + zones.ToString());
    //        sb.AppendLine("Misc: " + misc.ToString());

    //        return sb.ToString();
    //    }

    //    public bool Equals(FlagsCollection fc)
    //    {
    //        return items == fc.items
    //               && keys == fc.keys
    //               && instruments == fc.instruments
    //               && songs == fc.songs
    //               && dungeons == fc.dungeons
    //               && zones == fc.zones
    //               && misc == fc.misc;
    //    }
    //}

    //[Flags]
    //public enum Items
    //{
    //    None,
    //    Shield      = 0x1,
    //    Sword       = 0x2,
    //    Mushroom    = 0x4,
    //    Powder      = 0x8,
    //    Shovel      = 0x10,
    //    Bombs       = 0x20,
    //    Bow         = 0x40,
    //    Feather     = 0x80,
    //    Bracelet    = 0x100,
    //    Boots       = 0x200,
    //    Ocarina     = 0x400,
    //    Flippers    = 0x800,
    //    Hookshot    = 0x1000,
    //    L2Bracelet  = 0x2000,
    //    //L2Shield    = 0x4000,
    //    MagicRod    = 0x8000,
    //    //Boomerang   = 0x10000,
    //    All         = 0xBFFF
    //}

    //[Flags]
    //public enum Keys
    //{
    //    None,
    //    TailKey     = 0x1,
    //    SlimeKey    = 0x2,
    //    AnglerKey   = 0x4,
    //    FaceKey     = 0x8,
    //    BirdKey     = 0x10,
    //    All         = 0x1F
    //}

    //[Flags]
    //public enum Instruments
    //{
    //    None,
    //    FullMoonCello       = 0x1,
    //    ConchHorn           = 0x2,
    //    SeaLilysBell        = 0x4,
    //    SurfHarp            = 0x8,
    //    WindMarimba         = 0x10,
    //    CoralTriangle       = 0x20,
    //    OrganOfEveningCalm  = 0x40,
    //    ThunderDrum         = 0x80,
    //    All                 = 0xFF
    //}

    //[Flags]
    //public enum Songs
    //{
    //    None,
    //    Song1 = 0x1,
    //    Song2 = 0x2,
    //    Song3 = 0x4,
    //    All   = 0x7
    //}

    //[Flags]
    //public enum Dungeons
    //{
    //    None,
    //    D1  = 0x1,
    //    D2  = 0x2,
    //    D3  = 0x4,
    //    D4  = 0x8,
    //    D5  = 0x10,
    //    D6  = 0x20,
    //    D7  = 0x40,
    //    D8  = 0x80,
    //    Egg = 0x100,
    //    All = 0x1FF
    //}

    //[Flags]
    //public enum Zones
    //{
    //    None,
    //    Zone1  = 0x1,
    //    Zone2  = 0x2,
    //    Zone3  = 0x4,
    //    Zone4  = 0x8,
    //    Zone5  = 0x10,
    //    Zone6  = 0x20,
    //    Zone7  = 0x40,
    //    Zone8  = 0x80,
    //    Zone9  = 0x100,
    //    Zone10 = 0x200,
    //    Zone11 = 0x400,
    //    Zone12 = 0x800,
    //    Zone13 = 0x1000,
    //    All    = 0x1FFF
    //}

    //[Flags]
    //public enum MiscFlags
    //{
    //    None,
    //    BowWow        = 0x1,
    //    Rooster       = 0x2,
    //    KanaletSwitch = 0x4,
    //    Waterfall     = 0x8,
    //}
    
    //public enum BraceletLocation
    //{
    //    None,
    //    D2,
    //    D6,
    //}
}

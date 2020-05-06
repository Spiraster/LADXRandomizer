using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LADXRandomizer
{
    public class WarpData
    {
        public static Connection[][] ZoneConnections = new Connection[][]
        {
            new Connection[] //Zone 0
			{
                new Connection(4, Item.Flippers),
                new Connection(5, Item.Flippers),
                new Connection(6, Item.Flippers),
                new Connection(9, Item.Flippers),
            },
            new Connection[] //Zone 1
			{
                new Connection(2, Item.Feather),
                new Connection(3, Item.Feather, Item.Bracelet),
                new Connection(4, Item.Bracelet),
                new Connection(5, Item.Bracelet),
            },
            new Connection[] //Zone 2
			{
                new Connection(1, Item.Feather),
            },
            new Connection[] //Zone 3
			{
                new Connection(1, Item.Feather, Item.Bracelet),
                new Connection(4, Item.Bracelet),
            },
            new Connection[] //Zone 4
			{
                new Connection(0, Item.Flippers),
                new Connection(1, Item.Bracelet),
                new Connection(3, Item.Bracelet),
                new Connection(8, Item.Switch),
            },
            new Connection[] //Zone 5
			{
                new Connection(0, Item.Flippers),
                new Connection(1, Item.Bracelet),
            },
            new Connection[] //Zone 6
			{
                new Connection(0, Item.Flippers),
                new Connection(7, Item.Bracelet),
            },
            new Connection[] //Zone 7
			{
                new Connection(6, Item.Bracelet),
            },
            new Connection[] //Zone 8
			{
                new Connection(4, Item.Switch),
            },
            new Connection[] //Zone 9
			{
                new Connection(0, Item.Flippers),
            },
        };

        public WarpList Overworld1 { get; set; }
        public WarpList Overworld2 { get; set; }

        public WarpList AllWarps
        {
            get
            {
                var list = new WarpList();
                list.AddRanges(Overworld1, Overworld2);
                return list;
            }
        }

        public WarpData(RandomizerOptions options)
        {
            OverworldWarps1();
            OverworldWarps2();

            if (options["IncludeMarinHouse"].Enabled)
                IncludeMarinHouse();

            if (options["IncludeEgg"].Enabled)
                IncludeEgg();
        }

        public Warp GetPair(Warp warp)
        {
            return AllWarps.Where(x => x.Location == warp.Destination).FirstOrDefault();
        }

        public Warp GetPair(string code)
        {
            var warp = AllWarps[code];
            return AllWarps.Where(x => x.Location == warp.Destination).FirstOrDefault();
        }

        private void OverworldWarps1()
        {
            Overworld1 = new WarpList
            {
                new Warp
                {
                    Code = "OW1-00",
                    Description = "D8 mountain top #1",
                    Address = 0x24233,
                    Location = 0xE000004850,
                    Default = 0xE1073A5810,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-02"),
                    }
                },
                new Warp
                {
                    Code = "OW1-02",
                    Description = "D8 mountain top #2 ",
                    Address = 0x242ED,
                    Location = 0xE000023850,
                    Default = 0xE1073D5810,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-00"),
                    }
                },
                new Warp
                {
                    Code = "OW1-03",
                    Description = "Exit from flame skip cave",
                    Address = 0x24385,
                    Location = 0xE000034850,
                    Default = 0xE11FEE1840,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-10"),
                        new Connection("OW1-11"),
                    }
                },
                new Warp
                {
                    Code = "OW1-04",
                    Description = "Upgrade shrine (mountain)",
                    Address = 0x243D2,
                    Location = 0xE000047870,
                    Default = 0xE11FE28850,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-15", Item.Bracelet),
                    }
                },
                new Warp
                {
                    Code = "OW1-07",
                    Description = "Entrance to hookshot gap",
                    Address = 0x24538,
                    Location = 0xE000073850,
                    Default = 0xE10AEE7830,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-0A-2", Item.Hookshot),
                        new Connection("OW1-0A-3", Item.Hookshot),
                        new Connection("OW1-18-2", Item.Hookshot, Item.Flippers),
                        new Connection("OW1-19", Item.Hookshot, Item.Flippers),
                        new Connection("OW1-1D-1", Item.Hookshot, Item.Flippers),
                    }
                },
                new Warp
                {
                    Code = "OW1-0A-1",
                    Description = "Exit for Papahl cave",
                    Address = 0x24634,
                    Location = 0xE0000A1870,
                    Default = 0xE10A8B507C,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW1-0A-2",
                    Description = "Cucco house",
                    Address = 0x24646,
                    Location = 0xE0000A4822,
                    Default = 0xE1109F507C,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-07", Item.Hookshot),
                        new Connection("OW1-0A-3"),
                        new Connection("OW1-18-2", Item.Flippers),
                        new Connection("OW1-19", Item.Flippers),
                        new Connection("OW1-1D-1", Item.Flippers),
                    }
                },
                new Warp
                {
                    Code = "OW1-0A-3",
                    Description = "Bird key cave",
                    Address = 0x2463D,
                    Location = 0xE0000A7870,
                    Default = 0xE10A7E607C,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-07", Item.Hookshot),
                        new Connection("OW1-0A-2"),
                        new Connection("OW1-18-2", Item.Flippers),
                        new Connection("OW1-19", Item.Flippers),
                        new Connection("OW1-1D-1", Item.Flippers),
                    }
                },
                new Warp
                {
                    Code = "OW1-0D",
                    Description = "Exit from hidden part of water cave (before D7)",
                    Address = 0x24785,
                    Location = 0xE0000D1870,
                    Default = 0xE10AF2507C,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW1-0E",
                    Description = "Entrance to D7",
                    Address = 0x2484A,
                    Location = 0xE0000E5830,
                    Default = 0xE1060E507C,
                    Locked = true,
                },
                new Warp
                {
                    Code = "OW1-0F",
                    Description = "Exit from D7 cave",
                    Address = 0x2487D,
                    Location = 0xE0000F4850,
                    Default = 0xE10A8E707C,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW1-10",
                    Description = "Entrance to D8",
                    Address = 0x248B7,
                    Location = 0xE000105810,
                    Default = 0xE1075D507C,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-03", Item.Bombs),
                        new Connection("OW1-11", Item.Bombs),
                    }
                },
                new Warp
                {
                    Code = "OW1-11",
                    Description = "Telephone booth (turtle rock)",
                    Address = 0x24903,
                    Location = 0xE000116832,
                    Default = 0xE11099507C,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-03"),
                        new Connection("OW1-10"),
                    }
                },
                new Warp
                {
                    Code = "OW1-13",
                    Description = "Entrance to flame skip cave",
                    Address = 0x24995,
                    Location = 0xE000135810,
                    Default = 0xE11FFE707C,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-03"),
                        new Connection("OW1-04"),
                        new Connection("OW1-10"),
                        new Connection("OW1-11"),
                        new Connection("OW1-15"),
                    }
                },
                new Warp
                {
                    Code = "OW1-15",
                    Description = "Exit from hookshot gap",
                    Address = 0x24A05,
                    Location = 0xE000158840,
                    Default = 0xE10AEA507C,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-04"),
                    }
                },
                new Warp
                {
                    Code = "OW1-17",
                    Description = "Entrance to mountain access cave",
                    Address = 0x24AC1,
                    Location = 0xE000173832,
                    Default = 0xE10AB6507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1, Item.Bracelet),
                        new Connection(3, Item.Bracelet),
                    }
                },
                new Warp
                {
                    Code = "OW1-18-1",
                    Description = "Left exit from access cave (chest)",
                    Address = 0x24B14,
                    Location = 0xE000186812,
                    Default = 0xE10ABB507C,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW1-18-2",
                    Description = "Right exit from access cave",
                    Address = 0x24B1B,
                    Location = 0xE000188812,
                    Default = 0xE10ABC307C,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-07", Item.Hookshot, Item.Flippers),
                        new Connection("OW1-0A-2", Item.Flippers),
                        new Connection("OW1-0A-3", Item.Flippers),
                        new Connection("OW1-19"),
                        new Connection("OW1-1D-1", Item.Flippers),
                    }
                },
                new Warp
                {
                    Code = "OW1-19",
                    Description = "Entrance to Papahl cave",
                    Address = 0x24B3F,
                    Location = 0xE000198840,
                    Default = 0xE10A89407C,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-07", Item.Hookshot, Item.Flippers),
                        new Connection("OW1-0A-2", Item.Flippers),
                        new Connection("OW1-0A-3", Item.Flippers),
                        new Connection("OW1-18-2"),
                        new Connection("OW1-1D-1", Item.Flippers),
                    }
                },
                new Warp
                {
                    Code = "OW1-1D-1",
                    Description = "Entrance to water cave to D7",
                    Address = 0x24CE1,
                    Location = 0xE0001D1830,
                    Default = 0xE10AF9207C,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-07", Item.Hookshot, Item.Flippers),
                        new Connection("OW1-0A-2", Item.Flippers),
                        new Connection("OW1-0A-3", Item.Flippers),
                        new Connection("OW1-18-2", Item.Flippers),
                        new Connection("OW1-19", Item.Flippers),
                    }
                },
                new Warp
                {
                    Code = "OW1-1D-2",
                    Description = "Exit from water cave to D7",
                    Address = 0x24D03,
                    Location = 0xE0001D7850,
                    Default = 0xE10AFA707C,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-1E-1"),
                    }
                },
                new Warp
                {
                    Code = "OW1-1E-1",
                    Description = "Entrance to loop cave (outer)",
                    Address = 0x24D2E,
                    Location = 0xE0001E3810,
                    Default = 0xE10A80207C,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-1D-2"),
                    }
                },
                new Warp
                {
                    Code = "OW1-1E-2",
                    Description = "Entrance to loop cave (inner)",
                    Address = 0x24D50,
                    Location = 0xE0001E7810,
                    Default = 0xE10A83807C,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-1F-1"),
                    }
                },
                new Warp
                {
                    Code = "OW1-1F-1",
                    Description = "Exit from loop cave (outer)",
                    Address = 0x24D8A,
                    Location = 0xE0001F2810,
                    Default = 0xE10A82707C,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-1E-2"),
                    }
                },
                new Warp
                {
                    Code = "OW1-1F-2",
                    Description = "Exit from loop cave (inner)",
                    Address = 0x24DBF,
                    Location = 0xE0001F5840,
                    Default = 0xE10A87607C,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-1F-3"),
                        new Connection("OW1-1F-4"),
                    }
                },
                new Warp
                {
                    Code = "OW1-1F-3",
                    Description = "Fairy fountain (bombable wall)",
                    Address = 0x24DCD,
                    Location = 0xE0001F3850,
                    Default = 0xE11FFB507C,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-1F-2", Item.Bombs),
                        new Connection("OW1-1F-4", Item.Bombs),
                    }
                },
                new Warp
                {
                    Code = "OW1-1F-4",
                    Description = "Entrance to D7 cave",
                    Address = 0x24D94,
                    Location = 0xE0001F7810,
                    Default = 0xE10A8C607C,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-1F-2"),
                        new Connection("OW1-1F-3"),
                    }
                },
                new Warp
                {
                    Code = "OW1-20",
                    Description = "Left entrance to cave below D8",
                    Address = 0x24E04,
                    Location = 0xE000208832,
                    Default = 0xE111AE507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(2),
                    }
                },
                new Warp
                {
                    Code = "OW1-21",
                    Description = "Right entrance to cave below D8",
                    Address = 0x24E3F,
                    Location = 0xE000211832,
                    Default = 0xE111AF507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(2),
                    }
                },
                new Warp
                {
                    Code = "OW1-24",
                    Description = "Entrance to D2",
                    Address = 0x24F05,
                    Location = 0xE000243822,
                    Default = 0xE10136507C,
                    Locked = true,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1, Item.BowWow),
                    }
                },
                new Warp
                {
                    Code = "OW1-2B-1",
                    Description = "Entrance to D4",
                    Address = 0x250D8,
                    Location = 0xE0002B4822,
                    Default = 0xE1037A507C,
                    Locked = true,
                },
                new Warp
                {
                    Code = "OW1-2B-2",
                    Description = "Entrance to D4 cave",
                    Address = 0x250F3,
                    Location = 0xE0002B6830,
                    Default = 0xE11FE92820,
                    Locked = true,
                },
                new Warp
                {
                    Code = "OW1-2D",
                    Description = "Exit from D4 cave",
                    Address = 0x25164,
                    Location = 0xE0002D5850,
                    Default = 0xE11FEA8870,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(0, Item.Flippers),
                        new Connection(1, Item.Bracelet),
                        new Connection(3, Item.Bracelet),
                        new Connection(10, Item.Hookshot),
                    }
                },
                new Warp
                {
                    Code = "OW1-2F",
                    Description = "Exit from raft cave",
                    Address = 0x251DE,
                    Location = 0xE0002F1870,
                    Default = 0xE11FE74810,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(10),
                    }
                },
                new Warp
                {
                    Code = "OW1-30",
                    Description = "Writer's house",
                    Address = 0x2521A,
                    Location = 0xE000307832,
                    Default = 0xE110A8507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(2),
                    }
                },
                new Warp
                {
                    Code = "OW1-31",
                    Description = "Telephone booth (swamp)",
                    Address = 0x25253,
                    Location = 0xE000316852,
                    Default = 0xE1109B507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1),
                    }
                },
                new Warp
                {
                    Code = "OW1-35",
                    Description = "Moblin hideout",
                    Address = 0x25364,
                    Location = 0xE000356850,
                    Default = 0xE115F0507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(3),
                    }
                },
                new Warp
                {
                    Code = "OW1-37",
                    Description = "Photo gallery",
                    Address = 0x253F2,
                    Location = 0xE000374842,
                    Default = 0xE110B5507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(3),
                    }
                },
                new Warp
                {
                    Code = "OW1-3F",
                    Description = "Raft shop",
                    Address = 0x25615,
                    Location = 0xE0003F2822,
                    Default = 0xE110B0507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(10),
                    }
                },
                new Warp
                {
                    Code = "OW1-42",
                    Description = "Mysterious woods hookshot cave",
                    Address = 0x25724,
                    Location = 0xE000423842,
                    Default = 0xE111B3507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1, Item.Bracelet),
                    }
                },
                new Warp
                {
                    Code = "OW1-45",
                    Description = "Crazy Tracy's house",
                    Address = 0x257F6,
                    Location = 0xE000458842,
                    Default = 0xE10EAD507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1, Item.Bracelet),
                        new Connection(3, Item.Bracelet),
                    }
                },
                new Warp
                {
                    Code = "OW1-49",
                    Description = "Exit from Kanalet cave",
                    Address = 0x258E0,
                    Location = 0xE000496850,
                    Default = 0xE21FEB1830,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(8),
                    }
                },
                new Warp
                {
                    Code = "OW1-4A",
                    Description = "Entrance to Kanalet cave",
                    Address = 0x2591E,
                    Location = 0xE0004A8830,
                    Default = 0xE21FEC6830,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(9),
                    }
                },
                new Warp
                {
                    Code = "OW1-4B",
                    Description = "Telephone booth (Kanalet)",
                    Address = 0x25941,
                    Location = 0xE0004B4822,
                    Default = 0xE110CC507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(9),
                    }
                },
                new Warp
                {
                    Code = "OW1-50",
                    Description = "Exit from log cave",
                    Address = 0x25A89,
                    Location = 0xE000508832,
                    Default = 0xE10AAB507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1, Item.Feather),
                    }
                },
                new Warp
                {
                    Code = "OW1-52",
                    Description = "Upgrade shrine (woods)",
                    Address = 0x25B2F,
                    Location = 0xE000526830,
                    Default = 0xE11FE18850,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1, Item.Bracelet),
                    }
                },
                new Warp
                {
                    Code = "OW1-59-1",
                    Description = "Left Kanalet roof door",
                    Address = 0x25CB7,
                    Location = 0xE000591830,
                    Default = 0xE114D5507C,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-59-2"),
                    }
                },
                new Warp
                {
                    Code = "OW1-59-2",
                    Description = "Right Kanalet roof door",
                    Address = 0x25CCA,
                    Location = 0xE000595840,
                    Default = 0xE114D6507C,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-59-1"),
                    }
                },
                new Warp
                {
                    Code = "OW1-62",
                    Description = "Entrance to log cave",
                    Address = 0x25F28,
                    Location = 0xE000627842,
                    Default = 0xE10ABD507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1),
                    }
                },
                new Warp
                {
                    Code = "OW1-65",
                    Description = "Witch's hut",
                    Address = 0x25FBD,
                    Location = 0xE000654832,
                    Default = 0xE10EA2507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1),
                    }
                },
                new Warp
                {
                    Code = "OW1-69",
                    Description = "Kanalet entrance",
                    Address = 0x260C5,
                    Location = 0xE000695840,
                    Default = 0xE114D3507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(8),
                    }
                },
                new Warp
                {
                    Code = "OW1-6C",
                    Description = "Stairs from raft ride to D6",
                    Address = 0x2617F,
                    Location = 0xE0006C4840,
                    Default = 0xE105B07810,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW1-75",
                    Description = "Entrance to graveyard cave",
                    Address = 0x263D7,
                    Location = 0xE000753840,
                    Default = 0xE10ADE3840,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1, Item.Bracelet),
                    }
                },
                new Warp
                {
                    Code = "OW1-76",
                    Description = "Exit from graveyard cave",
                    Address = 0x2642C,
                    Location = 0xE000766850,
                    Default = 0xE10ADF3830,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(3),
                    }
                },
                new Warp
                {
                    Code = "OW1-77",
                    Description = "Entrance to D0",
                    Address = 0x26459,
                    Location = 0xE00077782E,
                    Default = 0xE1FF12505C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(3, Item.Bracelet),
                    }
                },
                new Warp
                {
                    Code = "OW1-78",
                    Description = "Kanalet moat stairs",
                    Address = 0x264BE,
                    Location = 0xE000782870,
                    Default = 0xE11FFD5850,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(4, Item.Hookshot),
                    }
                },
                new Warp
                {
                    Code = "OW1-82-1",
                    Description = "Left door to Papahl's house",
                    Address = 0x680AC,
                    Location = 0xE000825852,
                    Default = 0xE110A5507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1),
                    }
                },
                new Warp
                {
                    Code = "OW1-82-2",
                    Description = "Right door to Papahl's house",
                    Address = 0x680B1,
                    Location = 0xE000827852,
                    Default = 0xE110A6507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1),
                    }
                },
                new Warp
                {
                    Code = "OW1-83",
                    Description = "Dream shrine",
                    Address = 0x680E0,
                    Location = 0xE000832842,
                    Default = 0xE113AA507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1, Item.Bracelet),
                    }
                },
                new Warp
                {
                    Code = "OW1-84",
                    Description = "Cave beside Mabe Village",
                    Address = 0x68125,
                    Location = 0xE000849862,
                    Default = 0xE111CD507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(4),
                    }
                },
                new Warp
                {
                    Code = "OW1-86",
                    Description = "Crystal cave before D3 (bombable wall)",
                    Address = 0x681DB,
                    Location = 0xE000861840,
                    Default = 0xE111F4407C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(4, Item.Bombs),
                    }
                },
                new Warp
                {
                    Code = "OW1-87",
                    Description = "Fairy fountain at honey tree (bombable wall)",
                    Address = 0x68229,
                    Location = 0xE000872810,
                    Default = 0xE11FF3507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(4, Item.Bombs),
                    }
                },
                new Warp
                {
                    Code = "OW1-88",
                    Description = "Telephone booth (Ukuku Prairie)",
                    Address = 0x6824E,
                    Location = 0xE000885852,
                    Default = 0xE1109C507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(4),
                    }
                },
                new Warp
                {
                    Code = "OW1-8A",
                    Description = "Seashell mansion",
                    Address = 0x682B9,
                    Location = 0xE0008A5840,
                    Default = 0xE210E90870,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(4),
                    }
                },
                new Warp
                {
                    Code = "OW1-8C",
                    Description = "Entrance to D6",
                    Address = 0x683B3,
                    Location = 0xE0008C3840,
                    Default = 0xE105D4507C,
                    Locked = true,
                },
                new Warp
                {
                    Code = "OW1-8D",
                    Description = "Fairy fountain outside D6 (bombable wall)",
                    Address = 0x683F4,
                    Location = 0xE0008D3820,
                    Default = 0xE11FAC507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(0, Item.Flippers, Item.Bombs),
                        new Connection(6, Item.Bracelet, Item.Bombs),
                        new Connection(7, Item.Bracelet, Item.Bombs),
                    }
                },
                new Warp
                {
                    Code = "OW1-8F",
                    Description = "Entrance to raft cave",
                    Address = 0x68442,
                    Location = 0xE0008F0820,
                    Default = 0xE11FF78860,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(0, Item.Flippers),
                    }
                },
                new Warp
                {
                    Code = "OW1-92",
                    Description = "Rooster's grave",
                    Address = 0x68502,
                    Location = 0xE000925852,
                    Default = 0xE11FF45870,
                    Locked = true,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1, Item.Bracelet),
                    }
                },
                new Warp
                {
                    Code = "OW1-93",
                    Description = "Shop",
                    Address = 0x68530,
                    Location = 0xE000934862,
                    Default = 0xE10EA1507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1),
                    }
                },
                new Warp
                {
                    Code = "OW1-9C",
                    Description = "Exit from D6 cave",
                    Address = 0x68736,
                    Location = 0xE0009C5810,
                    Default = 0xE11FF03810,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW1-9D",
                    Description = "Entrance to D6 cave",
                    Address = 0x6877F,
                    Location = 0xE0009D3830,
                    Default = 0xE11FF18860,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(0, Item.Flippers),
                    }
                },
                new Warp
                {
                    Code = "OW1-A1-1",
                    Description = "Madame Meow Meow's house",
                    Address = 0x6889D,
                    Location = 0xE000A13842,
                    Default = 0xE110A7507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1),
                    }
                },
                new Warp
                {
                    Code = "OW1-A1-2",
                    Description = "Doghouse",
                    Address = 0x6886F,
                    Location = 0xE000A15842,
                    Default = 0xE112B2507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1),
                    }
                },
                new Warp
                {
                    Code = "OW1-A4",
                    Description = "Telephone booth (signpost maze)",
                    Address = 0x6891C,
                    Location = 0xE000A43842,
                    Default = 0xE110B4507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(4),
                    }
                },
                new Warp
                {
                    Code = "OW1-AA",
                    Description = "Entrance to animal village cave",
                    Address = 0x68A5F,
                    Location = 0xE000AA8840,
                    Default = 0xE111D02840,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(4),
                    }
                },
                new Warp
                {
                    Code = "OW1-AB",
                    Description = "Exit from animal village cave",
                    Address = 0x68A80,
                    Location = 0xE000AB7850,
                    Default = 0xE111D17840,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(6),
                    }
                },
                new Warp
                {
                    Code = "OW1-AC",
                    Description = "Southern face shrine",
                    Address = 0x68AF5,
                    Location = 0xE000AC5840,
                    Default = 0xE1168F507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(7),
                    }
                },
                new Warp
                {
                    Code = "OW1-AE",
                    Description = "Armos maze secret seashell cave",
                    Address = 0x68B39,
                    Location = 0xE000AE4870,
                    Default = 0xE111FC6860,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(7),
                    }
                },
                new Warp
                {
                    Code = "OW1-B0",
                    Description = "Library",
                    Address = 0x68BC2,
                    Location = 0xE000B03832,
                    Default = 0xE11DFA507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1),
                    }
                },
                new Warp
                {
                    Code = "OW1-B1",
                    Description = "Ulrira's house",
                    Address = 0x68BE5,
                    Location = 0xE000B14862,
                    Default = 0xE110A9507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1),
                    }
                },
                new Warp
                {
                    Code = "OW1-B2",
                    Description = "Telephone booth (Mabe Village)",
                    Address = 0x68C1A,
                    Location = 0xE000B25852,
                    Default = 0xE110CB507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1),
                    }
                },
                new Warp
                {
                    Code = "OW1-B3",
                    Description = "Trendy game",
                    Address = 0x68C3C,
                    Location = 0xE000B35852,
                    Default = 0xE10FA0507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1),
                    }
                },
                new Warp
                {
                    Code = "OW1-B5",
                    Description = "Entrance to D3",
                    Address = 0x68CDA,
                    Location = 0xE000B56820,
                    Default = 0xE10252507C,
                    Locked = true,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(4, Item.Flippers),
                    }
                },
                new Warp
                {
                    Code = "OW1-B8-1",
                    Description = "Hidden exit (top) from moblin maze cave",
                    Address = 0x68D9E,
                    Location = 0xE000B85830,
                    Default = 0xE10A95707C,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW1-B8-2",
                    Description = "Entrance to moblin maze cave",
                    Address = 0x68DB3,
                    Location = 0xE000B87860,
                    Default = 0xE10A92307C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(4),
                    }
                },
                new Warp
                {
                    Code = "OW1-C6",
                    Description = "Exit from villa cave",
                    Address = 0x690EA,
                    Location = 0xE000C63850,
                    Default = 0xE111C9807C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(4),
                    }
                },
                new Warp
                {
                    Code = "OW1-C8",
                    Description = "Exit from moblin maze cave",
                    Address = 0x69184,
                    Location = 0xE000C82850,
                    Default = 0xE10A93307C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(5),
                    }
                },
                new Warp
                {
                    Code = "OW1-CC-1",
                    Description = "Large house (Animal Village)",
                    Address = 0x69257,
                    Location = 0xE000CC2850,
                    Default = 0xE110DB507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(6),
                    }
                },
                new Warp
                {
                    Code = "OW1-CC-2",
                    Description = "Painter's house (Animal Village)",
                    Address = 0x69263,
                    Location = 0xE000CC7850,
                    Default = 0xE110DD507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(6),
                    }
                },
                new Warp
                {
                    Code = "OW1-CD-1",
                    Description = "HP cave behind animal village",
                    Address = 0x6927B,
                    Location = 0xE000CD8820,
                    Default = 0xE10AF7607C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(6, Item.Bombs),
                    }
                },
                new Warp
                {
                    Code = "OW1-CD-2",
                    Description = "Goat's house (Animal Village)",
                    Address = 0x69282,
                    Location = 0xE000CD2850,
                    Default = 0xE110D9507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(6),
                    }
                },
                new Warp
                {
                    Code = "OW1-CD-3",
                    Description = "Zora's house (Animal Village)",
                    Address = 0x69289,
                    Location = 0xE000CD5850,
                    Default = 0xE110DA507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(6),
                    }
                },
                new Warp
                {
                    Code = "OW1-CF",
                    Description = "Exit from Lanmolas cave",
                    Address = 0x69329,
                    Location = 0xE000CF5810,
                    Default = 0xE11FF97860,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(6, Item.Bombs),
                    }
                },
                new Warp
                {
                    Code = "OW1-D3",
                    Description = "Entrance to D1",
                    Address = 0x693F8,
                    Location = 0xE000D36822,
                    Default = 0xE10017507C,
                    Locked = true,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1),
                    }
                },
                new Warp
                {
                    Code = "OW1-D4",
                    Description = "Mamu's cave",
                    Address = 0x6942C,
                    Location = 0xE000D48830,
                    Default = 0xE111FB8870,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(4, Item.Feather, Item.Bracelet, Item.Hookshot),
                    }
                },
                new Warp
                {
                    Code = "OW1-D6",
                    Description = "Richard's villa",
                    Address = 0x69505,
                    Location = 0xE000D64850,
                    Default = 0xE110C7507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(4),
                    }
                },
                new Warp
                {
                    Code = "OW1-D9-1",
                    Description = "Entrance to D5",
                    Address = 0x695A3,
                    Location = 0xE000D95840,
                    Default = 0xE104A1507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(0, Item.Flippers),
                    }
                },
                new Warp
                {
                    Code = "OW1-DB",
                    Description = "Telephone booth (Animal Village)",
                    Address = 0x6961F,
                    Location = 0xE000DB7852,
                    Default = 0xE110E3507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(6),
                    }
                },
                new Warp
                {
                    Code = "OW1-DD",
                    Description = "Chef Bear's house (Animal Village)",
                    Address = 0x69681,
                    Location = 0xE000DD5842,
                    Default = 0xE110D7507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(6),
                    }
                },
                new Warp
                {
                    Code = "OW1-E3",
                    Description = "Crocodile's house",
                    Address = 0x697CE,
                    Location = 0xE000E34830,
                    Default = 0xE110FE507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1),
                    }
                },
                new Warp
                {
                    Code = "OW1-E6",
                    Description = "Upgrade shrine (Martha's Bay)",
                    Address = 0x6984C,
                    Location = 0xE000E64840,
                    Default = 0xE11FE08870,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-E7"),
                    }
                },
                new Warp
                {
                    Code = "OW1-E7",
                    Description = "Exit from cave to upgrade shrine (Martha's Bay)",
                    Address = 0x6988C,
                    Location = 0xE000E76820,
                    Default = 0xE11FE52830,
                    Connections = new Connection[]
                    {
                        new Connection("OW1-E6"),
                    }
                },
                new Warp
                {
                    Code = "OW1-E8",
                    Description = "Telephone booth (Martha's Bay)",
                    Address = 0x698D8,
                    Location = 0xE000E83862,
                    Default = 0xE1109D507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(5),
                    }
                },
                new Warp
                {
                    Code = "OW1-E9",
                    Description = "Magnifying lens cave (mermaid statue)",
                    Address = 0x69931,
                    Location = 0xE000E96830,
                    Default = 0xE10A986860,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(6, Item.Hookshot),
                    }
                },
                new Warp
                {
                    Code = "OW1-F4",
                    Description = "Boomerang moblin's cave",
                    Address = 0x69B9F,
                    Location = 0xE000F41820,
                    Default = 0xE11FF5487C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1, Item.Bombs),
                    }
                },
                new Warp
                {
                    Code = "OW1-F6",
                    Description = "House by the bay",
                    Address = 0x69C20,
                    Location = 0xE000F65842,
                    Default = 0xE11EE3507C,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(1, Item.Bracelet),
                        new Connection(5, Item.Feather, Item.Boots),
                    }
                },
                new Warp
                {
                    Code = "OW1-F9",
                    Description = "Entrance to cave to upgrade shrine (Martha's Bay)",
                    Address = 0x69CF4,
                    Location = 0xE000F97850,
                    Default = 0xE11FF68870,
                    ZoneConnections = new Connection[]
                    {
                        new Connection(5, Item.Feather),
                    }
                },
            };
        }

        private void OverworldWarps2()
        {
            Overworld2 = new WarpList
            {
                new Warp
                {
                    Code = "OW2-00",
                    Description = "D8 - Staircase to OW 00",
                    Address = 0x2CE03,
                    Location = 0xE1073A5810,
                    Default = 0xE000004850,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-02", Item.Feather),
                        new Connection("OW2-10", Item.Feather),
                    }
                },
                new Warp
                {
                    Code = "OW2-02",
                    Description = "D8 - Staircase to OW 02",
                    Address = 0x2CED7,
                    Location = 0xE1073D5810,
                    Default = 0xE000023850,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-00", Item.Feather),
                        new Connection("OW2-10", Item.Feather),
                    }
                },
                new Warp
                {
                    Code = "OW2-03",
                    Description = "Cave - Flame skip staircase",
                    Address = 0x2B669,
                    Location = 0xE11FEE1840,
                    Default = 0xE000034850,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-04",
                    Description = "Cave - Upgrade shrine (mountain)",
                    Address = 0x2B30E,
                    Location = 0xE11FE28850,
                    Default = 0xE000047870,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-07",
                    Description = "Cave - Entrance to hookshot gap",
                    Address = 0x2F93B,
                    Location = 0xE10AEE7830,
                    Default = 0xE000073850,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-15", Item.Hookshot, Item.Feather),
                    }
                },
                new Warp
                {
                    Code = "OW2-0A-1",
                    Description = "Cave - Exit for Papahl cave",
                    Address = 0x2E0B1,
                    Location = 0xE10A8B507C,
                    Default = 0xE0000A1870,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-19"),
                    }
                },
                new Warp
                {
                    Code = "OW2-0A-2",
                    Description = "House - Cucco house",
                    Address = 0x2E53E,
                    Location = 0xE1109F507C,
                    Default = 0xE0000A4822,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-0A-3",
                    Description = "Cave - Bird key cave entrance",
                    Address = 0x2DC70,
                    Location = 0xE10A7E607C,
                    Default = 0xE0000A7870,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-0D",
                    Description = "Cave - Exit from hidden part of water cave (before D7)",
                    Address = 0x2FA73,
                    Location = 0xE10AF2507C,
                    Default = 0xE0000D1870,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-1D-1", Item.Bombs),
                        new Connection("OW2-1D-2", Item.Bombs),
                    }
                },
                new Warp
                {
                    Code = "OW2-0E",
                    Description = "D7 - Entrance",
                    Address = 0x2C539,
                    Location = 0xE1060E507C,
                    Default = 0xE0000E5830,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-0F",
                    Description = "Cave - Exit from D7 cave",
                    Address = 0x2E1A2,
                    Location = 0xE10A8E707C,
                    Default = 0xE0000F4850,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-1F-4"),
                    }
                },
                new Warp
                {
                    Code = "OW2-10",
                    Description = "D8 - Entrance",
                    Address = 0x2D536,
                    Location = 0xE1075D507C,
                    Default = 0xE000105810,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-00"),
                        new Connection("OW2-02", Item.Feather),
                    }
                },
                new Warp
                {
                    Code = "OW2-11",
                    Description = "House - Telephone booth (turtle rock)",
                    Address = 0x2E460,
                    Location = 0xE11099507C,
                    Default = 0xE000116832,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-13",
                    Description = "Cave - Entrance to flame skip",
                    Address = 0x2BB31,
                    Location = 0xE11FFE707C,
                    Default = 0xE000135810,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-15",
                    Description = "Cave - Exit from hookshot gap",
                    Address = 0x2F7BA,
                    Location = 0xE10AEA507C,
                    Default = 0xE000158840,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-07", Item.Hookshot),
                    }
                },
                new Warp
                {
                    Code = "OW2-17",
                    Description = "Cave - Entrance to mountain access cave",
                    Address = 0x2EA6D,
                    Location = 0xE10AB6507C,
                    Default = 0xE000173832,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-18-1"),
                        new Connection("OW2-18-2", Item.Boots),
                    }
                },
                new Warp
                {
                    Code = "OW2-18-1",
                    Description = "Cave - Left exit from access cave (chest)",
                    Address = 0x2EBD7,
                    Location = 0xE10ABB507C,
                    Default = 0xE000186812,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-17"),
                        new Connection("OW2-18-2", Item.Boots),
                    }
                },
                new Warp
                {
                    Code = "OW2-18-2",
                    Description = "Cave - Right exit from access cave",
                    Address = 0x2EC20,
                    Location = 0xE10ABC307C,
                    Default = 0xE000188812,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-17", Item.Boots),
                        new Connection("OW2-18-1", Item.Boots),
                    }
                },
                new Warp
                {
                    Code = "OW2-19",
                    Description = "Cave - Entrance to Papahl cave",
                    Address = 0x2E013,
                    Location = 0xE10A89407C,
                    Default = 0xE000198840,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-0A-1"),
                    }
                },
                new Warp
                {
                    Code = "OW2-1D-1",
                    Description = "Cave - Entrance to water cave to D7",
                    Address = 0x2FC85,
                    Location = 0xE10AF9207C,
                    Default = 0xE0001D1830,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-1D-2"),
                        new Connection("OW2-0D"),
                    }
                },
                new Warp
                {
                    Code = "OW2-1D-2",
                    Description = "Cave - Exit from water cave to D7",
                    Address = 0x2FCE3,
                    Location = 0xE10AFA707C,
                    Default = 0xE0001D7850,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-1D-1"),
                        new Connection("OW2-0D"),
                    }
                },
                new Warp
                {
                    Code = "OW2-1E-1",
                    Description = "Cave - Entrance to loop cave (outer)",
                    Address = 0x2DD11,
                    Location = 0xE10A80207C,
                    Default = 0xE0001E3810,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-1F-1"),
                    }
                },
                new Warp
                {
                    Code = "OW2-1E-2",
                    Description = "Cave - Entrance to loop cave (inner)",
                    Address = 0x2DE13,
                    Location = 0xE10A83807C,
                    Default = 0xE0001E7810,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-1F-1",
                    Description = "Cave - Exit from loop cave (outer)",
                    Address = 0x2DDAE,
                    Location = 0xE10A82707C,
                    Default = 0xE0001F2810,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-1E-1"),
                    }
                },
                new Warp
                {
                    Code = "OW2-1F-2",
                    Description = "Cave - Exit from loop cave (inner)",
                    Address = 0x2DF40,
                    Location = 0xE10A87607C,
                    Default = 0xE0001F5840,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-1F-3",
                    Description = "Cave - Fairy fountain before D7",
                    Address = 0x2BA6D,
                    Location = 0xE11FFB507C,
                    Default = 0xE0001F3850,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-1F-4",
                    Description = "Cave - Entrance to D7 cave",
                    Address = 0x2E102,
                    Location = 0xE10A8C607C,
                    Default = 0xE0001F7810,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-0F"),
                    }
                },
                new Warp
                {
                    Code = "OW2-20",
                    Description = "Cave - Left entrance to cave below D8",
                    Address = 0x2E878,
                    Location = 0xE111AE507C,
                    Default = 0xE000208832,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-21"),
                    }
                },
                new Warp
                {
                    Code = "OW2-21",
                    Description = "Cave - Right entrance to cave below D8",
                    Address = 0x2E8D1,
                    Location = 0xE111AF507C,
                    Default = 0xE000211832,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-20"),
                    }
                },
                new Warp
                {
                    Code = "OW2-24",
                    Description = "D2 - Entrance",
                    Address = 0x28DB6,
                    Location = 0xE10136507C,
                    Default = 0xE000243822,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-2B-1",
                    Description = "D4 - Entrance",
                    Address = 0x29E68,
                    Location = 0xE1037A507C,
                    Default = 0xE0002B4822,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-2B-2",
                    Description = "Cave - Entrance to D4 cave",
                    Address = 0x2B532,
                    Location = 0xE11FE92820,
                    Default = 0xE0002B6830,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-2D",
                    Description = "Cave - Exit from D4 cave",
                    Address = 0x2B57E,
                    Location = 0xE11FEA8870,
                    Default = 0xE0002D5850,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-2F",
                    Description = "Cave - Exit from raft cave",
                    Address = 0x2B4B3,
                    Location = 0xE11FE74810,
                    Default = 0xE0002F1870,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-30",
                    Description = "House - Writer's house",
                    Address = 0x2E6EA,
                    Location = 0xE110A8507C,
                    Default = 0xE000307832,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-31",
                    Description = "House - Telephone booth (swamp)",
                    Address = 0x2E4C1,
                    Location = 0xE1109B507C,
                    Default = 0xE000316852,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-35",
                    Description = "Cave - Moblin hideout",
                    Address = 0x2F9D2,
                    Location = 0xE115F0507C,
                    Default = 0xE000356850,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-37",
                    Description = "House - Photo gallery",
                    Address = 0x2EA22,
                    Location = 0xE110B5507C,
                    Default = 0xE000374842,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-3F",
                    Description = "House - Raft shop",
                    Address = 0x2E906,
                    Location = 0xE110B0507C,
                    Default = 0xE0003F2822,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-42",
                    Description = "Cave - Mysterious woods hookshot cave",
                    Address = 0x2E9C8,
                    Location = 0xE111B3507C,
                    Default = 0xE000423842,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-45",
                    Description = "House - Crazy Tracy's house",
                    Address = 0x2E81C,
                    Location = 0xE10EAD507C,
                    Default = 0xE000458842,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-49",
                    Description = "SS - Exit from Kanalet cave",
                    Address = 0x2B5C0,
                    Location = 0xE21FEB1830,
                    Default = 0xE000496850,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-4A", Item.Feather),
                    }
                },
                new Warp
                {
                    Code = "OW2-4A",
                    Description = "SS - Entrance to Kanalet cave",
                    Address = 0x2B61E,
                    Location = 0xE21FEC6830,
                    Default = 0xE0004A8830,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-49", Item.Feather),
                    }
                },
                new Warp
                {
                    Code = "OW2-4B",
                    Description = "House - Telephone booth (Kanalet)",
                    Address = 0x2F06E,
                    Location = 0xE110CC507C,
                    Default = 0xE0004B4822,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-50",
                    Description = "Cave - Exit from log cave",
                    Address = 0x2E7A8,
                    Location = 0xE10AAB507C,
                    Default = 0xE000508832,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-62"),
                    }
                },
                new Warp
                {
                    Code = "OW2-52",
                    Description = "Cave - Upgrade shrine (woods)",
                    Address = 0x2B2BB,
                    Location = 0xE11FE18850,
                    Default = 0xE000526830,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-59-1",
                    Description = "DK - Left Kanalet roof door",
                    Address = 0x2F29A,
                    Location = 0xE114D5507C,
                    Default = 0xE000591830,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-69"),
                    }
                },
                new Warp
                {
                    Code = "OW2-59-2",
                    Description = "DK - Right Kanalet roof door",
                    Address = 0x2F2E3,
                    Location = 0xE114D6507C,
                    Default = 0xE000595840,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-62",
                    Description = "Cave - Entrance to log cave",
                    Address = 0x2EC6D,
                    Location = 0xE10ABD507C,
                    Default = 0xE000627842,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-50"),
                    }
                },
                new Warp
                {
                    Code = "OW2-65",
                    Description = "House - Witch's hut",
                    Address = 0x2E5C5,
                    Location = 0xE10EA2507C,
                    Default = 0xE000654832,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-69",
                    Description = "DK - Entrance",
                    Address = 0x2F21C,
                    Location = 0xE114D3507C,
                    Default = 0xE000695840,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-59-1"),
                    }
                },
                new Warp
                {
                    Code = "OW2-6C",
                    Description = "D6 - Stairs to raft ride",
                    Address = 0x2A85D,
                    Location = 0xE105B07810,
                    Default = 0xE0006C4840,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-8C", Item.Bracelet, Item.Bombs),
                    }
                },
                new Warp
                {
                    Code = "OW2-75",
                    Description = "Cave - Entrance to graveyard cave",
                    Address = 0x2F498,
                    Location = 0xE10ADE3840,
                    Default = 0xE000753840,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-76", Item.Feather),
                    }
                },
                new Warp
                {
                    Code = "OW2-76",
                    Description = "Cave - Exit from graveyard cave",
                    Address = 0x2F4F8,
                    Location = 0xE10ADF3830,
                    Default = 0xE000766850,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-75", Item.Feather),
                    }
                },
                new Warp
                {
                    Code = "OW2-77",
                    Description = "D0 - Entrance",
                    Address = 0x2BED1,
                    Location = 0xE1FF12505C,
                    Default = 0xE00077782E,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-78",
                    Description = "Cave - Kanalet moat stairs",
                    Address = 0x2BAE7,
                    Location = 0xE11FFD5850,
                    Default = 0xE000782870,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-82-1",
                    Description = "House - Left side of Papahl's house",
                    Address = 0x2E678,
                    Location = 0xE110A5507C,
                    Default = 0xE000825852,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-82-2"),
                    }
                },
                new Warp
                {
                    Code = "OW2-82-2",
                    Description = "House - Right side of Papahl's house",
                    Address = 0x2E6A5,
                    Location = 0xE110A6507C,
                    Default = 0xE000827852,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-82-1"),
                    }
                },
                new Warp
                {
                    Code = "OW2-83",
                    Description = "DS - Dream shrine door",
                    Address = 0x2E754,
                    Location = 0xE113AA507C,
                    Default = 0xE000832842,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-84",
                    Description = "Cave - Cave beside Mabe Village",
                    Address = 0x2F0AF,
                    Location = 0xE111CD507C,
                    Default = 0xE000849862,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-86",
                    Description = "Cave - Crystal cave before D3",
                    Address = 0x2FAFF,
                    Location = 0xE111F4407C,
                    Default = 0xE000861840,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-87",
                    Description = "Cave - Fairy fountain at honey tree",
                    Address = 0x2B7CB,
                    Location = 0xE11FF3507C,
                    Default = 0xE000872810,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-88",
                    Description = "House - Telephone booth (Ukuku Prairie)",
                    Address = 0x2E4EE,
                    Location = 0xE1109C507C,
                    Default = 0xE000885852,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-8A",
                    Description = "SS - Seashell mansion",
                    Address = 0x2F75D,
                    Location = 0xE210E90870,
                    Default = 0xE0008A5840,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-8C",
                    Description = "D6 - Entrance",
                    Address = 0x2AFCD,
                    Location = 0xE105D4507C,
                    Default = 0xE0008C3840,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-6C", Item.Bracelet, Item.Bombs),
                    }
                },
                new Warp
                {
                    Code = "OW2-8D",
                    Description = "Cave - Fairy fountain outside D6",
                    Address = 0x2A81F,
                    Location = 0xE11FAC507C,
                    Default = 0xE0008D3820,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-8F",
                    Description = "Cave - Entrance to raft cave",
                    Address = 0x2B982,
                    Location = 0xE11FF78860,
                    Default = 0xE0008F0820,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-92",
                    Description = "Cave - Rooster's grave",
                    Address = 0x2B815,
                    Location = 0xE11FF45870,
                    Default = 0xE000925852,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-93",
                    Description = "House - Shop",
                    Address = 0x2E593,
                    Location = 0xE10EA1507C,
                    Default = 0xE000934862,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-9C",
                    Description = "Cave - Exit from D6 cave",
                    Address = 0x2B706,
                    Location = 0xE11FF03810,
                    Default = 0xE0009C5810,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-9D", Item.Feather, Item.Flippers),
                    }
                },
                new Warp
                {
                    Code = "OW2-9D",
                    Description = "Cave - Entrance to D6 cave",
                    Address = 0x2B74D,
                    Location = 0xE11FF18860,
                    Default = 0xE0009D3830,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-9C", Item.Feather, Item.Flippers),
                    }
                },
                new Warp
                {
                    Code = "OW2-A1-1",
                    Description = "House - Madame Meow Meow's house",
                    Address = 0x2E6CB,
                    Location = 0xE110A7507C,
                    Default = 0xE000A13842,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-A1-2",
                    Description = "House - Doghouse",
                    Address = 0x2E989,
                    Location = 0xE112B2507C,
                    Default = 0xE000A15842,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-A4",
                    Description = "House - Telephone booth (signpost maze)",
                    Address = 0x2E9F5,
                    Location = 0xE110B4507C,
                    Default = 0xE000A43842,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-AA",
                    Description = "Cave - Entrance to animal village cave",
                    Address = 0x2F182,
                    Location = 0xE111D02840,
                    Default = 0xE000AA8840,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-AB", Item.Boots),
                    }
                },
                new Warp
                {
                    Code = "OW2-AB",
                    Description = "Cave - Exit from animal village cave",
                    Address = 0x2F1D6,
                    Location = 0xE111D17840,
                    Default = 0xE000AB7850,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-AA", Item.Boots),
                    }
                },
                new Warp
                {
                    Code = "OW2-AC",
                    Description = "D? - Southern face shrine",
                    Address = 0x2E1D5,
                    Location = 0xE1168F507C,
                    Default = 0xE000AC5840,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-AE",
                    Description = "Cave - Armos maze secret seashell cave",
                    Address = 0x2FD82,
                    Location = 0xE111FC6860,
                    Default = 0xE000AE4870,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-B0",
                    Description = "House - Library",
                    Address = 0x2BA1D,
                    Location = 0xE11DFA507C,
                    Default = 0xE000B03832,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-B1",
                    Description = "House - Ulrira's house",
                    Address = 0x2E718,
                    Location = 0xE110A9507C,
                    Default = 0xE000B14862,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-B2",
                    Description = "House - Telephone booth (Mabe Village)",
                    Address = 0x2F041,
                    Location = 0xE110CB507C,
                    Default = 0xE000B25852,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-B3",
                    Description = "House - Trendy game",
                    Address = 0x2E57A,
                    Location = 0xE10FA0507C,
                    Default = 0xE000B35852,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-B5",
                    Description = "D3 - Entrance",
                    Address = 0x29482,
                    Location = 0xE10252507C,
                    Default = 0xE000B56820,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-B8-1",
                    Description = "Cave - Hidden exit (top) from moblin maze cave",
                    Address = 0x2E37A,
                    Location = 0xE10A95707C,
                    Default = 0xE000B85830,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-B8-2", Item.Feather, Item.Bombs),
                        new Connection("OW2-C8", Item.Feather, Item.Bombs),
                    }
                },
                new Warp
                {
                    Code = "OW2-B8-2",
                    Description = "Cave - Entrance to moblin maze cave",
                    Address = 0x2E2B9,
                    Location = 0xE10A92307C,
                    Default = 0xE000B87860,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-C8"),
                        new Connection("OW2-B8-1", Item.Feather),
                    }
                },
                new Warp
                {
                    Code = "OW2-C6",
                    Description = "Cave - Exit from villa cave",
                    Address = 0x2EFC4,
                    Location = 0xE111C9807C,
                    Default = 0xE000C63850,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-D6", Item.Leaves),
                    }
                },
                new Warp
                {
                    Code = "OW2-C8",
                    Description = "Cave - Exit from moblin maze cave",
                    Address = 0x2E2F5,
                    Location = 0xE10A93307C,
                    Default = 0xE000C82850,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-B8-2"),
                        new Connection("OW2-B8-1", Item.Feather),
                    }
                },
                new Warp
                {
                    Code = "OW2-CC-1",
                    Description = "House - Large house (Animal Village)",
                    Address = 0x2F3FD,
                    Location = 0xE110DB507C,
                    Default = 0xE000CC2850,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-CC-2",
                    Description = "House - Painter's house (Animal Village)",
                    Address = 0x2F456,
                    Location = 0xE110DD507C,
                    Default = 0xE000CC7850,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-CD-1",
                    Description = "Cave - HP cave behind animal village",
                    Address = 0x2FBE6,
                    Location = 0xE10AF7607C,
                    Default = 0xE000CD8820,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-CD-2",
                    Description = "House - Goat's house (Animal Village)",
                    Address = 0x2F396,
                    Location = 0xE110D9507C,
                    Default = 0xE000CD2850,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-CD-3",
                    Description = "House - Zora's house (Animal Village)",
                    Address = 0x2F3CB,
                    Location = 0xE110DA507C,
                    Default = 0xE000CD5850,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-CF",
                    Description = "Cave - Exit from Lanmolas cave",
                    Address = 0x2B9FD,
                    Location = 0xE11FF97860,
                    Default = 0xE000CF5810,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-D3",
                    Description = "D1 - Entrance",
                    Address = 0x286C1,
                    Location = 0xE10017507C,
                    Default = 0xE000D36822,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-D4",
                    Description = "Cave - Mamu's cave",
                    Address = 0x2FD16,
                    Location = 0xE111FB8870,
                    Default = 0xE000D48830,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-D6",
                    Description = "House - Richard's villa",
                    Address = 0x2EF05,
                    Location = 0xE110C7507C,
                    Default = 0xE000D64850,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-C6"),
                    }
                },
                new Warp
                {
                    Code = "OW2-D9-1",
                    Description = "D5 - Entrance",
                    Address = 0x2A56B,
                    Location = 0xE104A1507C,
                    Default = 0xE000D95840,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-DB",
                    Description = "House - Telephone booth (Animal Village)",
                    Address = 0x2F5BE,
                    Location = 0xE110E3507C,
                    Default = 0xE000DB7852,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-DD",
                    Description = "House - Chef Bear's house (Animal Village)",
                    Address = 0x2F314,
                    Location = 0xE110D7507C,
                    Default = 0xE000DD5842,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-E3",
                    Description = "House - Crocodile's house",
                    Address = 0x2FDFB,
                    Location = 0xE110FE507C,
                    Default = 0xE000E34830,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-E6",
                    Description = "Cave - Upgrade shrine (Martha's Bay)",
                    Address = 0x2B268,
                    Location = 0xE11FE08870,
                    Default = 0xE000E64840,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-E7",
                    Description = "Cave - Exit from cave to upgrade shrine (Martha's Bay)",
                    Address = 0x2B3FB,
                    Location = 0xE11FE52830,
                    Default = 0xE000E76820,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-F9", Item.Flippers),
                    }
                },
                new Warp
                {
                    Code = "OW2-E8",
                    Description = "House - Telephone booth (Martha's Bay)",
                    Address = 0x2E51B,
                    Location = 0xE1109D507C,
                    Default = 0xE000E83862,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-E9",
                    Description = "Cave - Magnifying lens cave (mermaid statue)",
                    Address = 0x2E433,
                    Location = 0xE10A986860,
                    Default = 0xE000E96830,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-F4",
                    Description = "Cave - Boomerang moblin's cave (empty)",
                    Address = 0x2B84F,
                    Location = 0xE11FF5487C,
                    Default = 0xE000F41820,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-F6",
                    Description = "House - House by the bay",
                    Address = 0x2B340,
                    Location = 0xE11EE3507C,
                    Default = 0xE000F65842,
                    DeadEnd = true,
                },
                new Warp
                {
                    Code = "OW2-F9",
                    Description = "Cave - Entrance to cave to upgrade shrine (Martha's Bay)",
                    Address = 0x2B908,
                    Location = 0xE11FF68870,
                    Default = 0xE000F97850,
                    Connections = new Connection[]
                    {
                        new Connection("OW2-E7", Item.Flippers),
                    }
                },
            };
        }

        private void IncludeMarinHouse()
        {
            Overworld1.Add(new Warp
            {
                Code = "OW1-A2",
                Description = "Marin & Tarin's house",
                Address = 0x688C0,
                Location = 0xE000A25852,
                Default = 0xE110A3507C,
                ZoneConnections = new Connection[]
                    {
                        new Connection(1),
                    }
            });

            Overworld2.Add(new Warp
            {
                Code = "OW2-A2",
                Description = "House - Marin & Tarin's house",
                Address = 0x2E5F5,
                Location = 0xE110A3507C,
                Default = 0xE000A25852,
                DeadEnd = true,
            });
        }

        private void IncludeEgg()
        {
            Overworld1.Add(new Warp
            {
                Code = "OW1-06",
                Description = "Wind Fish egg",
                Address = 0x24505,
                Location = 0xE000065840,
                Default = 0xE10870507C,
                Locked = true,
                ZoneConnections = new Connection[]
                    {
                        new Connection(1, Item.Bracelet),
                        new Connection(3, Item.Bracelet),
                    }
            });

            Overworld2.Add(new Warp
            {
                Code = "OW2-06",
                Description = "Egg - Exit to mountain",
                Address = 0x2D96C,
                Location = 0xE10870507C,
                Default = 0xE000065840,
                DeadEnd = true,
            });
        }
    }

    public class Warp
    {
        private WarpList parentList;

        public string Code { get; set; }
        public string Description { get; set; }
        public int Address { get; set; }
        public int Address2 { get; set; }
        public long Location { get; set; }
        public long Default { get; set; }
        public long Destination { get; set; }
        public bool DeadEnd { get; set; } = false;
        public bool Locked { get; set; } = false;
        public bool Special { get; set; } = false;
        public Connection[] Connections { get; set; }
        public Connection[] ZoneConnections { get; set; }

        public Warp(WarpList parentList)
        {
            this.parentList = parentList;
        }
    }

    public class Connection
    {
        private int zone;
        private string code;
        private Item[] constraints;

        public int Zone { get { return zone; } }
        public string Code { get { return code; } }
        public Item[] Constraints { get { return constraints; } }
        
        public Connection(string code, Item constraint = 0)
        {
            this.code = code;
            this.constraints = new Item[] { constraint };
        }

        public Connection(string code, params Item[] constraints)
        {
            this.code = code;
            this.constraints = constraints;
        }

        public Connection(int zone, Item constraint = 0)
        {
            this.zone = zone;
            this.constraints = new Item[] { constraint };
        }

        public Connection(int zone, params Item[] constraints)
        {
            this.zone = zone;
            this.constraints = constraints;
        }

        public bool Accessible(Item allowedConstraint = 0)
        {
            if (allowedConstraint == 0)
            {
                if (constraints.ToList().Exists(x => x != Item.None && x != Item.Bombs && x != Item.BowWow))
                    return false;
            }
            else
            {
                if (constraints.ToList().Exists(x => x != Item.None && x != Item.Bombs && x != Item.BowWow && x != allowedConstraint))
                    return false;
            }

            return true;
        }
    }

    public class WarpList : List<Warp>
    {
        public Warp this[string name]
        {
            get { return this.First(w => w.Code == name); }
        }

        public void AddRanges(params List<Warp>[] lists)
        {
            foreach (var list in lists)
                this.AddRange(list);
        }
    }

    public enum Item
    {
        None,
        Bombs,
        Feather,
        Bracelet,
        Boots,
        Flippers,
        Hookshot,
        Leaves,
        BowWow,
        Switch,
    }
}

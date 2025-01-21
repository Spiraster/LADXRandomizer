using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADXRandomizer
{
    public class WarpList : List<WarpData>
    {
        public List<WarpData> Overworld1 => new List<WarpData>(this.Where(x => x.Code.Contains("OW1")).ToList());
        public List<WarpData> Overworld2 => new List<WarpData>(this.Where(x => x.Code.Contains("OW2")).ToList());

        public WarpData this[string name] => this.First(w => w.Code == name);
        
        public ZoneList ZoneList { get; }

        public int[] MapEdits { get; }

        private WarpList(List<WarpData> list)
        {
            AddRange(list);
        }

        public WarpList(Settings settings)
        {
            Initialize();
            ZoneList = new ZoneList();

            if (settings.HasFlag(Settings.ExcludeHouse))
            {
                this["OW1-A2"].WarpValue = this["OW1-A2"].DefaultWarpValue;
                this["OW1-A2"].Exclude = true;
                this["OW2-A2"].WarpValue = this["OW2-A2"].DefaultWarpValue;
                this["OW2-A2"].Exclude = true;
            }

            if (settings.HasFlag(Settings.ExcludeEgg))
            {
                this["OW1-06"].WarpValue = this["OW1-06"].DefaultWarpValue;
                this["OW1-06"].Exclude = true;
                this["OW2-06"].WarpValue = this["OW2-06"].DefaultWarpValue;
                this["OW2-06"].Exclude = true;
            }

            //exclude mermaid statue and ML cave
            this["OW1-E9"].WarpValue = this["OW1-E9"].DefaultWarpValue;
            this["OW1-E9"].Exclude = true;
            this["OW2-E9"].WarpValue = this["OW2-E9"].DefaultWarpValue;
            this["OW2-E9"].Exclude = true;
        }

        private WarpList(WarpList data)
        {
            foreach (var warp in data)
            {
                Add(new WarpData(this)
                {
                    Exclude = warp.Exclude,
                    Code = warp.Code,
                    Description = warp.Description,
                    LocationValue = warp.LocationValue,
                    DefaultWarpValue = warp.DefaultWarpValue,
                    WarpValue = warp.WarpValue,
                    DeadEnd = warp.DeadEnd,
                    Locked = warp.Locked,
                    WarpConnections = warp.WarpConnections,         //not deep copies, but shouldn't matter
                    ZoneConnections = warp.ZoneConnections, //
                });
            }
        }

        private void Initialize()
        {
            Add(new WarpData(this)
            {
                Code = "OW1-00",
                Description = "D8 mountain top #1",
                World = 0,
                Index = 0x00,
                LocationValue = 0xE000004850,
                DefaultWarpValue = 0xE1073A5810,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-02", Connection.Inward),
                    new Connection("OW1-02", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW1-02",
                Description = "D8 mountain top #2 ",
                World = 0,
                Index = 0x02,
                LocationValue = 0xE000023850,
                DefaultWarpValue = 0xE1073D5810,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-00", Connection.Inward),
                    new Connection("OW1-00", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW1-03",
                Description = "Exit from flame skip cave",
                World = 0,
                Index = 0x03,
                LocationValue = 0xE000034850,
                DefaultWarpValue = 0xE11FEE1840,
                ZoneConnections = new ConnectionList
                {
                    new Connection(14, Connection.Inward),
                    new Connection(14, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-04",
                Description = "Upgrade shrine (mountain)",
                World = 0,
                Index = 0x04,
                LocationValue = 0xE000047870,
                DefaultWarpValue = 0xE11FE28850,
                ZoneConnections = new ConnectionList
                {
                    new Connection(15, Connection.Inward, Items.Bracelet),
                    new Connection(15, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-06",
                Description = "Wind Fish egg",
                World = 0,
                Index = 0x06,
                LocationValue = 0xE000065840,
                DefaultWarpValue = 0xE10870507C,
                Locked = true,
                ZoneConnections = new ConnectionList
                {
                    new Connection(13, Connection.Inward, Items.Ocarina, Instruments.AllInstruments),
                    new Connection(13, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-07",
                Description = "Entrance to hookshot gap",
                World = 0,
                Index = 0x07,
                LocationValue = 0xE000073850,
                DefaultWarpValue = 0xE10AEE7830,
                ZoneConnections = new ConnectionList
                {
                    new Connection(17, Connection.Inward, Items.Hookshot),
                    new Connection(13, Connection.Outward),
                    new Connection(17, Connection.Outward, Items.Hookshot),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-0A-1",
                Description = "Exit for Papahl cave",
                World = 0,
                Index = 0x0A,
                LocationValue = 0xE0000A1870,
                DefaultWarpValue = 0xE10A8B507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(16, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-0A-2",
                Description = "Bird key cave",
                World = 0,
                Index = 0x0A,
                LocationValue = 0xE0000A7870,
                DefaultWarpValue = 0xE10A7E607C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(17, Connection.Inward),
                    new Connection(17, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-0A-3",
                Description = "Cucco house",
                World = 0,
                Index = 0x0A,
                LocationValue = 0xE0000A4822,
                DefaultWarpValue = 0xE1109F507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(17, Connection.Inward),
                    new Connection(17, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-0D",
                Description = "Exit from hidden part of water cave (before D7)",
                World = 0,
                Index = 0x0D,
                LocationValue = 0xE0000D1870,
                DefaultWarpValue = 0xE10AF2507C,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW1-0E",
                Description = "Entrance to D7",
                World = 0,
                Index = 0x0E,
                LocationValue = 0xE0000E5830,
                DefaultWarpValue = 0xE1060E507C,
                Locked = true,
                ZoneConnections = new ConnectionList
                {
                    new Connection(19, Connection.Inward, Keys.BirdKey),
                    new Connection(19, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-0F",
                Description = "Exit from D7 cave",
                World = 0,
                Index = 0x0F,
                LocationValue = 0xE0000F4850,
                DefaultWarpValue = 0xE10A8E707C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(19, Connection.Inward),
                    new Connection(19, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-10",
                Description = "Entrance to D8",
                World = 0,
                Index = 0x10,
                LocationValue = 0xE000105810,
                DefaultWarpValue = 0xE1075D507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(14, Connection.Inward, Songs.Song3),
                    new Connection(14, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-11",
                Description = "Telephone booth (turtle rock)",
                World = 0,
                Index = 0x11,
                LocationValue = 0xE000116832,
                DefaultWarpValue = 0xE11099507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(14, Connection.Inward),
                    new Connection(14, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-13",
                Description = "Entrance to flame skip cave",
                World = 0,
                Index = 0x13,
                LocationValue = 0xE000135810,
                DefaultWarpValue = 0xE11FFE707C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(14, Connection.Inward),
                    new Connection(15, Connection.Inward),
                    new Connection(2, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-15",
                Description = "Exit from hookshot gap",
                World = 0,
                Index = 0x15,
                LocationValue = 0xE000158840,
                DefaultWarpValue = 0xE10AEA507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(15, Connection.Inward),
                    new Connection(15, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-17",
                Description = "Entrance to mountain access cave",
                World = 0,
                Index = 0x17,
                LocationValue = 0xE000173832,
                DefaultWarpValue = 0xE10AB6507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(13, Connection.Inward, Items.Bracelet),
                    new Connection(13, Connection.Outward, Items.Bracelet),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-18-1",
                Description = "Left exit from access cave (chest)",
                World = 0,
                Index = 0x18,
                LocationValue = 0xE000186812,
                DefaultWarpValue = 0xE10ABB507C,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW1-18-2",
                Description = "Right exit from access cave",
                World = 0,
                Index = 0x18,
                LocationValue = 0xE000188812,
                DefaultWarpValue = 0xE10ABC307C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(16, Connection.Inward),
                    new Connection(16, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-19",
                Description = "Entrance to Papahl cave",
                World = 0,
                Index = 0x19,
                LocationValue = 0xE000198840,
                DefaultWarpValue = 0xE10A89407C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(16, Connection.Inward),
                    new Connection(16, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-1D-1",
                Description = "Entrance to water cave to D7",
                World = 0,
                Index = 0x1D,
                LocationValue = 0xE0001D1830,
                DefaultWarpValue = 0xE10AF9207C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(16, Connection.Inward, Items.Flippers),
                    new Connection(17, Connection.Inward, Items.Flippers),
                    new Connection(16, Connection.Outward, Items.Flippers),
                    new Connection(17, Connection.Outward, Items.Flippers),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-1D-2",
                Description = "Exit from water cave to D7",
                World = 0,
                Index = 0x1D,
                LocationValue = 0xE0001D7850,
                DefaultWarpValue = 0xE10AFA707C,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-1E-1", Connection.Inward),
                    new Connection("OW1-1E-1", Connection.Outward),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(19, Connection.Inward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-1E-1",
                Description = "Entrance to loop cave (outer)",
                World = 0,
                Index = 0x1E,
                LocationValue = 0xE0001E3810,
                DefaultWarpValue = 0xE10A80207C,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-1D-2", Connection.Inward),
                    new Connection("OW1-1D-2", Connection.Outward),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(19, Connection.Inward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-1E-2",
                Description = "Entrance to loop cave (inner)",
                World = 0,
                Index = 0x1E,
                LocationValue = 0xE0001E7810,
                DefaultWarpValue = 0xE10A83807C,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-1F-1", Connection.Inward),
                    new Connection("OW1-1F-1", Connection.Outward),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(19, Connection.Inward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-1F-1",
                Description = "Exit from loop cave (outer)",
                World = 0,
                Index = 0x1F,
                LocationValue = 0xE0001F2810,
                DefaultWarpValue = 0xE10A82707C,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-1E-2", Connection.Inward),
                    new Connection("OW1-1E-2", Connection.Outward),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(19, Connection.Inward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-1F-2",
                Description = "Entrance to D7 cave",
                World = 0,
                Index = 0x1F,
                LocationValue = 0xE0001F7810,
                DefaultWarpValue = 0xE10A8C607C,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-1F-3", Connection.Inward),
                    new Connection("OW1-1F-4", Connection.Inward),
                    new Connection("OW1-1F-3", Connection.Outward),
                    new Connection("OW1-1F-4", Connection.Outward, Items.Bombs),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW1-1F-3",
                Description = "Exit from loop cave (inner)",
                World = 0,
                Index = 0x1F,
                LocationValue = 0xE0001F5840,
                DefaultWarpValue = 0xE10A87607C,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-1F-2", Connection.Inward),
                    new Connection("OW1-1F-4", Connection.Inward),
                    new Connection("OW1-1F-3", Connection.Outward),
                    new Connection("OW1-1F-4", Connection.Outward, Items.Bombs),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW1-1F-4",
                Description = "Fairy fountain (bombable wall)",
                World = 0,
                Index = 0x1F,
                LocationValue = 0xE0001F3850,
                DefaultWarpValue = 0xE11FFB507C,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-1F-2", Connection.Inward, Items.Bombs),
                    new Connection("OW1-1F-3", Connection.Inward, Items.Bombs),
                    new Connection("OW1-1F-2", Connection.Outward),
                    new Connection("OW1-1F-3", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW1-20",
                Description = "Left entrance to cave below D8",
                World = 0,
                Index = 0x20,
                LocationValue = 0xE000208832,
                DefaultWarpValue = 0xE111AE507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(2, Connection.Inward),
                    new Connection(2, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-21",
                Description = "Right entrance to cave below D8",
                World = 0,
                Index = 0x21,
                LocationValue = 0xE000211832,
                DefaultWarpValue = 0xE111AF507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(2, Connection.Inward),
                    new Connection(2, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-24",
                Description = "Entrance to D2",
                World = 0,
                Index = 0x24,
                LocationValue = 0xE000243822,
                DefaultWarpValue = 0xE10136507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(11, Connection.Inward, MiscFlags.BowWow),
                    new Connection(11, Connection.Inward, Items.Bracelet),
                    new Connection(11, Connection.Inward, Items.MagicRod),
                    new Connection(11, Connection.Inward, Items.Hookshot),
                    new Connection(11, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-2B-1",
                Description = "Entrance to D4",
                World = 0,
                Index = 0x2B,
                LocationValue = 0xE0002B4822,
                DefaultWarpValue = 0xE1037A507C,
                Locked = true,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-2B-2", Connection.Inward, MiscFlags.Waterfall),
                    new Connection("OW1-2B-2", Connection.Outward, MiscFlags.Waterfall),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(10, Connection.Inward, Items.Flippers, MiscFlags.Waterfall),
                    new Connection(12, Connection.Inward, Items.Flippers, MiscFlags.Waterfall),
                    new Connection(16, Connection.Inward, MiscFlags.Waterfall),
                    new Connection(12, Connection.Outward, Items.Flippers),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-2B-2",
                Description = "Entrance to D4 cave",
                World = 0,
                Index = 0x2B,
                LocationValue = 0xE0002B6830,
                DefaultWarpValue = 0xE11FE92820,
                Locked = true,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-2B-1", Connection.Inward, MiscFlags.Waterfall),
                    new Connection("OW1-2B-1", Connection.Outward, MiscFlags.Waterfall),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(10, Connection.Inward, Items.Flippers, MiscFlags.Waterfall),
                    new Connection(12, Connection.Inward, Items.Flippers, MiscFlags.Waterfall),
                    new Connection(16, Connection.Inward, MiscFlags.Waterfall),
                    new Connection(12, Connection.Outward, Items.Flippers),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-2D",
                Description = "Exit from D4 cave",
                World = 0,
                Index = 0x2D,
                LocationValue = 0xE0002D5850,
                DefaultWarpValue = 0xE11FEA8870,
                ZoneConnections = new ConnectionList
                {
                    new Connection(12, Connection.Inward),
                    new Connection(12, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-2F",
                Description = "Exit from raft cave",
                World = 0,
                Index = 0x2F,
                LocationValue = 0xE0002F1870,
                DefaultWarpValue = 0xE11FE74810,
                ZoneConnections = new ConnectionList
                {
                    new Connection(10, Connection.Inward),
                    new Connection(10, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-30",
                Description = "Mr. Write's house",
                World = 0,
                Index = 0x30,
                LocationValue = 0xE000307832,
                DefaultWarpValue = 0xE110A8507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(2, Connection.Inward),
                    new Connection(2, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-31",
                Description = "Telephone booth (swamp)",
                World = 0,
                Index = 0x31,
                LocationValue = 0xE000316852,
                DefaultWarpValue = 0xE1109B507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(11, Connection.Inward),
                    new Connection(11, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-35",
                Description = "Moblin hideout",
                World = 0,
                Index = 0x35,
                LocationValue = 0xE000356850,
                DefaultWarpValue = 0xE115F0507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(3, Connection.Inward),
                    new Connection(3, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-37",
                Description = "Photo gallery",
                World = 0,
                Index = 0x37,
                LocationValue = 0xE000374842,
                DefaultWarpValue = 0xE110B5507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(3, Connection.Inward),
                    new Connection(3, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-3F",
                Description = "Raft shop",
                World = 0,
                Index = 0x3F,
                LocationValue = 0xE0003F2822,
                DefaultWarpValue = 0xE110B0507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(10, Connection.Inward),
                    new Connection(10, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-42",
                Description = "Mysterious woods hookshot cave",
                World = 0,
                Index = 0x42,
                LocationValue = 0xE000423842,
                DefaultWarpValue = 0xE111B3507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward, Items.Bracelet),
                    new Connection(1, Connection.Outward, Items.Bracelet),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-45",
                Description = "Crazy Tracy's house",
                World = 0,
                Index = 0x45,
                LocationValue = 0xE000458842,
                DefaultWarpValue = 0xE10EAD507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward, Items.Bracelet),
                    new Connection(3, Connection.Inward, Items.Bracelet),
                    new Connection(1, Connection.Outward, Items.Bracelet),
                    new Connection(3, Connection.Outward, Items.Bracelet),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-49",
                Description = "Exit from Kanalet cave",
                World = 0,
                Index = 0x49,
                LocationValue = 0xE000496850,
                DefaultWarpValue = 0xE21FEB1830,
                ZoneConnections = new ConnectionList
                {
                    new Connection(8, Connection.Inward),
                    new Connection(8, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-4A",
                Description = "Entrance to Kanalet cave",
                World = 0,
                Index = 0x4A,
                LocationValue = 0xE0004A8830,
                DefaultWarpValue = 0xE21FEC6830,
                ZoneConnections = new ConnectionList
                {
                    new Connection(9, Connection.Inward),
                    new Connection(9, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-4B",
                Description = "Telephone booth (Kanalet)",
                World = 0,
                Index = 0x4B,
                LocationValue = 0xE0004B4822,
                DefaultWarpValue = 0xE110CC507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(9, Connection.Inward),
                    new Connection(9, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-50",
                Description = "Exit from log cave",
                World = 0,
                Index = 0x50,
                LocationValue = 0xE000508832,
                DefaultWarpValue = 0xE10AAB507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(11, Connection.Inward, Items.Feather),
                    new Connection(11, Connection.Outward, Items.Feather),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-52",
                Description = "Upgrade shrine (woods)",
                World = 0,
                Index = 0x52,
                LocationValue = 0xE000526830,
                DefaultWarpValue = 0xE11FE18850,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward, Items.Bracelet),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-59-1",
                Description = "Left Kanalet roof door",
                World = 0,
                Index = 0x59,
                LocationValue = 0xE000591830,
                DefaultWarpValue = 0xE114D5507C,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-59-2", Connection.Inward),
                    new Connection("OW1-59-2", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW1-59-2",
                Description = "Right Kanalet roof door",
                World = 0,
                Index = 0x59,
                LocationValue = 0xE000595840,
                DefaultWarpValue = 0xE114D6507C,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-59-1", Connection.Inward),
                    new Connection("OW1-59-1", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW1-62",
                Description = "Entrance to log cave",
                World = 0,
                Index = 0x62,
                LocationValue = 0xE000627842,
                DefaultWarpValue = 0xE10ABD507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-65",
                Description = "Witch's hut",
                World = 0,
                Index = 0x65,
                LocationValue = 0xE000654832,
                DefaultWarpValue = 0xE10EA2507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-69",
                Description = "Kanalet entrance",
                World = 0,
                Index = 0x69,
                LocationValue = 0xE000695840,
                DefaultWarpValue = 0xE114D3507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(8, Connection.Inward),
                    new Connection(8, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-6C",
                Description = "Stairs from raft ride to D6",
                World = 0,
                Index = 0x6C,
                LocationValue = 0xE0006C4840,
                DefaultWarpValue = 0xE105B07810,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW1-75",
                Description = "Entrance to graveyard cave",
                World = 0,
                Index = 0x75,
                LocationValue = 0xE000753840,
                DefaultWarpValue = 0xE10ADE3840,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward, Items.Bracelet),
                    new Connection(1, Connection.Outward, Items.Bracelet),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-76",
                Description = "Exit from graveyard cave",
                World = 0,
                Index = 0x76,
                LocationValue = 0xE000766850,
                DefaultWarpValue = 0xE10ADF3830,
                ZoneConnections = new ConnectionList
                {
                    new Connection(3, Connection.Inward),
                    new Connection(3, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-77",
                Description = "Entrance to D0",
                World = 0,
                Index = 0x77,
                LocationValue = 0xE00077782E,
                DefaultWarpValue = 0xE1FF12505C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(3, Connection.Inward, Items.Bracelet),
                    new Connection(3, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-78",
                Description = "Kanalet moat stairs",
                World = 0,
                Index = 0x78,
                LocationValue = 0xE000782870,
                DefaultWarpValue = 0xE11FFD5850,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward, MiscFlags.Rooster),
                    new Connection(4, Connection.Outward, MiscFlags.Rooster),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-82-1",
                Description = "Left door to Papahl's house",
                World = 0,
                Index = 0x82,
                LocationValue = 0xE000825852,
                DefaultWarpValue = 0xE110A5507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-82-2",
                Description = "Right door to Papahl's house",
                World = 0,
                Index = 0x82,
                LocationValue = 0xE000827852,
                DefaultWarpValue = 0xE110A6507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-83",
                Description = "Dream shrine",
                World = 0,
                Index = 0x83,
                LocationValue = 0xE000832842,
                DefaultWarpValue = 0xE113AA507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward, Items.Bracelet),
                    new Connection(1, Connection.Outward, Items.Bracelet),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-84",
                Description = "Cave beside Mabe Village",
                World = 0,
                Index = 0x84,
                LocationValue = 0xE000849862,
                DefaultWarpValue = 0xE111CD507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-86",
                Description = "Crystal cave before D3 (bombable wall)",
                World = 0,
                Index = 0x86,
                LocationValue = 0xE000861840,
                DefaultWarpValue = 0xE111F4407C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward, Items.Bombs),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-87",
                Description = "Fairy fountain at honey tree (bombable wall)",
                World = 0,
                Index = 0x87,
                LocationValue = 0xE000872810,
                DefaultWarpValue = 0xE11FF3507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward, Items.Bombs),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-88",
                Description = "Telephone booth (Ukuku Prairie)",
                World = 0,
                Index = 0x88,
                LocationValue = 0xE000885852,
                DefaultWarpValue = 0xE1109C507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-8A",
                Description = "Seashell mansion",
                World = 0,
                Index = 0x8A,
                LocationValue = 0xE0008A5840,
                DefaultWarpValue = 0xE210E90870,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-8C",
                Description = "Entrance to D6",
                World = 0,
                Index = 0x8C,
                LocationValue = 0xE0008C3840,
                DefaultWarpValue = 0xE105D4507C,
                Locked = true,
                ZoneConnections = new ConnectionList
                {
                    new Connection(20, Connection.Inward, Keys.FaceKey),
                    new Connection(20, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-8D",
                Description = "Fairy fountain outside D6 (bombable wall)",
                World = 0,
                Index = 0x8D,
                LocationValue = 0xE0008D3820,
                DefaultWarpValue = 0xE11FAC507C,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-8F", Connection.Inward, Items.Flippers | Items.Bombs),
                    new Connection("OW1-9D", Connection.Inward, Items.Flippers | Items.Bombs),
                    new Connection("OW1-D9-1", Connection.Inward, Items.Flippers | Items.Bombs),
                    new Connection("OW1-8F", Connection.Outward, Items.Flippers),
                    new Connection("OW1-9D", Connection.Outward, Items.Flippers),
                    new Connection("OW1-D9-1", Connection.Outward, Items.Flippers),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward, Items.Flippers | Items.Bombs),
                    new Connection(5, Connection.Inward, Items.Flippers | Items.Bombs),
                    new Connection(6, Connection.Inward, Items.Flippers | Items.Bombs),
                    new Connection(6, Connection.Inward, Items.Bracelet | Items.Bombs),
                    new Connection(7, Connection.Inward, Items.Bracelet | Items.Bombs),
                    new Connection(9, Connection.Inward, Items.Flippers | Items.Bombs),
                    new Connection(12, Connection.Inward, Items.Flippers | Items.Bombs),
                    new Connection(20, Connection.Inward, Items.Flippers | Items.Bombs),
                    new Connection(4, Connection.Outward, Items.Flippers),
                    new Connection(5, Connection.Outward, Items.Flippers),
                    new Connection(6, Connection.Outward, Items.Flippers),
                    new Connection(6, Connection.Outward, Items.Bracelet),
                    new Connection(7, Connection.Outward, Items.Bracelet),
                    new Connection(9, Connection.Outward, Items.Flippers),
                    new Connection(12, Connection.Outward, Items.Flippers),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-8F",
                Description = "Entrance to raft cave",
                World = 0,
                Index = 0x8F,
                LocationValue = 0xE0008F0820,
                DefaultWarpValue = 0xE11FF78860,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-8D", Connection.Inward, Items.Flippers),
                    new Connection("OW1-9D", Connection.Inward, Items.Flippers),
                    new Connection("OW1-D9-1", Connection.Inward, Items.Flippers),
                    new Connection("OW1-8D", Connection.Outward, Items.Flippers | Items.Bombs),
                    new Connection("OW1-9D", Connection.Outward, Items.Flippers),
                    new Connection("OW1-D9-1", Connection.Outward, Items.Flippers),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward, Items.Flippers),
                    new Connection(5, Connection.Inward, Items.Flippers),
                    new Connection(6, Connection.Inward, Items.Flippers),
                    new Connection(7, Connection.Inward, Items.Flippers | Items.Bracelet),
                    new Connection(9, Connection.Inward, Items.Flippers),
                    new Connection(12, Connection.Inward, Items.Flippers),
                    new Connection(20, Connection.Inward, Items.Flippers),
                    new Connection(4, Connection.Outward, Items.Flippers),
                    new Connection(5, Connection.Outward, Items.Flippers),
                    new Connection(6, Connection.Outward, Items.Flippers),
                    new Connection(7, Connection.Outward, Items.Flippers | Items.Bracelet),
                    new Connection(9, Connection.Outward, Items.Flippers),
                    new Connection(12, Connection.Outward, Items.Flippers),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-92",
                Description = "Rooster's grave",
                World = 0,
                Index = 0x92,
                LocationValue = 0xE000925852,
                DefaultWarpValue = 0xE11FF45870,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward, Items.L2Bracelet),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-93",
                Description = "Shop",
                World = 0,
                Index = 0x93,
                LocationValue = 0xE000934862,
                DefaultWarpValue = 0xE10EA1507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-9C",
                Description = "Exit from D6 cave",
                World = 0,
                Index = 0x9C,
                LocationValue = 0xE0009C5810,
                DefaultWarpValue = 0xE11FF03810,
                ZoneConnections = new ConnectionList
                {
                    new Connection(20, Connection.Inward),
                    new Connection(20, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-9D",
                Description = "Entrance to D6 cave",
                World = 0,
                Index = 0x9D,
                LocationValue = 0xE0009D3830,
                DefaultWarpValue = 0xE11FF18860,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-8D", Connection.Inward, Items.Flippers),
                    new Connection("OW1-8F", Connection.Inward, Items.Flippers),
                    new Connection("OW1-D9-1", Connection.Inward, Items.Flippers),
                    new Connection("OW1-8D", Connection.Outward, Items.Flippers | Items.Bombs),
                    new Connection("OW1-8F", Connection.Outward, Items.Flippers),
                    new Connection("OW1-D9-1", Connection.Outward, Items.Flippers),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward, Items.Flippers),
                    new Connection(5, Connection.Inward, Items.Flippers),
                    new Connection(6, Connection.Inward, Items.Flippers),
                    new Connection(7, Connection.Inward, Items.Flippers | Items.Bracelet),
                    new Connection(9, Connection.Inward, Items.Flippers),
                    new Connection(12, Connection.Inward, Items.Flippers),
                    new Connection(20, Connection.Inward, Items.Flippers),
                    new Connection(4, Connection.Outward, Items.Flippers),
                    new Connection(5, Connection.Outward, Items.Flippers),
                    new Connection(6, Connection.Outward, Items.Flippers),
                    new Connection(7, Connection.Outward, Items.Flippers | Items.Bracelet),
                    new Connection(9, Connection.Outward, Items.Flippers),
                    new Connection(12, Connection.Outward, Items.Flippers),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-A1-1",
                Description = "Doghouse",
                World = 0,
                Index = 0xA1,
                LocationValue = 0xE000A15842,
                DefaultWarpValue = 0xE112B2507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-A1-2",
                Description = "Madame Meow Meow's house",
                World = 0,
                Index = 0xA1,
                LocationValue = 0xE000A13842,
                DefaultWarpValue = 0xE110A7507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-A2",
                Description = "Marin & Tarin's house",
                World = 0,
                Index = 0xA2,
                LocationValue = 0xE000A25852,
                DefaultWarpValue = 0xE110A3507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-A4",
                Description = "Telephone booth (signpost maze)",
                World = 0,
                Index = 0xA4,
                LocationValue = 0xE000A43842,
                DefaultWarpValue = 0xE110B4507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-AA",
                Description = "Entrance to animal village cave",
                World = 0,
                Index = 0xAA,
                LocationValue = 0xE000AA8840,
                DefaultWarpValue = 0xE111D02840,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-AB",
                Description = "Exit from animal village cave",
                World = 0,
                Index = 0xAB,
                LocationValue = 0xE000AB7850,
                DefaultWarpValue = 0xE111D17840,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward),
                    new Connection(6, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-AC",
                Description = "Southern face shrine",
                World = 0,
                Index = 0xAC,
                LocationValue = 0xE000AC5840,
                DefaultWarpValue = 0xE1168F507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(7, Connection.Inward),
                    new Connection(7, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-AE",
                Description = "Armos maze secret seashell cave",
                World = 0,
                Index = 0xAE,
                LocationValue = 0xE000AE4870,
                DefaultWarpValue = 0xE111FC6860,
                ZoneConnections = new ConnectionList
                {
                    new Connection(7, Connection.Inward),
                    new Connection(7, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-B0",
                Description = "Library",
                World = 0,
                Index = 0xB0,
                LocationValue = 0xE000B03832,
                DefaultWarpValue = 0xE11DFA507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-B1",
                Description = "Ulrira's house",
                World = 0,
                Index = 0xB1,
                LocationValue = 0xE000B14862,
                DefaultWarpValue = 0xE110A9507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-B2",
                Description = "Telephone booth (Mabe Village)",
                World = 0,
                Index = 0xB2,
                LocationValue = 0xE000B25852,
                DefaultWarpValue = 0xE110CB507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-B3",
                Description = "Trendy game",
                World = 0,
                Index = 0xB3,
                LocationValue = 0xE000B35852,
                DefaultWarpValue = 0xE10FA0507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-B5",
                Description = "Entrance to D3",
                World = 0,
                Index = 0xB5,
                LocationValue = 0xE000B56820,
                DefaultWarpValue = 0xE10252507C,
                Locked = true,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward, Items.Feather, Keys.SlimeKey),
                    new Connection(4, Connection.Inward, Items.Flippers, Keys.SlimeKey),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-B8-1",
                Description = "Hidden exit (top) from moblin maze cave",
                World = 0,
                Index = 0xB8,
                LocationValue = 0xE000B85830,
                DefaultWarpValue = 0xE10A95707C,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW1-B8-2",
                Description = "Entrance to moblin maze cave",
                World = 0,
                Index = 0xB8,
                LocationValue = 0xE000B87860,
                DefaultWarpValue = 0xE10A92307C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-C6",
                Description = "Exit from villa cave",
                World = 0,
                Index = 0xC6,
                LocationValue = 0xE000C63850,
                DefaultWarpValue = 0xE111C9807C,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW1-C8",
                Description = "Exit from moblin maze cave",
                World = 0,
                Index = 0xC8,
                LocationValue = 0xE000C82850,
                DefaultWarpValue = 0xE10A93307C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(5, Connection.Inward),
                    new Connection(5, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-CC-1",
                Description = "Large house (Animal Village)",
                World = 0,
                Index = 0xCC,
                LocationValue = 0xE000CC2850,
                DefaultWarpValue = 0xE110DB507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward),
                    new Connection(6, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-CC-2",
                Description = "Painter's house (Animal Village)",
                World = 0,
                Index = 0xCC,
                LocationValue = 0xE000CC7850,
                DefaultWarpValue = 0xE110DD507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward),
                    new Connection(6, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-CD-1",
                Description = "HP cave behind animal village",
                World = 0,
                Index = 0xCD,
                LocationValue = 0xE000CD8820,
                DefaultWarpValue = 0xE10AF7607C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward, Items.Bombs),
                    new Connection(6, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-CD-2",
                Description = "Goat's house (Animal Village)",
                World = 0,
                Index = 0xCD,
                LocationValue = 0xE000CD2850,
                DefaultWarpValue = 0xE110D9507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward),
                    new Connection(6, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-CD-3",
                Description = "Zora's house (Animal Village)",
                World = 0,
                Index = 0xCD,
                LocationValue = 0xE000CD5850,
                DefaultWarpValue = 0xE110DA507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward),
                    new Connection(6, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-CF",
                Description = "Exit from Lanmolas cave",
                World = 0,
                Index = 0xCF,
                LocationValue = 0xE000CF5810,
                DefaultWarpValue = 0xE11FF97860,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward, Items.Bombs),
                    new Connection(6, Connection.Outward, Items.Bombs),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-D3",
                Description = "Entrance to D1",
                World = 0,
                Index = 0xD3,
                LocationValue = 0xE000D36822,
                DefaultWarpValue = 0xE10017507C,
                Locked = true,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward, Keys.TailKey),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-D4",
                Description = "Mamu's cave",
                World = 0,
                Index = 0xD4,
                LocationValue = 0xE000D48830,
                DefaultWarpValue = 0xE111FB8870,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward, Items.Feather | Items.Boots | Items.Bracelet | Items.Hookshot),
                    new Connection(4, Connection.Outward, Items.Feather | Items.Boots),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-D6",
                Description = "Richard's villa",
                World = 0,
                Index = 0xD6,
                LocationValue = 0xE000D64850,
                DefaultWarpValue = 0xE110C7507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-D9-1",
                Description = "Entrance to D5",
                World = 0,
                Index = 0xD9,
                LocationValue = 0xE000D95840,
                DefaultWarpValue = 0xE104A1507C,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-8D", Connection.Inward, Items.Flippers),
                    new Connection("OW1-8F", Connection.Inward, Items.Flippers),
                    new Connection("OW1-9D", Connection.Inward, Items.Flippers),
                    new Connection("OW1-8D", Connection.Outward, Items.Flippers | Items.Bombs),
                    new Connection("OW1-8F", Connection.Outward, Items.Flippers),
                    new Connection("OW1-9D", Connection.Outward, Items.Flippers),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward, Items.Flippers),
                    new Connection(5, Connection.Inward, Items.Flippers),
                    new Connection(6, Connection.Inward, Items.Flippers),
                    new Connection(7, Connection.Inward, Items.Flippers | Items.Bracelet),
                    new Connection(9, Connection.Inward, Items.Flippers),
                    new Connection(12, Connection.Inward, Items.Flippers),
                    new Connection(20, Connection.Inward, Items.Flippers),
                    new Connection(4, Connection.Outward, Items.Flippers),
                    new Connection(5, Connection.Outward, Items.Flippers),
                    new Connection(6, Connection.Outward, Items.Flippers),
                    new Connection(7, Connection.Outward, Items.Flippers | Items.Bracelet),
                    new Connection(9, Connection.Outward, Items.Flippers),
                    new Connection(12, Connection.Outward, Items.Flippers),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-DB",
                Description = "Telephone booth (Animal Village)",
                World = 0,
                Index = 0xDB,
                LocationValue = 0xE000DB7852,
                DefaultWarpValue = 0xE110E3507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward),
                    new Connection(6, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-DD",
                Description = "Chef Bear's house (Animal Village)",
                World = 0,
                Index = 0xDD,
                LocationValue = 0xE000DD5842,
                DefaultWarpValue = 0xE110D7507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward),
                    new Connection(6, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-E3",
                Description = "Crocodile's house",
                World = 0,
                Index = 0xE3,
                LocationValue = 0xE000E34830,
                DefaultWarpValue = 0xE110FE507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-E6",
                Description = "Upgrade shrine (Martha's Bay)",
                World = 0,
                Index = 0xE6,
                LocationValue = 0xE000E64840,
                DefaultWarpValue = 0xE11FE08870,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-E7", Connection.Inward),
                    new Connection("OW1-E7", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW1-E7",
                Description = "Exit from cave to upgrade shrine (Martha's Bay)",
                World = 0,
                Index = 0xE7,
                LocationValue = 0xE000E76820,
                DefaultWarpValue = 0xE11FE52830,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW1-E6", Connection.Inward),
                    new Connection("OW1-E6", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW1-E8",
                Description = "Telephone booth (Martha's Bay)",
                World = 0,
                Index = 0xE8,
                LocationValue = 0xE000E83862,
                DefaultWarpValue = 0xE1109D507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(5, Connection.Inward),
                    new Connection(5, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-E9",
                Description = "Magnifying lens cave (mermaid statue)",
                World = 0,
                Index = 0xE9,
                LocationValue = 0xE000E96830,
                DefaultWarpValue = 0xE10A986860,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward, Items.Hookshot, MiscFlags.MermaidScale),
                    new Connection(6, Connection.Outward, Items.Hookshot),
                    new Connection(6, Connection.Outward, Items.Flippers),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-F4",
                Description = "Boomerang moblin's cave",
                World = 0,
                Index = 0xF4,
                LocationValue = 0xE000F41820,
                DefaultWarpValue = 0xE11FF5487C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward, Items.Bombs),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-F6",
                Description = "House by the bay",
                World = 0,
                Index = 0xF6,
                LocationValue = 0xE000F65842,
                DefaultWarpValue = 0xE11EE3507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward, Items.Bracelet),
                    new Connection(5, Connection.Inward, Items.Feather | Items.Boots),
                    new Connection(1, Connection.Outward, Items.Bracelet),
                    new Connection(5, Connection.Outward, Items.Feather | Items.Boots),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW1-F9",
                Description = "Entrance to cave to upgrade shrine (Martha's Bay)",
                World = 0,
                Index = 0xF9,
                LocationValue = 0xE000F97850,
                DefaultWarpValue = 0xE11FF68870,
                ZoneConnections = new ConnectionList
                {
                    new Connection(5, Connection.Inward, Items.Feather),
                    new Connection(5, Connection.Outward, Items.Feather),
                }
            });
            Add(new WarpData(this)
            {
                Code = "OW2-00",
                Description = "D8 - Staircase to OW 00",
                World = 2,
                Index = 0x3A,
                LocationValue = 0xE1073A5810,
                DefaultWarpValue = 0xE000004850,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-02", Connection.Inward, Items.Feather),
                    new Connection("OW2-10", Connection.Inward, Items.Feather | Items.Bombs | Items.Bow),
                    new Connection("OW2-10", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-02",
                Description = "D8 - Staircase to OW 02",
                World = 2,
                Index = 0x3D,
                LocationValue = 0xE1073D5810,
                DefaultWarpValue = 0xE000023850,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-00", Connection.Outward, Items.Feather),
                    new Connection("OW2-10", Connection.Outward, Items.Feather),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-03",
                Description = "Cave - Flame skip staircase",
                World = 1,
                Index = 0xEE,
                LocationValue = 0xE11FEE1840,
                DefaultWarpValue = 0xE000034850,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-13", Connection.Inward, Items.L2Shield),
                    new Connection("OW2-13", Connection.Outward, Items.L2Shield),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-04",
                Description = "Cave - Upgrade shrine (mountain)",
                World = 1,
                Index = 0xE2,
                LocationValue = 0xE11FE28850,
                DefaultWarpValue = 0xE000047870,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-06",
                Description = "Egg - Exit to mountain",
                World = 2,
                Index = 0x70,
                LocationValue = 0xE10870507C,
                DefaultWarpValue = 0xE000065840,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-07",
                Description = "Cave - Entrance to hookshot gap",
                World = 2,
                Index = 0xEE,
                LocationValue = 0xE10AEE7830,
                DefaultWarpValue = 0xE000073850,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-15", Connection.Outward, Items.Hookshot),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-0A-1",
                Description = "Cave - Exit for Papahl cave",
                World = 2,
                Index = 0x8B,
                LocationValue = 0xE10A8B507C,
                DefaultWarpValue = 0xE0000A1870,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-19", Connection.Inward),
                    new Connection("OW2-19", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-0A-2",
                Description = "Cave - Bird key cave entrance",
                World = 2,
                Index = 0x7E,
                LocationValue = 0xE10A7E607C,
                DefaultWarpValue = 0xE0000A7870,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-0A-3",
                Description = "House - Cucco house",
                World = 2,
                Index = 0x9F,
                LocationValue = 0xE1109F507C,
                DefaultWarpValue = 0xE0000A4822,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-0D",
                Description = "Cave - Exit from hidden part of water cave (before D7)",
                World = 2,
                Index = 0xF2,
                LocationValue = 0xE10AF2507C,
                DefaultWarpValue = 0xE0000D1870,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-1D-1", Connection.Inward, Items.Bombs),
                    new Connection("OW2-1D-2", Connection.Inward, Items.Bombs),
                    new Connection("OW2-1D-1", Connection.Outward),
                    new Connection("OW2-1D-2", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-0E",
                Description = "D7 - Entrance",
                World = 2,
                Index = 0x0E,
                Index2 = 0x2C,
                LocationValue = 0xE1060E507C,
                DefaultWarpValue = 0xE0000E5830,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-0F",
                Description = "Cave - Exit from D7 cave",
                World = 2,
                Index = 0x8E,
                LocationValue = 0xE10A8E707C,
                DefaultWarpValue = 0xE0000F4850,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-1F-2", Connection.Inward),
                    new Connection("OW2-1F-2", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-10",
                Description = "D8 - Entrance",
                World = 2,
                Index = 0x5D,
                Index2 = 0x30,
                LocationValue = 0xE1075D507C,
                DefaultWarpValue = 0xE000105810,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-00", Connection.Inward),
                    new Connection("OW2-02", Connection.Inward, Items.Feather),
                    new Connection("OW2-00", Connection.Outward, Items.Feather | Items.Bombs | Items.Bow),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-11",
                Description = "House - Telephone booth (turtle rock)",
                World = 2,
                Index = 0x99,
                LocationValue = 0xE11099507C,
                DefaultWarpValue = 0xE000116832,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-13",
                Description = "Cave - Entrance to flame skip",
                World = 1,
                Index = 0xFE,
                LocationValue = 0xE11FFE707C,
                DefaultWarpValue = 0xE000135810,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-03", Connection.Inward, Items.L2Shield),
                    new Connection("OW2-03", Connection.Outward, Items.L2Shield),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-15",
                Description = "Cave - Exit from hookshot gap",
                World = 2,
                Index = 0xEA,
                LocationValue = 0xE10AEA507C,
                DefaultWarpValue = 0xE000158840,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-07", Connection.Inward, Items.Hookshot),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-17",
                Description = "Cave - Entrance to mountain access cave",
                World = 2,
                Index = 0xB6,
                LocationValue = 0xE10AB6507C,
                DefaultWarpValue = 0xE000173832,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-18-1", Connection.Inward),
                    new Connection("OW2-18-2", Connection.Inward, Items.Boots),
                    new Connection("OW2-18-1", Connection.Outward),
                    new Connection("OW2-18-2", Connection.Outward, Items.Boots),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-18-1",
                Description = "Cave - Left exit from access cave (chest)",
                World = 2,
                Index = 0xBB,
                LocationValue = 0xE10ABB507C,
                DefaultWarpValue = 0xE000186812,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-17", Connection.Inward),
                    new Connection("OW2-18-2", Connection.Inward, Items.Boots),
                    new Connection("OW2-17", Connection.Outward),
                    new Connection("OW2-18-2", Connection.Outward, Items.Boots),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-18-2",
                Description = "Cave - Right exit from access cave",
                World = 2,
                Index = 0xBC,
                LocationValue = 0xE10ABC307C,
                DefaultWarpValue = 0xE000188812,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-17", Connection.Inward, Items.Boots),
                    new Connection("OW2-18-1", Connection.Inward, Items.Boots),
                    new Connection("OW2-17", Connection.Outward, Items.Boots),
                    new Connection("OW2-18-1", Connection.Outward, Items.Boots),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-19",
                Description = "Cave - Entrance to Papahl cave",
                World = 2,
                Index = 0x89,
                LocationValue = 0xE10A89407C,
                DefaultWarpValue = 0xE000198840,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-0A-1", Connection.Inward),
                    new Connection("OW2-0A-1", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-1D-1",
                Description = "Cave - Entrance to water cave to D7",
                World = 2,
                Index = 0xF9,
                LocationValue = 0xE10AF9207C,
                DefaultWarpValue = 0xE0001D1830,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-1D-2", Connection.Inward),
                    new Connection("OW2-0D", Connection.Inward),
                    new Connection("OW2-1D-2", Connection.Outward),
                    new Connection("OW2-0D", Connection.Outward, Items.Bombs),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-1D-2",
                Description = "Cave - Exit from water cave to D7",
                World = 2,
                Index = 0xFA,
                LocationValue = 0xE10AFA707C,
                DefaultWarpValue = 0xE0001D7850,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-1D-1", Connection.Inward),
                    new Connection("OW2-0D", Connection.Inward),
                    new Connection("OW2-1D-1", Connection.Outward),
                    new Connection("OW2-0D", Connection.Outward, Items.Bombs),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-1E-1",
                Description = "Cave - Entrance to loop cave (outer)",
                World = 2,
                Index = 0x80,
                LocationValue = 0xE10A80207C,
                DefaultWarpValue = 0xE0001E3810,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-1F-1", Connection.Inward),
                    new Connection("OW2-1F-1", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-1E-2",
                Description = "Cave - Entrance to loop cave (inner)",
                World = 2,
                Index = 0x83,
                LocationValue = 0xE10A83807C,
                DefaultWarpValue = 0xE0001E7810,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-1F-3", Connection.Outward, Items.Feather | Items.Hookshot),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-1F-1",
                Description = "Cave - Exit from loop cave (outer)",
                World = 2,
                Index = 0x82,
                LocationValue = 0xE10A82707C,
                DefaultWarpValue = 0xE0001F2810,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-1E-1", Connection.Inward),
                    new Connection("OW2-1E-1", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-1F-2",
                Description = "Cave - Entrance to D7 cave",
                World = 2,
                Index = 0x8C,
                LocationValue = 0xE10A8C607C,
                DefaultWarpValue = 0xE0001F7810,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-0F", Connection.Inward),
                    new Connection("OW2-0F", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-1F-3",
                Description = "Cave - Exit from loop cave (inner)",
                World = 2,
                Index = 0x87,
                LocationValue = 0xE10A87607C,
                DefaultWarpValue = 0xE0001F5840,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-1E-2", Connection.Inward, Items.Feather | Items.Hookshot),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-1F-4",
                Description = "Cave - Fairy fountain before D7",
                World = 1,
                Index = 0xFB,
                LocationValue = 0xE11FFB507C,
                DefaultWarpValue = 0xE0001F3850,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-20",
                Description = "Cave - Left entrance to cave below D8",
                World = 2,
                Index = 0xAE,
                LocationValue = 0xE111AE507C,
                DefaultWarpValue = 0xE000208832,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-21", Connection.Inward),
                    new Connection("OW2-21", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-21",
                Description = "Cave - Right entrance to cave below D8",
                World = 2,
                Index = 0xAF,
                LocationValue = 0xE111AF507C,
                DefaultWarpValue = 0xE000211832,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-20", Connection.Inward),
                    new Connection("OW2-20", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-24",
                Description = "D2 - Entrance",
                World = 1,
                Index = 0x36,
                Index2 = 0x2A,
                LocationValue = 0xE10136507C,
                DefaultWarpValue = 0xE000243822,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-2B-1",
                Description = "D4 - Entrance",
                World = 1,
                Index = 0x7A,
                Index2 = 0x62,
                LocationValue = 0xE1037A507C,
                DefaultWarpValue = 0xE0002B4822,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-2B-2",
                Description = "Cave - Entrance to D4 cave",
                World = 1,
                Index = 0xE9,
                LocationValue = 0xE11FE92820,
                DefaultWarpValue = 0xE0002B6830,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-2D", Connection.Outward, Items.Feather),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-2D",
                Description = "Cave - Exit from D4 cave",
                World = 1,
                Index = 0xEA,
                LocationValue = 0xE11FEA8870,
                DefaultWarpValue = 0xE0002D5850,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-2B-2", Connection.Inward, Items.Feather),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-2F",
                Description = "Cave - Exit from raft cave",
                World = 1,
                Index = 0xE7,
                LocationValue = 0xE11FE74810,
                DefaultWarpValue = 0xE0002F1870,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-8F", Connection.Inward, Items.Flippers),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-30",
                Description = "House - Writer's house",
                World = 2,
                Index = 0xA8,
                LocationValue = 0xE110A8507C,
                DefaultWarpValue = 0xE000307832,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-31",
                Description = "House - Telephone booth (swamp)",
                World = 2,
                Index = 0x9B,
                LocationValue = 0xE1109B507C,
                DefaultWarpValue = 0xE000316852,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-35",
                Description = "Cave - Moblin hideout",
                World = 2,
                Index = 0xF0,
                LocationValue = 0xE115F0507C,
                DefaultWarpValue = 0xE000356850,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-37",
                Description = "House - Photo gallery",
                World = 2,
                Index = 0xB5,
                LocationValue = 0xE110B5507C,
                DefaultWarpValue = 0xE000374842,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-3F",
                Description = "House - Raft shop",
                World = 2,
                Index = 0xB0,
                LocationValue = 0xE110B0507C,
                DefaultWarpValue = 0xE0003F2822,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-42",
                Description = "Cave - Mysterious woods hookshot cave",
                World = 2,
                Index = 0xB3,
                LocationValue = 0xE111B3507C,
                DefaultWarpValue = 0xE000423842,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-45",
                Description = "House - Crazy Tracy's house",
                World = 2,
                Index = 0xAD,
                LocationValue = 0xE10EAD507C,
                DefaultWarpValue = 0xE000458842,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-49",
                Description = "SS - Exit from Kanalet cave",
                World = 1,
                Index = 0xEB,
                LocationValue = 0xE21FEB1830,
                DefaultWarpValue = 0xE000496850,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-4A", Connection.Inward, Items.Feather),
                    new Connection("OW2-4A", Connection.Outward, Items.Feather),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-4A",
                Description = "SS - Entrance to Kanalet cave",
                World = 1,
                Index = 0xEC,
                LocationValue = 0xE21FEC6830,
                DefaultWarpValue = 0xE0004A8830,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-49", Connection.Inward, Items.Feather),
                    new Connection("OW2-49", Connection.Outward, Items.Feather),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-4B",
                Description = "House - Telephone booth (Kanalet)",
                World = 2,
                Index = 0xCC,
                LocationValue = 0xE110CC507C,
                DefaultWarpValue = 0xE0004B4822,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-50",
                Description = "Cave - Exit from log cave",
                World = 2,
                Index = 0xAB,
                LocationValue = 0xE10AAB507C,
                DefaultWarpValue = 0xE000508832,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-62", Connection.Inward),
                    new Connection("OW2-62", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-52",
                Description = "Cave - Upgrade shrine (woods)",
                World = 1,
                Index = 0xE1,
                LocationValue = 0xE11FE18850,
                DefaultWarpValue = 0xE000526830,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-59-1",
                Description = "DK - Left Kanalet roof door",
                World = 2,
                Index = 0xD5,
                LocationValue = 0xE114D5507C,
                DefaultWarpValue = 0xE000591830,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-69", Connection.Inward),
                    new Connection("OW2-69", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-59-2",
                Description = "DK - Right Kanalet roof door",
                World = 2,
                Index = 0xD6,
                LocationValue = 0xE114D6507C,
                DefaultWarpValue = 0xE000595840,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-62",
                Description = "Cave - Entrance to log cave",
                World = 2,
                Index = 0xBD,
                LocationValue = 0xE10ABD507C,
                DefaultWarpValue = 0xE000627842,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-50", Connection.Inward),
                    new Connection("OW2-50", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-65",
                Description = "House - Witch's hut",
                World = 2,
                Index = 0xA2,
                LocationValue = 0xE10EA2507C,
                DefaultWarpValue = 0xE000654832,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-69",
                Description = "DK - Entrance",
                World = 2,
                Index = 0xD3,
                LocationValue = 0xE114D3507C,
                DefaultWarpValue = 0xE000695840,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-59-1", Connection.Inward),
                    new Connection("OW2-59-1", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-6C",
                Description = "D6 - Stairs to raft ride",
                World = 1,
                Index = 0xB0,
                LocationValue = 0xE105B07810,
                DefaultWarpValue = 0xE0006C4840,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-8C", Connection.Inward, Items.L2Bracelet | Items.Bombs),
                    new Connection("OW2-8C", Connection.Outward, Items.Bracelet | Items.Bombs),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-75",
                Description = "Cave - Entrance to graveyard cave",
                World = 2,
                Index = 0xDE,
                LocationValue = 0xE10ADE3840,
                DefaultWarpValue = 0xE000753840,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-76", Connection.Inward, Items.Feather),
                    new Connection("OW2-76", Connection.Outward, Items.Feather),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-76",
                Description = "Cave - Exit from graveyard cave",
                World = 2,
                Index = 0xDF,
                LocationValue = 0xE10ADF3830,
                DefaultWarpValue = 0xE000766850,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-75", Connection.Inward, Items.Feather),
                    new Connection("OW2-75", Connection.Outward, Items.Feather),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-77",
                Description = "D0 - Entrance",
                World = 3,
                Index = 0x12,
                Index2 = 0x01,
                LocationValue = 0xE1FF12505C,
                DefaultWarpValue = 0xE00077782E,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-78",
                Description = "Cave - Kanalet moat stairs",
                World = 1,
                Index = 0xFD,
                LocationValue = 0xE11FFD5850,
                DefaultWarpValue = 0xE000782870,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-82-1",
                Description = "House - Left side of Papahl's house",
                World = 2,
                Index = 0xA5,
                LocationValue = 0xE110A5507C,
                DefaultWarpValue = 0xE000825852,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-82-2", Connection.Inward),
                    new Connection("OW2-82-2", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-82-2",
                Description = "House - Right side of Papahl's house",
                World = 2,
                Index = 0xA6,
                LocationValue = 0xE110A6507C,
                DefaultWarpValue = 0xE000827852,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-82-1", Connection.Inward),
                    new Connection("OW2-82-1", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-83",
                Description = "DS - Dream shrine door",
                World = 2,
                Index = 0xAA,
                LocationValue = 0xE113AA507C,
                DefaultWarpValue = 0xE000832842,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-84",
                Description = "Cave - Cave beside Mabe Village",
                World = 2,
                Index = 0xCD,
                LocationValue = 0xE111CD507C,
                DefaultWarpValue = 0xE000849862,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-86",
                Description = "Cave - Crystal cave before D3",
                World = 2,
                Index = 0xF4,
                LocationValue = 0xE111F4407C,
                DefaultWarpValue = 0xE000861840,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-87",
                Description = "Cave - Fairy fountain at honey tree",
                World = 1,
                Index = 0xF3,
                LocationValue = 0xE11FF3507C,
                DefaultWarpValue = 0xE000872810,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-88",
                Description = "House - Telephone booth (Ukuku Prairie)",
                World = 2,
                Index = 0x9C,
                LocationValue = 0xE1109C507C,
                DefaultWarpValue = 0xE000885852,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-8A",
                Description = "SS - Seashell mansion",
                World = 2,
                Index = 0xE9,
                LocationValue = 0xE210E90870,
                DefaultWarpValue = 0xE0008A5840,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-8C",
                Description = "D6 - Entrance",
                World = 1,
                Index = 0xD4,
                Index2 = 0xB5,
                LocationValue = 0xE105D4507C,
                DefaultWarpValue = 0xE0008C3840,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-6C", Connection.Inward, Items.Bracelet | Items.Bombs),
                    new Connection("OW2-6C", Connection.Outward, Items.L2Bracelet | Items.Bombs),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-8D",
                Description = "Cave - Fairy fountain outside D6",
                World = 1,
                Index = 0xAC,
                LocationValue = 0xE11FAC507C,
                DefaultWarpValue = 0xE0008D3820,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-8F",
                Description = "Cave - Entrance to raft cave",
                World = 1,
                Index = 0xF7,
                LocationValue = 0xE11FF78860,
                DefaultWarpValue = 0xE0008F0820,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-2F", Connection.Outward, Items.Flippers),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-92",
                Description = "Cave - Rooster's grave",
                World = 1,
                Index = 0xF4,
                LocationValue = 0xE11FF45870,
                DefaultWarpValue = 0xE000925852,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-93",
                Description = "House - Shop",
                World = 2,
                Index = 0xA1,
                LocationValue = 0xE10EA1507C,
                DefaultWarpValue = 0xE000934862,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-9C",
                Description = "Cave - Exit from D6 cave",
                World = 1,
                Index = 0xF0,
                LocationValue = 0xE11FF03810,
                DefaultWarpValue = 0xE0009C5810,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-9D", Connection.Inward, Items.Feather | Items.Flippers),
                    new Connection("OW2-9D", Connection.Inward, Items.Feather | Items.Boots),
                    new Connection("OW2-9D", Connection.Inward, Items.Hookshot | Items.Flippers),
                    new Connection("OW2-9D", Connection.Outward, Items.Feather | Items.Flippers),
                    new Connection("OW2-9D", Connection.Outward, Items.Feather | Items.Boots),
                    new Connection("OW2-9D", Connection.Outward, Items.Hookshot | Items.Flippers),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-9D",
                Description = "Cave - Entrance to D6 cave",
                World = 1,
                Index = 0xF1,
                LocationValue = 0xE11FF18860,
                DefaultWarpValue = 0xE0009D3830,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-9C", Connection.Inward, Items.Feather | Items.Flippers),
                    new Connection("OW2-9C", Connection.Inward, Items.Feather | Items.Boots),
                    new Connection("OW2-9C", Connection.Inward, Items.Hookshot | Items.Flippers),
                    new Connection("OW2-9C", Connection.Outward, Items.Feather | Items.Flippers),
                    new Connection("OW2-9C", Connection.Outward, Items.Feather | Items.Boots),
                    new Connection("OW2-9C", Connection.Outward, Items.Hookshot | Items.Flippers),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-A1-1",
                Description = "House - Doghouse",
                World = 2,
                Index = 0xB2,
                LocationValue = 0xE112B2507C,
                DefaultWarpValue = 0xE000A15842,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-A1-2",
                Description = "House - Madame Meow Meow's house",
                World = 2,
                Index = 0xA7,
                LocationValue = 0xE110A7507C,
                DefaultWarpValue = 0xE000A13842,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-A2",
                Description = "House - Marin & Tarin's house",
                World = 2,
                Index = 0xA3,
                LocationValue = 0xE110A3507C,
                DefaultWarpValue = 0xE000A25852,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-A4",
                Description = "House - Telephone booth (signpost maze)",
                World = 2,
                Index = 0xB4,
                LocationValue = 0xE110B4507C,
                DefaultWarpValue = 0xE000A43842,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-AA",
                Description = "Cave - Entrance to animal village cave",
                World = 2,
                Index = 0xD0,
                LocationValue = 0xE111D02840,
                DefaultWarpValue = 0xE000AA8840,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-AB", Connection.Inward, Items.Boots),
                    new Connection("OW2-AB", Connection.Outward, Items.Boots),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-AB",
                Description = "Cave - Exit from animal village cave",
                World = 2,
                Index = 0xD1,
                LocationValue = 0xE111D17840,
                DefaultWarpValue = 0xE000AB7850,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-AA", Connection.Inward, Items.Boots),
                    new Connection("OW2-AA", Connection.Outward, Items.Boots),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-AC",
                Description = "D? - Southern face shrine",
                World = 2,
                Index = 0x8F,
                LocationValue = 0xE1168F507C,
                DefaultWarpValue = 0xE000AC5840,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-AE",
                Description = "Cave - Armos maze secret seashell cave",
                World = 2,
                Index = 0xFC,
                LocationValue = 0xE111FC6860,
                DefaultWarpValue = 0xE000AE4870,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-B0",
                Description = "House - Library",
                World = 1,
                Index = 0xFA,
                LocationValue = 0xE11DFA507C,
                DefaultWarpValue = 0xE000B03832,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-B1",
                Description = "House - Ulrira's house",
                World = 2,
                Index = 0xA9,
                LocationValue = 0xE110A9507C,
                DefaultWarpValue = 0xE000B14862,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-B2",
                Description = "House - Telephone booth (Mabe Village)",
                World = 2,
                Index = 0xCB,
                LocationValue = 0xE110CB507C,
                DefaultWarpValue = 0xE000B25852,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-B3",
                Description = "House - Trendy game",
                World = 2,
                Index = 0xA0,
                LocationValue = 0xE10FA0507C,
                DefaultWarpValue = 0xE000B35852,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-B5",
                Description = "D3 - Entrance",
                World = 1,
                Index = 0x52,
                Index2 = 0x59,
                LocationValue = 0xE10252507C,
                DefaultWarpValue = 0xE000B56820,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-B8-1",
                Description = "Cave - Hidden exit (top) from moblin maze cave",
                World = 2,
                Index = 0x95,
                LocationValue = 0xE10A95707C,
                DefaultWarpValue = 0xE000B85830,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-B8-2", Connection.Inward, Items.Feather | Items.Bombs),
                    new Connection("OW2-C8", Connection.Inward, Items.Feather | Items.Bombs),
                    new Connection("OW2-B8-2", Connection.Outward, Items.Feather),
                    new Connection("OW2-C8", Connection.Outward, Items.Feather),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-B8-2",
                Description = "Cave - Entrance to moblin maze cave",
                World = 2,
                Index = 0x92,
                LocationValue = 0xE10A92307C,
                DefaultWarpValue = 0xE000B87860,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-C8", Connection.Inward),
                    new Connection("OW2-B8-1", Connection.Inward, Items.Feather),
                    new Connection("OW2-C8", Connection.Outward),
                    new Connection("OW2-B8-1", Connection.Outward, Items.Feather | Items.Bombs),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-C6",
                Description = "Cave - Exit from villa cave",
                World = 2,
                Index = 0xC9,
                LocationValue = 0xE111C9807C,
                DefaultWarpValue = 0xE000C63850,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-D6", Connection.Inward, Keys.SlimeKey),
                    new Connection("OW2-D6", Connection.Inward, MiscFlags.GoldenLeaves),
                    new Connection("OW2-D6", Connection.Outward),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-C8",
                Description = "Cave - Exit from moblin maze cave",
                World = 2,
                Index = 0x93,
                LocationValue = 0xE10A93307C,
                DefaultWarpValue = 0xE000C82850,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-B8-2", Connection.Inward),
                    new Connection("OW2-B8-1", Connection.Inward, Items.Feather),
                    new Connection("OW2-B8-2", Connection.Outward),
                    new Connection("OW2-B8-1", Connection.Outward, Items.Feather | Items.Bombs),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-CC-1",
                Description = "House - Large house (Animal Village)",
                World = 2,
                Index = 0xDB,
                LocationValue = 0xE110DB507C,
                DefaultWarpValue = 0xE000CC2850,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-CC-2",
                Description = "House - Painter's house (Animal Village)",
                World = 2,
                Index = 0xDD,
                LocationValue = 0xE110DD507C,
                DefaultWarpValue = 0xE000CC7850,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-CD-1",
                Description = "Cave - HP cave behind animal village",
                World = 2,
                Index = 0xF7,
                LocationValue = 0xE10AF7607C,
                DefaultWarpValue = 0xE000CD8820,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-CD-2",
                Description = "House - Goat's house (Animal Village)",
                World = 2,
                Index = 0xD9,
                LocationValue = 0xE110D9507C,
                DefaultWarpValue = 0xE000CD2850,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-CD-3",
                Description = "House - Zora's house (Animal Village)",
                World = 2,
                Index = 0xDA,
                LocationValue = 0xE110DA507C,
                DefaultWarpValue = 0xE000CD5850,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-CF",
                Description = "Cave - Exit from Lanmolas cave",
                World = 1,
                Index = 0xF9,
                LocationValue = 0xE11FF97860,
                DefaultWarpValue = 0xE000CF5810,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-D3",
                Description = "D1 - Entrance",
                World = 1,
                Index = 0x17,
                Index2 = 0x02,
                LocationValue = 0xE10017507C,
                DefaultWarpValue = 0xE000D36822,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-D4",
                Description = "Cave - Mamu's cave",
                World = 2,
                Index = 0xFB,
                LocationValue = 0xE111FB8870,
                DefaultWarpValue = 0xE000D48830,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-D6",
                Description = "House - Richard's villa",
                World = 2,
                Index = 0xC7,
                LocationValue = 0xE110C7507C,
                DefaultWarpValue = 0xE000D64850,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-C6", Connection.Inward),
                    new Connection("OW2-C6", Connection.Outward, Keys.SlimeKey),
                    new Connection("OW2-C6", Connection.Outward, MiscFlags.GoldenLeaves),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-D9-1",
                Description = "D5 - Entrance",
                World = 1,
                Index = 0xA1,
                Index2 = 0x82,
                LocationValue = 0xE104A1507C,
                DefaultWarpValue = 0xE000D95840,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-DB",
                Description = "House - Telephone booth (Animal Village)",
                World = 2,
                Index = 0xE3,
                LocationValue = 0xE110E3507C,
                DefaultWarpValue = 0xE000DB7852,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-DD",
                Description = "House - Chef Bear's house (Animal Village)",
                World = 2,
                Index = 0xD7,
                LocationValue = 0xE110D7507C,
                DefaultWarpValue = 0xE000DD5842,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-E3",
                Description = "House - Crocodile's house",
                World = 2,
                Index = 0xFE,
                LocationValue = 0xE110FE507C,
                DefaultWarpValue = 0xE000E34830,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-E6",
                Description = "Cave - Upgrade shrine (Martha's Bay)",
                World = 1,
                Index = 0xE0,
                LocationValue = 0xE11FE08870,
                DefaultWarpValue = 0xE000E64840,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-E7",
                Description = "Cave - Exit from cave to upgrade shrine (Martha's Bay)",
                World = 1,
                Index = 0xE5,
                LocationValue = 0xE11FE52830,
                DefaultWarpValue = 0xE000E76820,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-F9", Connection.Inward, Items.Flippers),
                    new Connection("OW2-F9", Connection.Outward, Items.Flippers),
                },
            });
            Add(new WarpData(this)
            {
                Code = "OW2-E8",
                Description = "House - Telephone booth (Martha's Bay)",
                World = 2,
                Index = 0x9D,
                LocationValue = 0xE1109D507C,
                DefaultWarpValue = 0xE000E83862,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-E9",
                Description = "Cave - Magnifying lens cave (mermaid statue)",
                World = 2,
                Index = 0x98,
                LocationValue = 0xE10A986860,
                DefaultWarpValue = 0xE000E96830,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-F4",
                Description = "Cave - Boomerang moblin's cave",
                World = 1,
                Index = 0xF5,
                LocationValue = 0xE11FF5487C,
                DefaultWarpValue = 0xE000F41820,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-F6",
                Description = "House - House by the bay",
                World = 1,
                Index = 0xE3,
                LocationValue = 0xE11EE3507C,
                DefaultWarpValue = 0xE000F65842,
                DeadEnd = true,
            });
            Add(new WarpData(this)
            {
                Code = "OW2-F9",
                Description = "Cave - Entrance to cave to upgrade shrine (Martha's Bay)",
                World = 1,
                Index = 0xF6,
                LocationValue = 0xE11FF68870,
                DefaultWarpValue = 0xE000F97850,
                WarpConnections = new ConnectionList
                {
                    new Connection("OW2-E7", Connection.Inward, Items.Flippers),
                    new Connection("OW2-E7", Connection.Outward, Items.Flippers),
                },
            });
        }

        public WarpList Copy()
        {
            return new WarpList(this);
        }
    }

    public class WarpData
    {
        private WarpList parentList;

        public string Code { get; set; }
        public string Description { get; set; }
        public int World { get; set; }
        public int Index { get; set; }
        public int Index2 { get; set; }
        public long LocationValue { get; set; }
        public long DefaultWarpValue { get; set; }
        public long WarpValue { get; set; }
        public bool DeadEnd { get; set; } = false;
        public bool Locked { get; set; } = false;
        public ConnectionList WarpConnections { get; set; } = new ConnectionList();
        public ConnectionList ZoneConnections { get; set; } = new ConnectionList();

        public bool Exclude { get; set; } = false;

        public WarpData(WarpList parentList)
        {
            this.parentList = parentList;
        }

        public WarpData GetOriginWarp()
        {
            return parentList.Where(x => x.WarpValue == LocationValue).FirstOrDefault();
        }

        public WarpData GetDestinationWarp()
        {
            return parentList.Where(x => x.LocationValue == WarpValue).FirstOrDefault();
        }
    }
}

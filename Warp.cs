using LADXRandomizer.Pathfinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADXRandomizer
{
    public class WarpData : List<Warp>
    {
        public WarpData Overworld1 { get { return new WarpData(this.Where(x => x.Code.Contains("OW1")).ToList()); } }
        public WarpData Overworld2 { get { return new WarpData(this.Where(x => x.Code.Contains("OW2")).ToList()); } }

        public Warp this[string name]
        {
            get { return this.First(w => w.Code == name); }
        }

        public WarpData(List<Warp> list)
        {
            AddRange(list);
        }

        public WarpData(RandomizerSettings settings)
        {
            Initialize();

            if (settings["ExcludeMarinHouse"].Enabled)
            {
                this["OW1-A2"].Exclude = true;
                this["OW2-A2"].Exclude = true;
            }

            if (settings["ExcludeEgg"].Enabled)
            {
                this["OW1-06"].Exclude = true;
                this["OW2-06"].Exclude = true;
            }
        }

        private WarpData(WarpData data)
        {
            foreach (var warp in data)
            {
                Add(new Warp(this)
                {
                    Exclude = warp.Exclude,
                    Code = warp.Code,
                    Description = warp.Description,
                    Address = warp.Address,
                    Address2 = warp.Address2,
                    Location = warp.Location,
                    Default = warp.Default,
                    Destination = warp.Destination,
                    DeadEnd = warp.DeadEnd,
                    Locked = warp.Locked,
                    Special = warp.Special,
                    Connections = warp.Connections,         //not deep copies, but doesn't matter
                    ZoneConnections = warp.ZoneConnections, //
                });
            }
        }

        private void Initialize()
        {
            Add(new Warp(this)
            {
                Code = "OW1-00",
                Description = "D8 mountain top #1",
                Address = 0x24233,
                Location = 0xE000004850,
                Default = 0xE1073A5810,
                Connections = new ConnectionList
                {
                    new Connection("OW1-02", Connection.Inward),
                    new Connection("OW1-02", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW1-02",
                Description = "D8 mountain top #2 ",
                Address = 0x242ED,
                Location = 0xE000023850,
                Default = 0xE1073D5810,
                Connections = new ConnectionList
                {
                    new Connection("OW1-00", Connection.Inward),
                    new Connection("OW1-00", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW1-03",
                Description = "Exit from flame skip cave",
                Address = 0x24385,
                Location = 0xE000034850,
                Default = 0xE11FEE1840,
                Connections = new ConnectionList
                {
                    new Connection("OW1-10", Connection.Inward),
                    new Connection("OW1-11", Connection.Inward),
                    new Connection("OW1-10", Connection.Outward, Items.Bombs),
                    new Connection("OW1-11", Connection.Outward),
                    new Connection("OW1-13", Connection.Outward, Items.Bombs),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(2, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-04",
                Description = "Upgrade shrine (mountain)",
                Address = 0x243D2,
                Location = 0xE000047870,
                Default = 0xE11FE28850,
                Connections = new ConnectionList
                {
                    new Connection("OW1-15", Connection.Inward, Items.Bracelet),
                    new Connection("OW1-13", Connection.Outward, Items.Bombs),
                    new Connection("OW1-15", Connection.Outward),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(2, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-06",
                Description = "Wind Fish egg",
                Address = 0x24505,
                Location = 0xE000065840,
                Default = 0xE10870507C,
                Locked = true,
                ZoneConnections = new ConnectionList
                {
                    new Connection(13, Connection.Inward, Instruments.All),
                    new Connection(13, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-07",
                Description = "Entrance to hookshot gap",
                Address = 0x24538,
                Location = 0xE000073850,
                Default = 0xE10AEE7830,
                Connections = new ConnectionList
                {
                    new Connection("OW1-0A-2", Connection.Inward, Items.Hookshot),
                    new Connection("OW1-0A-3", Connection.Inward, Items.Hookshot),
                    new Connection("OW1-18-2", Connection.Inward, Items.Hookshot | Items.Flippers),
                    new Connection("OW1-19", Connection.Inward, Items.Hookshot | Items.Flippers),
                    new Connection("OW1-1D-1", Connection.Inward, Items.Hookshot | Items.Flippers),
                    new Connection("OW1-0A-2", Connection.Outward, Items.Hookshot),
                    new Connection("OW1-0A-3", Connection.Outward, Items.Hookshot),
                    new Connection("OW1-18-2", Connection.Outward, Items.Hookshot | Items.Flippers),
                    new Connection("OW1-19", Connection.Outward, Items.Hookshot | Items.Flippers),
                    new Connection("OW1-1D-1", Connection.Outward, Items.Hookshot | Items.Flippers),
                    new Connection("OW1-2B-1", Connection.Outward, Items.Hookshot | Items.Flippers, MiscFlags.Waterfall),
                    new Connection("OW1-2B-2", Connection.Outward, Items.Hookshot | Items.Flippers, MiscFlags.Waterfall),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(13, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-0A-1",
                Description = "Exit for Papahl cave",
                Address = 0x24634,
                Location = 0xE0000A1870,
                Default = 0xE10A8B507C,
                DeadEnd = true,
                Connections = new ConnectionList
                {
                    new Connection("OW1-07", Connection.Outward, Items.Flippers | Items.Hookshot),
                    new Connection("OW1-0A-2", Connection.Outward, Items.Flippers),
                    new Connection("OW1-0A-3", Connection.Outward, Items.Flippers),
                    new Connection("OW1-18-2", Connection.Outward),
                    new Connection("OW1-19", Connection.Outward),
                    new Connection("OW1-1D-1", Connection.Outward, Items.Flippers),
                    new Connection("OW1-2B-1", Connection.Outward, MiscFlags.Waterfall),
                    new Connection("OW1-2B-2", Connection.Outward, MiscFlags.Waterfall),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(13, Connection.Outward, Items.Flippers | Items.Hookshot),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-0A-2",
                Description = "Cucco house",
                Address = 0x24646,
                Location = 0xE0000A4822,
                Default = 0xE1109F507C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-07", Connection.Inward, Items.Hookshot),
                    new Connection("OW1-0A-3", Connection.Inward),
                    new Connection("OW1-18-2", Connection.Inward, Items.Flippers),
                    new Connection("OW1-19", Connection.Inward, Items.Flippers),
                    new Connection("OW1-1D-1", Connection.Inward, Items.Flippers),
                    new Connection("OW1-07", Connection.Outward, Items.Hookshot),
                    new Connection("OW1-0A-3", Connection.Outward),
                    new Connection("OW1-18-2", Connection.Outward, Items.Flippers),
                    new Connection("OW1-19", Connection.Outward, Items.Flippers),
                    new Connection("OW1-1D-1", Connection.Outward, Items.Flippers),
                    new Connection("OW1-2B-1", Connection.Outward, Items.Flippers, MiscFlags.Waterfall),
                    new Connection("OW1-2B-2", Connection.Outward, Items.Flippers, MiscFlags.Waterfall),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(13, Connection.Outward, Items.Hookshot),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-0A-3",
                Description = "Bird key cave",
                Address = 0x2463D,
                Location = 0xE0000A7870,
                Default = 0xE10A7E607C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-07", Connection.Inward, Items.Hookshot),
                    new Connection("OW1-0A-2", Connection.Inward),
                    new Connection("OW1-18-2", Connection.Inward, Items.Flippers),
                    new Connection("OW1-19", Connection.Inward, Items.Flippers),
                    new Connection("OW1-1D-1", Connection.Inward, Items.Flippers),
                    new Connection("OW1-07", Connection.Outward, Items.Hookshot),
                    new Connection("OW1-0A-2", Connection.Outward),
                    new Connection("OW1-18-2", Connection.Outward, Items.Flippers),
                    new Connection("OW1-19", Connection.Outward, Items.Flippers),
                    new Connection("OW1-1D-1", Connection.Outward, Items.Flippers),
                    new Connection("OW1-2B-1", Connection.Outward, Items.Flippers, MiscFlags.Waterfall),
                    new Connection("OW1-2B-2", Connection.Outward, Items.Flippers, MiscFlags.Waterfall),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(13, Connection.Outward, Items.Hookshot),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-0D",
                Description = "Exit from hidden part of water cave (before D7)",
                Address = 0x24785,
                Location = 0xE0000D1870,
                Default = 0xE10AF2507C,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW1-0E",
                Description = "Entrance to D7",
                Address = 0x2484A,
                Address2 = 0x247E6,
                Location = 0xE0000E5830,
                Default = 0xE1060E507C,
                Locked = true,
                Connections = new ConnectionList
                {
                    new Connection("OW1-0F", Connection.Inward, Keys.BirdKey),
                    new Connection("OW1-0F", Connection.Outward),
                    new Connection("OW1-1D-2", Connection.Outward),
                    new Connection("OW1-1E-1", Connection.Outward),
                    new Connection("OW1-1E-2", Connection.Outward),
                    new Connection("OW1-1F-1", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW1-0F",
                Description = "Exit from D7 cave",
                Address = 0x2487D,
                Location = 0xE0000F4850,
                Default = 0xE10A8E707C,
                DeadEnd = true,
                Connections = new ConnectionList
                {
                    new Connection("OW1-0E", Connection.Inward),
                    new Connection("OW1-0E", Connection.Outward),
                    new Connection("OW1-1D-2", Connection.Outward),
                    new Connection("OW1-1E-1", Connection.Outward),
                    new Connection("OW1-1E-2", Connection.Outward),
                    new Connection("OW1-1F-1", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW1-10",
                Description = "Entrance to D8",
                Address = 0x248B7,
                Location = 0xE000105810,
                Default = 0xE1075D507C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-03", Connection.Inward, Items.Bombs),
                    new Connection("OW1-11", Connection.Inward, Items.Bombs),
                    new Connection("OW1-03", Connection.Outward),
                    new Connection("OW1-11", Connection.Outward),
                    new Connection("OW1-13", Connection.Outward, Items.Bombs),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(2, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-11",
                Description = "Telephone booth (turtle rock)",
                Address = 0x24903,
                Location = 0xE000116832,
                Default = 0xE11099507C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-03", Connection.Inward),
                    new Connection("OW1-10", Connection.Inward),
                    new Connection("OW1-03", Connection.Outward),
                    new Connection("OW1-10", Connection.Outward, Items.Bombs),
                    new Connection("OW1-13", Connection.Outward, Items.Bombs),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(2, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-13",
                Description = "Entrance to flame skip cave",
                Address = 0x24995,
                Location = 0xE000135810,
                Default = 0xE11FFE707C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-03", Connection.Inward, Items.Bombs),
                    new Connection("OW1-04", Connection.Inward, Items.Bombs),
                    new Connection("OW1-10", Connection.Inward, Items.Bombs),
                    new Connection("OW1-11", Connection.Inward, Items.Bombs),
                    new Connection("OW1-15", Connection.Inward, Items.Bombs),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(2, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-15",
                Description = "Exit from hookshot gap",
                Address = 0x24A05,
                Location = 0xE000158840,
                Default = 0xE10AEA507C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-04", Connection.Inward),
                    new Connection("OW1-04", Connection.Outward, Items.Bracelet),
                    new Connection("OW1-13", Connection.Outward, Items.Bombs),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW1-17",
                Description = "Entrance to mountain access cave",
                Address = 0x24AC1,
                Location = 0xE000173832,
                Default = 0xE10AB6507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(13, Connection.Inward, Items.Bracelet),
                    new Connection(13, Connection.Outward, Items.Bracelet),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-18-1",
                Description = "Left exit from access cave (chest)",
                Address = 0x24B14,
                Location = 0xE000186812,
                Default = 0xE10ABB507C,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW1-18-2",
                Description = "Right exit from access cave",
                Address = 0x24B1B,
                Location = 0xE000188812,
                Default = 0xE10ABC307C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-07", Connection.Inward, Items.Hookshot | Items.Flippers),
                    new Connection("OW1-0A-2", Connection.Inward, Items.Flippers),
                    new Connection("OW1-0A-3", Connection.Inward, Items.Flippers),
                    new Connection("OW1-19", Connection.Inward),
                    new Connection("OW1-1D-1", Connection.Inward, Items.Flippers),
                    new Connection("OW1-07", Connection.Outward, Items.Hookshot | Items.Flippers),
                    new Connection("OW1-0A-2", Connection.Outward, Items.Flippers),
                    new Connection("OW1-0A-3", Connection.Outward, Items.Flippers),
                    new Connection("OW1-19", Connection.Outward),
                    new Connection("OW1-1D-1", Connection.Outward, Items.Flippers),
                    new Connection("OW1-2B-1", Connection.Outward, MiscFlags.Waterfall),
                    new Connection("OW1-2B-2", Connection.Outward, MiscFlags.Waterfall),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(13, Connection.Outward, Items.Flippers | Items.Hookshot),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-19",
                Description = "Entrance to Papahl cave",
                Address = 0x24B3F,
                Location = 0xE000198840,
                Default = 0xE10A89407C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-07", Connection.Inward, Items.Hookshot | Items.Flippers),
                    new Connection("OW1-0A-2", Connection.Inward, Items.Flippers),
                    new Connection("OW1-0A-3", Connection.Inward, Items.Flippers),
                    new Connection("OW1-18-2", Connection.Inward),
                    new Connection("OW1-1D-1", Connection.Inward, Items.Flippers),
                    new Connection("OW1-07", Connection.Outward, Items.Hookshot | Items.Flippers),
                    new Connection("OW1-0A-2", Connection.Outward, Items.Flippers),
                    new Connection("OW1-0A-3", Connection.Outward, Items.Flippers),
                    new Connection("OW1-18-2", Connection.Outward),
                    new Connection("OW1-1D-1", Connection.Outward, Items.Flippers),
                    new Connection("OW1-2B-1", Connection.Outward, MiscFlags.Waterfall),
                    new Connection("OW1-2B-2", Connection.Outward, MiscFlags.Waterfall),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(13, Connection.Outward, Items.Flippers | Items.Hookshot),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-1D-1",
                Description = "Entrance to water cave to D7",
                Address = 0x24CE1,
                Location = 0xE0001D1830,
                Default = 0xE10AF9207C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-07", Connection.Inward, Items.Hookshot | Items.Flippers),
                    new Connection("OW1-0A-2", Connection.Inward, Items.Flippers),
                    new Connection("OW1-0A-3", Connection.Inward, Items.Flippers),
                    new Connection("OW1-18-2", Connection.Inward, Items.Flippers),
                    new Connection("OW1-19", Connection.Inward, Items.Flippers),
                    new Connection("OW1-07", Connection.Outward, Items.Hookshot | Items.Flippers),
                    new Connection("OW1-0A-2", Connection.Outward, Items.Flippers),
                    new Connection("OW1-0A-3", Connection.Outward, Items.Flippers),
                    new Connection("OW1-18-2", Connection.Outward, Items.Flippers),
                    new Connection("OW1-19", Connection.Outward, Items.Flippers),
                    new Connection("OW1-2B-1", Connection.Outward, Items.Flippers, MiscFlags.Waterfall),
                    new Connection("OW1-2B-2", Connection.Outward, Items.Flippers, MiscFlags.Waterfall),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(13, Connection.Outward, Items.Flippers | Items.Hookshot),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-1D-2",
                Description = "Exit from water cave to D7",
                Address = 0x24D03,
                Location = 0xE0001D7850,
                Default = 0xE10AFA707C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-0E", Connection.Inward),
                    new Connection("OW1-0F", Connection.Inward),
                    new Connection("OW1-1E-1", Connection.Inward),
                    new Connection("OW1-1E-1", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW1-1E-1",
                Description = "Entrance to loop cave (outer)",
                Address = 0x24D2E,
                Location = 0xE0001E3810,
                Default = 0xE10A80207C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-0E", Connection.Inward),
                    new Connection("OW1-0F", Connection.Inward),
                    new Connection("OW1-1D-2", Connection.Inward),
                    new Connection("OW1-1D-2", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW1-1E-2",
                Description = "Entrance to loop cave (inner)",
                Address = 0x24D50,
                Location = 0xE0001E7810,
                Default = 0xE10A83807C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-0E", Connection.Inward),
                    new Connection("OW1-0F", Connection.Inward),
                    new Connection("OW1-1F-1", Connection.Inward),
                    new Connection("OW1-1F-1", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW1-1F-1",
                Description = "Exit from loop cave (outer)",
                Address = 0x24D8A,
                Location = 0xE0001F2810,
                Default = 0xE10A82707C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-0E", Connection.Inward),
                    new Connection("OW1-0F", Connection.Inward),
                    new Connection("OW1-1E-2", Connection.Inward),
                    new Connection("OW1-1E-2", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW1-1F-2",
                Description = "Exit from loop cave (inner)",
                Address = 0x24DBF,
                Location = 0xE0001F5840,
                Default = 0xE10A87607C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-1F-3", Connection.Inward),
                    new Connection("OW1-1F-4", Connection.Inward),
                    new Connection("OW1-1F-3", Connection.Outward, Items.Bombs),
                    new Connection("OW1-1F-4", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW1-1F-3",
                Description = "Fairy fountain (bombable wall)",
                Address = 0x24DCD,
                Location = 0xE0001F3850,
                Default = 0xE11FFB507C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-1F-2", Connection.Inward, Items.Bombs),
                    new Connection("OW1-1F-4", Connection.Inward, Items.Bombs),
                    new Connection("OW1-1F-2", Connection.Outward),
                    new Connection("OW1-1F-4", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW1-1F-4",
                Description = "Entrance to D7 cave",
                Address = 0x24D94,
                Location = 0xE0001F7810,
                Default = 0xE10A8C607C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-1F-2", Connection.Inward),
                    new Connection("OW1-1F-3", Connection.Inward),
                    new Connection("OW1-1F-2", Connection.Outward),
                    new Connection("OW1-1F-3", Connection.Outward, Items.Bombs),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW1-20",
                Description = "Left entrance to cave below D8",
                Address = 0x24E04,
                Location = 0xE000208832,
                Default = 0xE111AE507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(2, Connection.Inward),
                    new Connection(2, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-21",
                Description = "Right entrance to cave below D8",
                Address = 0x24E3F,
                Location = 0xE000211832,
                Default = 0xE111AF507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(2, Connection.Inward),
                    new Connection(2, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-24",
                Description = "Entrance to D2",
                Address = 0x24F05,
                Location = 0xE000243822,
                Default = 0xE10136507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(11, Connection.Inward, MiscFlags.BowWow),
                    new Connection(11, Connection.Inward, Items.Bracelet),
                    new Connection(11, Connection.Inward, Items.MagicRod),
                    new Connection(11, Connection.Inward, Items.Hookshot),
                    new Connection(11, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-2B-1",
                Description = "Entrance to D4",
                Address = 0x250D8,
                Location = 0xE0002B4822,
                Default = 0xE1037A507C,
                Locked = true,
                Connections = new ConnectionList
                {
                    new Connection("OW1-07", Connection.Inward, Items.Hookshot | Items.Flippers, MiscFlags.Waterfall),
                    new Connection("OW1-0A-2", Connection.Inward, Items.Flippers, MiscFlags.Waterfall),
                    new Connection("OW1-0A-3", Connection.Inward, Items.Flippers, MiscFlags.Waterfall),
                    new Connection("OW1-18-2", Connection.Inward, MiscFlags.Waterfall),
                    new Connection("OW1-19", Connection.Inward, MiscFlags.Waterfall),
                    new Connection("OW1-1D-1", Connection.Inward, Items.Flippers, MiscFlags.Waterfall),
                    new Connection("OW1-2B-2", Connection.Inward, MiscFlags.Waterfall),
                    new Connection("OW1-2B-2", Connection.Outward, MiscFlags.Waterfall),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(10, Connection.Inward, Items.Flippers, MiscFlags.Waterfall),
                    new Connection(12, Connection.Inward, Items.Flippers, MiscFlags.Waterfall),
                    new Connection(12, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-2B-2",
                Description = "Entrance to D4 cave",
                Address = 0x250F3,
                Location = 0xE0002B6830,
                Default = 0xE11FE92820,
                Locked = true,
                Connections = new ConnectionList
                {
                    new Connection("OW1-07", Connection.Inward, Items.Hookshot | Items.Flippers, MiscFlags.Waterfall),
                    new Connection("OW1-0A-2", Connection.Inward, Items.Flippers, MiscFlags.Waterfall),
                    new Connection("OW1-0A-3", Connection.Inward, Items.Flippers, MiscFlags.Waterfall),
                    new Connection("OW1-18-2", Connection.Inward, MiscFlags.Waterfall),
                    new Connection("OW1-19", Connection.Inward, MiscFlags.Waterfall),
                    new Connection("OW1-1D-1", Connection.Inward, Items.Flippers, MiscFlags.Waterfall),
                    new Connection("OW1-2B-1", Connection.Inward, MiscFlags.Waterfall),
                    new Connection("OW1-2B-1", Connection.Outward, MiscFlags.Waterfall),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(10, Connection.Inward, Items.Flippers, MiscFlags.Waterfall),
                    new Connection(12, Connection.Inward, Items.Flippers, MiscFlags.Waterfall),
                    new Connection(12, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-2D",
                Description = "Exit from D4 cave",
                Address = 0x25164,
                Location = 0xE0002D5850,
                Default = 0xE11FEA8870,
                ZoneConnections = new ConnectionList
                {
                    new Connection(12, Connection.Inward),
                    new Connection(12, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-2F",
                Description = "Exit from raft cave",
                Address = 0x251DE,
                Location = 0xE0002F1870,
                Default = 0xE11FE74810,
                ZoneConnections = new ConnectionList
                {
                    new Connection(10, Connection.Inward),
                    new Connection(10, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-30",
                Description = "Writer's house",
                Address = 0x2521A,
                Location = 0xE000307832,
                Default = 0xE110A8507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(2, Connection.Inward),
                    new Connection(2, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-31",
                Description = "Telephone booth (swamp)",
                Address = 0x25253,
                Location = 0xE000316852,
                Default = 0xE1109B507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(11, Connection.Inward),
                    new Connection(11, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-35",
                Description = "Moblin hideout",
                Address = 0x25364,
                Location = 0xE000356850,
                Default = 0xE115F0507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(3, Connection.Inward),
                    new Connection(3, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-37",
                Description = "Photo gallery",
                Address = 0x253F2,
                Location = 0xE000374842,
                Default = 0xE110B5507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(3, Connection.Inward),
                    new Connection(3, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-3F",
                Description = "Raft shop",
                Address = 0x25615,
                Location = 0xE0003F2822,
                Default = 0xE110B0507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(10, Connection.Inward),
                    new Connection(10, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-42",
                Description = "Mysterious woods hookshot cave",
                Address = 0x25724,
                Location = 0xE000423842,
                Default = 0xE111B3507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward, Items.Bracelet),
                    new Connection(1, Connection.Outward, Items.Bracelet),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-45",
                Description = "Crazy Tracy's house",
                Address = 0x257F6,
                Location = 0xE000458842,
                Default = 0xE10EAD507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward, Items.Bracelet),
                    new Connection(3, Connection.Inward, Items.Bracelet),
                    new Connection(1, Connection.Outward, Items.Bracelet),
                    new Connection(3, Connection.Outward, Items.Bracelet),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-49",
                Description = "Exit from Kanalet cave",
                Address = 0x258E0,
                Location = 0xE000496850,
                Default = 0xE21FEB1830,
                ZoneConnections = new ConnectionList
                {
                    new Connection(8, Connection.Inward),
                    new Connection(8, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-4A",
                Description = "Entrance to Kanalet cave",
                Address = 0x2591E,
                Location = 0xE0004A8830,
                Default = 0xE21FEC6830,
                ZoneConnections = new ConnectionList
                {
                    new Connection(9, Connection.Inward),
                    new Connection(9, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-4B",
                Description = "Telephone booth (Kanalet)",
                Address = 0x25941,
                Location = 0xE0004B4822,
                Default = 0xE110CC507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(9, Connection.Inward),
                    new Connection(9, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-50",
                Description = "Exit from log cave",
                Address = 0x25A89,
                Location = 0xE000508832,
                Default = 0xE10AAB507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(11, Connection.Inward, Items.Feather),
                    new Connection(11, Connection.Outward, Items.Feather),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-52",
                Description = "Upgrade shrine (woods)",
                Address = 0x25B2F,
                Location = 0xE000526830,
                Default = 0xE11FE18850,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward, Items.Bracelet),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-59-1",
                Description = "Left Kanalet roof door",
                Address = 0x25CB7,
                Location = 0xE000591830,
                Default = 0xE114D5507C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-59-2", Connection.Inward),
                    new Connection("OW1-59-2", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW1-59-2",
                Description = "Right Kanalet roof door",
                Address = 0x25CCA,
                Location = 0xE000595840,
                Default = 0xE114D6507C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-59-1", Connection.Inward),
                    new Connection("OW1-59-1", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW1-62",
                Description = "Entrance to log cave",
                Address = 0x25F28,
                Location = 0xE000627842,
                Default = 0xE10ABD507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-65",
                Description = "Witch's hut",
                Address = 0x25FBD,
                Location = 0xE000654832,
                Default = 0xE10EA2507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-69",
                Description = "Kanalet entrance",
                Address = 0x260C5,
                Location = 0xE000695840,
                Default = 0xE114D3507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(8, Connection.Inward),
                    new Connection(8, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-6C",
                Description = "Stairs from raft ride to D6",
                Address = 0x2617F,
                Location = 0xE0006C4840,
                Default = 0xE105B07810,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW1-75",
                Description = "Entrance to graveyard cave",
                Address = 0x263D7,
                Location = 0xE000753840,
                Default = 0xE10ADE3840,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward, Items.Bracelet),
                    new Connection(1, Connection.Outward, Items.Bracelet),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-76",
                Description = "Exit from graveyard cave",
                Address = 0x2642C,
                Location = 0xE000766850,
                Default = 0xE10ADF3830,
                ZoneConnections = new ConnectionList
                {
                    new Connection(3, Connection.Inward),
                    new Connection(3, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-77",
                Description = "Entrance to D0",
                Address = 0x26459,
                Location = 0xE00077782E,
                Default = 0xE1FF12505C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(3, Connection.Inward, Items.Bracelet),
                    new Connection(3, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-78",
                Description = "Kanalet moat stairs",
                Address = 0x264BE,
                Location = 0xE000782870,
                Default = 0xE11FFD5850,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward, MiscFlags.Rooster),
                    new Connection(4, Connection.Outward, MiscFlags.Rooster),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-82-1",
                Description = "Left door to Papahl's house",
                Address = 0x680AC,
                Location = 0xE000825852,
                Default = 0xE110A5507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-82-2",
                Description = "Right door to Papahl's house",
                Address = 0x680B1,
                Location = 0xE000827852,
                Default = 0xE110A6507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-83",
                Description = "Dream shrine",
                Address = 0x680E0,
                Location = 0xE000832842,
                Default = 0xE113AA507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward, Items.Bracelet),
                    new Connection(1, Connection.Outward, Items.Bracelet),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-84",
                Description = "Cave beside Mabe Village",
                Address = 0x68125,
                Location = 0xE000849862,
                Default = 0xE111CD507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-86",
                Description = "Crystal cave before D3 (bombable wall)",
                Address = 0x681DB,
                Location = 0xE000861840,
                Default = 0xE111F4407C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward, Items.Bombs),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-87",
                Description = "Fairy fountain at honey tree (bombable wall)",
                Address = 0x68229,
                Location = 0xE000872810,
                Default = 0xE11FF3507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward, Items.Bombs),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-88",
                Description = "Telephone booth (Ukuku Prairie)",
                Address = 0x6824E,
                Location = 0xE000885852,
                Default = 0xE1109C507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-8A",
                Description = "Seashell mansion",
                Address = 0x682B9,
                Location = 0xE0008A5840,
                Default = 0xE210E90870,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-8C",
                Description = "Entrance to D6",
                Address = 0x683B3,
                Location = 0xE0008C3840,
                Default = 0xE105D4507C,
                Locked = true,
                Connections = new ConnectionList
                {
                    new Connection("OW1-9C", Connection.Inward, Keys.FaceKey),
                    new Connection("OW1-8D", Connection.Outward, Items.Flippers | Items.Bombs),
                    new Connection("OW1-8F", Connection.Outward, Items.Flippers),
                    new Connection("OW1-9C", Connection.Outward),
                    new Connection("OW1-9D", Connection.Outward, Items.Flippers),
                    new Connection("OW1-D9-1", Connection.Outward, Items.Flippers),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Outward, Items.Flippers),
                    new Connection(5, Connection.Outward, Items.Flippers),
                    new Connection(6, Connection.Outward, Items.Flippers),
                    new Connection(7, Connection.Outward, Items.Flippers | Items.Bracelet),
                    new Connection(9, Connection.Outward, Items.Flippers),
                    new Connection(12, Connection.Outward, Items.Flippers),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-8D",
                Description = "Fairy fountain outside D6 (bombable wall)",
                Address = 0x683F4,
                Location = 0xE0008D3820,
                Default = 0xE11FAC507C,
                Connections = new ConnectionList
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
                    new Connection(4, Connection.Outward, Items.Flippers),
                    new Connection(5, Connection.Outward, Items.Flippers),
                    new Connection(6, Connection.Outward, Items.Flippers),
                    new Connection(6, Connection.Outward, Items.Bracelet),
                    new Connection(7, Connection.Outward, Items.Bracelet),
                    new Connection(9, Connection.Outward, Items.Flippers),
                    new Connection(12, Connection.Outward, Items.Flippers),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-8F",
                Description = "Entrance to raft cave",
                Address = 0x68442,
                Location = 0xE0008F0820,
                Default = 0xE11FF78860,
                Connections = new ConnectionList
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
                    new Connection(4, Connection.Outward, Items.Flippers),
                    new Connection(5, Connection.Outward, Items.Flippers),
                    new Connection(6, Connection.Outward, Items.Flippers),
                    new Connection(7, Connection.Outward, Items.Flippers | Items.Bracelet),
                    new Connection(9, Connection.Outward, Items.Flippers),
                    new Connection(12, Connection.Outward, Items.Flippers),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-92",
                Description = "Rooster's grave",
                Address = 0x68502,
                Location = 0xE000925852,
                Default = 0xE11FF45870,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward, Items.L2Bracelet),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-93",
                Description = "Shop",
                Address = 0x68530,
                Location = 0xE000934862,
                Default = 0xE10EA1507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-9C",
                Description = "Exit from D6 cave",
                Address = 0x68736,
                Location = 0xE0009C5810,
                Default = 0xE11FF03810,
                DeadEnd = true,
                Connections = new ConnectionList
                {
                    new Connection("OW1-8C", Connection.Inward),
                    new Connection("OW1-8C", Connection.Outward),
                    new Connection("OW1-8D", Connection.Outward, Items.Flippers | Items.Bombs),
                    new Connection("OW1-8F", Connection.Outward, Items.Flippers),
                    new Connection("OW1-9D", Connection.Outward, Items.Flippers),
                    new Connection("OW1-D9-1", Connection.Outward, Items.Flippers),
                },
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Outward, Items.Flippers),
                    new Connection(5, Connection.Outward, Items.Flippers),
                    new Connection(6, Connection.Outward, Items.Flippers),
                    new Connection(7, Connection.Outward, Items.Flippers | Items.Bracelet),
                    new Connection(9, Connection.Outward, Items.Flippers),
                    new Connection(12, Connection.Outward, Items.Flippers),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-9D",
                Description = "Entrance to D6 cave",
                Address = 0x6877F,
                Location = 0xE0009D3830,
                Default = 0xE11FF18860,
                Connections = new ConnectionList
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
                    new Connection(4, Connection.Outward, Items.Flippers),
                    new Connection(5, Connection.Outward, Items.Flippers),
                    new Connection(6, Connection.Outward, Items.Flippers),
                    new Connection(7, Connection.Outward, Items.Flippers | Items.Bracelet),
                    new Connection(9, Connection.Outward, Items.Flippers),
                    new Connection(12, Connection.Outward, Items.Flippers),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-A1-1",
                Description = "Madame Meow Meow's house",
                Address = 0x6889D,
                Location = 0xE000A13842,
                Default = 0xE110A7507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-A1-2",
                Description = "Doghouse",
                Address = 0x6886F,
                Location = 0xE000A15842,
                Default = 0xE112B2507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-A2",
                Description = "Marin & Tarin's house",
                Address = 0x688C0,
                Location = 0xE000A25852,
                Default = 0xE110A3507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-A4",
                Description = "Telephone booth (signpost maze)",
                Address = 0x6891C,
                Location = 0xE000A43842,
                Default = 0xE110B4507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-AA",
                Description = "Entrance to animal village cave",
                Address = 0x68A5F,
                Location = 0xE000AA8840,
                Default = 0xE111D02840,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-AB",
                Description = "Exit from animal village cave",
                Address = 0x68A80,
                Location = 0xE000AB7850,
                Default = 0xE111D17840,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward),
                    new Connection(6, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-AC",
                Description = "Southern face shrine",
                Address = 0x68AF5,
                Location = 0xE000AC5840,
                Default = 0xE1168F507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(7, Connection.Inward),
                    new Connection(7, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-AE",
                Description = "Armos maze secret seashell cave",
                Address = 0x68B39,
                Location = 0xE000AE4870,
                Default = 0xE111FC6860,
                ZoneConnections = new ConnectionList
                {
                    new Connection(7, Connection.Inward),
                    new Connection(7, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-B0",
                Description = "Library",
                Address = 0x68BC2,
                Location = 0xE000B03832,
                Default = 0xE11DFA507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-B1",
                Description = "Ulrira's house",
                Address = 0x68BE5,
                Location = 0xE000B14862,
                Default = 0xE110A9507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-B2",
                Description = "Telephone booth (Mabe Village)",
                Address = 0x68C1A,
                Location = 0xE000B25852,
                Default = 0xE110CB507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-B3",
                Description = "Trendy game",
                Address = 0x68C3C,
                Location = 0xE000B35852,
                Default = 0xE10FA0507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-B5",
                Description = "Entrance to D3",
                Address = 0x68CDA,
                Location = 0xE000B56820,
                Default = 0xE10252507C,
                Locked = true,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward, Items.Feather, Keys.SlimeKey),
                    new Connection(4, Connection.Inward, Items.Flippers, Keys.SlimeKey),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-B8-1",
                Description = "Hidden exit (top) from moblin maze cave",
                Address = 0x68D9E,
                Location = 0xE000B85830,
                Default = 0xE10A95707C,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW1-B8-2",
                Description = "Entrance to moblin maze cave",
                Address = 0x68DB3,
                Location = 0xE000B87860,
                Default = 0xE10A92307C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-C6",
                Description = "Exit from villa cave",
                Address = 0x690EA,
                Location = 0xE000C63850,
                Default = 0xE111C9807C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-C8",
                Description = "Exit from moblin maze cave",
                Address = 0x69184,
                Location = 0xE000C82850,
                Default = 0xE10A93307C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(5, Connection.Inward),
                    new Connection(5, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-CC-1",
                Description = "Large house (Animal Village)",
                Address = 0x69257,
                Location = 0xE000CC2850,
                Default = 0xE110DB507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward),
                    new Connection(6, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-CC-2",
                Description = "Painter's house (Animal Village)",
                Address = 0x69263,
                Location = 0xE000CC7850,
                Default = 0xE110DD507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward),
                    new Connection(6, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-CD-1",
                Description = "HP cave behind animal village",
                Address = 0x6927B,
                Location = 0xE000CD8820,
                Default = 0xE10AF7607C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward, Items.Bombs),
                    new Connection(6, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-CD-2",
                Description = "Goat's house (Animal Village)",
                Address = 0x69282,
                Location = 0xE000CD2850,
                Default = 0xE110D9507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward),
                    new Connection(6, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-CD-3",
                Description = "Zora's house (Animal Village)",
                Address = 0x69289,
                Location = 0xE000CD5850,
                Default = 0xE110DA507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward),
                    new Connection(6, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-CF",
                Description = "Exit from Lanmolas cave",
                Address = 0x69329,
                Location = 0xE000CF5810,
                Default = 0xE11FF97860,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward, Items.Bombs),
                    new Connection(6, Connection.Outward, Items.Bombs),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-D3",
                Description = "Entrance to D1",
                Address = 0x693F8,
                Location = 0xE000D36822,
                Default = 0xE10017507C,
                Locked = true,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward, Keys.TailKey),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-D4",
                Description = "Mamu's cave",
                Address = 0x6942C,
                Location = 0xE000D48830,
                Default = 0xE111FB8870,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward, Items.Feather | Items.Bracelet | Items.Hookshot),
                    new Connection(4, Connection.Outward, Items.Feather),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-D6",
                Description = "Richard's villa",
                Address = 0x69505,
                Location = 0xE000D64850,
                Default = 0xE110C7507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Inward),
                    new Connection(4, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-D9-1",
                Description = "Entrance to D5",
                Address = 0x695A3,
                Location = 0xE000D95840,
                Default = 0xE104A1507C,
                Connections = new ConnectionList
                {
                    new Connection("OW1-8D", Connection.Inward, Items.Flippers),
                    new Connection("OW1-8F", Connection.Inward, Items.Flippers),
                    new Connection("OW1-9D", Connection.Inward, Items.Flippers),
                    new Connection("OW1-8D", Connection.Outward, Items.Flippers),
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
                    new Connection(4, Connection.Outward, Items.Flippers),
                    new Connection(5, Connection.Outward, Items.Flippers),
                    new Connection(6, Connection.Outward, Items.Flippers),
                    new Connection(7, Connection.Outward, Items.Flippers | Items.Bracelet),
                    new Connection(9, Connection.Outward, Items.Flippers),
                    new Connection(12, Connection.Outward, Items.Flippers),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-DB",
                Description = "Telephone booth (Animal Village)",
                Address = 0x6961F,
                Location = 0xE000DB7852,
                Default = 0xE110E3507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward),
                    new Connection(6, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-DD",
                Description = "Chef Bear's house (Animal Village)",
                Address = 0x69681,
                Location = 0xE000DD5842,
                Default = 0xE110D7507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward),
                    new Connection(6, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-E3",
                Description = "Crocodile's house",
                Address = 0x697CE,
                Location = 0xE000E34830,
                Default = 0xE110FE507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-E6",
                Description = "Upgrade shrine (Martha's Bay)",
                Address = 0x6984C,
                Location = 0xE000E64840,
                Default = 0xE11FE08870,
                Connections = new ConnectionList
                {
                    new Connection("OW1-E7", Connection.Inward),
                    new Connection("OW1-E7", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW1-E7",
                Description = "Exit from cave to upgrade shrine (Martha's Bay)",
                Address = 0x6988C,
                Location = 0xE000E76820,
                Default = 0xE11FE52830,
                Connections = new ConnectionList
                {
                    new Connection("OW1-E6", Connection.Inward),
                    new Connection("OW1-E6", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW1-E8",
                Description = "Telephone booth (Martha's Bay)",
                Address = 0x698D8,
                Location = 0xE000E83862,
                Default = 0xE1109D507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(5, Connection.Inward),
                    new Connection(5, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-E9",
                Description = "Magnifying lens cave (mermaid statue)",
                Address = 0x69931,
                Location = 0xE000E96830,
                Default = 0xE10A986860,
                ZoneConnections = new ConnectionList
                {
                    new Connection(6, Connection.Inward, Items.Hookshot),
                    new Connection(6, Connection.Outward, Items.Hookshot),
                    new Connection(6, Connection.Outward, Items.Flippers),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-F4",
                Description = "Boomerang moblin's cave",
                Address = 0x69B9F,
                Location = 0xE000F41820,
                Default = 0xE11FF5487C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward, Items.Bombs),
                    new Connection(1, Connection.Outward),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-F6",
                Description = "House by the bay",
                Address = 0x69C20,
                Location = 0xE000F65842,
                Default = 0xE11EE3507C,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Inward, Items.Bracelet),
                    new Connection(5, Connection.Inward, Items.Feather | Items.Boots),
                    new Connection(1, Connection.Outward, Items.Bracelet),
                    new Connection(5, Connection.Outward, Items.Feather | Items.Boots),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW1-F9",
                Description = "Entrance to cave to upgrade shrine (Martha's Bay)",
                Address = 0x69CF4,
                Location = 0xE000F97850,
                Default = 0xE11FF68870,
                ZoneConnections = new ConnectionList
                {
                    new Connection(5, Connection.Inward, Items.Feather),
                    new Connection(5, Connection.Outward, Items.Feather),
                }
            });
            Add(new Warp(this)
            {
                Code = "OW2-00",
                Description = "D8 - Staircase to OW 00",
                Address = 0x2CE03,
                Location = 0xE1073A5810,
                Default = 0xE000004850,
                Connections = new ConnectionList
                {
                    new Connection("OW2-02", Connection.Inward, Items.Feather),
                    new Connection("OW2-10", Connection.Inward, Items.Feather),
                    new Connection("OW2-02", Connection.Outward, Items.Feather),
                    new Connection("OW2-10", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-02",
                Description = "D8 - Staircase to OW 02",
                Address = 0x2CED7,
                Location = 0xE1073D5810,
                Default = 0xE000023850,
                Connections = new ConnectionList
                {
                    new Connection("OW2-00", Connection.Inward, Items.Feather),
                    new Connection("OW2-10", Connection.Inward, Items.Feather),
                    new Connection("OW2-00", Connection.Outward, Items.Feather),
                    new Connection("OW2-10", Connection.Outward, Items.Feather),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-03",
                Description = "Cave - Flame skip staircase",
                Address = 0x2B669,
                Location = 0xE11FEE1840,
                Default = 0xE000034850,
                DeadEnd = true,
                Connections = new ConnectionList
                {
                    new Connection("OW2-13", Connection.Inward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-04",
                Description = "Cave - Upgrade shrine (mountain)",
                Address = 0x2B30E,
                Location = 0xE11FE28850,
                Default = 0xE000047870,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-06",
                Description = "Egg - Exit to mountain",
                Address = 0x2D96C,
                Location = 0xE10870507C,
                Default = 0xE000065840,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-07",
                Description = "Cave - Entrance to hookshot gap",
                Address = 0x2F93B,
                Location = 0xE10AEE7830,
                Default = 0xE000073850,
                Connections = new ConnectionList
                {
                    new Connection("OW2-15", Connection.Inward, Items.Hookshot | Items.Feather),
                    new Connection("OW2-15", Connection.Outward, Items.Hookshot),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-0A-1",
                Description = "Cave - Exit for Papahl cave",
                Address = 0x2E0B1,
                Location = 0xE10A8B507C,
                Default = 0xE0000A1870,
                Connections = new ConnectionList
                {
                    new Connection("OW2-19", Connection.Inward),
                    new Connection("OW2-19", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-0A-2",
                Description = "House - Cucco house",
                Address = 0x2E53E,
                Location = 0xE1109F507C,
                Default = 0xE0000A4822,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-0A-3",
                Description = "Cave - Bird key cave entrance",
                Address = 0x2DC70,
                Location = 0xE10A7E607C,
                Default = 0xE0000A7870,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-0D",
                Description = "Cave - Exit from hidden part of water cave (before D7)",
                Address = 0x2FA73,
                Location = 0xE10AF2507C,
                Default = 0xE0000D1870,
                Connections = new ConnectionList
                {
                    new Connection("OW2-1D-1", Connection.Inward, Items.Bombs),
                    new Connection("OW2-1D-2", Connection.Inward, Items.Bombs),
                    new Connection("OW2-1D-1", Connection.Outward),
                    new Connection("OW2-1D-2", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-0E",
                Description = "D7 - Entrance",
                Address = 0x2C539,
                Address2 = 0x2CB8E,
                Location = 0xE1060E507C,
                Default = 0xE0000E5830,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-0F",
                Description = "Cave - Exit from D7 cave",
                Address = 0x2E1A2,
                Location = 0xE10A8E707C,
                Default = 0xE0000F4850,
                Connections = new ConnectionList
                {
                    new Connection("OW2-1F-4", Connection.Inward),
                    new Connection("OW2-1F-4", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-10",
                Description = "D8 - Entrance",
                Address = 0x2D536,
                Address2 = 0x2CC39,
                Location = 0xE1075D507C,
                Default = 0xE000105810,
                Connections = new ConnectionList
                {
                    new Connection("OW2-00", Connection.Inward),
                    new Connection("OW2-02", Connection.Inward, Items.Feather),
                    new Connection("OW2-00", Connection.Outward, Items.Feather),
                    new Connection("OW2-02", Connection.Outward, Items.Feather),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-11",
                Description = "House - Telephone booth (turtle rock)",
                Address = 0x2E460,
                Location = 0xE11099507C,
                Default = 0xE000116832,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-13",
                Description = "Cave - Entrance to flame skip",
                Address = 0x2BB31,
                Location = 0xE11FFE707C,
                Default = 0xE000135810,
                Connections = new ConnectionList
                {
                    new Connection("OW2-03", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-15",
                Description = "Cave - Exit from hookshot gap",
                Address = 0x2F7BA,
                Location = 0xE10AEA507C,
                Default = 0xE000158840,
                Connections = new ConnectionList
                {
                    new Connection("OW2-07", Connection.Inward, Items.Hookshot),
                    new Connection("OW2-07", Connection.Outward, Items.Hookshot | Items.Feather),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-17",
                Description = "Cave - Entrance to mountain access cave",
                Address = 0x2EA6D,
                Location = 0xE10AB6507C,
                Default = 0xE000173832,
                Connections = new ConnectionList
                {
                    new Connection("OW2-18-1", Connection.Inward),
                    new Connection("OW2-18-2", Connection.Inward, Items.Boots),
                    new Connection("OW2-18-1", Connection.Outward),
                    new Connection("OW2-18-2", Connection.Outward, Items.Boots),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-18-1",
                Description = "Cave - Left exit from access cave (chest)",
                Address = 0x2EBD7,
                Location = 0xE10ABB507C,
                Default = 0xE000186812,
                Connections = new ConnectionList
                {
                    new Connection("OW2-17", Connection.Inward),
                    new Connection("OW2-18-2", Connection.Inward, Items.Boots),
                    new Connection("OW2-17", Connection.Outward),
                    new Connection("OW2-18-2", Connection.Outward, Items.Boots),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-18-2",
                Description = "Cave - Right exit from access cave",
                Address = 0x2EC20,
                Location = 0xE10ABC307C,
                Default = 0xE000188812,
                Connections = new ConnectionList
                {
                    new Connection("OW2-17", Connection.Inward, Items.Boots),
                    new Connection("OW2-18-1", Connection.Inward, Items.Boots),
                    new Connection("OW2-17", Connection.Outward, Items.Boots),
                    new Connection("OW2-18-1", Connection.Outward, Items.Boots),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-19",
                Description = "Cave - Entrance to Papahl cave",
                Address = 0x2E013,
                Location = 0xE10A89407C,
                Default = 0xE000198840,
                Connections = new ConnectionList
                {
                    new Connection("OW2-0A-1", Connection.Inward),
                    new Connection("OW2-0A-1", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-1D-1",
                Description = "Cave - Entrance to water cave to D7",
                Address = 0x2FC85,
                Location = 0xE10AF9207C,
                Default = 0xE0001D1830,
                Connections = new ConnectionList
                {
                    new Connection("OW2-1D-2", Connection.Inward),
                    new Connection("OW2-0D", Connection.Inward),
                    new Connection("OW2-1D-2", Connection.Outward),
                    new Connection("OW2-0D", Connection.Outward, Items.Bombs),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-1D-2",
                Description = "Cave - Exit from water cave to D7",
                Address = 0x2FCE3,
                Location = 0xE10AFA707C,
                Default = 0xE0001D7850,
                Connections = new ConnectionList
                {
                    new Connection("OW2-1D-1", Connection.Inward),
                    new Connection("OW2-0D", Connection.Inward),
                    new Connection("OW2-1D-1", Connection.Outward),
                    new Connection("OW2-0D", Connection.Outward, Items.Bombs),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-1E-1",
                Description = "Cave - Entrance to loop cave (outer)",
                Address = 0x2DD11,
                Location = 0xE10A80207C,
                Default = 0xE0001E3810,
                Connections = new ConnectionList
                {
                    new Connection("OW2-1F-1", Connection.Inward),
                    new Connection("OW2-1F-1", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-1E-2",
                Description = "Cave - Entrance to loop cave (inner)",
                Address = 0x2DE13,
                Location = 0xE10A83807C,
                Default = 0xE0001E7810,
                Connections = new ConnectionList
                {
                    new Connection("OW2-1F-2", Connection.Outward, Items.Feather),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-1F-1",
                Description = "Cave - Exit from loop cave (outer)",
                Address = 0x2DDAE,
                Location = 0xE10A82707C,
                Default = 0xE0001F2810,
                Connections = new ConnectionList
                {
                    new Connection("OW2-1E-1", Connection.Inward),
                    new Connection("OW2-1E-1", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-1F-2",
                Description = "Cave - Exit from loop cave (inner)",
                Address = 0x2DF40,
                Location = 0xE10A87607C,
                Default = 0xE0001F5840,
                DeadEnd = true,
                Connections = new ConnectionList
                {
                    new Connection("OW2-1F-2", Connection.Inward, Items.Feather),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-1F-3",
                Description = "Cave - Fairy fountain before D7",
                Address = 0x2BA6D,
                Location = 0xE11FFB507C,
                Default = 0xE0001F3850,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-1F-4",
                Description = "Cave - Entrance to D7 cave",
                Address = 0x2E102,
                Location = 0xE10A8C607C,
                Default = 0xE0001F7810,
                Connections = new ConnectionList
                {
                    new Connection("OW2-0F", Connection.Inward),
                    new Connection("OW2-0F", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-20",
                Description = "Cave - Left entrance to cave below D8",
                Address = 0x2E878,
                Location = 0xE111AE507C,
                Default = 0xE000208832,
                Connections = new ConnectionList
                {
                    new Connection("OW2-21", Connection.Inward),
                    new Connection("OW2-21", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-21",
                Description = "Cave - Right entrance to cave below D8",
                Address = 0x2E8D1,
                Location = 0xE111AF507C,
                Default = 0xE000211832,
                Connections = new ConnectionList
                {
                    new Connection("OW2-20", Connection.Inward),
                    new Connection("OW2-20", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-24",
                Description = "D2 - Entrance",
                Address = 0x28DB6,
                Address2 = 0x28ACB,
                Location = 0xE10136507C,
                Default = 0xE000243822,
                DeadEnd = true,
                Special = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-2B-1",
                Description = "D4 - Entrance",
                Address = 0x29E68,
                Address2 = 0x297F2,
                Location = 0xE1037A507C,
                Default = 0xE0002B4822,
                DeadEnd = true,
                Special = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-2B-2",
                Description = "Cave - Entrance to D4 cave",
                Address = 0x2B532,
                Location = 0xE11FE92820,
                Default = 0xE0002B6830,
                Connections = new ConnectionList
                {
                    new Connection("OW2-2D", Connection.Outward, Items.Feather),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-2D",
                Description = "Cave - Exit from D4 cave",
                Address = 0x2B57E,
                Location = 0xE11FEA8870,
                Default = 0xE0002D5850,
                DeadEnd = true,
                Connections = new ConnectionList
                {
                    new Connection("OW2-2D", Connection.Inward, Items.Feather),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-2F",
                Description = "Cave - Exit from raft cave",
                Address = 0x2B4B3,
                Location = 0xE11FE74810,
                Default = 0xE0002F1870,
                DeadEnd = true,
                Connections = new ConnectionList
                {
                    new Connection("OW2-8F", Connection.Inward, Items.Flippers),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-30",
                Description = "House - Writer's house",
                Address = 0x2E6EA,
                Location = 0xE110A8507C,
                Default = 0xE000307832,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-31",
                Description = "House - Telephone booth (swamp)",
                Address = 0x2E4C1,
                Location = 0xE1109B507C,
                Default = 0xE000316852,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-35",
                Description = "Cave - Moblin hideout",
                Address = 0x2F9D2,
                Location = 0xE115F0507C,
                Default = 0xE000356850,
                DeadEnd = true,
                Special = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-37",
                Description = "House - Photo gallery",
                Address = 0x2EA22,
                Location = 0xE110B5507C,
                Default = 0xE000374842,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-3F",
                Description = "House - Raft shop",
                Address = 0x2E906,
                Location = 0xE110B0507C,
                Default = 0xE0003F2822,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-42",
                Description = "Cave - Mysterious woods hookshot cave",
                Address = 0x2E9C8,
                Location = 0xE111B3507C,
                Default = 0xE000423842,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-45",
                Description = "House - Crazy Tracy's house",
                Address = 0x2E81C,
                Location = 0xE10EAD507C,
                Default = 0xE000458842,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-49",
                Description = "SS - Exit from Kanalet cave",
                Address = 0x2B5C0,
                Location = 0xE21FEB1830,
                Default = 0xE000496850,
                Connections = new ConnectionList
                {
                    new Connection("OW2-4A", Connection.Inward, Items.Feather),
                    new Connection("OW2-4A", Connection.Outward, Items.Feather),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-4A",
                Description = "SS - Entrance to Kanalet cave",
                Address = 0x2B61E,
                Location = 0xE21FEC6830,
                Default = 0xE0004A8830,
                Connections = new ConnectionList
                {
                    new Connection("OW2-49", Connection.Inward, Items.Feather),
                    new Connection("OW2-49", Connection.Outward, Items.Feather),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-4B",
                Description = "House - Telephone booth (Kanalet)",
                Address = 0x2F06E,
                Location = 0xE110CC507C,
                Default = 0xE0004B4822,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-50",
                Description = "Cave - Exit from log cave",
                Address = 0x2E7A8,
                Location = 0xE10AAB507C,
                Default = 0xE000508832,
                Connections = new ConnectionList
                {
                    new Connection("OW2-62", Connection.Inward),
                    new Connection("OW2-62", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-52",
                Description = "Cave - Upgrade shrine (woods)",
                Address = 0x2B2BB,
                Location = 0xE11FE18850,
                Default = 0xE000526830,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-59-1",
                Description = "DK - Left Kanalet roof door",
                Address = 0x2F29A,
                Location = 0xE114D5507C,
                Default = 0xE000591830,
                Connections = new ConnectionList
                {
                    new Connection("OW2-69", Connection.Inward),
                    new Connection("OW2-69", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-59-2",
                Description = "DK - Right Kanalet roof door",
                Address = 0x2F2E3,
                Location = 0xE114D6507C,
                Default = 0xE000595840,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-62",
                Description = "Cave - Entrance to log cave",
                Address = 0x2EC6D,
                Location = 0xE10ABD507C,
                Default = 0xE000627842,
                Connections = new ConnectionList
                {
                    new Connection("OW2-50", Connection.Inward),
                    new Connection("OW2-50", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-65",
                Description = "House - Witch's hut",
                Address = 0x2E5C5,
                Location = 0xE10EA2507C,
                Default = 0xE000654832,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-69",
                Description = "DK - Entrance",
                Address = 0x2F21C,
                Location = 0xE114D3507C,
                Default = 0xE000695840,
                Connections = new ConnectionList
                {
                    new Connection("OW2-59-1", Connection.Inward),
                    new Connection("OW2-59-1", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-6C",
                Description = "D6 - Stairs to raft ride",
                Address = 0x2A85D,
                Location = 0xE105B07810,
                Default = 0xE0006C4840,
                Connections = new ConnectionList
                {
                    new Connection("OW2-8C", Connection.Inward, Items.L2Bracelet | Items.Bombs),
                    new Connection("OW2-8C", Connection.Outward, Items.Bracelet | Items.Bombs),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-75",
                Description = "Cave - Entrance to graveyard cave",
                Address = 0x2F498,
                Location = 0xE10ADE3840,
                Default = 0xE000753840,
                Connections = new ConnectionList
                {
                    new Connection("OW2-76", Connection.Inward, Items.Feather),
                    new Connection("OW2-76", Connection.Outward, Items.Feather),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-76",
                Description = "Cave - Exit from graveyard cave",
                Address = 0x2F4F8,
                Location = 0xE10ADF3830,
                Default = 0xE000766850,
                Connections = new ConnectionList
                {
                    new Connection("OW2-75", Connection.Inward, Items.Feather),
                    new Connection("OW2-75", Connection.Outward, Items.Feather),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-77",
                Description = "D0 - Entrance",
                Address = 0x2BED1,
                Address2 = 0x2BBE0,
                Location = 0xE1FF12505C,
                Default = 0xE00077782E,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-78",
                Description = "Cave - Kanalet moat stairs",
                Address = 0x2BAE7,
                Location = 0xE11FFD5850,
                Default = 0xE000782870,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-82-1",
                Description = "House - Left side of Papahl's house",
                Address = 0x2E678,
                Location = 0xE110A5507C,
                Default = 0xE000825852,
                Connections = new ConnectionList
                {
                    new Connection("OW2-82-2", Connection.Inward),
                    new Connection("OW2-82-2", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-82-2",
                Description = "House - Right side of Papahl's house",
                Address = 0x2E6A5,
                Location = 0xE110A6507C,
                Default = 0xE000827852,
                Connections = new ConnectionList
                {
                    new Connection("OW2-82-1", Connection.Inward),
                    new Connection("OW2-82-1", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-83",
                Description = "DS - Dream shrine door",
                Address = 0x2E754,
                Location = 0xE113AA507C,
                Default = 0xE000832842,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-84",
                Description = "Cave - Cave beside Mabe Village",
                Address = 0x2F0AF,
                Location = 0xE111CD507C,
                Default = 0xE000849862,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-86",
                Description = "Cave - Crystal cave before D3",
                Address = 0x2FAFF,
                Location = 0xE111F4407C,
                Default = 0xE000861840,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-87",
                Description = "Cave - Fairy fountain at honey tree",
                Address = 0x2B7CB,
                Location = 0xE11FF3507C,
                Default = 0xE000872810,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-88",
                Description = "House - Telephone booth (Ukuku Prairie)",
                Address = 0x2E4EE,
                Location = 0xE1109C507C,
                Default = 0xE000885852,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-8A",
                Description = "SS - Seashell mansion",
                Address = 0x2F75D,
                Location = 0xE210E90870,
                Default = 0xE0008A5840,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-8C",
                Description = "D6 - Entrance",
                Address = 0x2AFCD,
                Address2 = 0x2A98A,
                Location = 0xE105D4507C,
                Default = 0xE0008C3840,
                Special = true,
                Connections = new ConnectionList
                {
                    new Connection("OW2-6C", Connection.Inward, Items.Bracelet | Items.Bombs),
                    new Connection("OW2-6C", Connection.Outward, Items.L2Bracelet | Items.Bombs),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-8D",
                Description = "Cave - Fairy fountain outside D6",
                Address = 0x2A81F,
                Location = 0xE11FAC507C,
                Default = 0xE0008D3820,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-8F",
                Description = "Cave - Entrance to raft cave",
                Address = 0x2B982,
                Location = 0xE11FF78860,
                Default = 0xE0008F0820,
                Connections = new ConnectionList
                {
                    new Connection("OW2-2F", Connection.Outward, Items.Flippers),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-92",
                Description = "Cave - Rooster's grave",
                Address = 0x2B815,
                Location = 0xE11FF45870,
                Default = 0xE000925852,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-93",
                Description = "House - Shop",
                Address = 0x2E593,
                Location = 0xE10EA1507C,
                Default = 0xE000934862,
                DeadEnd = true,
                Special = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-9C",
                Description = "Cave - Exit from D6 cave",
                Address = 0x2B706,
                Location = 0xE11FF03810,
                Default = 0xE0009C5810,
                Connections = new ConnectionList
                {
                    new Connection("OW2-9D", Connection.Inward, Items.Feather | Items.Flippers),
                    new Connection("OW2-9D", Connection.Inward, Items.Feather | Items.Boots),
                    new Connection("OW2-9D", Connection.Inward, Items.Hookshot | Items.Flippers),
                    new Connection("OW2-9D", Connection.Outward, Items.Feather | Items.Flippers),
                    new Connection("OW2-9D", Connection.Outward, Items.Feather | Items.Boots),
                    new Connection("OW2-9D", Connection.Outward, Items.Hookshot | Items.Flippers),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-9D",
                Description = "Cave - Entrance to D6 cave",
                Address = 0x2B74D,
                Location = 0xE11FF18860,
                Default = 0xE0009D3830,
                Connections = new ConnectionList
                {
                    new Connection("OW2-9C", Connection.Inward, Items.Feather | Items.Flippers),
                    new Connection("OW2-9C", Connection.Inward, Items.Feather | Items.Boots),
                    new Connection("OW2-9C", Connection.Inward, Items.Hookshot | Items.Flippers),
                    new Connection("OW2-9C", Connection.Outward, Items.Feather | Items.Flippers),
                    new Connection("OW2-9C", Connection.Outward, Items.Feather | Items.Boots),
                    new Connection("OW2-9C", Connection.Outward, Items.Hookshot | Items.Flippers),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-A1-1",
                Description = "House - Madame Meow Meow's house",
                Address = 0x2E6CB,
                Location = 0xE110A7507C,
                Default = 0xE000A13842,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-A1-2",
                Description = "House - Doghouse",
                Address = 0x2E989,
                Location = 0xE112B2507C,
                Default = 0xE000A15842,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-A2",
                Description = "House - Marin & Tarin's house",
                Address = 0x2E5F5,
                Location = 0xE110A3507C,
                Default = 0xE000A25852,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-A4",
                Description = "House - Telephone booth (signpost maze)",
                Address = 0x2E9F5,
                Location = 0xE110B4507C,
                Default = 0xE000A43842,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-AA",
                Description = "Cave - Entrance to animal village cave",
                Address = 0x2F182,
                Location = 0xE111D02840,
                Default = 0xE000AA8840,
                Connections = new ConnectionList
                {
                    new Connection("OW2-AB", Connection.Inward, Items.Boots),
                    new Connection("OW2-AB", Connection.Outward, Items.Boots),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-AB",
                Description = "Cave - Exit from animal village cave",
                Address = 0x2F1D6,
                Location = 0xE111D17840,
                Default = 0xE000AB7850,
                Connections = new ConnectionList
                {
                    new Connection("OW2-AA", Connection.Inward, Items.Boots),
                    new Connection("OW2-AA", Connection.Outward, Items.Boots),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-AC",
                Description = "D? - Southern face shrine",
                Address = 0x2E1D5,
                Location = 0xE1168F507C,
                Default = 0xE000AC5840,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-AE",
                Description = "Cave - Armos maze secret seashell cave",
                Address = 0x2FD82,
                Location = 0xE111FC6860,
                Default = 0xE000AE4870,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-B0",
                Description = "House - Library",
                Address = 0x2BA1D,
                Location = 0xE11DFA507C,
                Default = 0xE000B03832,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-B1",
                Description = "House - Ulrira's house",
                Address = 0x2E718,
                Location = 0xE110A9507C,
                Default = 0xE000B14862,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-B2",
                Description = "House - Telephone booth (Mabe Village)",
                Address = 0x2F041,
                Location = 0xE110CB507C,
                Default = 0xE000B25852,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-B3",
                Description = "House - Trendy game",
                Address = 0x2E57A,
                Location = 0xE10FA0507C,
                Default = 0xE000B35852,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-B5",
                Description = "D3 - Entrance",
                Address = 0x29482,
                Address2 = 0x29644,
                Location = 0xE10252507C,
                Default = 0xE000B56820,
                DeadEnd = true,
                Special = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-B8-1",
                Description = "Cave - Hidden exit (top) from moblin maze cave",
                Address = 0x2E37A,
                Location = 0xE10A95707C,
                Default = 0xE000B85830,
                Connections = new ConnectionList
                {
                    new Connection("OW2-B8-2", Connection.Inward, Items.Feather | Items.Bombs),
                    new Connection("OW2-C8", Connection.Inward, Items.Feather | Items.Bombs),
                    new Connection("OW2-B8-2", Connection.Outward, Items.Feather),
                    new Connection("OW2-C8", Connection.Outward, Items.Feather),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-B8-2",
                Description = "Cave - Entrance to moblin maze cave",
                Address = 0x2E2B9,
                Location = 0xE10A92307C,
                Default = 0xE000B87860,
                Connections = new ConnectionList
                {
                    new Connection("OW2-C8", Connection.Inward),
                    new Connection("OW2-B8-1", Connection.Inward, Items.Feather),
                    new Connection("OW2-C8", Connection.Outward),
                    new Connection("OW2-B8-1", Connection.Outward, Items.Feather | Items.Bombs),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-C6",
                Description = "Cave - Exit from villa cave",
                Address = 0x2EFC4,
                Location = 0xE111C9807C,
                Default = 0xE000C63850,
                Connections = new ConnectionList
                {
                    new Connection("OW2-D6", Connection.Inward, Keys.SlimeKey),
                    new Connection("OW2-D6", Connection.Outward),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-C8",
                Description = "Cave - Exit from moblin maze cave",
                Address = 0x2E2F5,
                Location = 0xE10A93307C,
                Default = 0xE000C82850,
                Connections = new ConnectionList
                {
                    new Connection("OW2-B8-2", Connection.Inward),
                    new Connection("OW2-B8-1", Connection.Inward, Items.Feather),
                    new Connection("OW2-B8-2", Connection.Outward),
                    new Connection("OW2-B8-1", Connection.Outward, Items.Feather | Items.Bombs),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-CC-1",
                Description = "House - Large house (Animal Village)",
                Address = 0x2F3FD,
                Location = 0xE110DB507C,
                Default = 0xE000CC2850,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-CC-2",
                Description = "House - Painter's house (Animal Village)",
                Address = 0x2F456,
                Location = 0xE110DD507C,
                Default = 0xE000CC7850,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-CD-1",
                Description = "Cave - HP cave behind animal village",
                Address = 0x2FBE6,
                Location = 0xE10AF7607C,
                Default = 0xE000CD8820,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-CD-2",
                Description = "House - Goat's house (Animal Village)",
                Address = 0x2F396,
                Location = 0xE110D9507C,
                Default = 0xE000CD2850,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-CD-3",
                Description = "House - Zora's house (Animal Village)",
                Address = 0x2F3CB,
                Location = 0xE110DA507C,
                Default = 0xE000CD5850,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-CF",
                Description = "Cave - Exit from Lanmolas cave",
                Address = 0x2B9FD,
                Location = 0xE11FF97860,
                Default = 0xE000CF5810,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-D3",
                Description = "D1 - Entrance",
                Address = 0x286C1,
                Address2 = 0x28281,
                Location = 0xE10017507C,
                Default = 0xE000D36822,
                DeadEnd = true,
                Special = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-D4",
                Description = "Cave - Mamu's cave",
                Address = 0x2FD16,
                Location = 0xE111FB8870,
                Default = 0xE000D48830,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-D6",
                Description = "House - Richard's villa",
                Address = 0x2EF05,
                Location = 0xE110C7507C,
                Default = 0xE000D64850,
                Connections = new ConnectionList
                {
                    new Connection("OW2-C6", Connection.Inward),
                    new Connection("OW2-C6", Connection.Outward, Keys.SlimeKey),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-D9-1",
                Description = "D5 - Entrance",
                Address = 0x2A56B,
                Address2 = 0x29F90,
                Location = 0xE104A1507C,
                Default = 0xE000D95840,
                DeadEnd = true,
                Special = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-DB",
                Description = "House - Telephone booth (Animal Village)",
                Address = 0x2F5BE,
                Location = 0xE110E3507C,
                Default = 0xE000DB7852,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-DD",
                Description = "House - Chef Bear's house (Animal Village)",
                Address = 0x2F314,
                Location = 0xE110D7507C,
                Default = 0xE000DD5842,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-E3",
                Description = "House - Crocodile's house",
                Address = 0x2FDFB,
                Location = 0xE110FE507C,
                Default = 0xE000E34830,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-E6",
                Description = "Cave - Upgrade shrine (Martha's Bay)",
                Address = 0x2B268,
                Location = 0xE11FE08870,
                Default = 0xE000E64840,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-E7",
                Description = "Cave - Exit from cave to upgrade shrine (Martha's Bay)",
                Address = 0x2B3FB,
                Location = 0xE11FE52830,
                Default = 0xE000E76820,
                Connections = new ConnectionList
                {
                    new Connection("OW2-F9", Connection.Inward, Items.Flippers),
                    new Connection("OW2-F9", Connection.Outward, Items.Flippers),
                },
            });
            Add(new Warp(this)
            {
                Code = "OW2-E8",
                Description = "House - Telephone booth (Martha's Bay)",
                Address = 0x2E51B,
                Location = 0xE1109D507C,
                Default = 0xE000E83862,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-E9",
                Description = "Cave - Magnifying lens cave (mermaid statue)",
                Address = 0x2E433,
                Location = 0xE10A986860,
                Default = 0xE000E96830,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-F4",
                Description = "Cave - Boomerang moblin's cave",
                Address = 0x2B84F,
                Address2 = 0x2B891,
                Location = 0xE11FF5487C,
                Default = 0xE000F41820,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-F6",
                Description = "House - House by the bay",
                Address = 0x2B340,
                Location = 0xE11EE3507C,
                Default = 0xE000F65842,
                DeadEnd = true,
            });
            Add(new Warp(this)
            {
                Code = "OW2-F9",
                Description = "Cave - Entrance to cave to upgrade shrine (Martha's Bay)",
                Address = 0x2B908,
                Location = 0xE11FF68870,
                Default = 0xE000F97850,
                Connections = new ConnectionList
                {
                    new Connection("OW2-E7", Connection.Inward, Items.Flippers),
                    new Connection("OW2-E7", Connection.Outward, Items.Flippers),
                },
            });
        }

        public WarpData Copy()
        {
            return new WarpData(this);
        }
    }

    public static class ZoneData
    {
        public static Connection[][] Connections = new Connection[][]
        {
            new Connection[] //Zone 1
			{
                new Connection(3, Connection.Outward, Items.Feather),
                new Connection(3, Connection.Outward, Items.Bracelet),
                new Connection(4, Connection.Outward, Items.Bracelet),
                new Connection(5, Connection.Outward, Items.Bracelet | Items.Boots | Items.Feather),
                new Connection(11, Connection.Outward, Items.Bombs),
                new Connection(11, Connection.Outward, Items.Powder),
                new Connection(11, Connection.Outward, Items.Feather),
            },
            new Connection[] //Zone 2
			{
                new Connection(11, Connection.Outward, Items.Feather),
            },
            new Connection[] //Zone 3
			{
                new Connection(1, Connection.Outward, Items.Feather),
                new Connection(1, Connection.Outward, Items.Bracelet),
                new Connection(4, Connection.Outward, Items.Bracelet),
                new Connection(11, Connection.Outward, Items.Bracelet),
                new Connection(13, Connection.Outward, Items.Bracelet),
            },
            new Connection[] //Zone 4
			{
                new Connection(1, Connection.Outward, Items.Bracelet),
                new Connection(3, Connection.Outward, Items.Bracelet),
                new Connection(5, Connection.Outward, Items.Flippers),
                new Connection(6, Connection.Outward, Items.Flippers),
                new Connection(6, Connection.Outward, Items.Hookshot),
                new Connection(7, Connection.Outward, Items.Flippers | Items.Bracelet),
                new Connection(8, Connection.Outward, MiscFlags.KanaletSwitch),
                new Connection(9, Connection.Outward, Items.Flippers),
                new Connection(12, Connection.Outward, Items.Flippers),
            },
            new Connection[] //Zone 5
			{
                new Connection(1, Connection.Outward, Items.Bracelet | Items.Boots | Items.Feather),
                new Connection(4, Connection.Outward, Items.Flippers),
                new Connection(6, Connection.Outward, Items.Flippers),
                new Connection(7, Connection.Outward, Items.Flippers | Items.Bracelet),
                new Connection(9, Connection.Outward, Items.Flippers),
                new Connection(12, Connection.Outward, Items.Flippers),
            },
            new Connection[] //Zone 6
			{
                new Connection(4, Connection.Outward, Items.Flippers),
                new Connection(5, Connection.Outward, Items.Flippers),
                new Connection(7, Connection.Outward, Items.Bracelet),
                new Connection(9, Connection.Outward, Items.Flippers),
                new Connection(12, Connection.Outward, Items.Flippers),
            },
            new Connection[] //Zone 7
			{
                new Connection(4, Connection.Outward, Items.Bracelet | Items.Flippers),
                new Connection(5, Connection.Outward, Items.Bracelet | Items.Flippers),
                new Connection(6, Connection.Outward, Items.Bracelet),
                new Connection(9, Connection.Outward, Items.Bracelet | Items.Flippers),
                new Connection(12, Connection.Outward, Items.Bracelet | Items.Flippers),
            },
            new Connection[] //Zone 8
			{
                new Connection(4, Connection.Outward, MiscFlags.KanaletSwitch),
            },
            new Connection[] //Zone 9
			{
                new Connection(4, Connection.Outward, Items.Flippers),
                new Connection(5, Connection.Outward, Items.Flippers),
                new Connection(6, Connection.Outward, Items.Flippers),
                new Connection(7, Connection.Outward, Items.Flippers | Items.Bracelet),
                new Connection(12, Connection.Outward, Items.Flippers),
            },
            new Connection[] //Zone 10
			{
                new Connection(12, Connection.Outward, Items.Hookshot),
                new Connection(12, Connection.Outward, Items.Flippers),
            },
            new Connection[] //Zone 11
			{
                new Connection(1, Connection.Outward),
                new Connection(2, Connection.Outward, Items.Feather),
                new Connection(3, Connection.Outward, Items.Bracelet),
                new Connection(13, Connection.Outward, Items.Bracelet),
            },
            new Connection[] //Zone 12
			{
                new Connection(4, Connection.Outward, Items.Flippers),
                new Connection(5, Connection.Outward, Items.Flippers),
                new Connection(6, Connection.Outward, Items.Flippers),
                new Connection(7, Connection.Outward, Items.Flippers | Items.Bracelet),
                new Connection(9, Connection.Outward, Items.Flippers),
                new Connection(10, Connection.Outward, Items.Hookshot),
                new Connection(13, Connection.Outward, Items.Bracelet),
            },
            new Connection[] //Zone 13
			{
                new Connection(3, Connection.Outward),
                new Connection(11, Connection.Outward, Items.Bracelet),
                new Connection(12, Connection.Outward, Items.Bracelet),
            },
        };
    }

    public class Warp
    {
        public WarpData parentList { get; }

        public bool Exclude { get; set; } = false;

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
        public ConnectionList Connections { get; set; } = new ConnectionList();
        public ConnectionList ZoneConnections { get; set; } = new ConnectionList();

        public Warp() { }

        public Warp(WarpData parentList)
        {
            this.parentList = parentList;
        }

        public Warp GetOriginWarp()
        {
            return parentList.Where(x => x.Destination == Location).FirstOrDefault();
        }

        public Warp GetDestinationWarp()
        {
            return parentList.Where(x => x.Location == Destination).FirstOrDefault();
        }
    }

    public class Connection
    {
        public static readonly bool Inward = true;
        public static readonly bool Outward = false;

        public int Zone { get; }
        public string Code { get; }
        public bool Direction { get; }
        public FlagsCollection Constraints { get; }
        
        public Connection(string code, bool direction, params Enum[] constraints)
        {
            Code = code;
            Direction = direction;
            Constraints = new FlagsCollection(constraints);
        }

        public Connection(int zone, bool direction, params Enum[] constraints)
        {
            Zone = zone;
            Direction = direction;
            Constraints = new FlagsCollection(constraints);
        }

        public bool IsAccessible(string allowedConstraint = null)
        {
            if (allowedConstraint != null)
            {
                if (Constraints.ToStringList().Exists(x => x != "Bombs" && x != "BowWow" && x != allowedConstraint))
                    return false;
            }
            else
            {
                if (Constraints.ToStringList().Exists(x => x != "Bombs" && x != "BowWow"))
                    return false;
            }

            return true;
        }
    }

    public class ConnectionList : List<Connection>
    {
        public List<Connection> Inward
        {
            get
            {
                if (this.Exists(x => x.Direction == Connection.Inward))
                    return this.Where(x => x.Direction == Connection.Inward).ToList();
                else
                    return new ConnectionList();
            }
        }

        public List<Connection> Outward
        {
            get
            {
                if (this.Exists(x => x.Direction == Connection.Outward))
                    return this.Where(x => x.Direction == Connection.Outward).ToList();
                else
                    return new ConnectionList();
            }
        }
    }
}

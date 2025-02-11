﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADXRandomizer
{
    public class ZoneList : List<ZoneData>
    {
        new public ZoneData this[int zone] => this.First(w => w.Zone == zone);

        public ZoneList()
        {
            Initialize();
        }

        private void Initialize()
        {
            Add(new ZoneData
            {
                Zone = 1,
                ZoneConnections = new ConnectionList
                {
                    new Connection(3, Connection.Outward, Items.Feather),
                    new Connection(3, Connection.Outward, Items.Bracelet),
                    new Connection(4, Connection.Outward, Items.Bracelet),
                    new Connection(5, Connection.Outward, Items.Bracelet | Items.Boots | Items.Feather),
                    new Connection(11, Connection.Outward, Items.Powder),
                    new Connection(11, Connection.Outward, Items.Feather),
                },
            });
            Add(new ZoneData
            {
                Zone = 2,
                ZoneConnections = new ConnectionList
                {
                    new Connection(11, Connection.Outward, Items.Feather),
                },
            });
            Add(new ZoneData
            {
                Zone = 3,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Outward, Items.Feather),
                    new Connection(1, Connection.Outward, Items.Bracelet),
                    new Connection(4, Connection.Outward, Items.Bracelet),
                    new Connection(11, Connection.Outward, Items.Bracelet),
                    new Connection(13, Connection.Outward, Items.Bracelet),
                },
            });
            Add(new ZoneData
            {
                Zone = 4,
                ZoneConnections = new ConnectionList
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
                ItemConnections = new ConnectionList
                {
                    new Connection("OW-C6", Connection.Outward, Items.Sword | Items.Shovel),
                    new Connection("OW-C6", Connection.Outward, Items.MagicRod | Items.Shovel),
                },
            });
            Add(new ZoneData
            {
                Zone = 5,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Outward, Items.Bracelet | Items.Boots | Items.Feather),
                    new Connection(4, Connection.Outward, Items.Flippers),
                    new Connection(6, Connection.Outward, Items.Flippers),
                    new Connection(7, Connection.Outward, Items.Flippers | Items.Bracelet),
                    new Connection(9, Connection.Outward, Items.Flippers),
                    new Connection(12, Connection.Outward, Items.Flippers),
                },
            });
            Add(new ZoneData
            {
                Zone = 6,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Outward, Items.Flippers),
                    new Connection(5, Connection.Outward, Items.Flippers),
                    new Connection(7, Connection.Outward, Items.Bracelet),
                    new Connection(9, Connection.Outward, Items.Flippers),
                    new Connection(12, Connection.Outward, Items.Flippers),
                },
                ItemConnections = new ConnectionList
                {
                    new Connection("OW-CE", Connection.Outward, Items.Bombs | Items.Sword),
                    new Connection("OW-CE", Connection.Outward, Items.Bombs | Items.Bow),
                    new Connection("OW-CE", Connection.Outward, Items.Bombs | Items.Hookshot),
                    new Connection("OW-CE", Connection.Outward, Items.Bombs | Items.MagicRod),
                },
            });
            Add(new ZoneData
            {
                Zone = 7,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Outward, Items.Bracelet | Items.Flippers),
                    new Connection(5, Connection.Outward, Items.Bracelet | Items.Flippers),
                    new Connection(6, Connection.Outward, Items.Bracelet),
                    new Connection(9, Connection.Outward, Items.Bracelet | Items.Flippers),
                    new Connection(12, Connection.Outward, Items.Bracelet | Items.Flippers),
                },
            });
            Add(new ZoneData
            {
                Zone = 8,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Outward, MiscFlags.KanaletSwitch),
                },
            });
            Add(new ZoneData
            {
                Zone = 9,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Outward, Items.Flippers),
                    new Connection(5, Connection.Outward, Items.Flippers),
                    new Connection(6, Connection.Outward, Items.Flippers),
                    new Connection(7, Connection.Outward, Items.Flippers | Items.Bracelet),
                    new Connection(12, Connection.Outward, Items.Flippers),
                },
            });
            Add(new ZoneData
            {
                Zone = 10,
                ZoneConnections = new ConnectionList
                {
                    new Connection(12, Connection.Outward, Items.Hookshot),
                    new Connection(12, Connection.Outward, Items.Flippers),
                },
            });
            Add(new ZoneData
            {
                Zone = 11,
                ZoneConnections = new ConnectionList
                {
                    new Connection(1, Connection.Outward),
                    new Connection(2, Connection.Outward, Items.Feather),
                    new Connection(3, Connection.Outward, Items.Bracelet),
                    new Connection(13, Connection.Outward, Items.Bracelet),
                },
                ItemConnections = new ConnectionList
                {
                    new Connection("OW-41", Connection.Outward, Items.Sword),
                    new Connection("OW-41", Connection.Outward, Items.MagicRod),
                    new Connection("OW-50", Connection.Outward, Items.Feather),
                },
            });
            Add(new ZoneData
            {
                Zone = 12,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Outward, Items.Flippers),
                    new Connection(5, Connection.Outward, Items.Flippers),
                    new Connection(6, Connection.Outward, Items.Flippers),
                    new Connection(7, Connection.Outward, Items.Flippers | Items.Bracelet),
                    new Connection(9, Connection.Outward, Items.Flippers),
                    new Connection(10, Connection.Outward, Items.Hookshot),
                    new Connection(13, Connection.Outward, Items.Bracelet),
                },
            });
            Add(new ZoneData
            {
                Zone = 13,
                ZoneConnections = new ConnectionList
                {
                    new Connection(3, Connection.Outward),
                    new Connection(11, Connection.Outward, Items.Bracelet),
                    new Connection(12, Connection.Outward, Items.Bracelet),
                },
            });
            Add(new ZoneData
            {
                Zone = 14,
                ZoneConnections = new ConnectionList
                {
                    new Connection(2, Connection.Outward),
                },
            });
            Add(new ZoneData
            {
                Zone = 15,
                ZoneConnections = new ConnectionList
                {
                    new Connection(2, Connection.Outward),
                },
            });
            Add(new ZoneData
            {
                Zone = 16,
                ZoneConnections = new ConnectionList
                {
                    new Connection(17, Connection.Outward, Items.Flippers),
                },
            });
            Add(new ZoneData
            {
                Zone = 17,
                ZoneConnections = new ConnectionList
                {
                    new Connection(13, Connection.Outward, Items.Hookshot),
                    new Connection(16, Connection.Outward, Items.Flippers),
                },
            });
            Add(new ZoneData
            {
                Zone = 19,
            });
            Add(new ZoneData
            {
                Zone = 20,
                ZoneConnections = new ConnectionList
                {
                    new Connection(4, Connection.Outward, Items.Flippers),
                    new Connection(5, Connection.Outward, Items.Flippers),
                    new Connection(6, Connection.Outward, Items.Flippers),
                    new Connection(7, Connection.Outward, Items.Flippers | Items.Bracelet),
                    new Connection(9, Connection.Outward, Items.Flippers),
                    new Connection(12, Connection.Outward, Items.Flippers),
                },
            });
        }
    }

    public class ZoneData
    {
        public int Zone { get; set; }
        public ConnectionList ZoneConnections { get; set; } = new ConnectionList();
        public ConnectionList ItemConnections { get; set; } = new ConnectionList();

        public ZoneData() { }
    }
}

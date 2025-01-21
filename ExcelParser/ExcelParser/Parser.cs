using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;

namespace ExcelParser
{
    [DelimitedRecord(",")]
    public class WarpRecord
    {
        public string num;
        public string Address;
        public string Address2;
        public string DefaultWarpValue;
        public string LocationValue;
        public string World;
        public string Index;
        public string Index2;
        public string Locked;
        public string Code;
        public string DeadEnd;
        public string WarpConnectionsInward;
        public string WarpConnectionsOutward;
        public string ZoneConnectionsInward;
        public string ZoneConnectionsOutward;
        public string ItemConnections;
        public string Description;
    }

    [DelimitedRecord(",")]
    public class ZoneRecord
    {
        public string Zone;
        public string ZoneConnections;
        public string ItemConnections;
        public string Description;
    }
}

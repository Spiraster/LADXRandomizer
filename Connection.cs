using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADXRandomizer
{
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

        public void Remove(int zone)
        {
            foreach (var connection in this.Where(x => x.Zone == zone).ToList())
                this.Remove(connection);
        }

        public void Remove(string code)
        {
            foreach (var connection in this.Where(x => x.Code == code).ToList())
                this.Remove(connection);
        }

        public void RemoveConstraint(Enum flag)
        {
            foreach (var connection in this.ToList())
                connection.Constraints.Remove(flag);
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
    }

    public class Constraint : List<Enum>
    {
        public bool AllRequired { get; set; } = false;

        public Constraint(params Enum[] flags)
        {
            foreach (var flag in flags)
                Add(flag);
        }
    }
}

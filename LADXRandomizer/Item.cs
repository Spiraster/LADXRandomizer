using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LADXRandomizer
{
    public class ItemList : List<ItemData>
    {

    }

    public class ItemData
    {
        private int world;
        private int index;
        private Enum defaultItem;

        public ItemData(int world, int index, Enum defaultItem)
        {
            this.world = world;
            this.index = index;
            this.defaultItem = defaultItem;
        }
    }
}

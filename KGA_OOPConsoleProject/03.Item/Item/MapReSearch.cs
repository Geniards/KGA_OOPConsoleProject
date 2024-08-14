using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGA_OOPConsoleProject._03.Item.Item
{
    public class MapReSearch : Item
    {
        public MapReSearch()
        {

        }

        public override int Use(int count = 0)
        {
            return effect;
        }
    }
}

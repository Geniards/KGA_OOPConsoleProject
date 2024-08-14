using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGA_OOPConsoleProject._03.Item.Item
{
    public class Potion : Item
    {
        public Potion() 
        {

        }

        public override int Use(int count = 0)
        {
            return count + effect;
        }
    }
}

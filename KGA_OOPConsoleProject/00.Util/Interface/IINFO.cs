using KGA_OOPConsoleProject.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGA_OOPConsoleProject.Interface
{
    public interface IINFO
    {
        public string name { get; protected set; }
        public int hp { get; protected set; }
        public int maxHp { get; protected set; }
        public (int,int) pos { get; set; }

        public EState state { get; protected set; }

        public void Die();
    }
}

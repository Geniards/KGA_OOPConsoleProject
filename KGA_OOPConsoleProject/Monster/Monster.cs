using KGA_OOPConsoleProject.Interface;
using KGA_OOPConsoleProject.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGA_OOPConsoleProject
{
    public class Monster : ICreate, IINFO
    {
        public string name { get; set; }
        public int hp { get; set; }
        public int maxHp { get; set; }
        public int[,] pos { get; set; }
        public EState state { get; set; }

        public void Init()
        {
        }

        public void Generate()
        {
        }

        public void Render()
        {
        }

        public void Dead()
        {

        }
    }
}

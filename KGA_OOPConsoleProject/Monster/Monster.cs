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
        public (int, int) pos { get; set; }
        public EState state { get; set; }

        public Monster(string _name, int _maxHp, (int,int) _pos) 
        {
            name = _name;
            maxHp = _maxHp;
            hp = maxHp;
            pos = _pos;
            Init();
        }

        public void Init()
        {
            state = EState.Alive;
        }

        public virtual void Generate()
        {
        }

        public virtual void Render()
        {
        }

        public virtual void Dead()
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGA_OOPConsoleProject
{
    public class Obstacle : Monster
    {
        public Obstacle(string _name, int _maxHp, (int, int) _pos) : base(_name, _maxHp, _pos)
        {
            state = Util.EState.Alive;
        }

        public override void Generate()
        {

        }

        public override void Render()
        {
        }

        public override void Dead()
        {
        }

    }
}

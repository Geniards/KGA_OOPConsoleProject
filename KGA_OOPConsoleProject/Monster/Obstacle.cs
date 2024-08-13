﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGA_OOPConsoleProject
{
    public class Obstacle : Monster
    {
        public (int, int) pos;
        public Obstacle(string _name, int _maxHp, (int, int) _pos) : base(_name, _maxHp, _pos)
        {
            pos = _pos;
        }

        public override void Generate()
        {
            if (state == Util.EState.Alive)
            {
                Console.SetCursorPosition(pos.Item2, pos.Item1);
                Console.WriteLine($"@");
            }
            else
            {
                pos.Item1 = 0;
                pos.Item2 = 0;
            }
        }

        public override void Render()
        {
        }

        public override void Dead()
        {
        }

    }
}

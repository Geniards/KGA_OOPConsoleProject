﻿using KGA_OOPConsoleProject.Interface;
using KGA_OOPConsoleProject.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGA_OOPConsoleProject
{
    public class Player : ICreate, IINFO
    {
        public int count { get; protected set; }          // 움직일 수 있는 카운트
        public int maxCount { get; protected set; }       // 총 움직일 수 있는 카운트

        public string name { get; set; }
        public int hp { get; set; }
        public int maxHp { get; set; }
        public (int, int) pos { get; set; }
        public EState state { get; set; }

        public Player(string _name)
        {
            name = _name;
            Init();
        }

        public void Init()
        {
            maxHp = 5;
            hp = maxHp;
            state = EState.Alive;
            Console.WriteLine("플레이어 초기화");
            pos = (0, 0);
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

using KGA_OOPConsoleProject.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGA_OOPConsoleProject
{
    public class Navi
    {
        Maze maze;
        Player player;

        public Navi(Maze _maze, Player _player) 
        {
            this.maze = _maze;
            this.player = _player;
        }

        public void Shortest_Path(int dir)
        {
            // BFS 확인 'M'
            if (dir == 5)
            {
                foreach (var v in maze.roadList2)
                {
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(v.Item2, v.Item1);
                    Console.Write($" ");
                    Thread.Sleep(100);
                    Console.ResetColor();
                }
                Thread.Sleep(500);
            }

        }
    }
}

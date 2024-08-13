using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGA_OOPConsoleProject.Input
{
    public class InputComponent
    {


        public void Move(int[,] graph, ref (int x, int y) pos, ref int count)
        {
            ConsoleKeyInfo cki = Console.ReadKey();
            // Up
            if (pos.x - 1 >= 0 && cki.Key == ConsoleKey.UpArrow)
            {
                if (graph[pos.x - 1, pos.y] != 0)
                    pos.x -= 1;
                else
                {
                    Console.WriteLine("위가 벽이라서 움직일 수 없습니다.");
                }

                count--;
            }
            // Down
            else if (cki.Key == ConsoleKey.DownArrow)
            {
                if (graph[pos.x + 1, pos.y] != 0)
                    pos.x += 1;
                else
                {
                    Console.WriteLine("아래가 벽이라서 움직일 수 없습니다.");
                }
                count--;
            }
            // Left
            else if (cki.Key == ConsoleKey.LeftArrow)
            {
                if (graph[pos.x, pos.y - 1] != 0)
                    pos.y -= 1;
                else
                {
                    Console.WriteLine("왼쪽이 벽이라서 움직일 수 없습니다.");
                }
                count--;
            }
            // Right
            else if (cki.Key == ConsoleKey.RightArrow)
            {
                if (graph[pos.x, pos.y + 1] != 0)
                    pos.y += 1;
                else
                {
                    Console.WriteLine("오른쪽이 벽이라서 움직일 수 없습니다.");
                }
                count--;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
            }
        }
    }
}

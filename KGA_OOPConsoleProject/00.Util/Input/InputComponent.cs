using KGA_OOPConsoleProject.Util;

namespace KGA_OOPConsoleProject.Input
{
    public class InputComponent
    {
        Maze maze = Maze.Instance;

        public ConsoleKey key;

        public void Input(ConsoleKey _key, ref (int x, int y) pos, ref int count, ref int dir)
        {
            key = _key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                case ConsoleKey.DownArrow:
                case ConsoleKey.LeftArrow:
                case ConsoleKey.RightArrow:
                    Move(ref pos, ref count, ref dir);
                    break;
                case ConsoleKey.I:
                    ShortCutKey(ref dir);
                    break;
                
                default:
                    break;
            }

        }

        private void Move(ref (int x, int y) pos, ref int count, ref int dir)
        {
            // Up
            if (key == ConsoleKey.UpArrow)
            {
                if (maze.GetMap()[pos.x - 1, pos.y] != 0)
                {
                    pos.x -= 1;
                    dir = (int)EShortKey.Up;
                }
                else
                {
                    Console.WriteLine("위가 벽이라서 움직일 수 없습니다.");
                    Thread.Sleep(500);
                }

                count--;
            }
            // Down
            else if (key == ConsoleKey.DownArrow)
            {
                if (maze.GetMap()[pos.x + 1, pos.y] != 0)
                {
                    pos.x += 1;
                    dir = (int)EShortKey.Down;
                }
                else
                {
                    Console.WriteLine("아래가 벽이라서 움직일 수 없습니다.");
                    Thread.Sleep(500);
                }
                count--;
            }
            // Left
            else if (key == ConsoleKey.LeftArrow)
            {
                if (maze.GetMap()[pos.x, pos.y - 1] != 0)
                {
                    pos.y -= 1;
                    dir = (int)EShortKey.Left;
                }
                else
                {
                    Console.WriteLine("왼쪽이 벽이라서 움직일 수 없습니다.");
                    Thread.Sleep(500);
                }
                count--;
            }
            // Right
            else if (key == ConsoleKey.RightArrow)
            {
                if (maze.GetMap()[pos.x, pos.y + 1] != 0)
                {
                    pos.y += 1;
                    dir = (int)EShortKey.Right;
                }
                else
                {
                    Console.WriteLine("오른쪽이 벽이라서 움직일 수 없습니다.");
                    Thread.Sleep(500);
                }
                count--;
            }
            else
            {
                Console.WriteLine("잘못된 입력입니다.");
                Thread.Sleep(500);
            }
        }

        private void ShortCutKey(ref int dir)
        {
            switch (key)
            {
                case ConsoleKey.I:
                    dir = (int)EShortKey.Inven;
                    break;
                case ConsoleKey.M:
                    break;

                default:
                    break;
            }
        }
    }
}

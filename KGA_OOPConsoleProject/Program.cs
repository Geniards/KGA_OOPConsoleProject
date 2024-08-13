using KGA_OOPConsoleProject.Input;
using KGA_OOPConsoleProject.Util;

namespace KGA_OOPConsoleProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            Maze maze = new Maze(11);
            maze.Generate();
            maze.Render();
            maze.searchLoard();
            
            int currLeft = Console.CursorLeft;
            int currTop = Console.CursorTop;

            Random rand = new Random();
            int num = rand.Next(2, maze.GetroadList().Count - 2);

            Player player = new Player("ABC", maze.GetCount());
            Obstacle obstacle = new Obstacle("ABC", 2, (maze.GetroadList()[num].Item1, maze.GetroadList()[num].Item2));
            InputComponent input = new InputComponent();

            (int x, int y) pos = player.pos;
            int count = player.count;
            int dir = 0;

            while (player.count >= 0 && (pos.x != maze.GetGraph().GetLength(0) - 2 || pos.y != maze.GetGraph().GetLength(1) - 2))
            {
                Console.Clear();
                // 맵 랜더
                maze.Render();
                currLeft = Console.CursorLeft;
                currTop = Console.CursorTop;

                // 장애물 랜더
                obstacle.Generate();
                Console.SetCursorPosition(currLeft, currTop);
                // 플레이어 랜더
                player.Generate();
                Console.SetCursorPosition(currLeft, currTop);
                player.Render();

                Console.WriteLine(obstacle.pos);
                Console.WriteLine(player.pos);

                // 입력
                input.Move(maze.GetGraph(), ref pos, ref count, ref dir);

                player.pos = pos;
                player.count = count;

                // 장애물 충돌 && 벽에 닿으면 박살
                if (player.pos == obstacle.pos)
                {
                    player.count--;
                    int x = obstacle.pos.Item1;
                    int y = obstacle.pos.Item2;

                    if (dir == 1)
                    {
                        if (maze.search(x - 1, y))
                        {
                            x -= 1;
                            obstacle.pos.Item1 = x;
                        }
                        else
                            obstacle.state = EState.Dead;
                    }
                    else if (dir == 2)
                    {
                        if (maze.search(x + 1, y))
                        {
                            x += 1;
                            obstacle.pos.Item1 = x;
                        }
                        else
                            obstacle.state = EState.Dead;
                    }
                    else if (dir == 3)
                    {
                        if (maze.search(x, y-1))
                        {
                            y -= 1;
                            obstacle.pos.Item2 = y;
                        }
                        else
                            obstacle.state = EState.Dead;
                    }
                    else if (dir == 4)
                    {
                        if (maze.search(x, y+1))
                        {
                            y += 1;
                            obstacle.pos.Item2 = y;
                        }
                        else
                            obstacle.state = EState.Dead;
                    }

                }

                Thread.Sleep(500);

            }

            if ((pos.x == maze.GetGraph().GetLength(0) - 2 && pos.y == maze.GetGraph().GetLength(1) - 2))
                Console.WriteLine("도착");
            else
            {
                player.Dead();
            }
        }


    }
}

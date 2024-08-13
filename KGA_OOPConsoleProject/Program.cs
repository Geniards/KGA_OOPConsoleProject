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
            List<int> list = new List<int>();
            Random rand = new Random();

            Obstacle[] obstacle = new Obstacle[5];
            for (int i = 0; i < obstacle.Length; i++)
            {
                int num = rand.Next(2, maze.GetroadList().Count - 2);

                while (!list.Contains(num))
                {
                    list.Add(num);
                }

                obstacle[i] = new Obstacle("ABC", 2, (maze.GetroadList()[list.Last()].Item1, maze.GetroadList()[num].Item2));

            }

            Player player = new Player("ABC", maze.GetCount() + obstacle.Length);
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
                //if (player.count % 2 == 0)
                {
                    for (int i = 0; i < obstacle.Length; i++)
                    {
                        obstacle[i].Generate();
                    }
                }

                Console.SetCursorPosition(currLeft, currTop);
                // 플레이어 랜더
                player.Generate();
                Console.SetCursorPosition(currLeft, currTop);
                player.Render();

                //Console.WriteLine(obstacle.pos);
                Console.WriteLine(player.pos);

                Console.SetCursorPosition(currLeft, currTop);

                // 입력
                input.Move(maze.GetGraph(), ref pos, ref count, ref dir);



                player.pos = pos;
                player.count = count;

                // 장애물 충돌 && 벽에 닿으면 박살
                for (int i = 0; i < obstacle.Length; i++)
                {
                    if (player.pos == obstacle[i].pos)
                    {
                        player.count--;
                        int x = obstacle[i].pos.Item1;
                        int y = obstacle[i].pos.Item2;

                        if (dir == 1)
                        {
                            if (maze.search(x - 1, y))
                            {
                                x -= 1;
                                obstacle[i].pos.Item1 = x;
                            }
                            else
                                obstacle[i].state = EState.Dead;
                        }
                        else if (dir == 2)
                        {
                            if (maze.search(x + 1, y))
                            {
                                x += 1;
                                obstacle[i].pos.Item1 = x;
                            }
                            else
                                obstacle[i].state = EState.Dead;
                        }
                        else if (dir == 3)
                        {
                            if (maze.search(x, y - 1))
                            {
                                y -= 1;
                                obstacle[i].pos.Item2 = y;
                            }
                            else
                                obstacle[i].state = EState.Dead;
                        }
                        else if (dir == 4)
                        {
                            if (maze.search(x, y + 1))
                            {
                                y += 1;
                                obstacle[i].pos.Item2 = y;
                            }
                            else
                                obstacle[i].state = EState.Dead;
                        }

                    }
                }

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

                    dir = 0;
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

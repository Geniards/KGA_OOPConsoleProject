using KGA_OOPConsoleProject.Input;
using KGA_OOPConsoleProject.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGA_OOPConsoleProject.Game
{
    public class GamePlay
    {
        Maze maze;
        Obstacle[] obstacle;
        Player player;
        InputComponent input;
        int stage = 0;
        bool bNextStage;

        // 초기 맵 생성
        public GamePlay()
        {
            stage = 0;
            bNextStage = false;

            // 맵 생성
            maze = new Maze(11);
            maze.Generate();
            maze.Render();
            maze.searchLoard();

            // 장애물 생성
            obstacle = new Obstacle[maze.GetGraph().GetLength(0) / 2];
            Obstacle_Reset(obstacle);

            // player 초기화 && 입력 시스템 초기화
            player = new Player("ABC", maze.GetroadList2().Count - 1 + obstacle.Length);
            input = new InputComponent();
        }


        public  void Run()
        {
            while (player.state != EState.Dead && stage < 3)
            {
                Render();
                update();
            }
            
        }

        public void update()
        {
            (int x, int y) pos = player.pos;
            int count = player.count;
            int dir = 0;

            {
                // 입력
                input.Move(maze.GetGraph(), ref pos, ref count, ref dir);

                // 플레이어 위치 초기화
                player.pos = pos;
                player.count = count;

                Obstacle_Coll(dir);

                // 최단경로 확인 'M'
                Shortest_Path(dir);
                // 스테이지를 클리어시
                NextStage();

                if (player.state == EState.Dead)
                    return;

                if (bNextStage)
                {
                    stage++;
                    reset();
                    bNextStage= false;
                }
            }
        }

        public void Render()
        {
            Console.Clear();
            // 맵 랜더
            maze.Render();
            int currLeft = Console.CursorLeft;
            int currTop = Console.CursorTop;

            // 장애물 랜더
            if (stage >= 0 )
            {
                if(player.count % 2 == 0)
                for (int i = 0; i < obstacle.Length; i++)
                {
                    obstacle[i].Generate();
                }
            }
            else
            {
                for (int i = 0; i < obstacle.Length; i++)
                {
                    obstacle[i].Generate();
                }
            }
            // 플레이어 랜더
            player.Generate();
            Console.SetCursorPosition(currLeft, currTop);
            player.Render();

            //Console.WriteLine(obstacle.pos);
            Console.WriteLine(player.pos);
        }

        public void reset()
        {
            maze = new Maze(11 + 2 * stage);
            maze.Generate();
            maze.Render();
            maze.searchLoard();

            List<int> list = new List<int>();
            Random rand = new Random();

            obstacle = new Obstacle[maze.GetGraph().GetLength(0) / 2];
            Obstacle_Reset(obstacle);

            player = new Player("ABC", maze.GetroadList2().Count - 1 + obstacle.Length);
        }

        private void Obstacle_Reset(Obstacle[] _obstacle)
        {
            List<int> list = new List<int>();
            Random rand = new Random();

            for (int i = 0; i < _obstacle.Length; i++)
            {
                int num = rand.Next(2, maze.GetroadList().Count - 2);

                while (!list.Contains(num))
                {
                    list.Add(num);
                }
                _obstacle[i] = new Obstacle("ABC", 2, (maze.GetroadList()[list.Last()].Item1, maze.GetroadList()[num].Item2));
            }
        }

        private void Obstacle_Coll(int dir)
        {
            // 장애물 충돌 && 벽에 닿으면 박살
            for (int i = 0; i < obstacle.Length; i++)
            {
                if (player.pos == obstacle[i].pos)
                {
                    //player.count--;
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
        }

        private void Shortest_Path(int dir)
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
            }
            Thread.Sleep(500);
        }

        private void NextStage()
        {
            if ((player.pos.Item1 == maze.GetGraph().GetLength(0) - 2 && player.pos.Item2 == maze.GetGraph().GetLength(1) - 2))
            {
                Console.WriteLine("도착");
                bNextStage = true;
                Thread.Sleep(500);
            }
            else
            {
                player.Dead();
                Thread.Sleep(500);
            }
        }
    }
}

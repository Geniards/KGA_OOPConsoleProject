using KGA_OOPConsoleProject.Input;
using KGA_OOPConsoleProject.Util;

namespace KGA_OOPConsoleProject.Game
{
    public class GamePlay
    {
        Maze maze = Maze.Instance;
        Obstacle[] obstacle;
        Player player;
        InputComponent input;
        Navi navi;

        int stage = 0;
        bool bNextStage;

        // 초기 맵 생성
        public GamePlay()
        {
            stage = 0;
            bNextStage = false;

            // 맵 생성
            maze.Render();
            maze.searchLoard();

            // 장애물 생성
            obstacle = new Obstacle[maze.GetMap().GetLength(0) / 2];
            Obstacle_Reset(obstacle);

            // player 초기화 && 입력 시스템 초기화
            player = new Player("ABC", maze.Get_shortest_Path().Count - 1 + obstacle.Length);
            input = new InputComponent();

            // 장애물 이동 및 길 찾기
            navi = new Navi(maze, player);
        }


        public void Run()
        {
            while (player.state != EState.Dead && stage < 3)
            {
                (int x, int y) pos = player.pos;
                int count = player.count;
                int dir = 0;

                Render();

                // 입력
                input.Move(maze.GetMap(), ref pos, ref count, ref dir);

                // 플레이어 위치 초기화
                player.pos = pos;
                player.count = count;

                update(dir);
            }

            Console.Clear();
            Console.WriteLine("게임이 끝났습니다.");

        }

        private void update(int dir)
        {
            Obstacle_Coll(dir);

            // 최단경로 확인 'M'
            navi.Shortest_Path(dir);
            // 스테이지를 클리어시
            NextStage();

            if (player.state == EState.Dead)
                return;

            if (bNextStage)
            {
                stage++;
                reset();
                bNextStage = false;
            }
        }

        private void Render()
        {
            Console.Clear();
            // 맵 랜더
            maze.Render();
            int currLeft = Console.CursorLeft;
            int currTop = Console.CursorTop;

            // 장애물 랜더
            if (stage >= 2)
            {
                if (player.count % 2 == 0)
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
            Title();
            player.Render();

            //Console.WriteLine(obstacle.pos);
            Console.WriteLine(player.pos);
        }

        private void reset()
        {
            maze.SetSize(11 + 4 * stage);
            maze.Render();
            maze.searchLoard();

            List<int> list = new List<int>();
            Random rand = new Random();

            obstacle = new Obstacle[maze.GetMap().GetLength(0) / 2];
            Obstacle_Reset(obstacle);

            player = new Player("ABC", maze.Get_shortest_Path().Count - 1 + obstacle.Length);
        }

        private void Obstacle_Reset(Obstacle[] _obstacle)
        {
            List<int> list = new List<int>();
            Random rand = new Random();

            for (int i = 0; i < _obstacle.Length; i++)
            {
                int num = rand.Next(2, maze.Get_Is_Path().Count - 2);

                while (!list.Contains(num))
                {
                    list.Add(num);
                }
                _obstacle[i] = new Obstacle("ABC", 2, (maze.Get_Is_Path()[list.Last()].Item1, maze.Get_Is_Path()[num].Item2));
            }
        }

        private void Obstacle_Coll(int dir)
        {
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
        }

        private void NextStage()
        {
            if ((player.pos.Item1 == maze.GetMap().GetLength(0) - 2 && player.pos.Item2 == maze.GetMap().GetLength(1) - 2))
            {
                Console.WriteLine("도착");
                bNextStage = true;
                if(stage < 2)
                    Console.WriteLine("다음 스테이지로 이동합니다.");
                Thread.Sleep(1000);
            }
            else if(player.count < 0)
            {
                player.Dead();
                Thread.Sleep(1000);
            }
        }

        private void Title()
        {
            Console.WriteLine($"{stage + 1} Stage");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}

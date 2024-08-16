using KGA_OOPConsoleProject._03.Item.Inventory;
using KGA_OOPConsoleProject._03.Item.Item;
using KGA_OOPConsoleProject.Input;
using KGA_OOPConsoleProject.Util;
using System;
using System.Numerics;

namespace KGA_OOPConsoleProject.Game
{
    public class GamePlay
    {
        Maze maze = Maze.Instance;
        Obstacle[] obstacle;
        Player player;
        InputComponent input;
        ItemManager itemManager;
        Inventory inventory;

        int stage = 0;
        int score = 0;
        bool bNextStage;

        // 초기 맵 생성
        public GamePlay()
        {
            stage = 0;
            bNextStage = false;

            // 맵 생성
            maze.Render();
            maze.searchLoard();

            // 아이템 생성
            itemManager = new ItemManager();
            inventory = new Inventory();

            // 장애물 생성
            obstacle = new Obstacle[maze.GetMap().GetLength(0) / 2];
            Obstacle_Reset(obstacle);

            // player 초기화 && 입력 시스템 초기화
            player = new Player("ABC", maze.Get_shortest_Path().Count - 1 + obstacle.Length);
            input = new InputComponent();
        }


        public void Run()
        {
            while (player.state != EState.Dead && stage < 1)
            {
                (int x, int y) pos = player.pos;
                int count = player.hp;
                int dir = 0;

                Render();

                // 입력
                input.Input(Console.ReadKey().Key, ref pos, ref count, ref dir);

                // 플레이어 위치 초기화
                player.pos = pos;
                player.hp = count;

                update(dir);
            }

            Console.Clear();
            Console.WriteLine($"총 점수는 {score}점");
            Console.WriteLine($"<점수의 합계>");
            Console.WriteLine($"이동 포인트 : {score}점");
            Console.WriteLine($"남은 아이템 포인트 : ");
            Console.WriteLine("게임이 끝났습니다.");

        }

        private void update(int dir)
        {
            // 장애물과 충돌 했는지 및 아이템이 발생했을 때
            inventory.Item_PickUp(Obstacle_Coll(dir));
            
            if(dir==(int)EShortKey.Inven)
                inventory.Inven_open(player);
    
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
            if (stage >= (int)EStage.ESTAGE_MAX - 1)
            {
                if (player.hp % 2 == 0)
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

            Console.WriteLine("<플레이어의 위치>");
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
                _obstacle[i].OnDied += itemManager.Create;
            }
        }

        private Items Obstacle_Coll(int dir)
        {
            (int, int)[] dirPos = { (-1, 0), (1, 0), (0, -1), (0, 1) };

            if (dir - 1 > dirPos.Length)
                return null;

            // 장애물 충돌 && 벽에 닿으면 박살
            for (int i = 0; i < obstacle.Length; i++)
            {
                if (player.pos == obstacle[i].pos)
                {
                    player.hp--;
                    int x = obstacle[i].pos.Item1;
                    int y = obstacle[i].pos.Item2;

                    // TODO : 장애물이 부서진 위치에서 i를 누르면 dir이 상호작용이 되서 문제가 발생
                    // 
                    if (maze.search(x + dirPos[dir - 1].Item1, y + dirPos[dir - 1].Item2))
                    {
                        obstacle[i].pos = (x + dirPos[dir - 1].Item1, y + dirPos[dir - 1].Item2);
                        return null;
                    }
                    else
                    {
                        obstacle[i].state = EState.Dead;
                        obstacle[i].Dead();
                        return itemManager.GetItem();
                    }
                }
            }

            return null;
        }

        private void NextStage()
        {
            if (player.hp <= 0)
            {
                player.Dead();
                Thread.Sleep(1000);
                return;
            }

            if ((player.pos.Item1 == maze.GetMap().GetLength(0) - 2 && player.pos.Item2 == maze.GetMap().GetLength(1) - 2))
            {
                if (stage < (int)EStage.ESTAGE_MAX)
                    score += player.hp;

                Console.WriteLine("도착");
                bNextStage = true;

                if (stage < (int)EStage.ESTAGE_MAX - 1)
                    Console.WriteLine("다음 스테이지로 이동합니다.");

                Thread.Sleep(1000);
            }
        }

        private void Title()
        {
            Console.WriteLine($"{stage + 1} Stage \t 점수 : {score}");
            Console.WriteLine();
            player.Render();
        }
    }
}

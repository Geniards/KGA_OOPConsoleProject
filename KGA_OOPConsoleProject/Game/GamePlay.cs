using KGA_OOPConsoleProject._01.Unit.Monster;
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

        Obstacle[] obstacles;
        ObstacleManager obstacleManager;

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
            maze.searchLoard();

            // 아이템 생성
            itemManager = new ItemManager();
            inventory = new Inventory();

            // 장애물 생성
            obstacleManager = new ObstacleManager();
            obstacles = obstacleManager.CreateObstacles(maze.GetMap().GetLength(0) / 2);

            // player 초기화 && 입력 시스템 초기화
            player = new Player("Player", maze.Get_shortest_Path().Count - 1 + obstacles.Length);
            input = new InputComponent();
        }

        private void Title()
        {
            Console.Clear();

            string[] titleLines = new string[]
            {

               @"___  ___  ___   ______ _____   _____   ___  ___  ___ _____ ",
               @"|  \/  | / _ \ |___  /|  ___| |  __ \ / _ \ |  \/  ||  ___|",
               @"| .  . |/ /_\ \   / / | |__   | |  \// /_\ \| .  . || |__  ",
               @"| |\/| ||  _  |  / /  |  __|  | | __ |  _  || |\/| ||  __| ",
               @"| |  | || | | |./ /___| |___  | |_\ \| | | || |  | || |___ ",
               @"\_|  |_/\_| |_/\_____/\____/   \____/\_| |_/\_|  |_/\____/ "
            };

            Console.ForegroundColor = ConsoleColor.Green;
            foreach (string line in titleLines)
            {
                Console.WriteLine(line);
                Thread.Sleep(100);
            }
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("\t시작하려면 아무키나 눌러 주세요.");
            Console.ReadKey();
        }

        public void Run()
        {
            Title();

            while (player.state != EState.Dead && stage < (int)EStage.ESTAGE_MAX)
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

            GameClear();
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
                StageReset(stage);
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
                    for (int i = 0; i < obstacles.Length; i++)
                    {
                        obstacles[i].Generate();
                    }
            }
            else
            {
                for (int i = 0; i < obstacles.Length; i++)
                {
                    obstacles[i].Generate();
                }
            }
            // 플레이어 랜더
            player.Generate();
            Console.SetCursorPosition(currLeft, currTop);

            // 점수판 표시
            ScoreBoard();


        }

        private void StageReset(int _stage)
        {
            stage = _stage;
            maze.SetSize(11 + 4 * stage);
            maze.searchLoard();
            
            List<int> list = new List<int>();
            Random rand = new Random();

            obstacles = obstacleManager.CreateObstacles(maze.GetMap().GetLength(0) / 2);

            // 스테이지가 변경될때 마다 캐릭터의 위치 및 기본값 초기화
            player.maxHp = maze.Get_shortest_Path().Count - 1 + obstacles.Length;
            player.hp = player.maxHp;
            player.pos = (1, 1);
            
            // 스테이지가 0이면 처음부터이기에 초기화
            if(stage == 0)
            {
                player.state = EState.Alive;
                inventory.InvenClear();
                score = 0;
            }
        }

        // TODO : Obstacle_Reset을  Obstacle에 넣기
        // 
        private Items Obstacle_Coll(int dir)
        {
            (int, int)[] dirPos = { (-1, 0), (1, 0), (0, -1), (0, 1) };

            if (dir - 1 > dirPos.Length)
                return null;

            // 장애물 충돌 && 벽에 닿으면 박살
            for (int i = 0; i < obstacles.Length; i++)
            {
                if (player.pos == obstacles[i].pos)
                {
                    player.hp--;
                    int x = obstacles[i].pos.Item1;
                    int y = obstacles[i].pos.Item2;

                    if (maze.search(x + dirPos[dir - 1].Item1, y + dirPos[dir - 1].Item2))
                    {
                        obstacles[i].pos = (x + dirPos[dir - 1].Item1, y + dirPos[dir - 1].Item2);
                        return null;
                    }
                    else
                    {
                        obstacles[i].state = EState.Dead;
                        itemManager.Create();
                        return itemManager.GetItem();
                    }
                }
            }

            return null;
        }

        private void NextStage()
        {
            if (player.hp < 0)
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

        private void ScoreBoard()
        {
            Console.WriteLine($"{stage + 1} Stage \t 점수 : {score}");
            Console.WriteLine();
            // 플레이어 이동가능 범위
            player.Render();

            Console.WriteLine("(아이템창 확인은 I를 눌러주세요.)");
            Console.WriteLine();

            Console.WriteLine("<플레이어의 위치>");
            Console.WriteLine(player.pos);
        }

        public void GameClear()
        {
            Console.Clear();
            Console.WriteLine($"<점수의 합계>");
            Console.WriteLine("===================================");
            Console.WriteLine($"이동 포인트 : {score}점");
            int i = 0; 
            int itemScore = 0;
            Console.WriteLine("-----------------------------------");
            inventory.ItemScore(ref itemScore);
            Console.WriteLine();
            Console.WriteLine($"남은 아이템 포인트 :{" ",4} {itemScore}점");
            Console.WriteLine("-----------------------------------");

            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"총 점수는 {score + itemScore}점");
            Console.ResetColor();
            Console.WriteLine("===================================");

            Console.WriteLine("게임이 끝났습니다.");

            Console.WriteLine();
            Console.WriteLine("5초 뒤 타이틀 화면으로 이동합니다.");

            StageReset(0);
            Thread.Sleep(5000);
        }
    }
}

using KGA_OOPConsoleProject.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace KGA_OOPConsoleProject
{
    #region DFS Maze Create
    /*
    * 1 - 먼저 미로를 구성할 2차원 배열을 생성하고, 
    *      이 배열을 전부 벽으로 초기화한다.
    *      
    * 2 - 그중에서 시작점을 하나 정해서 벽 대신 빈 공간으로 바꾸고, 
    *      시작점에 방문했음을 방문 여부 배열에 표시한다.
    *      
    * 3 - 그 후에 4방향 중 무작위로 하나의 방향을 정해서 그곳으로 이동하고 
    *      벽을 빈 공간으로 교체한다.
    *      
    * 4 - 그리고 그 위치를 기준으로 다시 탐색 후 방문하지 않은 공간에 방문하여 
    *      벽을 빈 공간으로 교체한다.
    *      
    * 5 - 만약 더 이상 진행할 수 없다면 3의 과정으로 돌아간 후, 
    *      남은 방향 중 하나를 선택해서 다시 4의 과정을 진행한다.
    * 
    */
    #endregion

    public class Maze : ICreate
    {
        const int INF = 0;
        int[,] graph;
        int[,] graphTemp;
        int size = 0;
        public List<(int, int)> roadList = new List<(int, int)> ();
        public List<(int, int)> roadList2 = new List<(int, int)> ();

        //bool[] bVisite;
        //int[] distance;
        //int start = 0;

        public int[,] GetGraph() { return graph; }
        public List<(int, int)> GetroadList() { return roadList; }
        public List<(int, int)> GetroadList2() { return roadList2; }

        // 상하좌우 이동
        private int[] DirY = { 0, 0, -2, 2 };
        private int[] DirX = { -2, 2, 0, 0 };

        public Maze(int _size = 0)
        {
            size = _size;
            Init();
        }

        public void Init()
        {
            // 맵 초기화
            graph = new int[size,size];
            graphTemp = new int[size,size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    graph[i, j] = INF;
                    graphTemp[i, j] = 0;
                }
            }

            // 방문 및 거리 초기화
            //bVisite = new bool[size];
            //distance = new int[size];
        }

        public void Generate()
        {
            DFS(graph, 1,1);
        }

        public void Render()
        {
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    if (graph[i, j] == INF)
                    {
                        // 벽
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("#");
                        Console.ResetColor();
                    }
                    else
                    {
                        if (i == graph.GetLength(0) - 2 && j == graph.GetLength(1) - 2)
                        {
                            // 도착지점
                            Console.Write("0");
                        }
                        else
                        {
                            // 이동통로
                            Console.Write(" ");
                        }
                        graphTemp[i, j] = 1;

                        (int, int) num;
                        num.Item1 = i;
                        num.Item2 = j;
                        roadList.Add(num);
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public void searchLoard()
        {
            BFS(graphTemp);
        }

        public bool search(int x, int y)
        {
            if (graph[x, y] == 1)
                return true;
            return false;
        }



        private void DFS(int[,] map, int X, int Y)
        {
            // 시작 위치 세팅
            map[X, Y] = 1;          // 0 이동가능한 공간
            //bVisite[start] = true;  
            int CurX = X;
            int CurY = Y;

            // 3 - 그 후에 4방향 중 무작위로 하나의 방향을 정해서 그곳으로 이동하고
            //      벽을 빈 공간으로 교체한다.
            Random random = new Random();
            int[] directions = { 0, 1, 2, 3 };
            directions = Shuffle(directions, random);
            
            foreach (int dir in directions)
            {
                int nextX = X + DirX[dir];
                int nextY = Y + DirY[dir];

                if (IsValidMove(graph, nextX, nextY))
                {
                    graph[nextX, nextY] = 1;    // 빈 공간으로 설정
                    graph[X + DirX[dir] / 2, Y + DirY[dir] / 2] = 1; // 벽 제거
                    DFS(graph, nextX, nextY);
                }
            }
        }

        private bool IsValidMove(int[,] map, int nextX, int nextY)
        {
            if(nextX > 0 && nextX < map.GetLength(0)-1 &&
                nextY > 0 && nextY < map.GetLength(1)-1 &&
                map[nextX, nextY] == INF)
                return true;

            return false;
        }

        private int[] Shuffle(int[] array, Random random)
        {
            for(int i = 0; i < array.Length; i++)
            {
                int j = random.Next(i+1);
                int tempInt = array[i];
                array[i] = array[j];
                array[j] = tempInt;
            }

            return array;
        }

        private void BFS(int[,] maps)
        {
            #region 풀이
            int w = maps.GetLength(0);
            int h = maps.GetLength(1);

            Queue<(int, int)> queue = new Queue<(int x, int y)>();

            // 부모의 위치
            Dictionary<(int x, int y), (int x, int y)> parents = new Dictionary<(int x, int y), (int x, int y)>();
            // 움직일 방향(상하좌우)
            (int, int)[] dirs = new (int x, int y)[4] { (-1, 0), (1, 0), (0, -1), (0, 1) };

            // 시작위치 설정
            queue.Enqueue((1, 1));

            // 시작위치 초기화
            maps[1, 1] = 0;

            // 도착지점 
            bool found = false;

            // 움직일 경로를 파악할게 남았다면 확인한다.
            while (queue.Count > 0)
            {
                // 현재 위치
                (int x, int y) cur = queue.Dequeue();

                // 도착지점에 도달했는지 파악
                if (cur.x == w - 2 && cur.y == h - 2) // 도달!
                {
                    found = true;
                    break;
                }

                foreach ((int x, int y) dir in dirs)
                {
                    // 시작점(0,0)에서 한칸씩 상 하 좌 우 이동 후
                    // 움직일 공간이 있는지를 파악
                    (int x, int y) p = (cur.x + dir.x, cur.y + dir.y);

                    // 이동후 좌표가 범위를 벗어날시 다른 움직임을 확인한다.
                    if (p.x < 1 || p.x >= w-1 || p.y < 1 || p.y >= h-1)
                        continue;

                    // 아직 움직이지 않은 곳이라면 1 움직인 곳은 0으로 표시한다.
                    if (maps[p.x, p.y] != 1)
                        continue;


                    parents[p] = cur;   // 이동하기 전 위치를 부모의 값으로 저장한다.
                    queue.Enqueue(p);   // 이동한 위치의 값을 큐에 저장 후 다음에 불러내도록 한다.
                    maps[p.x, p.y] = 0; // 방문했으니 0으로 표시

                }
            }

            if (found)
            {
                int result = 0;
                // 목적지의 부모의 위치
                (int x, int y) cur = (w - 2, h - 2);
                // 시작점까지 거슬러 올라가자(최단 루트)
                while (!(cur.x == 1 && cur.y == 1))
                {
                    cur = parents[cur];
                    roadList2.Add(cur);

                    // 이때 비용이 있다면 다 더해주면 최단 비용.
                    result++;
                }
                Console.SetCursorPosition(maps.GetLength(0), maps.GetLength(1));
            }
            #endregion
        }
    }
}

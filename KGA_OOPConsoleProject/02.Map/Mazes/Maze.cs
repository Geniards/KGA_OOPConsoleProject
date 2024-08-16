using KGA_OOPConsoleProject.Interface;
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
        // 싱글톤
        private static readonly Maze _instance;
        static Maze()
        {
            _instance = new Maze(11);
        }
        private Maze(int _size = 0)
        {
            size = _size;
            Init();
            Generate();
        }
        public static Maze Instance
        {
            get { return _instance; }
        }

        // 맵 사이즈 리셋
        public void SetSize(int _size = 0)
        {
            size = _size;
            Init();
            Generate();
        }

        const int INF = 0;
        private int[,] graph;
        private int size = 0;
        private List<(int, int)> Is_Path = new List<(int, int)> ();
        private List<(int, int)> shortest_Path = new List<(int, int)> ();

        public int[,] GetMap() { return graph; }
        public List<(int, int)> Get_Is_Path() { return Is_Path; }
        public List<(int, int)> Get_shortest_Path() { return shortest_Path; }

        // 상하좌우 이동(벽 때문에)
        private int[] DirY = { 0, 0, -2, 2 };
        private int[] DirX = { -2, 2, 0, 0 };

        public void Init()
        {
            // 맵 초기화
            graph = new int[size,size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    graph[i, j] = INF;
                }
            }
        }

        public void Generate()
        {
            DFS(1, 1);

            Is_Path.Clear();
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    if (graph[i, j] != INF)
                    {
                        // 모든 이동 통로
                        (int, int) num;
                        num.Item1 = i;
                        num.Item2 = j;
                        Is_Path.Add(num);
                    }
                }
                Console.WriteLine();
            }
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
                    }
                }
                Console.WriteLine();
            }
        }

        public void searchLoard()
        {
            FindshortestPath();
        }

        public bool search(int x, int y)
        {
            if (x > 0 && x < graph.GetLength(0) - 1 &&
                y > 0 && y < graph.GetLength(1) - 1)
            {
                if (graph[x, y] == 1)
                    return true;

            }

            return false;
        }

        private void DFS(int X, int Y)
        {
            // 시작 위치 세팅
            graph[X, Y] = 1;          // 1 이동가능한 공간

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

                if (IsValidMove(nextX, nextY))
                {
                    graph[nextX, nextY] = 1;    // 빈 공간으로 설정
                    graph[X + DirX[dir] / 2, Y + DirY[dir] / 2] = 1; // 벽 제거
                    DFS(nextX, nextY);
                }
            }
        }

        private bool IsValidMove(int nextX, int nextY)
        {
            if(nextX > 0 && nextX < graph.GetLength(0)-1 &&
                nextY > 0 && nextY < graph.GetLength(1)-1 &&
                graph[nextX, nextY] == INF)
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

        // BFS
        private void FindshortestPath()
        {
            shortest_Path.Clear();
            Queue<(int, int)> queue = new Queue<(int x, int y)>();

            // 부모의 위치
            Dictionary<(int x, int y), (int x, int y)> parents = new Dictionary<(int x, int y), (int x, int y)>();
            // 움직일 방향(상하좌우)
            (int, int)[] dirs = new (int x, int y)[4] { (-1, 0), (1, 0), (0, -1), (0, 1) };
            // 방문했는지 파악
            bool[,] visited = new bool[size, size];

            // 시작위치 설정
            queue.Enqueue((1, 1));

            // 시작 방문표시 초기화
            visited[1, 1] = true;

            // 도착지점 
            bool found = false;

            // 움직일 경로를 파악할게 남았다면 확인한다.
            while (queue.Count > 0)
            {
                // 현재 위치
                (int x, int y) cur = queue.Dequeue();

                // 도착지점에 도달했는지 파악
                if (cur.x == graph.GetLength(0) - 2 && cur.y == graph.GetLength(1) - 2) // 도달!
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
                    if (p.x < 1 || p.x >= graph.GetLength(0) - 1 
                        || p.y < 1 || p.y >= graph.GetLength(1) - 1)
                        continue;

                    // 아직 움직이지 않은 곳이라면 1 움직인 곳은 0으로 표시한다.
                    if (graph[p.x, p.y] != 1 || visited[p.x, p.y])
                        continue;

                    parents[p] = cur;   // 이동하기 전 위치를 부모의 값으로 저장한다.
                    queue.Enqueue(p);   // 이동한 위치의 값을 큐에 저장 후 다음에 불러내도록 한다.
                    visited[p.x, p.y] = true; // 방문했으니 true로 표시
                }
            }

            if (found)
            {
                // 목적지의 부모의 위치
                (int x, int y) cur = (graph.GetLength(0) - 2, graph.GetLength(1) - 2);
                // 시작점까지 거슬러 올라가자(최단 루트)
                while (!(cur.x == 1 && cur.y == 1))
                {
                    cur = parents[cur];
                    shortest_Path.Add(cur);
                }
            }
        }
    }
}

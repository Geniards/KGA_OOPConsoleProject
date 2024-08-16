using KGA_OOPConsoleProject._03.Item.Item;
using KGA_OOPConsoleProject.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KGA_OOPConsoleProject._01.Unit.Monster
{
    public class ObstacleManager
    {
        private Maze maze = Maze.Instance;
        private ItemManager itemManager;

        public ObstacleManager(ItemManager itemManager)
        {
            this.itemManager = itemManager;
        }

        public Obstacle[] CreateObstacles(int count)
        {
            Obstacle[] obstacles = new Obstacle[count];
            List<int> list = new List<int>();
            Random rand = new Random();

            for (int i = 0; i < count; i++)
            {
                int num;
                do
                {
                    num = rand.Next(2, maze.Get_Is_Path().Count - 2);
                } while (list.Contains(num));

                list.Add(num);
                var path = maze.Get_Is_Path();
                obstacles[i] = new Obstacle("ABC", 2, (path[list.Last()].Item1, path[num].Item2));
            }

            return obstacles;
        }

        public Items Obstacle_Coll(ref int dir, ref Player player, ref Obstacle[] obstacles)
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
    }
}

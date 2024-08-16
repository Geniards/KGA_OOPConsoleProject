using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGA_OOPConsoleProject._01.Unit.Monster
{
    public class ObstacleManager
    {
        private Maze maze = Maze.Instance;

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
    }
}

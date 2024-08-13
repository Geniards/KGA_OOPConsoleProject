using KGA_OOPConsoleProject.Input;

namespace KGA_OOPConsoleProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            Maze maze = new Maze(11);
            maze.Generate();
            maze.Render();

            Player player = new Player("ABC", maze.GetCount());
            Monster obstacle = new Obstacle("ABC", 2, (3, 3));
            InputComponent input = new InputComponent();

            (int x, int y) pos = player.pos;
            int count = player.count;
            
            while (pos.x != maze.GetGraph().GetLength(0)-2 || pos.y != maze.GetGraph().GetLength(1) - 2)
            {
                Console.Clear();
                maze.Render();
                int currLeft = Console.CursorLeft;
                int currTop = Console.CursorTop;
                player.Generate();
                Console.SetCursorPosition(currLeft, currTop);
                player.Render();

                input.Move(maze.GetGraph(), ref pos, ref count);
                player.pos = pos;
                player.count = count;

                Console.WriteLine("====================");
                Console.WriteLine("Player");
                Console.WriteLine(player.pos);
                Console.WriteLine(player.count);

                Thread.Sleep(1000);
            }
            Console.WriteLine("도착");
        }


    }
}

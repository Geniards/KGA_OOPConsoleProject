namespace KGA_OOPConsoleProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            Maze maze = new Maze(11);
            maze.Generate();
            maze.Render();
        }

        
    }
}

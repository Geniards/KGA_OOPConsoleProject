using KGA_OOPConsoleProject.Game;


namespace KGA_OOPConsoleProject
{
    public class Program
    {
        static void Main(string[] args)
        {
            int num = 0;
            GamePlay gamePlay = new GamePlay();
         
            while (true)
            {
                gamePlay.Run();
            }
        }

    }
}

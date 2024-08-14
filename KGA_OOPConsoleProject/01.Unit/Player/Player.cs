using KGA_OOPConsoleProject.Interface;
using KGA_OOPConsoleProject.Util;

namespace KGA_OOPConsoleProject
{
    public class Player : ICreate, IINFO
    {
        public int count { get; set; }          // 움직일 수 있는 카운트
        public int maxCount { get; set; }       // 총 움직일 수 있는 카운트

        public string name { get; set; }
        public int hp { get; set; }
        public int maxHp { get; set; }
        public (int, int) pos { get; set; }
        public EState state { get; set; }

        public Player(string _name, int _maxCount)
        {
            name = _name;
            maxCount = _maxCount;
            count = maxCount;
            Init();
        }

        public void Init()
        {
            maxHp = 5;
            hp = maxHp;
            state = EState.Alive;
            Console.WriteLine("플레이어 초기화");
            pos = (1, 1);
        }

        public void Generate()
        {
            Console.SetCursorPosition(pos.Item2, pos.Item1);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(" ");
            Console.ResetColor();
        }

        public void Render()
        {
            Console.WriteLine($"움직일 수 있는 횟수 {count} / {maxCount}");
        }

        public void Dead()
        {
            if (count < 0)
            {
                Console.WriteLine("에너지가 다 떨어졌습니다.");
                state = EState.Dead;
            }
        }
    }
}

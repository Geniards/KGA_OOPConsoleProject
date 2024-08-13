using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGA_OOPConsoleProject
{
    public class Obstacle : Monster
    {
        private bool bButton = true;
        public void SetButton( bool Button) { bButton = Button; }
        public Obstacle(string _name, int _maxHp, (int, int) _pos) : base(_name, _maxHp, _pos)
        {
            state = Util.EState.Alive;
        }

        public override void Generate()
        {
            if (bButton)
            {
                Console.SetCursorPosition(pos.Item2, pos.Item1);
                Console.WriteLine($"@");
            }
            else
            {
                
            }
        }

        public override void Render()
        {
        }

        public override void Dead()
        {
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KGA_OOPConsoleProject.Util
{
    public class DoubleBuffer
    {
        private char[,] backBuffer;
        private char[,] frontBuffer;
        private int width;
        private int height;

        public DoubleBuffer(int _width, int _height)
        {
            this.width = _width;
            this.height = _height;
            backBuffer = new char[this.width, this.height];
            frontBuffer = new char[this.width, this.height];

            ResetBuffer(backBuffer);
            ResetBuffer(frontBuffer);
        }

        public void Draw(Action<char[,]> drawAction)
        {
            ResetBuffer(backBuffer);
            drawAction(backBuffer);
            UpdateBuffer();
        }

        private void UpdateBuffer()
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (backBuffer[i, j] != frontBuffer[i, j])
                    {
                        Console.SetCursorPosition(j, i);
                        Console.Write(backBuffer[i, j]);
                        frontBuffer[i, j] = backBuffer[i, j];
                    }
                }
            }
        }

        private void ResetBuffer(char[,] buffer)
        {
            for(int i=0; i< height; i++)
            {
                for (int j=0; j< width; j++)
                {
                    buffer[i, j] = ' ';
                }
            }
        }
    }
}

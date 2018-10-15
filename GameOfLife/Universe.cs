using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Universe
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public bool[,] cells;

        public Universe(int width, int height)
        {
            Width = width;
            Height = height;
            cells = new bool[Height, Width];

            Console.SetWindowSize(Width, Height);

            GenerateField();
            DrawGame();
        }

        void GenerateField()
        {
            Random generator = new Random();
            int number;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    number = generator.Next(2);
                    cells[i, j] = false;// ((number == 0) ? false : true);
                    cells[5, 5] = true;
                }
            }
        }

        private void DrawGame()
        {
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(cells[i, j] ? "x" : " ");
                    if (j == Width - 1) Console.WriteLine("\r");
                }
            }
            Console.SetCursorPosition(0, Console.WindowTop);
        }

    }
}

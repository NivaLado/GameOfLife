using System;

namespace GameOfLife.Services
{
    internal class ConsoleRenderer
    {
        public void Render(bool[,] grid)
        {
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    Console.Write(grid[j, i] ? "x" : " ");

                    if (j == grid.GetLength(0) - 1 && !(i == grid.GetLength(1) - 1))
                        Console.WriteLine("\r");
                }
            }
            Console.SetCursorPosition(0, 0);
        }

        public void Color( ConsoleColor color )
        {
            Console.ForegroundColor = color;
        }

        public void CursorVisible(bool visibility)
        {
            Console.CursorVisible = visibility;
        }
    }
}
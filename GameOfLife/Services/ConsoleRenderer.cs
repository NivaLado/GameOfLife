using System;
using GameOfLife.Interfaces;

namespace GameOfLife.Services
{
    internal class ConsoleRenderer : IRenderer
    {
        public ConsoleRenderer()
        {
            Color(ConsoleColor.Green);
            CursorVisible(false);
        }

        public void Render(bool[,] grid)
        {
            DrawTopLine(grid.GetLength(0));
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    if (j == 0)
                        Console.Write("║");

                    Console.Write(grid[j, i] ? "x" : " "); 

                    if (j == grid.GetLength(0) - 1) //&& !(i == grid.GetLength(1) - 1)
                    {
                        Console.Write("║");
                        Console.WriteLine("\r");
                    }       
                }
            }
            DrawBottomLine(grid.GetLength(0));
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

        private void DrawTopLine(int width)
        {
            Console.Write("╔");

            for (int i = 0; i < width; i++)
                Console.Write("═");

            Console.Write("╗");

            Console.WriteLine("\r");
        }

        private void DrawBottomLine(int width)
        {
            Console.Write("╚");

            for (int i = 0; i < width; i++)
                Console.Write("═");

            Console.Write("╝");

            Console.WriteLine("\r");
        }
    }
}
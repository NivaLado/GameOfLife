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

        public void RenderOneField(Universe field, int xOffset, int yOffset)
        {
            var x = Console.CursorLeft; var y = Console.CursorTop;
            FieldInfo(field.uState.cells, field.uState.generation , xOffset, y);

            Console.SetCursorPosition(xOffset, Console.CursorTop);
            DrawTopLine(field.uState.grid.GetLength(0));
            for (int i = 0; i < field.uState.grid.GetLength(1); i++)
            {
                for (int j = 0; j < field.uState.grid.GetLength(0); j++)
                {
                    if (j == 0)
                    {
                        Console.SetCursorPosition(xOffset, Console.CursorTop);
                        Console.Write("║");
                    }

                    Console.Write(field.uState.grid[j, i] ? "x" : " ");

                    if (j == field.uState.grid.GetLength(0) - 1)
                    {
                        Console.Write("║");
                        Console.WriteLine("\r");
                        Console.SetCursorPosition(xOffset, Console.CursorTop);
                    }
                }
            }
            DrawBottomLine(field.uState.grid.GetLength(0));
            Console.SetCursorPosition(xOffset, yOffset);
        }

        public void RenderMultipleFields(Universe[] _universe)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            int borderOffset = 2;
            int xOffset = 0, yOffset = 0;

            int height = _universe[0].uState.grid.GetLength(1);
            int width = _universe[0].uState.grid.GetLength(0);

            for (int i = 0; i < _universe.Length; i++)
            {
                if (xOffset >= 120)
                {
                    xOffset = 0;
                    yOffset += height + borderOffset + 2;
                }

                RenderOneField(_universe[i], xOffset, yOffset);

                xOffset += width + borderOffset;
            }
            //System.Threading.Thread.Sleep(1000);
        }

        public void Clear()
        {
            Console.Clear();
        }

        private void Color(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        private void CursorVisible(bool visibility)
        {
            Console.CursorVisible = visibility;
        }

        private void DrawTopLine(int width)
        {
            Console.Write("╔");

            for (int i = 0; i < width; i++)
            {
                Console.Write("═");
            }

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

        private void FieldInfo(int cells, int generation, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.WriteLine("c:" + cells);
            Console.SetCursorPosition(x, y + 1);
            Console.WriteLine("g:" + generation);
            //Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop);
        }
    }
}
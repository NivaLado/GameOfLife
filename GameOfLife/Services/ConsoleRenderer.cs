using System;
using System.Collections.Generic;
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
            Console.SetCursorPosition(xOffset, yOffset);
            FieldColor(field.uState.notDead);

            var y = Console.CursorTop;
            var grid = field.uState.grid;
            FieldInfo(
                grid.GetLength(0),
                field.uState.cells, 
                field.uState.generation, 
                field.uState.id, xOffset, y);

            Console.SetCursorPosition(xOffset, Console.CursorTop);
            DrawTopLine(grid.GetLength(0));
            for (int i = 0; i < grid.GetLength(1); i++)
            {
                for (int j = 0; j < grid.GetLength(0); j++)
                {
                    DrawLeftBorder(j , xOffset);
                    Console.Write(grid[j, i] ? "x" : " ");
                    DrawRightBorder(j, xOffset, grid);
                }
            }
            DrawBottomLine(grid.GetLength(0));
        }

        public void RenderMultipleFields(List<Universe> _universe)
        {
            //Console.Clear();
            Console.SetCursorPosition(0, 0);
            int borderOffset = 2;
            int xOffset = 0, yOffset = 0;

            int height = _universe[0].uState.grid.GetLength(1);
            int width = _universe[0].uState.grid.GetLength(0);

            for (int i = 0; i < _universe.Count; i++)
            {
                if (xOffset >= 110)
                {
                    xOffset = 0;
                    yOffset += height + borderOffset + 3;
                }

                RenderOneField(_universe[i], xOffset, yOffset);

                xOffset += width + borderOffset;
            }
        }

        public void Clear()
        {
            Console.Clear();
        }

        private void Color(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        private void FieldColor(bool notDead)
        {
            if(notDead)
            {
                
                Color(ConsoleColor.Green);
            }
            else
            {
                Color(ConsoleColor.DarkRed);
            } 
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

        private void DrawLeftBorder(int width, int xOffset)
        {
            if (width == 0)
            {
                Console.SetCursorPosition(xOffset, Console.CursorTop);
                Console.Write("║");
            }
        }

        private void DrawRightBorder(int width, int xOffset, bool[,] grid)
        {
            if (width == grid.GetLength(0) - 1)
            {
                Console.Write("║");
                Console.WriteLine("\r");
                Console.SetCursorPosition(xOffset, Console.CursorTop);
            }
        }

        private void FieldInfo(int width, int cells, int generation, int id, int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("c:" + cells);
            CountWidthOfInfoString(cells , width);

            Console.SetCursorPosition(x, y + 1);
            Console.Write("g:" + generation);
            CountWidthOfInfoString(cells, width);

            Console.SetCursorPosition(x, y + 2);
            Console.Write("id:" + id);
            CountWidthOfInfoString(cells, width);

            Console.SetCursorPosition(x, y + 3);
        }

        private void CountWidthOfInfoString(int input, int width)
        {
            int num = input.ToString().Length;
            int newWidth = width - num - 2;
            for (int i = 0; i < newWidth; i++) Console.Write(" "); 
        }
    }
}
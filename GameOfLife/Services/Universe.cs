using GameOfLife.Services;
using System;

namespace GameOfLife
{
    class Universe
    {
        public int years, cells;
        int Height, Width;
        bool[,] grid, newGrid;

        public Universe(int width, int height, int pattern)
        {
            Width = width;
            Height = height;

            grid = new bool[Width, Height];
            newGrid = new bool[Width, Height];

            Console.SetWindowSize(Width + 15, Height + 5);

            Patterns genezis = new Patterns(pattern, grid);
        }

        public void DrawFrame()
        {
            //Save Copy of parent genereation
            newGrid = grid.Clone() as bool[,];
            cells = 0;
            //Count live neightbors
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    bool state = grid[i,j];

                    int neighbors = CountNeightbors(grid, i, j);

                    if (state == true)
                        cells++;

                    if (state == false && neighbors == 3)
                    {
                        newGrid[i, j] = true;
                    }
                    else if (state == true && (neighbors < 2 || neighbors > 3))
                    {
                        newGrid[i, j] = false;
                    }
                    else
                        newGrid[i, j] = state;
                }
            }

            Render(newGrid);
            years++;
            grid = newGrid.Clone() as bool[,];
        }

        public int CountNeightbors(bool[,] grid, int x, int y)
        {
            int sum = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int cols = (x + i + Width) % Width;
                    int rows = (y + j + Height) % Height;

                    if (grid[cols, rows] == true)
                        sum++;
                }
            }

            if(grid[x, y] == true)
            {
                --sum;
            }

            return sum;
        }

        public void Render(bool[,] grid)
        {
            Console.SetCursorPosition(0, Console.WindowTop);
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    Console.Write(grid[j, i] ? "x" : " ");

                    if (j == Width - 1) Console.WriteLine("\r");
                }
            }
        }

    }
}

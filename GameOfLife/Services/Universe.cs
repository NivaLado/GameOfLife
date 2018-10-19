using System;
using GameOfLife.Models;
using GameOfLife.Services;

namespace GameOfLife
{
    internal class Universe
    {
        private ConsoleRenderer renderer;
        public UniverseState uState;
        public static int count = 0;

        public Universe()
        {
            uState = new UniverseState();
            renderer = new ConsoleRenderer();
            count++;
            renderer.CursorVisible(false);
            renderer.Color(ConsoleColor.Green);
        }

        public void NewUniverse(int width, int height, int pattern)
        {
            uState.Width = width;
            uState.Height = height;
            uState.grid = uState.newGrid = new bool[width, height];

            Patterns genezis = new Patterns(pattern, uState.grid);
            GenezisCountOfCells();
        }

        public void LoadUniverse()
        {
            FileReadWrite rw = new FileReadWrite();
            uState = rw.Deserialize();
        }

        public void SaveUniverse()
        {
            FileReadWrite rw = new FileReadWrite();
            rw.Serialize(uState);
        }

        public void Generation()
        {
            int neighbors;
            bool state;

            uState.newGrid = uState.grid.Clone() as bool[,];

            for (int i = 0; i < uState.Width; i++)
            {
                for (int j = 0; j < uState.Height; j++)
                {
                    state = uState.grid[i, j];
                    neighbors = CountNeightbors(i, j);
                    GameRules(i, j, state, neighbors);
                }
            }

            renderer.Render(uState.newGrid);

            uState.generation++;
            uState.grid = uState.newGrid.Clone() as bool[,];
        }

        public int CountNeightbors(int x, int y)
        {
            int sum = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int cols = (x + i + uState.Width) % uState.Width;
                    int rows = (y + j + uState.Height) % uState.Height;

                    if (uState.grid[cols, rows])
                        sum++;
                }
            }

            if (uState.grid[x, y] == true)
                --sum;

            return sum;
        }

        public void GameRules(int i, int j, bool state, int neighbors)
        {
            if (state == false && neighbors == 3)
            {
                uState.newGrid[i, j] = true;
                uState.cells++;
            }
            else if (state == true && (neighbors < 2 || neighbors > 3))
            {
                uState.newGrid[i, j] = false;
                uState.cells--;
            }
            else
                uState.newGrid[i, j] = state;
        }

        public void GenezisCountOfCells()
        {
            for (int i = 0; i < uState.grid.GetLength(0); i++)
            {
                for (int j = 0; j < uState.grid.GetLength(1); j++)
                {
                    if (uState.grid[i, j])
                        uState.cells++;
                }
            }
        }
    }
}
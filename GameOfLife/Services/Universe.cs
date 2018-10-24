using GameOfLife.Models;
using GameOfLife.Services;

namespace GameOfLife
{
    public class Universe
    {
        public UniverseState UState { get; set; }
        public static int UniverseCounter = 0;
        private static int _id = 0;

        public Universe()
        {
            UState = new UniverseState();
            UState.id = _id;
            _id++;
        }

        public void CreateUniverse(int width, int height, int pattern)
        {
            UState.Width = width;
            UState.Height = height;
            UState.grid = UState.newGrid = new bool[width, height];

            Patterns genezis = new Patterns(pattern, UState.grid);
            GenezisCountOfCells();
        }

        public void UniverseIteration()
        {
            int neighbors;
            bool state;

            if (UState.notDead)
            {
                UState.newGrid = UState.grid.Clone() as bool[,];

                for (int i = 0; i < UState.Width; i++)
                {
                    for (int j = 0; j < UState.Height; j++)
                    {
                        state = UState.grid[i, j];
                        neighbors = CountNeightborsSum(i, j);
                        GameRules(i, j, state, neighbors);
                    }
                }

                LiveCheck();
                UState.generation++;
                UState.grid = UState.newGrid.Clone() as bool[,];
            }
        }

        private int CountNeightborsSum(int x, int y)
        {
            int sum = 0;

            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int cols = (x + i + UState.Width) % UState.Width;
                    int rows = (y + j + UState.Height) % UState.Height;

                    if (UState.grid[cols, rows])
                        sum++;
                }
            }

            if (UState.grid[x, y] == true)
                --sum;

            return sum;
        }

        private void GameRules(int i, int j, bool state, int neighbors)
        {
            if (state == false && neighbors == 3)
            {
                UState.newGrid[i, j] = true;
                UState.cells++;
            }
            else if (state == true && (neighbors < 2 || neighbors > 3))
            {
                UState.newGrid[i, j] = false;
                UState.cells--;
            }
            else
                UState.newGrid[i, j] = state;
        }

        private void GenezisCountOfCells()
        {
            for (int i = 0; i < UState.grid.GetLength(0); i++)
            {
                for (int j = 0; j < UState.grid.GetLength(1); j++)
                {
                    if (UState.grid[i, j])
                    {
                        UState.cells++;
                    }
                }
            }
        }

        private void LiveCheck()
        {
            if (UState.cells == 0)
            {
                UState.notDead = false;
                UniverseCounter--;
            }
        }
    }
}
using GameOfLife.Interfaces;
using GameOfLife.Models;
using GameOfLife.Services;

namespace GameOfLife
{
    internal class Universe : IUniverse
    {
        public UniverseState uState { get; set; }
        public static int UniverseCounter = 0;

        public Universe()
        {
            uState = new UniverseState();
            UniverseCounter++;
        }

        public void CreateUniverse(int width, int height, int pattern)
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

        public void UniverseIteration()
        {
            int neighbors;
            bool state;

            uState.newGrid = uState.grid.Clone() as bool[,];

            for (int i = 0; i < uState.Width; i++)
            {
                for (int j = 0; j < uState.Height; j++)
                {
                    state = uState.grid[i, j];
                    neighbors = CountNeightborsSum(i, j);
                    GameRules(i, j, state, neighbors);
                }
            }

            uState.generation++;
            uState.grid = uState.newGrid.Clone() as bool[,];
        }

        private int CountNeightborsSum(int x, int y)
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

        private void GameRules(int i, int j, bool state, int neighbors)
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

        private void GenezisCountOfCells()
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
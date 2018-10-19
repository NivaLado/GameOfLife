using System;

namespace GameOfLife.Services
{
    public class Patterns
    {
        Action action;
        bool[,] grid;
        int x, y;

        public Patterns(int select, bool[,] grid)
        {
            this.grid = grid;
            x = grid.GetLength(0);
            y = grid.GetLength(1);

            switch (select)
            {
                case 0:
                    action = new Action(AddBlinker);
                    break;
                case 1:
                    action = new Action(AddGlider);
                    break;
                case 2:
                    action = new Action(Random);
                    break;
                default:
                    break;
            }

            action.Invoke();
        }

        private void AddBlinker()
        {
            grid[x / 2, y / 2 - 1] = true;
            grid[x / 2, y / 2] = true;
            grid[x / 2, y / 2 + 1] = true;
        }

        private void AddGlider()
        {
            grid[x / 2, y / 2 - 1] = true;
            grid[x / 2 + 1, y / 2] = true;
            grid[x / 2 - 1, y / 2 + 1] = true; grid[x / 2, y / 2 + 1] = true; grid[x / 2 + 1, y / 2 + 1] = true;
        }

        private void Random()
        {
            Random generator = new Random();
            int number;
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    number = generator.Next(101);
                    grid[i, j] = ((number < 70) ? false : true);
                }
            }
        }
    }
}

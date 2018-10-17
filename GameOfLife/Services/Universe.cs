using System;
using GameOfLife.Enums;
using GameOfLife.Models;
using GameOfLife.Services;

namespace GameOfLife
{
    class Universe
    {
        public UniverseState uState;

        public Universe(int width, int height, int pattern, int choice)
        {
            Console.CursorVisible = false;
            Console.ForegroundColor = ConsoleColor.Green;

            uState = new UniverseState();

            if(choice == (int)Choice.LoadGame)
            {
                UniverseDataRW rw = new UniverseDataRW();
                uState = rw.Deserialize();
            }
            else if (choice == (int)Choice.NewGame)
            {
                uState.Width = width;
                uState.Height = height;
                uState.grid = uState.newGrid = new bool[width, height];
       
                Patterns genezis = new Patterns(pattern, uState.grid);
            }
            GenezisCountOfCells();
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
                    state = uState.grid[i,j];
                    neighbors = CountNeightbors(i, j);
                    GameRules(i, j, state, neighbors);
                }
            }

            Render(uState.newGrid);
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

            if(uState.grid[x, y] == true)
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
                    if(uState.grid[i,j])
                        uState.cells++;
                }
            }
        }


        //Make separate
        //IRenderer Iterface
        public void Render(bool[,] grid)
        {
            for (int i = 0; i < uState.Height; i++)
            {
                for (int j = 0; j < uState.Width; j++)
                {
                    Console.Write(grid[j, i] ? "x" : " ");
                    //Draw();

                    if (j == uState.Width - 1 && !(i == uState.Height - 1)) Console.WriteLine("\r");
                }
            }
            Console.SetCursorPosition(0, 0);
        }       
    }
}
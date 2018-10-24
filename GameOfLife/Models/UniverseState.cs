using System;

namespace GameOfLife.Models
{
    [Serializable]
    public class UniverseState
    {
        public int id;
        public int Height { get; set; }
        public int Width { get; set; }
        public int generation, cells;
        public bool[,] grid, newGrid;
        public bool notDead = true;
    }
}
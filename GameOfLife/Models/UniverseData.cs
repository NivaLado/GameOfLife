using System;
namespace GameOfLife.Models
{
    [Serializable]
    class UniverseState
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public int generation, cells;
        public bool[,] grid, newGrid;
    }
}

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

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

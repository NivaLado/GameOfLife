using System.Collections.Generic;

namespace GameOfLife.Interfaces
{
    internal interface IRenderer
    {
        void Render(bool[,] grid, int xOffset, int yOffset);
        void MultipleRender(List<bool[,]> grid);
    }
}

using System.Collections.Generic;

namespace GameOfLife.Interfaces
{
    internal interface IRenderer
    {
        void RenderOneField(Universe grid, int xOffset, int yOffset);
        void RenderMultipleFields(List<Universe> universe);
        void Clear();
    }
}
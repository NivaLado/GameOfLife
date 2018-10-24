using System;
using System.Collections.Generic;

namespace GameOfLife.Interfaces
{
    internal interface IRenderer
    {
        void RenderOneField(Universe grid, int xOffset, int yOffset);
        void RenderMultipleFields(List<Universe> universe);
        void CursorVisible(bool visibility);
        void Color(ConsoleColor color);
        void Clear();
    }
}
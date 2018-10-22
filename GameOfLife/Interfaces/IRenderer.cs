using System;

namespace GameOfLife.Interfaces
{
    internal interface IRenderer
    {
        void Render(bool[,] grid);
        void Color(ConsoleColor color);
        void CursorVisible(bool visibility);
    }
}

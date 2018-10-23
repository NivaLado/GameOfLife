namespace GameOfLife.Interfaces
{
    internal interface IRenderer
    {
        void RenderOneField(Universe grid, int xOffset, int yOffset);
        void RenderMultipleFields(Universe[] _universe);
        void Clear();
    }
}
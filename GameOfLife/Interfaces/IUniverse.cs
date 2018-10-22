using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    interface IUniverse
    {
        UniverseState uState { get; set; }

        void Generation();
        void NewUniverse(int width, int height, int pattern);
        void LoadUniverse();
        void SaveUniverse();
    }
}

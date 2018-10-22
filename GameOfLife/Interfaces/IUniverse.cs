using GameOfLife.Models;

namespace GameOfLife.Interfaces
{
    public interface IUniverse
    {
        UniverseState uState { get; set; }

        void UniverseIteration();
        void CreateUniverse(int width, int height, int pattern);
        void LoadUniverse();
        void SaveUniverse();
    }
}

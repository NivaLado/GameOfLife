using GameOfLife.Services;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInterfaceIO userInitialization = UserInterfaceIO.GetInstance;
            UniverseInitManager gameInitialization = new UniverseInitManager();  

            GenerationManager life = new GenerationManager(gameInitialization.universes);
        }
    }
}
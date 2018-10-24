using GameOfLife.Services;

namespace GameOfLife
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var gameSetup = new GameInitialization();
            gameSetup.SetupGame();

            var life = new GenerationManager(
                gameSetup,
                new ConsoleRenderer()
            );
            
            life.StartLife();
        }
    }
}

//Nuget Package -Simple injector 
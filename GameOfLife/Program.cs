using System;
using GameOfLife.Services;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            GameInitialization gameSetup = new GameInitialization();
            gameSetup.SetupGame();

            GenerationManager life = new GenerationManager(gameSetup.universes, 
                                                            new ConsoleRenderer());
            life.StartLife();
        }
    }
}
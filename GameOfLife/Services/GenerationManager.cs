using System;
using GameOfLife.Constants;

namespace GameOfLife.Services
{
    class GenerationManager
    {
        Universe universe;
        public GenerationManager(Universe universe)
        {
            this.universe = universe;
        }

        public void StartLife(int wait)
        {
            while (true)
            {
                if (Globals.Save)
                {
                    SaveLife();
                }
                else if (!Globals.Pause)
                {
                    Console.Title = "Generation : " + universe.uState.generation +
                    " with " + universe.uState.cells + " cells";
                    universe.Generation();
                    System.Threading.Thread.Sleep(wait);
                }
            }
        }

        public void SaveLife()
        {
            universe.SaveUniverse();
            Globals.Save = false;
            Globals.Pause = true;
            Console.WriteLine("Game Was Saved");
        }
    }
}

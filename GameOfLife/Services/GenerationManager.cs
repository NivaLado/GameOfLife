using System;
using GameOfLife.Constants;

namespace GameOfLife.Services
{
    class GenerationManager
    {
        Universe[] _universe;
        public GenerationManager(Universe[] universe)
        {
            _universe = universe;
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
                    Console.Title = "Generation : " + _universe[0].uState.generation +
                    " with " + _universe[0].uState.cells + " cells" + 
                    " Count of Universes : " + Universe.count;
                    UniversesIteratot();
                    System.Threading.Thread.Sleep(wait);
                }
            }
        }

        public void SaveLife()
        {
            //universe.SaveUniverse();
            Globals.Save = false;
            Globals.Pause = true;
            Console.WriteLine("Game Was Saved");
        }

        public void UniversesIteratot()
        {
            for (int i = 0; i < _universe.Length; ++i)
                _universe[i].Generation();
        }
    }
}

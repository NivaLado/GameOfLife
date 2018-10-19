using System;
using System.Threading.Tasks;
using GameOfLife.Constants;

namespace GameOfLife.Services
{
    //Make singleton dependency inj
    internal class GenerationManager
    {
        private ConsoleRenderer renderer;
        private Universe[] _universe;
        private Task[] tasks;

        public GenerationManager(Universe[] universe)
        {
            _universe = universe;
            renderer = new ConsoleRenderer();
            tasks = new Task[_universe.Length];
            StartLife();
        }

        public void StartLife()
        {
            //"Generation : " + _universe.uState.generation +
            //" with " + _universe.uState.cells + " cells" +
            Console.Title = " Count of Universes : " + Universe.count;
            tasks[0] = new Task(() => Loop(_universe[0]));
            tasks[0].Start();
            tasks[0].Wait();
            //for (int i = 0; i < _universe.Length; i++)
            //{
            //    tasks[i] = new Task(() => Loop(_universe[i]));
            //    tasks[i].Start();
            //}
            tasks[0].Wait();
        }

        private void SaveLife()
        {
            _universe[0].SaveUniverse();
            Globals.Save = false;
            Globals.Pause = true;
            Console.WriteLine("Game Was Saved");
        }

        private void Loop(Universe universe)
        {
            while (true)
            {
                if (!Globals.Pause)
                {
                    universe.Generation();
                    renderer.Render(universe.uState.grid);
                    System.Threading.Thread.Sleep(100);
                }
            }
        }
    }
}
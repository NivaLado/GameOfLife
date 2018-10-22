using System;
using System.Threading.Tasks;
using GameOfLife.Constants;
using GameOfLife.Interfaces;

namespace GameOfLife.Services
{
    //Make singleton dependency inj
    internal class GenerationManager
    {
        private IRenderer renderer;
        private IUniverse[] _universe;
        private Task[] tasks;

        public GenerationManager(IUniverse[] universe)
        {
            _universe = universe;
            renderer = new ConsoleRenderer();
            tasks = new Task[_universe.Length];
            TestLife();
            //StartLife();
        }

        public void StartLife()
        {
            //"Generation : " + _universe.uState.generation +
            //" with " + _universe.uState.cells + " cells" +
            Console.Title = " Count of Universes : " + Universe.UniverseCounter;
            //tasks[0] = new Task(() => Loop(_universe[0]));
            //tasks[0].Start();
            //tasks[0].Wait();
            for (int i = 0; i < _universe.Length; i++)
            {
                int j = i;
                if(j == 0)
                {
                    tasks[j] = Task.Factory.StartNew(() => Loop(_universe[j])).ContinueWith((parent) => renderer.Render(_universe[0].uState.grid));
                } else
                {
                    tasks[j] = Task.Factory.StartNew(() => Loop(_universe[j]));
                }
            }
            tasks[0].Wait();
        }

        public void TestLife()
        {
            while (true)
            {
                Console.Title = " Count of Universes : " + Universe.UniverseCounter;

                Parallel.For(0, _universe.Length, 
                    (i) => Task.Factory.StartNew(
                    ()  => Loop(_universe[i])).ContinueWith(
                    (p) => ShouldRender(i)
                ));

                //renderer.Render(_universe[0].uState.grid);

                System.Threading.Thread.Sleep(500);
            }
        }

        private void SaveLife()
        {
            _universe[0].SaveUniverse();
            Globals.Save = false;
            Globals.Pause = true;
            Console.WriteLine("Game Was Saved");
        }

        private void Loop(IUniverse universe)
        {
            //while (true)
            //{
                if (!Globals.Pause)
                {
                    universe.Generation();
                    //renderer.Render(universe.uState.grid);
                }
            //}
        }

        private void ShouldRender(int visibleUniverse)
        {
            if(visibleUniverse == 0)
            {
                renderer.Render(_universe[visibleUniverse].uState.grid);
            }
        }
    }
}
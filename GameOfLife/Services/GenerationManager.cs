using System;
using System.Threading;
using System.Threading.Tasks;
using GameOfLife.Constants;
using GameOfLife.Interfaces;

namespace GameOfLife.Services
{
    internal class GenerationManager
    {
        private IRenderer renderer;
        private IUniverse[] _universe;
        bool go = true;

        public GenerationManager(IUniverse[] universe)
        {
            _universe = universe;
            renderer = new ConsoleRenderer();
        }

        public void StartLife()
        {
            while (true && go)
            {
                go = false;
                Timer t = new Timer(TimerCallback, null, 0, 1000);
                if (!Globals.Pause)
                {
                    Console.Title = " Count of Universes : " + Universe.UniverseCounter;

                    Parallel.For(0, _universe.Length,
                        (i) => Task.Factory.StartNew(
                        () => Loop(_universe[i]))
                    );

                    renderer.Render(_universe[0].uState.grid);
                }

                if (Globals.Save)
                {
                    SaveLife();
                }

                //System.Threading.Thread.Sleep(1000);
            }
        }

        private void TimerCallback(Object o)
        {
            go = true;
            //Console.WriteLine("In TimerCallback: " + DateTime.Now);
            //GC.Collect();
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
            universe.UniverseIteration();
        }

        private void ShouldIRender(int visibleUniverse)
        {
            if (visibleUniverse == 0)
            {
                renderer.Render(_universe[visibleUniverse].uState.grid);
            }
        }
    }
}
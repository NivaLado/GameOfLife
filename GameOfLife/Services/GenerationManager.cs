using System;
using System.Threading.Tasks;
using GameOfLife.Constants;
using GameOfLife.Interfaces;
using GameOfLife.Models;

namespace GameOfLife.Services
{   
    internal class GenerationManager
    {
        private IRenderer _renderer;
        private Universe[] _universe;

        public GenerationManager(Universe[] universe, IRenderer renderer)
        {
            _universe = universe;
            _renderer = renderer;
        }

        public void StartLife()
        {
            while (true)
            {
                if (!Globals.Pause)
                {
                    Console.Title = 
                        "Live Universes : {" + 
                        Universe.UniverseCounter + "}" +
                        " (c) is live cells! (g) is generation"
                        ;

                    Parallel.For(0, _universe.Length,
                        (i) => Task.Factory.StartNew(
                        () => Loop(_universe[i]))
                    );

                    var renderTask = Task.Run(() => RenderTask());
                    renderTask.Wait();
                }

                if (Globals.Save)
                {
                    SaveLife();
                }

                Task.Delay(500).Wait();
                //Thread.sllep(1000);
            }
        }

        private void SaveLife()
        {
            FileReadWrite rw = FileReadWrite.GetReadWriteService;
            UniverseState[] st = new UniverseState[_universe.Length];

            for (int i = 0; i < _universe.Length; i++)
            {
                st[i] = _universe[i].uState;
            }
            rw.Serialize(st);

            Globals.Save = false;
            Globals.Pause = true;
            _renderer.Clear();
            Console.WriteLine("Game Was Saved");
        }

        private void RenderTask()
        {
            _renderer.RenderMultipleFields(_universe);
        }

        private void Loop(Universe universe)
        {
            universe.UniverseIteration();
        }
    }
}
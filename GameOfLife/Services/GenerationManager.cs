using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GameOfLife.Constants;
using GameOfLife.Interfaces;
using GameOfLife.Models;

namespace GameOfLife.Services
{
    internal class GenerationManager
    {
        private IRenderer _renderer;
        private IUniverse[] _universe;
        private List<bool[,]> buffer;

        public GenerationManager(IUniverse[] universe, IRenderer renderer)
        {
            _universe = universe;
            _renderer = renderer;

            buffer = new List<bool[,]>();
            for (int i = 0; i < _universe.Length; i++)
            {
                buffer.Add(_universe[i].uState.grid);
            }
        }

        public void StartLife()
        {
            while (true)
            {
                if (!Globals.Pause)
                {
                    Console.Title = " Count of Universes : " + Universe.UniverseCounter;

                    Parallel.For(0, _universe.Length,
                        (i) => Task.Factory.StartNew(
                        () => Loop(_universe[i]))
                    );



                    _renderer.MultipleRender(buffer);

                }

                if (Globals.Save)
                {
                    SaveLife();
                }

                Task.Delay(1000).Wait();
            }
        }

        private void SaveLife()
        {
            FileReadWrite rw = new FileReadWrite();
            UniverseState[] st = new UniverseState[_universe.Length];

            for (int i = 0; i < _universe.Length; i++)
            {
                st[i] = _universe[i].uState;
            }
            rw.Serialize(st);

            Globals.Save = false;
            Globals.Pause = true;
            Console.WriteLine("Game Was Saved");
        }

        private void Loop(IUniverse universe)
        {
            universe.UniverseIteration();
        }
    }
}
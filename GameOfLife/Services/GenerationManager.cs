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
        private GameInitialization _gameSetup;

        public GenerationManager(GameInitialization gameSetup, IRenderer renderer)
        {
            _gameSetup = gameSetup;
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

                    Parallel.For(0, _gameSetup.universes.Length,
                        (i) => Task.Factory.StartNew(
                        () => _gameSetup.universes[i].UniverseIteration()
                    ));

                    var renderTask = Task.Run(() => RenderTask());
                    renderTask.Wait();
                }

                if (Globals.Save)
                {
                    SaveLife();
                }

                if (Globals.ChangeVisibleGames)
                {
                    var changeTask = Task.Run(() => ChangeVisibleGames());
                    changeTask.Wait();
                }

                Task.Delay(500).Wait();
                //Thread.Sleep(1000);
            }
        }

        private void SaveLife()
        {
            FileReadWrite rw = FileReadWrite.GetReadWriteService;
            UniverseState[] st = new UniverseState[_gameSetup.universes.Length];

            for (int i = 0; i < _gameSetup.universes.Length; i++)
            {
                st[i] = _gameSetup.universes[i].uState;
            }
            rw.Serialize(st);

            Globals.Save = false;
            Globals.Pause = true;
            _renderer.Clear();
            Console.WriteLine("Game Was Saved");
        }

        private void ChangeVisibleGames()
        {
            Globals.Pause = !Globals.Pause;
            _renderer.Clear();

            Console.Write("Enter games to render int format (1 2 5 122 ...) -> ");

            string line = Console.ReadLine();

            Validator validator = Validator.GetInstance;

            Globals.GamesToRender = validator.StringToInt(line);
            _gameSetup.VisibleUniverses();

            Globals.ChangeVisibleGames = false;
            Globals.Pause = !Globals.Pause;
        }

        private void RenderTask()
        {
            _renderer.RenderMultipleFields(_gameSetup.visibleUniverses);
        }

        private void Loop(Universe universe)
        {
            universe.UniverseIteration();
        }
    }
}
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
                    ConsoleTitle();

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

                Task.Delay(500).Wait();//Magic num
            }
        }

        private void SaveLife()
        {
            FileReadWrite rw = FileReadWrite.GetReadWriteService;
            UniverseState[] st = new UniverseState[_gameSetup.universes.Length];
            for (int i = 0; i < _gameSetup.universes.Length; i++)
            {
                st[i] = _gameSetup.universes[i].UState;
            }
            rw.Serialize(st);

            Globals.Save = false;  //Class should controll self state
            Globals.Pause = true;

            _renderer.Clear();
            _renderer.Color(ConsoleColor.White);
            Console.WriteLine("Game Was Saved Press Enter to continue");
            Console.ReadLine();
            Globals.Pause = false;
            _renderer.Clear();
        }

        private void ChangeVisibleGames()
        {
            Globals.Pause = !Globals.Pause;
            _renderer.Clear();
            _renderer.CursorVisible(true);
            _renderer.Color(ConsoleColor.White);

            Console.Write("Enter games to render int format (1 2 5 122 ...) -> ");

            string line = Console.ReadLine();
            Validator validator = Validator.GetInstance;

            Globals.GamesToRender = validator.StringToInt(line);
            Globals.ChangeVisibleGames = false;
            Globals.Pause = !Globals.Pause;

            _gameSetup.VisibleUniverses();

            _renderer.CursorVisible(false);
            _renderer.Clear();
        }

        private void RenderTask()
        {
            _renderer.RenderMultipleFields(_gameSetup.visibleUniverses);
        }

        private void ConsoleTitle()
        {
            Console.Title =
                "Live Universes : {" +
                Universe.UniverseCounter + "}" +
                "Live Cells : {" +
                LiveCellsCounter() + "}" +
                " (c) is live cells! (g) is generation";
        }

        private int LiveCellsCounter()
        {
            int sum = 0;
            for (int i = 0; i < _gameSetup.universes.Length; i++)
            {
                sum += _gameSetup.universes[i].UState.cells;
            }
            return sum;
        }
    }
}
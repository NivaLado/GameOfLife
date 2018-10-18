using System;
using System.Threading.Tasks;
using GameOfLife.Enums;
using GameOfLife.Services;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInput init = new UserInput();
            Universe universe = new Universe();

            if (init.choice == (int)Choice.NewGame)
                universe.NewUniverse(init.width, init.height, init.pattern);
            if (init.choice == (int)Choice.LoadGame)
                universe.LoadUniverse();

            Task UserInput = new Task(() => init.TrackUserUnput());
            UserInput.Start();

            GenerationManager life = new GenerationManager(universe);

            Task task = new Task(() => life.StartLife(100));
            task.Start();
            task.Wait();
        }
    }
}
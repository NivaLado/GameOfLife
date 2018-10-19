using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GameOfLife.Constants;
using GameOfLife.Services;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInterfaceIO init = new UserInterfaceIO();
            Universe[] universe = new Universe[100];
            UniverseInitManager universeManger = new UniverseInitManager(universe);

            universeManger.NewGameOrLoad(init);

            Task UserInput = new Task(() => new InputManager());
            UserInput.Start();

            GenerationManager life = new GenerationManager(universe);

            Task task = new Task(() => life.StartLife(100));
            task.Start();

            task.Wait();
        }
    }
}
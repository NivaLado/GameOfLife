using System;
using System.Threading.Tasks;
using GameOfLife.Services;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInterfaceIO init = new UserInterfaceIO();
            Universe[] universe = new Universe[1];
            UniverseInitManager universeManger = new UniverseInitManager(universe, init);

            Task UserInput = new Task(() => new InputManager());
            UserInput.Start();

            GenerationManager life = new GenerationManager(universe);
            
            //Console.ReadLine();
            //ConsoleRenderer renderer = new ConsoleRenderer();
            //while(true)
            //{
            //    Universe[] temp =  life.test();
            //    renderer.Render(temp[0].uState.grid);
            //    System.Threading.Thread.Sleep(100);
            //}
        }
    }
}
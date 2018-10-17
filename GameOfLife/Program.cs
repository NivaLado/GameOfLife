using System;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInput init = new UserInput();    
            Universe universe = new Universe(init.width, init.height, init.pattern, init.choice);

            while (true)
            {
                Console.Title = "Generation : " + universe.uState.generation +
                            " with " + universe.uState.cells + " cells";
                //Task task = Task.Run(() => universe.Generation();
                universe.Generation();
                System.Threading.Thread.Sleep(100);
            }

        }

    }
}
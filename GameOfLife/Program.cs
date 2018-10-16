using System;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInput init = new UserInput();    
            Universe universe = new Universe(30, 10, init.pattern);

            while(true)
            {
                Console.Title = "Universe is " + universe.years.ToString() + " years with " + universe.cells.ToString() + " cells";
                universe.DrawFrame();
                System.Threading.Thread.Sleep(100);
            }

        }

    }
}

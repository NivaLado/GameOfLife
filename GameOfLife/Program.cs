using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Program
    {

        static void Main(string[] args)
        {
            UserInput init = new UserInput();

            Universe universe = new Universe(init.width, init.height);

            Console.ReadKey();
        }

    }
}

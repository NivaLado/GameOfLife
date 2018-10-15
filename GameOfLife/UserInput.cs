using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class UserInput
    {
        public int width;
        public int height;


        public UserInput()
        {
            Console.Write("Universe Width : ");
            width = Convert.ToInt32(Console.ReadLine());

            Console.Write("Universe Height : ");
            height = Convert.ToInt32(Console.ReadLine());

            Console.Clear();
        }

    }
}

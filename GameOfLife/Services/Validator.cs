using System;

namespace GameOfLife.Services
{
    public class Validator
    {
        public int ValidateInt(string message, string errorMessage)
        {
            int number;

            Console.Write(message);

            string input = Console.ReadLine();
            while (!Int32.TryParse(input, out number))
            {
                Console.WriteLine(errorMessage);
                Console.Write(message);
                input = Console.ReadLine();
            }
            return number;
        }

        public int ValidateIntMinMax(string message, string errorMessage, int min, int max)
        {
            int number;
            do
            {
                number = ValidateInt(message, errorMessage);
            } while (Min(number, min) || Max(number, max));
            
            return number;
        }

        private bool Min(int input, int min)
        {
            if (input < min)
            {
                Console.WriteLine("Should be great than " + min);
                return true;
            }
            return false;
        }

        private bool Max(int input, int max)
        {
            if (input > max)
            {
                Console.WriteLine("Should be less than " + max);
                return true;
            }
            return false;
        }
    }
}
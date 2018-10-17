using System;
using GameOfLife.Enums;

namespace GameOfLife
{
    class UserInput
    {
        public int width, height, pattern, choice;

        public UserInput()
        {
            Validate(
                "Enter 1 to Generate new Universe \n" +
                "Enter 2 to Load Existing Universe \n" +
                "Enter 3 to Exit Game \n",
                "Not a valid number, try again.", out choice, 1, 3);
            switch (choice)
            {
                case (int)Choice.NewGame : NewGame();
                    break;
                case (int)Choice.LoadGame : LoadGame();
                    break;
                case (int)Choice.Exit : Environment.Exit(0);
                    break;
            }

            Console.Clear();
        }

        public void NewGame()
        {
            Console.Clear();
            Validate("Universe Width: ", "Not a valid number, try again.", out width, 0, 119);
            Validate("Universe Height: ", "Not a valid number, try again.", out height, 0);
            Validate("Universe Pattern (from 0 to 2): ", "Not a valid number, try again.", out pattern, 0, 2);
        }

        public void LoadGame()
        {
            Console.WriteLine("Pres any button to start game");
            Console.ReadKey();
        }

        public void Validate(string message, string errMsg, out int number, int? min = null, int? max = null)
        {
            Val:
            Console.Write(message);
            string input = Console.ReadLine();
            while (!Int32.TryParse(input, out number))
            {
                Console.WriteLine(errMsg);
                Console.Write(message);
                input = Console.ReadLine();
            }

            if (min != null || max != null)
            {
                int num = Int32.Parse(input);
                if (Min(num, min) || Max(num, max))
                {
                    goto Val;
                }
            }
        }

        public bool Min(int input,int? min)
        {
            if(input < min)
            {
                Console.WriteLine("Should be great than " + min);
                return true;
            }
            return false;
        }

        public bool Max(int input, int? max)
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

using System;
using GameOfLife.Constants;
using GameOfLife.Services;

namespace GameOfLife
{
    internal class UserInterfaceIO
    {
        public int width, height, pattern, choice;
        Validator validate;

        public UserInterfaceIO()
        {
            validate = new Validator();

            choice = validate.ValidateIntMinMax(
                "Enter 1 to Generate new Universe \n" +
                "Enter 2 to Load Existing Universe \n" +
                "Enter 3 to Exit Game \n",
                "Not a valid number, try again.", 1, 3);

            switch (choice)
            {
                case Choice.NewGame:
                    NewGame();
                    break;

                case Choice.LoadGame:
                    LoadGame();
                    break;

                case Choice.Exit:
                    Environment.Exit(0);
                    break;
            }

            Console.Clear();
        }

        public void NewGame()
        {
            Console.Clear();
            width = validate.ValidateIntMinMax("Universe Width: ", "Not a valid number, try again.", 0, 119);
            height = validate.ValidateIntMinMax("Universe Height: ", "Not a valid number, try again.", 0, 30);
            pattern = validate.ValidateIntMinMax("Universe Pattern (from 0 to 2): ", "Not a valid number, try again.", 0, 2);
        }

        public void LoadGame()
        {
            Console.WriteLine("Pres any button to start game");
            Console.ReadKey();
        }

        public void NewGameOrLoad(Universe[] universe)
        {
            if (choice == Choice.NewGame)
                universe[0].NewUniverse(width, height, pattern);
            if (choice == Choice.LoadGame)
                universe[0].LoadUniverse();
        }
    }
}
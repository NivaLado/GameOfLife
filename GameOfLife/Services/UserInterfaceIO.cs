using System;
using System.Threading.Tasks;
using GameOfLife.Constants;
using GameOfLife.Services;

namespace GameOfLife
{
    public class UserInterfaceIO
    {
        Validator validate = Validator.GetInstance;

        public UserInterfaceIO()
        {
            Globals.UserMenuChoice = validate.ValidateIntMinMax(
                "Enter 1 to Generate new Universe \n" +
                "Enter 2 to Load Existing Universe \n" +
                "Enter 3 to Exit Game \n",
                "Not a valid number, try again.", 1, 3);

            switch (Globals.UserMenuChoice)
            {
                case GameStartupChoice.NewGame:
                    NewGame();
                    break;

                case GameStartupChoice.LoadGame:
                    LoadGame();
                    break;

                case GameStartupChoice.Exit:
                    Environment.Exit(0);
                    break;
            }

            Task.Factory.StartNew(() => new InputManager());
            Console.Clear();
        }

        private void NewGame()
        {
            Console.Clear();
            Globals.Universes = validate.ValidateIntMinMax("Universe Count: ", "Not a valid number, try again.", 1, 1000);
            Globals.Width = validate.ValidateIntMinMax("Universe Width: ", "Not a valid number, try again.", 0, 119);
            Globals.Height = validate.ValidateIntMinMax("Universe Height: ", "Not a valid number, try again.", 0, 30);
            Globals.Pattern = validate.ValidateIntMinMax("Universe Pattern (from 0 to 2): ", "Not a valid number, try again.", 0, 2);
        }

        private void LoadGame()
        {
            Console.WriteLine("Pres any button to start game");
            Console.ReadKey();
        }

        public void ErrorMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
using System;
using GameOfLife.Constants;

namespace GameOfLife.Services
{
    public class InputManager
    {
        public InputManager()
        {
            TrackUserUnput();
        }

        private void TrackUserUnput()
        {
            while (true)
            {
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;

                    case ConsoleKey.P:
                        Globals.Pause = !Globals.Pause;
                        break;

                    case ConsoleKey.S:
                        Globals.Save = !Globals.Save;
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
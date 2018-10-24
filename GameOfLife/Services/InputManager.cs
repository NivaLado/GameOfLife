using System;
using System.Threading.Tasks;
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

                    case ConsoleKey.D:
                        Globals.ChangeVisibleGames = true;
                        Task.Delay(1000).Wait();
                        break;

                    default:
                        break;
                }
            }
        }
    }
}
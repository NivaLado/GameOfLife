using GameOfLife.Constants;
using GameOfLife.Interfaces;

namespace GameOfLife.Services
{
    public class GameInitialization
    {
        public IUniverse[] universes;

        public GameInitialization()
        {
            UserInterfaceIO userInitialization = new UserInterfaceIO();

            universes = new Universe[Globals.Universes];
            for (int i = 0; i < universes.Length; ++i)
                universes[i] = new Universe();
        }

        public void SetupGame()
        {
            if(Globals.Choice == GameStartupChoice.NewGame)
            {
                for (int i = 0; i < universes.Length; ++i)
                    universes[i].CreateUniverse(Globals.Width, Globals.Height, Globals.Pattern);
            }
            else if (Globals.Choice == GameStartupChoice.LoadGame)
            {
                for (int i = 0; i < universes.Length; ++i)
                    universes[i].LoadUniverse();
            }

        }
    }
}

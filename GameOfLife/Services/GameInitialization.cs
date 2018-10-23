using GameOfLife.Constants;
using GameOfLife.Models;

namespace GameOfLife.Services
{
    public class GameInitialization
    {
        public Universe[] universes;

        public GameInitialization()
        {
            UserInterfaceIO userInitialization = new UserInterfaceIO();
        }

        public void SetupGame()
        {
            if (Globals.UserMenuChoice == GameStartupChoice.NewGame)
            {
                universes = new Universe[Globals.Universes];

                for (int i = 0; i < universes.Length; ++i)
                {
                    universes[i] = new Universe();
                    universes[i].CreateUniverse(Globals.Width, Globals.Height, Globals.Pattern);
                }
            }
            else if (Globals.UserMenuChoice == GameStartupChoice.LoadGame)
            {
                FileReadWrite rw = FileReadWrite.GetReadWriteService;
                UniverseState[] obj = rw.Deserialize();

                universes = new Universe[obj.Length];

                for (int i = 0; i < universes.Length; ++i)
                {
                    universes[i] = new Universe();
                    universes[i].uState = obj[i];
                }
            }
        }
    }
}
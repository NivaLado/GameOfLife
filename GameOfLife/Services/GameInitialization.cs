using System.Collections.Generic;
using GameOfLife.Constants;
using GameOfLife.Models;

namespace GameOfLife.Services
{
    public class GameInitialization
    {
        public Universe[] universes;
        public List<Universe> visibleUniverses = new List<Universe>();

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
                    universes[i].UState = obj[i];
                }
            }

            LiveUniverses();
            VisibleUniverses();
        }

        public void VisibleUniverses()
        {
            visibleUniverses.Clear();
            for (int i = 0; i < Globals.GamesToRender.Length; i++)
            {
                visibleUniverses.Add(universes[Globals.GamesToRender[i]]);
            }
        }

        private void LiveUniverses()
        {
            for (int i = 0; i < universes.Length; i++)
            {
                if(universes[i].UState.notDead)
                {
                    Universe.UniverseCounter++;
                }
            }
        }
    }
}
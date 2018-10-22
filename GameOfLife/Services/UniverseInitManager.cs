using GameOfLife.Constants;
using GameOfLife.Interfaces;

namespace GameOfLife.Services
{
    class UniverseInitManager
    {
        public IUniverse[] universes;

        public UniverseInitManager()
        {
            universes = new Universe[Globals.Universes];
            for (int i = 0; i < universes.Length; ++i)
                universes[i] = new Universe();
            NewGameOrLoad();
        }

        private void NewGameOrLoad()
        {
            if(Globals.Choice == Choice.NewGame)
            {
                for (int i = 0; i < universes.Length; ++i)
                    universes[i].NewUniverse(Globals.Width, Globals.Height, Globals.Pattern);
            }
            else if (Globals.Choice == Choice.LoadGame)
            {
                for (int i = 0; i < universes.Length; ++i)
                    universes[i].LoadUniverse();
            }

        }
    }
}

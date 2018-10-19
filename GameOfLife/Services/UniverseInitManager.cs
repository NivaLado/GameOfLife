using GameOfLife.Constants;

namespace GameOfLife.Services
{
    class UniverseInitManager
    {
        private Universe[] _universes;

        public UniverseInitManager(Universe[] universes, UserInterfaceIO init)
        {
            _universes = universes;
            for (int i = 0; i < universes.Length; ++i)
                universes[i] = new Universe();
            NewGameOrLoad(init);
        }

        private void NewGameOrLoad(UserInterfaceIO init)
        {
            if(init.choice == Choice.NewGame)
            {
                for (int i = 0; i < _universes.Length; ++i)
                    _universes[i].NewUniverse(init.width, init.height, init.pattern);
            }
            else if (init.choice == Choice.LoadGame)
            {
                for (int i = 0; i < _universes.Length; ++i)
                    _universes[i].LoadUniverse();
            }

        }
    }
}

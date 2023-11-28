using Systemagedon.App.Gameplay;

namespace Systemagedon.App.Services
{
    public class StarSystemSwitcherSettings
    {
        public StarSystemSwitcherSettings(
            int levelsToSwitch,
            IAnimation switchCallback)
        {
            LevelsToSwitch = levelsToSwitch;
            SwitchAnimation = switchCallback;
        }

        public int LevelsToSwitch { get; }
        public IAnimation SwitchAnimation { get; }
    }
}

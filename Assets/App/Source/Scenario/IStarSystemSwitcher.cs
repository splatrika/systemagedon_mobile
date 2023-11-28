using System;

namespace Systemagedon.App.Services
{
    public interface IStarSystemSwitcher
    {
        public event Action SwitchEnded;
        public event Action SwitchStarted;

        public void RaiseDifficulty();
    }
}

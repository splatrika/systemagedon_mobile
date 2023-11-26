using System;

namespace Systemagedon.App.Gameplay
{

    public interface ISwitchCallback
    {
        public event Action CallbackEnded;
        public void Run();
    }

}

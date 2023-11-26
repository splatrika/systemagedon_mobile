using System;

namespace Systemagedon.App.Gameplay
{

    public interface ITutorialState
    {
        public void OnStart(ITutorialContext context);
        public void OnFinish(ITutorialContext context);
    }

}

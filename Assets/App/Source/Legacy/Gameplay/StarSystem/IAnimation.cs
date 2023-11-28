using System;
using System.Collections;

namespace Systemagedon.App.Gameplay
{

    public interface IAnimation
    {
        public event Action CallbackEnded;
        public void Run(); // todo remove
        public IEnumerator RunAndWaitUntilEnd();
    }

}

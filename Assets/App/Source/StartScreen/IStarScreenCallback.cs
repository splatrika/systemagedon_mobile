using System;

namespace Systemagedon
{

    public interface IStarScreenCallback
    {
        public event Action StartGameCallbackEnded;


        public void RunStartGameCallback();
    }

}
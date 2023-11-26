using System;
using UnityEngine;


namespace Systemagedon.App.Gameplay
{

    public class StarSystemTransferService
    {
        private StarSystem _stored;


        public bool IsNotEmpty()
        {
            return _stored;
        }


        public void Give(StarSystem starSystem)
        {
            if (IsNotEmpty())
            {
                throw new InvalidOperationException("Star system already given and not taken yet");
            }
            GameObject.DontDestroyOnLoad(starSystem);
            _stored = starSystem;
        }


        public StarSystem Take()
        {
            if (!IsNotEmpty())
            {
                throw new InvalidOperationException("We haven't star system to transfer");
            }
            StarSystem result = _stored;
            _stored = null;
            return result;
        }
    }

}

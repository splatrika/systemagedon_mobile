using System;
using Systemagedon.App.Services;
using UnityEngine;


namespace Systemagedon.App.Gameplay
{

    public class StarSystemTransferService
    {
        private StarSystemSnapshot _starSystemSnapshot;

        public bool IsSnapshotStored()
        {
            return _starSystemSnapshot != null;
        }

        public void GiveSnapshot(StarSystemSnapshot starSystemSnapshot)
        {
            if (IsSnapshotStored())
            {
                throw new InvalidOperationException("Star system already given and not taken yet");
            }
            _starSystemSnapshot = starSystemSnapshot;
        }

        public StarSystemSnapshot TakeSnapshot()
        {
            if (!IsSnapshotStored())
            {
                throw new InvalidOperationException("We haven't star system to transfer");
            }
            var result = _starSystemSnapshot;
            _starSystemSnapshot = null;
            return result;
        }
    }

}

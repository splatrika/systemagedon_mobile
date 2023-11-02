using System;
using Systemagedon.App.Gameplay;
using UnityEngine;

namespace Systemagedon.App
{

    public static class GlobalInstaller
    {
        public static StarSystemTransferService StarSystemTransferService
            { get; private set; }


        static GlobalInstaller()
        {
            StarSystemTransferService = new StarSystemTransferService();
        }
    }

}

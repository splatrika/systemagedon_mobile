using System;
using Systemagedon.App.Gameplay;
using UnityEngine;

namespace Systemagedon.App
{

    public static class SystemagedonApp
    {
        public static HighscoresService HighscoresService { get; private set; }
        public static StarSystemTransferService StarSystemTransferService
            { get; private set; }


        static SystemagedonApp()
        {
            string highscoresPath = Application.persistentDataPath + "highscores.json";
            HighscoresService = new HighscoresService(highscoresPath);
            StarSystemTransferService = new StarSystemTransferService();
        }
    }

}

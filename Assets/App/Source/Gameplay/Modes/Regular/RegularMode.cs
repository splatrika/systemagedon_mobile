﻿using System;
using UnityEngine.SceneManagement;

namespace Systemagedon.App.Gameplay
{

    public class RegularMode : IGameMode
    {
        public const string SceneName = "RegularGameplay";


        public string GetStaticName()
        {
            return "Regular";
        }

        public void LoadAndPlay()
        {
            SceneManager.LoadScene(SceneName);
        }
    }

}

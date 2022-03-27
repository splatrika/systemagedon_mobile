using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Systemagedon.App.Gameplay;
using System.IO;

namespace Systemagedon.Tests
{

    public class HighscoresServiceTests
    {
        [Test]
        public void SendAndLoad()
        {
            string path =
                Application.persistentDataPath + "testHighscores.json";
            int expectedScore = 10;
            HighscoresService service = new HighscoresService(path);
            RegularMode regularMode = new RegularMode();
            service.Send(expectedScore, regularMode);
            service = new HighscoresService(path); //Reload
            int actualScore = service.GetHighscore(regularMode);
            Assert.AreEqual(expectedScore, actualScore);
            File.Delete(path);
        }


        [Test]
        public void MultipleSends()
        {
            string path =
                Application.persistentDataPath + "testHighscores.json";
            HighscoresService service = new HighscoresService(path);
            RegularMode regularMode = new RegularMode();
            int score = 10;
            int expectedHighscore = score;
            service.Send(score, regularMode);
            int actualHighscore = service.GetHighscore(regularMode);
            Assert.AreEqual(expectedHighscore, actualHighscore);

            score = 5;
            expectedHighscore = 10;
            service.Send(score, regularMode);
            actualHighscore = service.GetHighscore(regularMode);
            Assert.AreEqual(expectedHighscore, actualHighscore);

            score = 50;
            expectedHighscore = score;
            service.Send(score, regularMode);
            actualHighscore = service.GetHighscore(regularMode);
            Assert.AreEqual(expectedHighscore, actualHighscore);

            File.Delete(path);
        }
    }

}

using UnityEngine;
using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;

namespace Systemagedon.App.Gameplay
{

    public class HighscoresService
    {
        [Serializable]
        private struct Highscore
        {
            public string Mode;
            public int Value;
        }


        [Serializable]
        private class Highscores
        {
            public List<Highscore> Values = new List<Highscore>();
        }


        private static string _path;
        private static Highscores _highscores;


        public HighscoresService(string path)
        {
            _path = path;
            if (!File.Exists(_path))
            {
                _highscores = new Highscores();
                return;
            }
            StreamReader reader = File.OpenText(_path);
            string json = reader.ReadToEnd();
            _highscores = JsonUtility.FromJson<Highscores>(json);
            reader.Close();
        }


        public int GetHighscore(IGameMode mode)
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer) return 0;
            if (_highscores == null) return 0;
            Highscore highscore = _highscores.Values.Find(
                (highscore) => highscore.Mode == mode.GetStaticName());
            return highscore.Value;
        }


        public void Send(int score, IGameMode mode)
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer) return;
            Highscore newHighscore = new Highscore()
            {
                Mode = mode.GetStaticName(),
                Value = score
            };
            int index = _highscores.Values.FindIndex(ByMode(mode));
            bool updated = false;
            if (index != -1)
            {
                if (_highscores.Values[index].Value < newHighscore.Value)
                {
                    _highscores.Values[index] = newHighscore;
                    updated = true;
                }
            }
            else
            {
                _highscores.Values.Add(newHighscore);
                updated = true;
            }
            if (updated)
            {
                string json = JsonUtility.ToJson(_highscores);
                StreamWriter writer = new StreamWriter(_path);
                writer.Write(json);
                writer.Close();
            }
        }


        private Predicate<Highscore> ByMode(IGameMode mode)
        {
            return (highscore) => highscore.Mode == mode.GetStaticName();
        }
    }

}

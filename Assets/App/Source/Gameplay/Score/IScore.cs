using System;

namespace Systemagedon.App.Gameplay
{

    public interface IScore
    {
        public event Action<int> ScoreChanged;
        public int Score { get; }
    }

}


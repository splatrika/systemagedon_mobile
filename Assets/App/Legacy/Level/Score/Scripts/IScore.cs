using System;

namespace Systemagedon.App.Score
{

    public interface IScore
    {
        public event Action Updated;
        public int Score { get; }
    }

}

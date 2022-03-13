using System;

namespace Systemagedon.App.GameComplicaton
{

    public interface IComplication
    {
        public event Action LevelUp;
        public int Level { get; }
    }

}
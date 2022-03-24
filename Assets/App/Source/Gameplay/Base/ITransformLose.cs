using System;
using UnityEngine;

namespace Systemagedon.App.Gameplay
{

    public interface ITransformLose : IGameplay
    {
        public event Action<Transform> Lose;
    }

}

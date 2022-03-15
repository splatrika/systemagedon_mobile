using UnityEngine;


namespace Systemagedon.App.Gameplay
{

    public interface IMovable
    {
        public Vector3 CalculatePoint(float afterSeconds);
    }

}
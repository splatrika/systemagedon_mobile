using UnityEngine;


namespace Systemagedon.App.Movement
{
    public interface IOneAxisTransform
    {
        public float Position { get; }


        public void SetPosition(float value);
        public Vector3 CalculatePoint(float position);
    }
}

using Systemagedon.App.Extensions;
using Systemagedon.App.Movement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Systemagedon.App.Gameplay
{

    [CreateAssetMenu(menuName = "Systemagedon/AsteroidsGenerator Config")]
    public class AsteroidsGenerator : ScriptableObject
    {
        public RangeFloat LeverLength { get => _leverLength; }
        public RangeFloat AsteroidVelocity { get => _asteroidVelocity; }
        public float TopBorder { get => _topBorder; }
        public float BottomBorder { get => _bottomBorder; }


        [SerializeField] private RangeFloat _leverLength;
        [SerializeField] private RangeFloat _asteroidVelocity;
        [SerializeField] private float _topBorder;
        [SerializeField] private float _bottomBorder;


        public Asteroid GenerateAndSpawn(Asteroid prefab, Planet target)
        {
            Asteroid instance = Instantiate(prefab);
            RandomlySetup(instance, target);
            return instance;
        }


        public void RandomlySetup(Asteroid instance, Planet target,
            RangeFloat additionalVelocity = new RangeFloat())
        {
            RangeFloat actualVelocity = _asteroidVelocity + additionalVelocity;
            float speed = actualVelocity.SelectRandom();
            Bezier path = new Bezier();
            path.PointA.y = _topBorder;
            path.PointB.y = _bottomBorder;
            MakeRandomLevers(ref path, LeverLength);
            instance.Init(target, path, speed);
        }


        private void MakeRandomLevers(ref Bezier curve, RangeFloat length)
        {
            float leversAngle = Random.Range(0f, Mathf.PI * 2);
            float leversLength = Random.Range(length.Min, length.Min);
            curve.LerpA.y = _topBorder;
            curve.LerpB.y = _bottomBorder;
            curve.LerpA.x = Mathf.Sin(leversAngle) * leversLength;
            curve.LerpA.z = Mathf.Cos(leversAngle) * leversLength;
            curve.LerpB.x = curve.LerpA.x;
            curve.LerpB.z = curve.LerpA.z;
        }


        public void DrawGizmos()
        {
            Gizmos.color = new Color(0, 0, 1, 0.4f);
            Gizmos.DrawCube(new Vector3(0, _topBorder, 0),
                new Vector3(10, 0.1f, 10));
            Gizmos.DrawCube(new Vector3(0, _bottomBorder, 0),
                new Vector3(10, 0.1f, 10));
        }
    }

}
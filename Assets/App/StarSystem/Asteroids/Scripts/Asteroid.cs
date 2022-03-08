using UnityEngine;
using System.Collections;
using Systemagedon.App.Collisions;
using Systemagedon.App.Movement;

namespace Systemagedon.App.StarSystem
{

    public class Asteroid : MonoBehaviour
    {
        public Ruiner Ruiner { get => _ruiner; }
        public CurveMovement Transform { get => _transform; }
        public OneAxisMovement Movement { get => _movement; }


        [SerializeField] private Ruiner _ruiner;
        [SerializeField] private CurveMovement _transform;
        [SerializeField] private OneAxisMovement _movement;


        private void Start()
        {
            Validate();
        }


        private void Validate()
        {
            bool movementIsNotValid = _movement.Target != _transform;
            if (movementIsNotValid)
            {
                _movement = null;
                Debug.LogError("Target of movement of this asteroid " +
                    "must be transform that assigned to this asteroid");
            }
        }
    }

}

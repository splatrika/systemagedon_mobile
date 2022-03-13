using UnityEngine;
using Systemagedon.App.Movement;


namespace Systemagedon.App.StarSystem
{

    public class Planet : MonoBehaviour
    {
        public OneAxisMovement Movement { get => _movement; }
        public OrbitTransform Orbit { get => _orbit; }
        public LegacyDash Dash { get => _dash; } 


        [SerializeField] private OneAxisMovement _movement;
        [SerializeField] private OrbitTransform _orbit;
        [SerializeField] private LegacyDash _dash;


        private void Start()
        {
            Validate();
        }


        private void Validate()
        {
            bool movementNotValid = _movement
                && (_movement.Target == null
                || _movement.Target != _orbit);
            if (movementNotValid)
            {
                Debug.LogError("Target of Movement must be orbit transform of this planet");
                _movement = null;
            }

            bool dashNotValid = _dash
                && (_dash.Target == null
                || _dash.Target != _movement);
            if (dashNotValid)
            {
                Debug.LogError("Target of Dash must be Movement of this planet");
                _dash = null;
            }
        }
    }

}


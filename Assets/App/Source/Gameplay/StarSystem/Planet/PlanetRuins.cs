using UnityEngine;
using System;

namespace Systemagedon.App.Gameplay
{

    /// <summary>
    /// Only init from prefab allowed. Use PlanetRuins.InitFrom
    /// </summary>
    public class PlanetRuins : MonoBehaviour
    {
        private Planet _storedPrefab;
        private float _storedPosition;
        private float _storedOrbitRadius;
        private float _storedScale;
        private float _storedVelocity;


        public static PlanetRuins InitFrom(PlanetRuins prefab, Planet sender)
        {
            PlanetRuins instance = Instantiate(prefab);
            instance.transform.position = sender.transform.position;
            instance.transform.localScale = sender.transform.localScale;
            instance._storedPrefab = sender.OriginalPrefab;
            instance._storedPosition = sender.AnglePosition;
            instance._storedOrbitRadius = sender.Radius;
            instance._storedScale = sender.Scale;
            instance._storedVelocity = sender.Velocity;
            return instance;
        }


        public Planet Restore()
        {
            Planet instance = Planet.InitFrom(_storedPrefab, _storedOrbitRadius,
                _storedVelocity, _storedScale, _storedPosition);
            Destroy(gameObject);
            return instance;
        }
    }

}
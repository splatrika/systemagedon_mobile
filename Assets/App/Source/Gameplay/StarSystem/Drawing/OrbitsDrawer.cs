using UnityEngine;
using System.Linq;
using Systemagedon.App.FX;

namespace Systemagedon.App.Gameplay
{

    public class OrbitsDrawer : MonoBehaviour
    {
        [SerializeField] private CircleDrawer _drawerPrefab;
        [SerializeField] private StarSystem _target;


        private CircleDrawer[] _createdDrawers;


        private void Start()
        {
            _createdDrawers = new CircleDrawer[_target.Planets.Count()];
            int i = 0;
            foreach (Planet planet in _target.Planets)
            {
                _createdDrawers[i] = Instantiate(_drawerPrefab);
                _createdDrawers[i].Init(planet);
                i++;
            }
        }


        private void OnDestroy()
        {
            foreach (CircleDrawer drawer in _createdDrawers)
            {
                Destroy(drawer);
            }
        }
    }

}


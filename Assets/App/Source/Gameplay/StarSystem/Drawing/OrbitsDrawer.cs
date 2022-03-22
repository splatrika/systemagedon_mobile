using UnityEngine;
using System.Linq;
using Systemagedon.App.FX;

namespace Systemagedon.App.Gameplay
{

    public class OrbitsDrawer : MonoBehaviour
    {
        [SerializeField] private CircleDrawer _drawerPrefab;
        [SerializeField] private GameObject _starSystemObject;


        private CircleDrawer[] _createdDrawers;
        private IStarSystemProvider _starSystem;
        private string invalidStarSystemMessage = "StarSystemObject must have" +
            "component that implements IStarSystemProvider";


        private void Start()
        {
            _starSystem = _starSystemObject.GetComponent<IStarSystemProvider>();
            if (_starSystem == null)
            {
                Debug.LogError(invalidStarSystemMessage);
            }
            _starSystem.ModelUpdated += OnStarSystemUpdated;
            OnStarSystemUpdated(_starSystem);
        }


        private void OnDestroy()
        {
            _starSystem.ModelUpdated -= OnStarSystemUpdated;
            OnRemoveDrawers();
        }


        private void OnStarSystemUpdated(IStarSystemProvider sender)
        {
            OnRemoveDrawers();
            _createdDrawers = new CircleDrawer[_starSystem.Planets.Count()];
            int i = 0;
            foreach (Planet planet in _starSystem.Planets)
            {
                _createdDrawers[i] = Instantiate(_drawerPrefab);
                _createdDrawers[i].Init(planet);
                i++;
            }
        }


        private void OnRemoveDrawers()
        {
            if (_createdDrawers != null)
            {
                foreach (CircleDrawer drawer in _createdDrawers)
                {
                    Destroy(drawer.gameObject);
                }
            }
        }


        private void OnValidate()
        {
            bool invalidStarSystem =
                _starSystemObject.GetComponent<IStarSystemProvider>() == null;
            if (invalidStarSystem)
            {
                Debug.LogError(invalidStarSystemMessage);
                _starSystemObject = null;
            }
        }
    }

}

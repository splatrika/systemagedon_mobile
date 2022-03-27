using UnityEngine;
using System.Linq;
using Systemagedon.App.FX;
using Systemagedon.App.Extensions;
using System;

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
            OnValidate();
            if (_starSystem == null)
            {
                throw new NullReferenceException(nameof(_starSystem));
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
            this.AssignInterfaceField(ref _starSystemObject, ref _starSystem,
                nameof(_starSystemObject));
        }
    }

}

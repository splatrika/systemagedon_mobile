using UnityEngine;
using System.Collections.Generic;

namespace Systemagedon.App.Gameplay.Drawing
{
    
    public class AsteroidsHighlighter : MonoBehaviour
    {
        [SerializeField] private AsteroidPathHighlight _highlightPrefab;
        [SerializeField] private AsteroidsSpawner _asteroidsSpawner;


        private List<AsteroidPathHighlight> _spawnedHighlights;


        private void Awake()
        {
            _spawnedHighlights = new List<AsteroidPathHighlight>();
            _asteroidsSpawner.Spawned += OnAsteroidSpawned;
        }


        private void OnDestroy()
        {
            _asteroidsSpawner.Spawned -= OnAsteroidSpawned;
            _spawnedHighlights.ForEach((highlight) => Destroy(highlight));
        }


        private void OnAsteroidSpawned(Asteroid asteroid)
        {
            AsteroidPathHighlight highlight = Instantiate(_highlightPrefab);
            highlight.Init(asteroid);
            highlight.Destroyed += OnHighlightDestroyed;
            _spawnedHighlights.Add(highlight);
        }


        private void OnHighlightDestroyed(AsteroidPathHighlight sender)
        {
            Destroy(sender.gameObject);
            _spawnedHighlights.Remove(sender);
        }

    }

}

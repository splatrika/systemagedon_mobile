using UnityEngine;
using DG.Tweening;
using Systemagedon.App.Gameplay;
using UnityEngine.UI;

namespace Systemagedon.App.UI
{

    [RequireComponent(typeof(Image))]
    public class ExplosionBlink : MonoBehaviour
    {
        [SerializeField] private float _duration;
        [SerializeField] private Color _blinkColor = Color.white;


        private Image _solid;
        private Color _transperent;
        private Tween _current;


        private void Awake()
        {
            _solid = GetComponent<Image>();
            _transperent = _blinkColor;
            _transperent.a = 0;
            _solid.color = _transperent;
            Explosions.OnExplosion += OnExplosion;
        }


        private void OnDestroy()
        {
            _solid = null;
            Explosions.OnExplosion -= OnExplosion;
            if (_current != null & _current.active)
            {
                _current.Kill();
            }
        }


        private void OnExplosion(Transform sender)
        {
            _current = DOTween.To(() => _solid.color, x => _solid.color = x,
                _transperent, _duration).From(_blinkColor);
        }
    }

}

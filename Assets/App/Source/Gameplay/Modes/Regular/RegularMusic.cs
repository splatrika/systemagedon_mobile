using UnityEngine;
using System.Collections;

namespace Systemagedon.App.Gameplay
{

    public class RegularMusic : MonoBehaviour
    {
        [SerializeField] private RegularGameplay _gameplay;
        [SerializeField] private RegularLoseVisualizer _loseVisualizer;
        [Header("Music")]
        [SerializeField] private AudioSource _regular;
        [SerializeField] private AudioSource _lose;


        private void Awake()
        {
            _regular.Play();
            _gameplay.Lose += OnLose;
            _loseVisualizer.AnimationEnded += OnLoseAnimationEnded;
        }


        private void OnDestroy()
        {
            _gameplay.Lose -= OnLose;
            _loseVisualizer.AnimationEnded -= OnLoseAnimationEnded;
        }


        private void OnLose(RegularLoseContext context)
        {
            _regular.Stop();
        }


        private void OnLoseAnimationEnded()
        {
            _lose.Play();
        }
    }

}

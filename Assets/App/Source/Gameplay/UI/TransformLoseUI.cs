using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Systemagedon.App.Gameplay;

namespace Systemagedon.App.UI
{
    /// <summary>
    /// Init from script required
    /// </summary>
    public class TransformLoseUI : MonoBehaviour
    {
        [SerializeField] private Button _restart;
        private IGameplay _gameplay;
        private bool _inited = false;


        public void Init(IGameplay gameplay)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }    
            _gameplay = gameplay;
            _inited = true;
        }


        private void Awake()
        {
            _restart.onClick.AddListener(OnDestroyButton);
        }


        private void Start()
        {
            if (!_inited)
            {
                Debug.LogError("Init from script required");
            }
        }


        private void OnDestroy()
        {
            _restart.onClick.RemoveListener(OnDestroyButton);
        }

        
        private void OnDestroyButton()
        {
            SceneManager.LoadScene("RegularGameplay"); //TODO abstract
        }
    }


}

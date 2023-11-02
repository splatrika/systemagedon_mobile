using UnityEngine;
using UnityEngine.UI;
using System;

namespace Systemagedon.App.Gameplay
{
    /// <summary>
    /// Init from script requiered
    /// </summary>
    public class RegularLoseUI : MonoBehaviour
    {
        [SerializeField] private Button _restart;
        [SerializeField] private Text _score;


        private RegularMode _mode = new RegularMode();
        private bool _inited = false;


        public void Init(RegularLoseContext context)
        {
            if (_inited)
            {
                throw new InvalidOperationException("Already inited");
            }
            _score.text = context.Score.ToString();
            _inited = true;
        }


        private void Start()
        {
            if (!_inited)
            {
                Debug.LogError("Init from script requiered");
            }
            _restart.onClick.AddListener(OnRestartButton);
        }


        private void OnDestroy()
        {
            _restart.onClick.RemoveListener(OnRestartButton);

        }


        private void OnRestartButton()
        {
            _mode.LoadAndPlay();
        }
    }

}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Systemagedon.App.UI
{

    public class StartScreenView : MonoBehaviour
    {
        [SerializeField] private StartScreen _screen;
        [SerializeField] private Button _play;
        [SerializeField] private Button _thirdPartyInfo;


        private void Start()
        {
            _play.onClick.AddListener(OnPlayButton);
            _thirdPartyInfo.onClick.AddListener(OnThirdPartyInfoButton);
        }


        private void OnDestroy()
        {
            _play.onClick.RemoveListener(OnPlayButton);
            _thirdPartyInfo.onClick.RemoveListener(OnThirdPartyInfoButton);
        }


        private void OnPlayButton()
        {
            _screen.StartGame();
        }


        private void OnThirdPartyInfoButton()
        {
            _screen.ShowThirdPartyInfo();
        }
    }

}

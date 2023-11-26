using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Systemagedon.App.UI
{

    public class ThirdPartyInfoView : MonoBehaviour
    {
        [SerializeField] private Button _back;
        [SerializeField] private string _sceneForBack;


        private void Start()
        {
            _back.onClick.AddListener(OnBackButton);
        }


        private void OnDestroy()
        {
            _back.onClick.RemoveListener(OnBackButton);
        }


        private void OnBackButton()
        {
            SceneManager.LoadScene(_sceneForBack);
        }
    }

}

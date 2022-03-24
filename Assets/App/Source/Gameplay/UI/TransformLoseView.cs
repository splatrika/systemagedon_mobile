using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Systemagedon.App.Gameplay;

namespace Systemagedon.App.UI
{

    public class TransformLoseView : MonoBehaviour
    {
        [SerializeField] private Button _restert;
        private IGameplay _gameplay;


        public void Init(IGameplay gameplay)
        {
            _gameplay = gameplay;
        }


        private void Awake()
        {
            _restert.onClick.AddListener(OnDestroyButton);
        }


        private void OnDestroy()
        {
            _restert.onClick.RemoveListener(OnDestroyButton);
        }

        
        private void OnDestroyButton()
        {
            print("Click");
            SceneManager.LoadScene("RegularGameplay"); //TODO abstract
        }
    }


}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Systemagedon.App.Score
{

    public class ScoreViewer : MonoBehaviour
    {
        [SerializeField] private GameObject _scoreObject;
        [SerializeField] private Text _output;


        private IScore _score;


        private void OnEnable()
        {
            OnValidate();
            _score.Updated += OnScoreUpdated;
            _output.text = _score.Score.ToString();
        }


        private void OnDisable()
        {
            _score.Updated -= OnScoreUpdated;
        }


        private void OnScoreUpdated()
        {
            _output.text = _score.Score.ToString();
        }


        private void OnValidate()
        {
            if (_scoreObject)
            {
                _score = _scoreObject.GetComponent<IScore>();
                if (_score == null)
                {
                    _scoreObject = null;
                    Debug.LogError("Score Object must have component that " +
                        "implements IScore");
                }
            }
        }
    }

}

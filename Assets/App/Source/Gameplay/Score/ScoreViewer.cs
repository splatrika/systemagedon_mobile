using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Systemagedon.App.Gameplay
{

    public class ScoreViewer : MonoBehaviour
    {
        [SerializeField] private GameObject _scoreObject;
        [SerializeField] private Text _output;


        private IScore _score;
        private string _invalidScoreMessage = "ScoreObject must have component" +
            "that implements IScore";


        private void Start()
        {
            _score = _scoreObject.GetComponent<IScore>();
            if (_score == null)
            {
                Debug.LogError(_invalidScoreMessage);
            }
            _score.ScoreChanged += OnScoreChanged;
            _output.text = _score.Score.ToString();
        }


        private void OnDestroy()
        {
            _score.ScoreChanged -= OnScoreChanged;
        }


        private void OnScoreChanged(int score)
        {
            _output.text = score.ToString();
        }


        private void OnValidate()
        {
            bool scoreInvalid = _scoreObject
                && _scoreObject.GetComponent<IScore>() == null;
            if (scoreInvalid)
            {
                Debug.LogError(_invalidScoreMessage);
                _scoreObject = null;
            }
        }
    }

}
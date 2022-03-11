using UnityEngine;
using System.Collections;

namespace Systemagedon.App.GameComplicaton
{

    public abstract class Complicator : MonoBehaviour
    {
        [SerializeField] private GameObject _complicationObject;
        private IComplication _complication;


        private void Start()
        {
            OnValidate();
        }


        protected virtual void Validate() { }


        private void OnValidate()
        {
            if (!_complicationObject)
            {
                return;
            }
            _complication = _complicationObject.GetComponent<IComplication>();
            if (_complication == null)
            {
                _complicationObject = null;
                Debug.LogError("Complication object must have component " +
                    "that implements IComplication");
            }
            Validate();
        }


        private void OnEnable()
        {
            _complication.LevelUp += OnLevelUp;
        }


        private void OnDisable()
        {
            _complication.LevelUp -= OnLevelUp;
        }


        private void OnLevelUp()
        {
            OnComplicate(_complication.Level);
        }


        protected abstract void OnComplicate(int level);
    }

}

using UnityEngine;
using UnityEngine.UI;

namespace Systemagedon.App
{

    public class VersionLabel : MonoBehaviour
    {
        [SerializeField] private Text _label;


        private void Start()
        {
            _label.text = $"{VersionData.Major}.{VersionData.Minor}." +
                $"{VersionData.Patch}";
        }
    }

}

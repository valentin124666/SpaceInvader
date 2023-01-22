using UnityEngine;
using UnityEngine.UI;

namespace Objects
{
    public class Check : MonoBehaviour
    {
        [SerializeField] private Text _text;

        public string Text {
            set => _text.text = value;
        }

    }
}

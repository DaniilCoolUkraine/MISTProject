using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MistProject.UI
{
    public class ShortDataController : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _value;

        public void SetValue(string value)
        {
            _value.text = value;
        }

        public void FillElement(string name, string value)
        {
            _name.text = name;
            _value.text = value;
        }

        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }
    }
}
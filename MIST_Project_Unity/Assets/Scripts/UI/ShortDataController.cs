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
    }
}
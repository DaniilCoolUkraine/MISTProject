using System;
using UnityEngine;
using UnityEngine.UI;

namespace MistProject.UI.Settings
{
    public class ToggleController : MonoBehaviour
    {
        public event Action<bool> OnToggleClick;

        [SerializeField] private Image _toggleFill;
        [SerializeField] private Toggle _toggle;

        [SerializeField] private Sprite _onSprite;
        [SerializeField] private Sprite _offSprite;

        private void OnEnable()
        {
            _toggle.onValueChanged.AddListener(OnToggleStateChanged);
        }

        private void OnDisable()
        {
            _toggle.onValueChanged.RemoveListener(OnToggleStateChanged);
        }

        public void SetToggleState(bool isActive)
        {
            _toggle.SetIsOnWithoutNotify(isActive);

            if (isActive)
            {
                _toggleFill.sprite = _onSprite;
            }
            else
            {
                _toggleFill.sprite = _offSprite;
            }
        }

        public void SetToggleAnimationState(bool animationsEnabled)
        {
            _toggle.toggleTransition = animationsEnabled ? Toggle.ToggleTransition.Fade : Toggle.ToggleTransition.None;
        }

        private void OnToggleStateChanged(bool state)
        {
            OnToggleClick?.Invoke(state);
            SetToggleState(state);
        }
    }
}
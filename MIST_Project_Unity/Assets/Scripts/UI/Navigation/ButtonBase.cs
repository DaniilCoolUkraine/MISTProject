using System;
using MistProject.UI.Screen;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MistProject.UI.Navigation
{
    public class ButtonBase : MonoBehaviour
    {
        public event Action<ScreenBase> OnClick;

        [SerializeField] private Button _button;
        [SerializeField] private ScreenBase _gotoScreen;

        private void OnEnable()
        {
            _button.onClick.AddListener(ButtonAction);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ButtonAction);
        }

        [Inject]
        public void InjectDependencies(NavigationManager navigationManager)
        {
            navigationManager.AddButton(this);
        }

        private void ButtonAction()
        {
            OnClick?.Invoke(_gotoScreen);
        }
    }
}
using System.Collections.Generic;
using MistProject.UI.Screen;
using UnityEngine;
using Zenject;

namespace MistProject.UI.Navigation
{
    public class NavigationManager : MonoBehaviour
    {
        private List<ButtonBase> _buttons;
        private ScreenManager _screenManager;

        [Inject]
        public void InjectDependencies(ScreenManager screenManager)
        {
            _screenManager = screenManager;
        }
        
        public void AddButton(ButtonBase button)
        {
            if (_buttons == null)
            {
                _buttons = new List<ButtonBase>();
            }

            button.OnClick += SwitchScreens;
            
            _buttons.Add(button);
        }

        private void SwitchScreens(ScreenBase screen)
        {
            _screenManager.SwitchScreens(screen);
        }
    }
}
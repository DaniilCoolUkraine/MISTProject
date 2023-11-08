using System.Collections.Generic;
using MistProject.Config;
using MistProject.General;
using UnityEngine;
using Zenject;

namespace MistProject.UI.Screen
{
    public class ScreenManager : MonoBehaviour
    {
        private List<ScreenBase> _screens;

        private GlobalSettingsSO _globalSettings;

        [Inject]
        public void InjectDependencies(GlobalSettingsSO globalSettings)
        {
            _globalSettings = globalSettings;
        }

        public void AddScreen(ScreenBase screen)
        {
            if (_screens == null)
            {
                _screens = new List<ScreenBase>();
            }

            _screens.Add(screen);
        }

        public void SwitchScreens(ScreenBase gotoScreen, bool doFade = true)
        {
            if (_globalSettings.EnableAnimations)
            {
                foreach (var screen in _screens)
                {
                    screen.SwitchScreen(screen == gotoScreen, Constants.ANIMATIONS_DURATION);
                }
            }
        }
    }
}
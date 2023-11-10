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
        private MainScrollbar _mainScrollbar;

        [Inject]
        public void InjectDependencies(GlobalSettingsSO globalSettings, MainScrollbar mainScrollbar)
        {
            _globalSettings = globalSettings;
            _mainScrollbar = mainScrollbar;
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
            float switchDuration = 0;

            if (_globalSettings.EnableAnimations)
            {
                switchDuration = Constants.ANIMATIONS_DURATION;
            }

            foreach (var screen in _screens)
            {
                screen.SwitchScreen(screen == gotoScreen, switchDuration);
            }

            _mainScrollbar.SetScrollContent(gotoScreen.transform);
        }
    }
}
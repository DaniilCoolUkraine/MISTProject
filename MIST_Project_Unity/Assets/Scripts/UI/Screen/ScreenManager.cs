using System.Collections.Generic;
using UnityEngine;

namespace MistProject.UI.Screen
{
    public class ScreenManager : MonoBehaviour
    {
        private List<ScreenBase> _screens;

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
            foreach (var screen in _screens)
            {
                screen.SwitchScreen(screen == gotoScreen);
            }
        }
    }
}
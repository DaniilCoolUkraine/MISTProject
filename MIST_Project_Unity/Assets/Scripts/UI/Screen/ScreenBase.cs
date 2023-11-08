using System;
using UnityEngine;
using Zenject;

namespace MistProject.UI.Screen
{
    public abstract class ScreenBase : MonoBehaviour
    {
        [Inject]
        public void InjectDependencies(ScreenManager screenManager)
        {
            screenManager.AddScreen(this);
        }
        
        public void SwitchScreen(bool isEnabled, float duration = 0)
        {
            throw new NotImplementedException();
        }
    }
}
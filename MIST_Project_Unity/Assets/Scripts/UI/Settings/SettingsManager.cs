using System;
using MistProject.Config;
using UnityEngine;
using Zenject;

namespace MistProject.UI.Settings
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private ToggleController _useCelsius;
        [SerializeField] private ToggleController _useMetricSystem;
        [SerializeField] private ToggleController _useTwelveHoursSystem;
        [SerializeField] private ToggleController _enableAnimations;

        private GlobalSettingsSO _globalSettings;

        private void OnEnable()
        {
            SetToggleInfo(_useCelsius, _globalSettings.UseCelsius);
            SetToggleInfo(_useMetricSystem, _globalSettings.UseMetricSystem);
            SetToggleInfo(_useTwelveHoursSystem, _globalSettings.UseTwelveHoursSystem);
            SetToggleInfo(_enableAnimations, _globalSettings.EnableAnimations);
        }

        [Inject]
        public void InjectDependencies(GlobalSettingsSO globalSettings)
        {
            _globalSettings = globalSettings;

            _useCelsius.OnToggleClick += isOn => SetFieldValue((field => _globalSettings.UseCelsius = field), isOn);
            _useMetricSystem.OnToggleClick += isOn => SetFieldValue((field => _globalSettings.UseMetricSystem = field), isOn);
            _useTwelveHoursSystem.OnToggleClick += isOn => SetFieldValue((field => _globalSettings.UseTwelveHoursSystem = field), isOn);
            _enableAnimations.OnToggleClick += isOn => SetFieldValue((field => _globalSettings.EnableAnimations = field), isOn);
        }

        private void SetToggleInfo(ToggleController toggleController, bool isOn)
        {
            toggleController.SetToggleState(isOn);
            toggleController.SetToggleAnimationState(_globalSettings.EnableAnimations);
        }

        private void SetFieldValue(Action<bool> field, bool value)
        {
            field?.Invoke(value);
        }
    }
}
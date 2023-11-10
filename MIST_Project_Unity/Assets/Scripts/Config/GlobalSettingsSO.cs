using System;
using UnityEngine;

namespace MistProject.Config
{
    [CreateAssetMenu(fileName = "GlobalSettings", menuName = "Config/GlobalSettings", order = 0)]
    public class GlobalSettingsSO : ScriptableObject
    {
        public event Action OnSettingsUpdated;

        [SerializeField] private bool _useCelsius;
        [SerializeField] private bool _useMetricSystem;
        [SerializeField] private bool _useTwelveHoursSystem;
        [SerializeField] private bool _enableAnimations;

        public bool UseCelsius
        {
            get => _useCelsius;
            set
            {
                _useCelsius = value;
                OnSettingsUpdated?.Invoke();
            }
        }

        public bool UseMetricSystem
        {
            get => _useMetricSystem;
            set
            {
                _useMetricSystem = value;
                OnSettingsUpdated?.Invoke();
            }
        }

        public bool UseTwelveHoursSystem
        {
            get => _useTwelveHoursSystem;
            set
            {
                _useTwelveHoursSystem = value;
                OnSettingsUpdated?.Invoke();
            }
        }

        public bool EnableAnimations
        {
            get => _enableAnimations;
            set
            {
                _enableAnimations = value;
                OnSettingsUpdated?.Invoke();
            }
        }
    }
}
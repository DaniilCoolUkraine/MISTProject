using System;
using MistProject.General;
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

        public void LoadSettings()
        {
            if (PlayerPrefs.HasKey(Constants.USE_CELSIUS_KEY))
                UseCelsius = Convert.ToBoolean(PlayerPrefs.GetString(Constants.USE_CELSIUS_KEY));

            if (PlayerPrefs.HasKey(Constants.USE_METRIC_SYSTEM_KEY))
                UseMetricSystem = Convert.ToBoolean(PlayerPrefs.GetString(Constants.USE_METRIC_SYSTEM_KEY));

            if (PlayerPrefs.HasKey(Constants.USE_TWELVE_HOURS_KEY))
                UseTwelveHoursSystem = Convert.ToBoolean(PlayerPrefs.GetString(Constants.USE_TWELVE_HOURS_KEY));

            if (PlayerPrefs.HasKey(Constants.ENABLE_ANIMATIONS_KEY))
                EnableAnimations = Convert.ToBoolean(PlayerPrefs.GetString(Constants.ENABLE_ANIMATIONS_KEY));
        }

        public void SaveSettings()
        {
            PlayerPrefs.SetString(Constants.USE_CELSIUS_KEY, UseCelsius.ToString());
            PlayerPrefs.SetString(Constants.USE_METRIC_SYSTEM_KEY, UseMetricSystem.ToString());
            PlayerPrefs.SetString(Constants.USE_TWELVE_HOURS_KEY, UseTwelveHoursSystem.ToString());
            PlayerPrefs.SetString(Constants.ENABLE_ANIMATIONS_KEY, EnableAnimations.ToString());
        }
    }
}
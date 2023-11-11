using System;
using System.IO;
using Newtonsoft.Json;
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

        public void LoadSettings(string path)
        {
            Debug.Log("<color=green>Loading settings</color>");
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                SettingsStorage storage = JsonConvert.DeserializeObject<SettingsStorage>(json);

                UseCelsius = storage.useCelsius;
                UseMetricSystem = storage.useMetricSystem;
                UseTwelveHoursSystem = storage.useTwelveHoursSystem;
                EnableAnimations = storage.enableAnimations;

                Debug.Log($"<color=green>Loaded settings: {json}</color>");
            }
            else
            {
                Debug.LogError("Couldn't find file!");
            }
        }

        public void SaveSettings(string path)
        {
            Debug.Log($"Saving settings to {path}");

            SettingsStorage storage = new SettingsStorage();

            storage.useCelsius = UseCelsius;
            storage.useMetricSystem = UseMetricSystem;
            storage.useTwelveHoursSystem = UseTwelveHoursSystem;
            storage.enableAnimations = EnableAnimations;

            string json = JsonConvert.SerializeObject(storage);
            File.WriteAllText(path, json);
        }
    }
}
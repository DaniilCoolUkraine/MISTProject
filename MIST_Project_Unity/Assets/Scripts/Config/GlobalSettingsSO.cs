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
        
        [JsonIgnore]
        public bool UseCelsius
        {
            get => _useCelsius;
            set
            {
                _useCelsius = value;
                OnSettingsUpdated?.Invoke();
            }
        }

        [JsonIgnore]
        public bool UseMetricSystem
        {
            get => _useMetricSystem;
            set
            {
                _useMetricSystem = value;
                OnSettingsUpdated?.Invoke();
            }
        }

        [JsonIgnore]
        public bool UseTwelveHoursSystem
        {
            get => _useTwelveHoursSystem;
            set
            {
                _useTwelveHoursSystem = value;
                OnSettingsUpdated?.Invoke();
            }
        }

        [JsonIgnore]
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
                Debug.Log(json);
            }
        }

        public void SaveSettings(string path)
        {
            Debug.Log($"Saving settings to {path}");
            
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText(path, json);
        }
    }
}
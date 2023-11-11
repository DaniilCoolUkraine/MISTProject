using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MistProject.Config;
using MistProject.General;
using MistProject.UI.JsonData;
using UnityEngine;
using Zenject;

namespace MistProject.UI.Forecast
{
    public class ShortForecastManager : MonoBehaviour
    {
        public event Action<List<string>> OnLinksFound; 

        [SerializeField] private ShortDataController[] _shortForecastElements;
        [SerializeField] private string[] _keyTimes;

        private GlobalSettingsSO _globalSettings;

        private ForecastData _currentForecastData;

        [Inject]
        public void InjectDependencies(GlobalSettingsSO globalSettings)
        {
            _globalSettings = globalSettings;
            _globalSettings.OnSettingsUpdated += () => UpdateValues(_currentForecastData);
        }

        public void UpdateValues(ForecastData forecastData)
        {
            if (forecastData == null) 
            {
                Debug.LogWarning("Trying to set null");
                return;
            }
            
            _currentForecastData = forecastData;
            
            List<string> iconsUrl = new List<string>(_shortForecastElements.Length);
            
            var currentDayHours = forecastData.forecast.forecastday.Select(day => day.hour).ToList()[0];

            int j = 0;
            
            for (int i = 0; i < _keyTimes.Length; i++)
            {
                var keyTime = _keyTimes[i];
                var element = _shortForecastElements[i];

                foreach (var hour in currentDayHours)
                {
                    var time = hour.time.Split(" ")[1];
                    if (time == keyTime)
                    {
                        StringBuilder temperature = new StringBuilder();

                        if (_globalSettings.UseCelsius)
                        {
                            temperature.Append(hour.temp_c).Append(Constants.DEGREES).Append(" ")
                                .Append(Constants.CELSIUS_SHORT);
                        }
                        else
                        {
                            temperature.Append(hour.temp_f).Append(Constants.DEGREES).Append(" ")
                                .Append(Constants.FAHRENHEITS_SHORT);
                        }

                        element.FillElement(
                            _globalSettings.UseTwelveHoursSystem ? keyTime.ToTwelveHoursFormat() : keyTime,
                            temperature.ToString());

                        iconsUrl.Add(hour.condition.icon);
                    }
                }
            }
            
            Debug.Log($"Links extracted: {iconsUrl.Count}");
            OnLinksFound?.Invoke(iconsUrl);
        }

        public void UpdateIcons(IEnumerable<SpriteHolder> icons)
        {
            var iconsElements = icons.Zip(_shortForecastElements, (s, c) => new {Sprite = s, Element = c});

            foreach (var iconElement in iconsElements)
            {
                iconElement.Element.SetIcon(iconElement.Sprite.Sprite);
            }
        }
    }
}
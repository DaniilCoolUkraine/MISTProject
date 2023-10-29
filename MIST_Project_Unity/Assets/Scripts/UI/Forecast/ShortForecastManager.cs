﻿using System;
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
        [SerializeField] private ShortDataController[] _shortForecastElements;
        [SerializeField] private string[] _keyTimes;

        private GlobalSettingsSO _globalSettings;

        [Inject]
        public void InjectDependencies(GlobalSettingsSO globalSettings)
        {
            _globalSettings = globalSettings;
        }

        public void UpdateValues(ForecastData forecastData)
        {
            var currentDayHours = forecastData.forecast.forecastday.Select(day => day.hour).ToList()[0];

            for (int i = 0; i < _keyTimes.Length; i++)
            {
                var keyTime = _keyTimes[i];
                var element = _shortForecastElements[i];

                Debug.Log(keyTime);
                
                DateTime dateTime = DateTime.ParseExact(keyTime, "HH:mm", System.Globalization.CultureInfo.InvariantCulture);
                string shortTime = dateTime.ToString("h tt", System.Globalization.CultureInfo.InvariantCulture);
                
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

                        element.FillElement(shortTime, temperature.ToString());
                    }
                }
            }
        }
    }
}
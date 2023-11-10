using System;
using System.Text;
using MistProject.Config;
using MistProject.General;
using MistProject.Utils.Context;
using UnityEngine;
using Zenject;

namespace MistProject.UI.MainWeather
{
    public class AdditionalWeatherInfoController : MonoBehaviour
    {
        [SerializeField] private ShortDataController _windData;
        [SerializeField] private ShortDataController _humidityData;
        [SerializeField] private ShortDataController _feelsLikeData;
        [SerializeField] private ShortDataController _pressureData;

        private GlobalSettingsSO _globalSettings;

        [Inject]
        public void InjectDependencies(GlobalSettingsSO globalSettings)
        {
            _globalSettings = globalSettings;
            _globalSettings.OnSettingsUpdated += UpdateValues;
        }

        private void OnDestroy()
        {
            _globalSettings.OnSettingsUpdated -= UpdateValues;
        }

        public void UpdateValues()
        {
            if (!ContextManager.Instance.TryGetContext<WeatherDataContext>(out var weatherData)) return;

            StringBuilder sb = new StringBuilder();

            var newWeatherData = weatherData.WeatherData.current;
            if (_globalSettings.UseMetricSystem)
                sb.Append(newWeatherData.wind_kph).Append(" ").Append(Constants.KILOMETERS_HOUR);
            else
                sb.Append(newWeatherData.wind_mph).Append(" ").Append(Constants.MILES_HOUR);
            _windData.SetValue(sb.ToString());

            sb.Clear();

            sb.Append(newWeatherData.humidity).Append('%');
            _humidityData.SetValue(sb.ToString());

            sb.Clear();

            if (_globalSettings.UseCelsius)
                sb.Append(newWeatherData.feelslike_c).Append(Constants.DEGREES).Append(Constants.CELSIUS_SHORT);
            else
                sb.Append(newWeatherData.feelslike_f).Append(Constants.DEGREES).Append(Constants.FAHRENHEITS_SHORT);

            _feelsLikeData.SetValue(sb.ToString());

            sb.Clear();

            sb.Append(newWeatherData.pressure_mb).Append(" ").Append(Constants.MILLIBAR);

            _pressureData.SetValue(sb.ToString());
        }
    }
}
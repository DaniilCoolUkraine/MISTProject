using System.Globalization;
using System.Text;
using MistProject.Config;
using MistProject.General;
using MistProject.UI.JsonData;
using MistProject.Utils.Context;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace MistProject.UI
{
    public class MainWeatherWidgetController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _cityCountry;
        [SerializeField] private TextMeshProUGUI _temperature;
        [SerializeField] private TextMeshProUGUI _unitsOfMeasurement;
        [SerializeField] private TextMeshProUGUI _generalDescription;
        
        [SerializeField] private Image _weatherTypeIcon;

        private GlobalSettingsSO _globalSettings;

        [Inject]
        public void InjectDependencies(GlobalSettingsSO globalSettings)
        {
            _globalSettings = globalSettings;
        }

        public void SetTexts(WeatherData weatherData)
        {
            StringBuilder sb = new StringBuilder();

            _cityCountry.text = sb.Append(weatherData.location.name).Append(", ").Append(weatherData.location.country).ToString();
            sb.Clear();

            if (_globalSettings.UseCelsius)
            {
                _temperature.text = sb.Append(weatherData.current.temp_c.ToString(CultureInfo.CreateSpecificCulture("en-GB"))).Append(Constants.DEGREES).ToString();
                _unitsOfMeasurement.text = Constants.CELSIUS;
            }
            else
            {
                _temperature.text = sb.Append(weatherData.current.temp_f.ToString(CultureInfo.CreateSpecificCulture("en-GB"))).Append(Constants.DEGREES).ToString();
                _unitsOfMeasurement.text = Constants.FAHRENHEITS;
            }

            sb.Clear();
            _generalDescription.text = sb.Append(weatherData.current.condition.text).Append(" with ")
                .Append(weatherData.current.humidity).Append("% humidity").ToString();

            SaveToContext(weatherData);
        }

        public void SetWeatherIcon(Sprite weatherIcon)
        {
            _weatherTypeIcon.sprite = weatherIcon;
            _weatherTypeIcon.color = Color.white;
        }

        private void SaveToContext(WeatherData weatherData)
        {
            ContextManager.Instance.BindContext<WeatherDataContext>(new WeatherDataContext(weatherData));
        }
    }
}
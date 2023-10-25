using System.Globalization;
using System.Text;
using MistProject.General;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MistProject.UI
{
    public class MainWeatherWidgetController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _cityCountry;
        [SerializeField] private TextMeshProUGUI _temperature;
        [SerializeField] private TextMeshProUGUI _unitsOfMeasurement;
        [SerializeField] private TextMeshProUGUI _generalDescription;
        
        [SerializeField] private Image _weatherTypeIcon;

        public void SetTexts(MainWeatherData weatherData)
        {
            StringBuilder sb = new StringBuilder();

            _cityCountry.text = sb.Append(weatherData.location.name).Append(", ").Append(weatherData.location.country).ToString();
            sb.Clear();
            
            // todo add faringates holder
            _temperature.text = sb.Append(weatherData.current.temp_c.ToString(CultureInfo.CreateSpecificCulture("en-GB"))).Append(Constants.DEGREES).ToString();
            // _unitsOfMeasurement.text = here faringate

            sb.Clear();
            _generalDescription.text = sb.Append(weatherData.current.condition.text).Append(" with ")
                .Append(weatherData.current.humidity).Append("% humidity").ToString();
        }

        public void SetWeatherIcon(Sprite weatherIcon)
        {
            _weatherTypeIcon.sprite = weatherIcon;
        }
    }
}
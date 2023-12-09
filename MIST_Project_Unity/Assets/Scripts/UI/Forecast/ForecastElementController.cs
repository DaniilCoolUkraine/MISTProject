using MistProject.General;
using MistProject.UI.JsonData;
using TMPro;
using UnityEngine;

namespace MistProject.UI.Forecast
{
    public class ForecastElementController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _conditions;
        [SerializeField] private TextMeshProUGUI _date;
        [SerializeField] private TextMeshProUGUI _maxTemperature;
        [SerializeField] private TextMeshProUGUI _minTemperature;

        public void Initialize(Forecastday day, bool useCelsius)
        {
            _conditions.text = day.day.condition.text;
            _date.text = day.date.ToAppDate();

            _maxTemperature.text = useCelsius ? ((int) day.day.maxtemp_c).ToString() : ((int) day.day.maxtemp_f).ToString();
            _minTemperature.text = useCelsius ? ((int) day.day.mintemp_c).ToString() : ((int) day.day.mintemp_f).ToString();
        }
    }
}
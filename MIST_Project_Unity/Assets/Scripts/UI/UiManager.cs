using MistProject.UI.Forecast;
using MistProject.UI.MainWeather;
using MistProject.Utils.Context;
using UnityEngine;

namespace MistProject.UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private MainWeatherWidgetController _mainWeatherWidgetController;
        [SerializeField] private MainWeatherWidgetRequestController _mainWeatherRequest;
        [SerializeField] private AdditionalWeatherInfoController _additionalWeatherInfoController;
        [SerializeField] private ForecastRequestController _forecastRequestController;
        [SerializeField] private ShortForecastManager _shortForecastManager;

        private void OnEnable()
        {
            _mainWeatherRequest.OnRequestSuccess += _mainWeatherWidgetController.SetTexts;
            _mainWeatherRequest.OnImageLoaded += _mainWeatherWidgetController.SetWeatherIcon;
            _forecastRequestController.OnRequestSuccess += _shortForecastManager.UpdateValues;

            ContextManager.Instance.OnContextUpdated += _additionalWeatherInfoController.UpdateValues;
        }

        private void OnDisable()
        {
            _mainWeatherRequest.OnRequestSuccess -= _mainWeatherWidgetController.SetTexts;
            _mainWeatherRequest.OnImageLoaded -= _mainWeatherWidgetController.SetWeatherIcon;
            _forecastRequestController.OnRequestSuccess -= _shortForecastManager.UpdateValues;

            ContextManager.Instance.OnContextUpdated -= _additionalWeatherInfoController.UpdateValues;
        }
    }
}
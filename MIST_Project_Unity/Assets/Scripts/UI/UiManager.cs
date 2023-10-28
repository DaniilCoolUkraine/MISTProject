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

        private void OnEnable()
        {
            _mainWeatherRequest.OnRequestSuccess += _mainWeatherWidgetController.SetTexts;
            _mainWeatherRequest.OnImageLoaded += _mainWeatherWidgetController.SetWeatherIcon;
            ContextManager.Instance.OnContextUpdated += _additionalWeatherInfoController.UpdateValues;
        }

        private void OnDisable()
        {
            _mainWeatherRequest.OnRequestSuccess -= _mainWeatherWidgetController.SetTexts;
            _mainWeatherRequest.OnImageLoaded -= _mainWeatherWidgetController.SetWeatherIcon;
            ContextManager.Instance.OnContextUpdated -= _additionalWeatherInfoController.UpdateValues;
        }
    }
}
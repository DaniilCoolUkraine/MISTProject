using MistProject.Config;
using MistProject.General;
using MistProject.UI.JsonData;
using MistProject.Utils.Context;
using UnityEngine;
using Zenject;

namespace MistProject.UI.Forecast
{
    public class DetailedForecastManager : MonoBehaviour
    {
        [SerializeField] private Transform _elementsParent;
        [SerializeField] private ForecastElementController _forecastPrefab;

        private GlobalSettingsSO _globalSettings;

        private ForecastData _currentForecastData;

        [Inject]
        public void InjectDependencies(GlobalSettingsSO globalSettings)
        {
            _globalSettings = globalSettings;
            _globalSettings.OnSettingsUpdated += () => UpdateValues(_currentForecastData);
        }

        private void OnEnable()
        {
            ContextManager.Instance.TryGetContext<ForecastContext>(out var context);
            UpdateValues(context.ForecastData);
        }

        private void UpdateValues(ForecastData currentForecastData)
        {
            _currentForecastData = currentForecastData;
            if (currentForecastData == null)
                return;
            
            _elementsParent.KillAllChildren<ForecastElementController>();

            ForecastElementController temp;

            foreach (Forecastday forecast in currentForecastData.forecast.forecastday)
            {
                temp = Instantiate(_forecastPrefab, _elementsParent);
                temp.Initialize(forecast, _globalSettings.UseCelsius);
            }
        }
    }
}
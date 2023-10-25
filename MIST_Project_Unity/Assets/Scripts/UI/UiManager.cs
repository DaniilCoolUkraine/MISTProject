using UnityEngine;

namespace MistProject.UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private MainWeatherWidgetController _mainWeatherWidgetController;
        [SerializeField] private MainWeatherWidgetRequestController _mainWeatherRequest;

        private void OnEnable()
        {
            _mainWeatherRequest.OnRequestSuccess += _mainWeatherWidgetController.SetTexts;
        }

        private void OnDisable()
        {
            _mainWeatherRequest.OnRequestSuccess -= _mainWeatherWidgetController.SetTexts;
        }
    }
}
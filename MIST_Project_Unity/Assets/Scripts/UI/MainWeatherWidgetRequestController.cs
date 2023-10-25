using System;
using System.Globalization;
using MistProject.General;
using MistProject.Requests;
using MistProject.Requests.Response;
using MistProject.Utils;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace MistProject.UI
{
    public class MainWeatherWidgetRequestController : MonoBehaviour
    {
        public event Action<MainWeatherData> OnRequestSuccess;
        public event Action<Sprite> OnImageLoaded; 
        public event Action OnRequestFailed;

        private string _emptyApiLink = Constants.GLOBAL_API_LINK + "current.json?" + "q=&" + $"key={Constants.API_KEY}";

        private RequestHolder _requestHolder;
        private LocationUtils _locationUtils;

        private int _currentLocationRequestAttempt = 0;

        [Inject]
        public void InjectDependencies(RequestHolder requestHolder, LocationUtils locationUtils)
        {
            _requestHolder = requestHolder;
            _locationUtils = locationUtils;

            RequestLocation();
        }

        private void RequestLocation()
        {
            if (_currentLocationRequestAttempt < Constants.MAX_REQUEST_ATTEMPTS_COUNT)
            {
                Debug.LogWarning($"Location attempt {_currentLocationRequestAttempt}");
                _locationUtils.OnLocationGetError += LocationGetError;
                _locationUtils.GetLocation(LocationGetSuccessCallback);
                _currentLocationRequestAttempt++;
            }
        }

        private void LocationGetSuccessCallback(LocationInfo location)
        {
            _locationUtils.OnLocationGetError -= LocationGetError;

            var filledApiLink = _emptyApiLink.Insert(_emptyApiLink.IndexOf("q=") + 2,
                $"{location.latitude.ToString(CultureInfo.CreateSpecificCulture("en-GB"))}{Constants.COMMA_CODE}{location.longitude.ToString(CultureInfo.CreateSpecificCulture("en-GB"))}");

            Debug.Log($"<color=green>Sending to {filledApiLink}</color>");

            _requestHolder.SendGetRequest(filledApiLink, null, ResponseActions);
        }

        private void LocationGetError(LocationErrors locationErrors)
        {
            _locationUtils.OnLocationGetError -= LocationGetError;

            if (locationErrors == LocationErrors.TimeOut)
            {
                Debug.Log("TimeOut");
            }
            else if (locationErrors == LocationErrors.UnableToDetermineLocation)
            {
                Debug.Log("UnableToDetermineLocation");
            }

            RequestLocation();
        }

        private void ResponseActions(IResponseData responseData)
        {
            if (responseData is SuccessResponseData)
            {
                try
                {
                    Debug.Log(responseData.GetText());
                    MainWeatherData mainWeatherData =
                        JsonConvert.DeserializeObject<MainWeatherData>(responseData.GetText());

                    if (mainWeatherData == null || mainWeatherData.current == null || mainWeatherData.location == null)
                    {
                        throw new NullReferenceException("Some values are null");
                    }

                    OnRequestSuccess?.Invoke(mainWeatherData);
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                    OnRequestFailed?.Invoke();
                }
            }
            else
            {
                Debug.LogError(responseData.GetText());
                OnRequestFailed?.Invoke();
            }
        }
    }
}
using System;
using System.Globalization;
using MistProject.General;
using MistProject.Requests;
using MistProject.Requests.Response;
using MistProject.UI.JsonData;
using MistProject.Utils.Context;
using Newtonsoft.Json;
using UnityEngine;
using Zenject;

namespace MistProject.UI.Forecast
{
    public class ForecastRequestController : UiLocationRequestController
    {
        public event Action<ForecastData> OnRequestSuccess;
        public event Action OnServerRequestFailed;

        private string _emptyApiLink =
            Constants.GLOBAL_API_LINK + "forecast.json?" + "q=&" + $"days=3&key={Constants.API_KEY}";
        
        private RequestHolder _requestHolder;
        
        private SpriteHolder _spriteHolder;
        private TextureHolder _textureHolder;

        [Inject]
        public void InjectDependencies(RequestHolder requestHolder)
        {
            _requestHolder = requestHolder;
            RequestLocation();
        }
        
        protected override void LocationGetSuccessCallback(LocationInfo location)
        {
            LocationUtils.OnLocationGetError -= LocationGetError;
            ContextManager.Instance.BindContext<LocationContext>(new LocationContext(location));

            var filledApiLink = _emptyApiLink.Insert(_emptyApiLink.IndexOf("q=") + 2,
                $"{location.latitude.ToString(CultureInfo.CreateSpecificCulture("en-GB"))}{Constants.COMMA_CODE}{location.longitude.ToString(CultureInfo.CreateSpecificCulture("en-GB"))}");

            Debug.Log($"<color=green>Sending to {filledApiLink}</color>");

            _requestHolder.SendGetRequest(filledApiLink, null, RequestType.Json, ResponseActions);
        }
        
        private void ResponseActions(IResponseData responseData)
        {
            if (responseData is SuccessResponseData)
            {
                try
                {
                    Debug.Log($"<color=#46ABF2>Forecast</color> {responseData.GetText()}");
                    ForecastData data =
                        JsonConvert.DeserializeObject<ForecastData>(responseData.GetText());

                    if (data == null || data.current == null || data.location == null || data.forecast == null)
                    {
                        throw new NullReferenceException("Some values are null");
                    }

                    OnRequestSuccess?.Invoke(data);
                    ContextManager.Instance.BindContext<ForecastContext>(new ForecastContext(data));

                    // RequestImage(mainWeatherData.current.condition.icon);
                }
                catch (Exception e)
                {
                    Debug.LogError(e.Message);
                    OnServerRequestFailed?.Invoke();
                }
            }
            else
            {
                Debug.LogError(responseData.GetText());
                OnServerRequestFailed?.Invoke();
            }
        }
    }
}
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

namespace MistProject.UI.MainWeather
{
    public class MainWeatherWidgetRequestController : UiLocationRequestController
    {
        public event Action<WeatherData> OnRequestSuccess;
        public event Action<Sprite> OnImageLoaded;
        public event Action OnServerRequestFailed;

        private string _emptyApiLink = Constants.GLOBAL_API_LINK + "current.json?" + "q=&" + $"key={Constants.API_KEY}";

        private RequestHolder _requestHolder;
        
        private SpriteHolder _spriteHolder;
        private TextureHolder _textureHolder;

        [Inject]
        public void InjectDependencies(RequestHolder requestHolder)
        {
            _requestHolder = requestHolder;
        }

        private void OnDestroy()
        {
            _spriteHolder?.Dispose();
            _textureHolder?.Dispose();
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
                    Debug.Log($"<color=#46ABF2>Current weather</color> {responseData.GetText()}");
                    WeatherData mainWeatherData =
                        JsonConvert.DeserializeObject<WeatherData>(responseData.GetText());

                    if (mainWeatherData == null || mainWeatherData.current == null || mainWeatherData.location == null)
                    {
                        throw new NullReferenceException("Some values are null");
                    }

                    OnRequestSuccess?.Invoke(mainWeatherData);
                    ContextManager.Instance.BindContext<WeatherDataContext>(new WeatherDataContext(mainWeatherData));

                    RequestImage(mainWeatherData.current.condition.icon);
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

        private void RequestImage(string link)
        {
            _requestHolder.SendGetRequest(link, null, RequestType.Image, ImageResponseActions);
        }

        private void ImageResponseActions(IResponseData responseData)
        {
            if (responseData is ImageResponseData imageResponseData)
            {
                _spriteHolder?.Dispose();
                _textureHolder?.Dispose();
                
                _spriteHolder = new SpriteHolder();
                _textureHolder = imageResponseData.Texture;
                
                Sprite loadedSprite = Sprite.Create(_textureHolder.Texture, new Rect(0,0,Constants.LOADED_IMAGE_SIZE,Constants.LOADED_IMAGE_SIZE), new Vector2(0.5f, 0.5f));
                _spriteHolder.SetSprite(loadedSprite);
                
                OnImageLoaded?.Invoke(loadedSprite);
            }
            else
            {
                Debug.LogError(responseData.GetType() + " " + responseData.GetText());
                OnServerRequestFailed?.Invoke();
            }
        }
    }
}
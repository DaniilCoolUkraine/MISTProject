using System;
using System.Collections.Generic;
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

        public event Action<List<SpriteHolder>> OnIconsLoaded;

        private string _emptyApiLink =
            Constants.GLOBAL_API_LINK + "forecast.json?" + "q=&" + $"days=3&key={Constants.API_KEY}";
        
        private RequestHolder _requestHolder;
        
        private SpriteHolder _spriteHolder;
        private TextureHolder _textureHolder;

        private List<SpriteHolder> _loadedIcons;
        private int _maxIcons;
        private int _currentIcon = 0;

        [Inject]
        public void InjectDependencies(RequestHolder requestHolder)
        {
            _requestHolder = requestHolder;
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

        public void RequestIcons(List<string> iconsUrls)
        {
            if (!iconsUrls.ListIsEmptyOrNull())
            {
                _loadedIcons?.ForEach(s => s.Dispose());
                
                _maxIcons = iconsUrls.Count;
                _loadedIcons = new List<SpriteHolder>(_maxIcons);

                foreach (string url in iconsUrls)
                {
                    Debug.Log($"<color=green>Sending to {url}</color>");
                    _requestHolder.SendGetRequest(url, null, RequestType.Image, OnImageLoaded);
                }
            }
        }

        private void OnImageLoaded(IResponseData responseData)
        {
            if (responseData is ImageResponseData imageResponseData)
            {
                Debug.Log($"<color=#46ABF2>Loaded icons {_currentIcon + 1}/{_maxIcons}</color>");
                
                var tempSprite = new SpriteHolder();
                var tempTexture = imageResponseData.Texture;
                
                Sprite loadedSprite = Sprite.Create(tempTexture.Texture, new Rect(0,0,Constants.LOADED_IMAGE_SIZE,Constants.LOADED_IMAGE_SIZE), new Vector2(0.5f, 0.5f));
                tempSprite.SetSprite(loadedSprite);
                
                _loadedIcons.Add(tempSprite);
                _currentIcon++;

                if (_loadedIcons.Count == _maxIcons)
                {
                    OnIconsLoaded?.Invoke(_loadedIcons);
                }
            }
            else
            {
                Debug.LogError($"Cannot load icon({responseData.GetType()}): {responseData.GetText()}");
            }
        }
    }
}
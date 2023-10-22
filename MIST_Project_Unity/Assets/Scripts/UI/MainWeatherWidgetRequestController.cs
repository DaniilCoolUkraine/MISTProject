using System;
using MistProject.General;
using MistProject.Requests;
using MistProject.Requests.Response;
using MistProject.Utils;
using UnityEngine;
using Zenject;

namespace MistProject.UI
{
    public class MainWeatherWidgetRequestController : MonoBehaviour
    {
        private static readonly string API_LINK = Constants.GLOBAL_API_LINK + "current.json?" + "q=46.476608%2C30.707310" + "&" +
                                                  $"key={Constants.API_KEY}";

        private RequestHolder _requestHolder;
        private LocationUtils _locationUtils;
        
        private void Awake()
        {
            _requestHolder.SendGetRequest(API_LINK, null, ResponseActions);
        }

        [Inject]
        public void InjectDependencies(RequestHolder requestHolder, LocationUtils locationUtils)
        {
            _requestHolder = requestHolder;
            _locationUtils = locationUtils;

            _locationUtils.OnLocationGetError += LocationGetError;
            
            _locationUtils.GetLocation(LocationGetSuccess);
        }

        private void LocationGetSuccess(LocationInfo location)
        {
            _locationUtils.OnLocationGetError -= LocationGetError;
            
            Debug.Log($"{location.latitude}, {location.longitude}");
            
            throw new NotImplementedException("Location success isn`t implemented");
        }

        private void LocationGetError(LocationErrors locationErrors)
        {
            _locationUtils.OnLocationGetError -= LocationGetError;
            throw new NotImplementedException("Location error isn`t implemented");
        }
        
        private void ResponseActions(IResponseData responseData)
        {
            Debug.Log(responseData.GetText());
        }
    }
}
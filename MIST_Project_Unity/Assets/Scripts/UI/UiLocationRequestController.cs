using System;
using MistProject.General;
using MistProject.Utils.Context;
using MistProject.Utils.Location;
using UnityEngine;
using Zenject;

namespace MistProject.UI
{
    public abstract class UiLocationRequestController : MonoBehaviour
    {
        public event Action OnLocationRequestFailed;
        
        protected LocationUtils LocationUtils { get; private set; }

        private int _currentLocationRequestAttempt = 0;

        [Inject]
        public void InjectDependencies(LocationUtils locationUtils)
        {
            LocationUtils = locationUtils;
        }
        
        protected void RequestLocation()
        {
            if (ContextManager.Instance.TryGetContext<LocationContext>(out var location))
            {
                LocationGetSuccessCallback(location.LocationInfo);
                return;
            }

            if (_currentLocationRequestAttempt < Constants.MAX_REQUEST_ATTEMPTS_COUNT)
            {
                Debug.LogWarning($"Location attempt {_currentLocationRequestAttempt}");
                LocationUtils.OnLocationGetError += LocationGetError;
                LocationUtils.GetLocation(LocationGetSuccessCallback);
                _currentLocationRequestAttempt++;
            }
        }
        
        protected abstract void LocationGetSuccessCallback(LocationInfo location);
        
        protected void LocationGetError(LocationErrors locationErrors)
        {
            LocationUtils.OnLocationGetError -= LocationGetError;

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
    }
}
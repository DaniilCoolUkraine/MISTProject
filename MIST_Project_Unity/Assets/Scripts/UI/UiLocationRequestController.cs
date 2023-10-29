using System;
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
            
            LocationUtils.OnLocationGetError += LocationGetError;
            LocationUtils.OnLocationGetSuccess += LocationGetSuccessCallback;
        }

        private void OnDisable()
        {
            LocationUtils.OnLocationGetError -= LocationGetError;
            LocationUtils.OnLocationGetSuccess -= LocationGetSuccessCallback;
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
        }
    }
}
using System;
using System.Collections;
using MistProject.General;
using UnityEngine;

namespace MistProject.Utils
{
    public class LocationUtils : MonoBehaviour
    {
        public event Action<LocationInfo> OnLocationGetSuccess;
        public event Action<LocationErrors> OnLocationGetError;
        
        public IEnumerator GetLocation()
        {
            if (RequestPermissions()) yield break;

            LocationService location = Input.location;
            location.Start(Constants.LOCATION_ACCURACY, Constants.LOCATION_UPDATE);
            
            yield return new WaitForSeconds(Constants.WAIT_BEFORE_TIMEOUT);

            if (location.status == LocationServiceStatus.Initializing)
            {
                OnLocationGetError?.Invoke(LocationErrors.TimeOut);
            }
            else if (location.status != LocationServiceStatus.Running)
            {
                OnLocationGetError?.Invoke(LocationErrors.UnableToDetermineLocation);
            }
            else
            {
                OnLocationGetSuccess?.Invoke(location.lastData);
            }
            
            location.Stop();
        }

        private bool RequestPermissions()
        {
#if UNITY_ANDROID
            if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(UnityEngine.Android.Permission.CoarseLocation))
            {
                UnityEngine.Android.Permission.RequestUserPermission(UnityEngine.Android.Permission.CoarseLocation);

                if (!Input.location.isEnabledByUser)
                {
                    OnLocationGetError?.Invoke(LocationErrors.PermissionNotGranted);
                    return true;
                }
            }
#elif UNITY_IOS
            if (!Input.location.isEnabledByUser)
            {
                OnLocationGetError?.Invoke(LocationErrors.PermissionNotGranted);
                return true;
            }
#endif
            return false;
        }
    }
}
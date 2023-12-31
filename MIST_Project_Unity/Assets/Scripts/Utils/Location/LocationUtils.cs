﻿using System;
using System.Collections;
using MistProject.General;
using UnityEngine;
using UnityEngine.Android;

namespace MistProject.Utils.Location
{
    public class LocationUtils : MonoBehaviour
    {
        public event Action<LocationInfo> OnLocationGetSuccess;
        public event Action<LocationErrors> OnLocationGetError;

        private void Start()
        {
            StopAllCoroutines();
            StartCoroutine(GetLocationCoroutine());
        }

        private IEnumerator GetLocationCoroutine()
        {
            if (!Input.location.isEnabledByUser)
            {
                Debug.Log("gps isn't enabled");
                Permission.RequestUserPermission(Permission.FineLocation);
            }

            Input.location.Start(Constants.LOCATION_ACCURACY, Constants.LOCATION_UPDATE);

            float maxWait = Constants.WAIT_BEFORE_TIMEOUT;

            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1);
                maxWait--;
                Debug.Log(
                    $"Waiting for {maxWait} from {Constants.WAIT_BEFORE_TIMEOUT} with status {Input.location.status}");
            }

            if (maxWait <= 0)
            {
                OnLocationGetError?.Invoke(LocationErrors.TimeOut);
            }

            if (Input.location.status == LocationServiceStatus.Failed)
            {
                OnLocationGetError?.Invoke(LocationErrors.UnableToDetermineLocation);
            }
            else
            {
                OnLocationGetSuccess?.Invoke(Input.location.lastData);
            }

            Input.location.Stop();
        }
    }
}
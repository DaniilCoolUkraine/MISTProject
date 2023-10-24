using System.Globalization;
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
        private string _emptyApiLink = Constants.GLOBAL_API_LINK + "current.json?" + "q=&" + $"key={Constants.API_KEY}";

        private RequestHolder _requestHolder;
        private LocationUtils _locationUtils;

        [Inject]
        public void InjectDependencies(RequestHolder requestHolder, LocationUtils locationUtils)
        {
            _requestHolder = requestHolder;
            _locationUtils = locationUtils;

            _locationUtils.OnLocationGetError += LocationGetError;
            _locationUtils.GetLocation(LocationGetSuccessCallback);
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

            // todo add multiple requests
        }

        private void ResponseActions(IResponseData responseData)
        {
            Debug.Log(responseData.GetText());
        }
    }
}
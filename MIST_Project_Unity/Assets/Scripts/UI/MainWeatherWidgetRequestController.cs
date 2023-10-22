using MistProject.General;
using MistProject.Requests;
using MistProject.Requests.Response;
using UnityEngine;
using Zenject;

namespace MistProject.UI
{
    public class MainWeatherWidgetRequestController : MonoBehaviour
    {
        private static readonly string API_LINK = Constants.GLOBAL_API_LINK + "current.json?" + "q=46.476608%2C30.707310" + "&" +
                                                  $"key={Constants.API_KEY}";

        private RequestHolder _requestHolder;
        
        private void Awake()
        {
            _requestHolder.SendGetRequest(API_LINK, null, ResponseActions);
        }

        [Inject]
        public void InjectDependencies(RequestHolder requestHolder)
        {
            _requestHolder = requestHolder;
        }
        
        private void ResponseActions(IResponseData responseData)
        {
            Debug.Log(responseData.GetText());
        }
    }
}
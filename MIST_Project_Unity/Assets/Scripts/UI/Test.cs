using MistProject.General;
using MistProject.Requests;
using MistProject.Requests.Response;
using UnityEngine;

namespace MistProject.UI
{
    public class Test : MonoBehaviour
    {
        [SerializeField] private RequestHolder _requestHolder;

        private static readonly string API_LINK = Constants.GLOBAL_API_LINK + "current.json?" + "q=46.476608%2C30.707310" + "&" +
                                                  $"key={Constants.API_KEY}";

        private void Awake()
        {
            _requestHolder.SendGetRequest(API_LINK, null, ResponseActions);
        }

        private void ResponseActions(IResponseData responseData)
        {
            Debug.Log(responseData.GetText());
        }
    }
}
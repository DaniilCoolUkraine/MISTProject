using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using MistProject.Requests.Post;
using MistProject.Requests.Response;
using UnityEngine;
using UnityEngine.Networking;
using MistProject.General;

namespace MistProject.Requests
{
    public class RequestHolder : MonoBehaviour
    {
        private Dictionary<string, Coroutine> _activeRequests;

        public void SendGetRequest(string url, [CanBeNull] IPostData postData, Action<IResponseData> callback)
        {
            if (_activeRequests == null)
                _activeRequests = new Dictionary<string, Coroutine>();

            if (_activeRequests.TryGetValue(url, out var request))
                StopCoroutine(request);

            _activeRequests[url] = StartCoroutine(GetRequestCoroutine(url, postData, callback));
        }

        private IEnumerator GetRequestCoroutine(string url, [CanBeNull] IPostData postData,
            Action<IResponseData> callback)
        {
            UnityWebRequest getRequest = UnityWebRequest.Get(url);
            getRequest.uploadHandler = new UploadHandlerRaw(postData?.RawData);

            yield return getRequest.SendWebRequest();

            IResponseData responseData;

            if (getRequest.result == UnityWebRequest.Result.Success)
            {
                responseData = new SuccessResponseData();
                responseData.Data = getRequest.downloadHandler.data;
            }
            else
            {
                responseData = new ErrorResponseData();
                responseData.Data = getRequest.error.ToByte();
            }

            responseData.ResponseCode = getRequest.responseCode;

            callback?.Invoke(responseData);
            _activeRequests.Remove(url);
        }
    }
}
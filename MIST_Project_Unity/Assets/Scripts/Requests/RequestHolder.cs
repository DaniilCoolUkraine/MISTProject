using System;
using System.Collections;
using MistProject.Requests.Post;
using MistProject.Requests.Response;
using UnityEngine;
using UnityEngine.Networking;
using MistProject.General;

namespace MistProject.Requests
{
    public class RequestHolder : MonoBehaviour
    {
        public void SendGetRequest(string url, IPostData? postData, RequestType requestType,
            Action<IResponseData> callback)
        {
            if (requestType == RequestType.Json)
            {
                StartCoroutine(GetRequestCoroutine(url, postData, callback));
            }
            else if (requestType == RequestType.Image)
            {
                StartCoroutine(ImageGetRequestCoroutine(url, postData, callback));
            }
        }

        private IEnumerator GetRequestCoroutine(string url, IPostData? postData, Action<IResponseData> callback)
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
        }

        private IEnumerator ImageGetRequestCoroutine(string url, IPostData? postData, Action<IResponseData> callback)
        {
            UnityWebRequest getRequest = UnityWebRequestTexture.GetTexture(url);
            getRequest.uploadHandler = new UploadHandlerRaw(postData?.RawData);

            yield return getRequest.SendWebRequest();

            IResponseData responseData;

            if (getRequest.result == UnityWebRequest.Result.Success)
            {
                var textureResponse = new ImageResponseData();

                textureResponse.Texture = new TextureHolder();
                textureResponse.Texture.SetTexture(((DownloadHandlerTexture) getRequest.downloadHandler).texture);

                responseData = textureResponse;
            }
            else
            {
                responseData = new ErrorResponseData();
                responseData.Data = getRequest.error.ToByte();
            }

            callback?.Invoke(responseData);
        }
    }
}
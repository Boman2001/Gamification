using System;
using System.Collections;
using System.Text;
using Config;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Helpers
{
    public class RestHelper : MonoBehaviour
    {
        private IEnumerator Post(string url, string bodyJsonString)
        {
            var request = new UnityWebRequest(ServerConfig.API_URL + url, "POST");
            var bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                var responseText = request.downloadHandler.text;
                try
                {
                    var response = JObject.Parse(responseText);

                    if (response["message"] != null)
                    {
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
            else
            {
                var response = JObject.Parse(request.downloadHandler.text);
                if (response["token"] != null)
                {
                    DataStorageManager.Instance.RequestToken = response["token"].ToString();
                }
            }
        }

        private IEnumerator Get(string url)
        {
            var request = new UnityWebRequest(ServerConfig.API_URL + url, "GET")
            {
                downloadHandler = new DownloadHandlerBuffer(),
            };

            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                var responseText = request.downloadHandler.text;
                try
                {
                    var response = JObject.Parse(responseText);

                    if (response["message"] != null)
                    {
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
            else
            {
                var response = JObject.Parse(request.downloadHandler.text);
                if (response["token"] != null)
                {
                    DataStorageManager.Instance.RequestToken = response["token"].ToString();
                }
            }
        }

        private IEnumerator Put(string url, string bodyJsonString)
        {
            var request = new UnityWebRequest(ServerConfig.API_URL + url, "POST");
            var bodyRaw = Encoding.UTF8.GetBytes(bodyJsonString);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                var responseText = request.downloadHandler.text;
                try
                {
                    var response = JObject.Parse(responseText);

                    if (response["message"] != null)
                    {
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
            else
            {
                var response = JObject.Parse(request.downloadHandler.text);
                if (response["token"] != null)
                {
                    DataStorageManager.Instance.RequestToken = response["token"].ToString();
                }
            }
        }

        private IEnumerator Delete(string url)
        {
            var request = new UnityWebRequest(ServerConfig.API_URL + url, "DELETE")
            {
                downloadHandler = new DownloadHandlerBuffer(),
            };
            
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            if (request.isNetworkError || request.isHttpError)
            {
                var responseText = request.downloadHandler.text;
                try
                {
                    var response = JObject.Parse(responseText);

                    if (response["message"] != null)
                    {
                    }
                }
                catch (Exception e)
                {
                    Debug.Log(e);
                }
            }
            else
            {
                var response = JObject.Parse(request.downloadHandler.text);
                if (response["token"] != null)
                {
                    DataStorageManager.Instance.RequestToken = response["token"].ToString();
                }
            }
        }
    }
}
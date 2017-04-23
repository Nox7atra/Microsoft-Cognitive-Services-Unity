using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
namespace Nox7atra.Services
{
    public class FaceService
    {
        private const string REQUEST_DETECT_API = ".api.cognitive.microsoft.com/face/v1.0/detect";
        private string _SubscriptionKey;
        private string _DetectRequestURL;

        private Face[] _LastDetectedFaces;
        public FaceService(string subscriptionKey, 
            ServerLocation location = ServerLocation.WestUS)
        {
            _SubscriptionKey = subscriptionKey;
            _DetectRequestURL =
                string.Concat(
                    Constants.REQUEST_PROTOCOL_PREFIX,
                    location.GetLocationString(),
                    REQUEST_DETECT_API
                );
        }
        public IEnumerator GetDetectFaceCoroutine(Texture2D texture,
           bool isReturnFaceId = true,
           bool isReturnFaceLandmarks = false,
           string faceAttributes = "age")
        {
            yield return CreateDetectRequestAndSaveResponse(
                Constants.CONTENT_FILE_HEADER,
                texture.EncodeToJPG(),
                isReturnFaceId,
                isReturnFaceLandmarks,
                faceAttributes);
        }
        public IEnumerator GetDetectFaceCoroutine(string json,
           bool isReturnFaceId = true,
           bool isReturnFaceLandmarks = false,
           string faceAttributes = "age")
        {
            yield return CreateDetectRequestAndSaveResponse(
                Constants.CONTENT_JSON_HEADER,
                Encoding.Default.GetBytes(json),
                isReturnFaceId,
                isReturnFaceLandmarks,
                faceAttributes);
        }
        private IEnumerator CreateDetectRequestAndSaveResponse(
           string contentHeader,
           byte[] data, 
           bool isReturnFaceId,
           bool isReturnFaceLandmarks,
           string faceAttributes)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add(Constants.SUB_KEY_HEADER, _SubscriptionKey);
            headers.Add(Constants.CONTENT_TYPE_HEADER, contentHeader);
            WWW request = new WWW(
                string.Concat(
                    _DetectRequestURL,
                    MakeDetectUrlAttributes(
                        isReturnFaceId,
                        isReturnFaceLandmarks,
                        faceAttributes)),
                data,
                headers);
            yield return new WaitUntil(() => request.isDone);
            ParseDetectedFacesFromJson(request.text);
        }
        private string MakeDetectUrlAttributes(
            bool isReturnFaceId,
            bool isReturnFaceLandmarks,
            string faceAttributes)
        {
            return string.Concat(
                "?returnFaceId=",
                isReturnFaceId.ToString(),
                "&returnFaceLandmarks=",
                isReturnFaceLandmarks.ToString(),
                "&returnFaceAttributes=",
                faceAttributes);
        }
        private void ParseDetectedFacesFromJson(string json)
        {
            Debug.Log(json);
            _LastDetectedFaces = JsonConvert.DeserializeObject<Face[]>(json);
        }
    }
    

}
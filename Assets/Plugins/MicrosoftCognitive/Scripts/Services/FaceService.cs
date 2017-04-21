using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Nox7atra.Services
{
    public class FaceService
    {
        private const string REQUEST_DETECT_API = ".api.cognitive.microsoft.com/face/v1.0/detect";
        private string _SubscriptionKey;
        private string _DetectRequestURL;
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

        private IEnumerator CreateDetectRequestAndSaveResponse(
           string contentHeader,
           byte[] data)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add(Constants.SUB_KEY_HEADER, _SubscriptionKey);
            headers.Add(Constants.CONTENT_TYPE_HEADER, contentHeader);
            WWW request = new WWW(REQUEST_DETECT_API, data, headers);
            yield return new WaitUntil(() => request.isDone);
            //ParseEmotionsFromJson(request.text);
        }
    }
    public class Face
    {
        public string FaceID;
        public FaceRectangle FaceRectangle;
        public FaceLandmarks Landmarks;
        public FaceAttributes Attributes;
    }
    public struct FaceLandmarks
    {

    }
    public class FaceAttributes
    {

    }
}
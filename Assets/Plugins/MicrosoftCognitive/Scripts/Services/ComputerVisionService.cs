using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nox7atra.Services
{
    public class ComputerVisionService
    {
        private const string REQUEST_ANALYZE_API = ".api.cognitive.microsoft.com/vision/v1.0/analyze";
        private string _SubscriptionKey;
        private string _AnalyzeUrl;

        public ComputerVisionService(
            string subscriptionKey,
            ServerLocation location = ServerLocation.WestUS)
        {
            _SubscriptionKey = subscriptionKey;
            _AnalyzeUrl = string.Concat(
                Constants.REQUEST_PROTOCOL_PREFIX,
                location.GetLocationString(),
                REQUEST_ANALYZE_API);
        }

        private IEnumerator CreateAnalyzeRequestAndSaveResponse(
           string contentHeader,
           byte[] data,
           string visualFeatures,
           string details)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add(Constants.SUB_KEY_HEADER, _SubscriptionKey);
            headers.Add(Constants.CONTENT_TYPE_HEADER, contentHeader);
            WWW request = new WWW(
                string.Concat(
                    _AnalyzeUrl,
                    MakeAnalyzeUrlAttributes(
                        visualFeatures, 
                        details)),
                data,
                headers);
            yield return new WaitUntil(() => request.isDone);
            //ParseDetectedFacesFromJson(request.text); 
            //TODO: create data saving
        }

        private string MakeAnalyzeUrlAttributes(
            string visualFeatures,
            string details)
        {
            return string.Concat(
                "?visualFeatures=",
                visualFeatures,
                "&details=",
                details);
        }
    }
}
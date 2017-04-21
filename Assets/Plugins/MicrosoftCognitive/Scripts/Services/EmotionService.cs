using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Newtonsoft.Json;
using System.Text;

namespace Nox7atra.Services
{
    public sealed class EmotionService
    {
        private const string REQUEST_RECOGNIZE_API = ".api.cognitive.microsoft.com/emotion/v1.0/recognize";

        private string _SubscriptionKey;
        private string _RecognizeRequestUrl;
        private Emotion[] _LastEmotions;
        public List<Emotion> LastEmotions
        {
            get
            {
                return new List<Emotion>(_LastEmotions);
            }
        }
        public EmotionService(string subsKey)
        {
            _SubscriptionKey = subsKey;
            _RecognizeRequestUrl = string.Concat(
                Constants.REQUEST_PROTOCOL_PREFIX, 
                ServerLocation.WestUS.GetLocationString(),
                REQUEST_RECOGNIZE_API);
        }
        public IEnumerator GetEmoInfoCoroutine(Texture2D texture)
        {
            yield return CreateRecognizeRequestAndSaveResponse(
                Constants.CONTENT_FILE_HEADER,
                texture.EncodeToJPG());
        }
        public IEnumerator GetEmoInfoCoroutine(string json)
        {
            yield return CreateRecognizeRequestAndSaveResponse(
                Constants.CONTENT_JSON_HEADER, 
                Encoding.Default.GetBytes(json));
        }
        private IEnumerator CreateRecognizeRequestAndSaveResponse(
            string contentHeader, 
            byte[] data)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add(Constants.SUB_KEY_HEADER, _SubscriptionKey);
            headers.Add(Constants.CONTENT_TYPE_HEADER, contentHeader);
            WWW request = new WWW(_RecognizeRequestUrl, data, headers);
            yield return new WaitUntil(() => request.isDone);
            ParseEmotionsFromJson(request.text);
        }
        private void ParseEmotionsFromJson(string json)
        {
            Debug.Log(json);
            _LastEmotions = JsonConvert.DeserializeObject<Emotion[]>(json);
        }
    }
    [Serializable]
    public sealed class Emotion
    {
        public FaceRectangle FaceRectangle;
        public Dictionary<EmotionType, float> Scores;
    }

}
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Newtonsoft.Json;
namespace Nox7atra.Services
{
    public sealed class EmotionService
    {
        private const string REQUEST_URL = "https://westus.api.cognitive.microsoft.com/emotion/v1.0/recognize";
        private string _SubscriptionKey;

        public Emotion[] Emotions;
        public EmotionService(string subsKey)
        {
            _SubscriptionKey = subsKey;
        }

        public IEnumerator GetEmoInfo(Texture2D texture)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("Ocp-Apim-Subscription-Key", _SubscriptionKey);
            headers.Add("Content-Type", "application/octet-stream");
            var jpgData = texture.EncodeToJPG();
            WWW request = new WWW(REQUEST_URL, jpgData, headers);
            yield return new WaitUntil(() => request.isDone);
            ParseEmotionsFromJson(request.text);
        }

        private void ParseEmotionsFromJson(string json)
        {
            Emotions = JsonConvert.DeserializeObject<Emotion[]>(json);
        }
    }
    [Serializable]
    public sealed class Emotion
    {
        public FaceRectangle FaceRectangle;
        public Dictionary<EmotionType, float> Scores;

    }
    [Serializable]
    public struct FaceRectangle
    {
        public FaceRectangle(
            int left,
            int top,
            int width,
            int height)
        {
            Left = left;
            Top = top;
            Width = width;
            Height = height;
        }
        public readonly int Left;
        public readonly int Top;
        public readonly int Width;
        public readonly int Height;
    }
    public enum EmotionType
    {
        Anger,
        Contempt,
        Disgust,
        Fear,
        Happiness,
        Neutral,
        Sadness,
        Surprise
    }
}
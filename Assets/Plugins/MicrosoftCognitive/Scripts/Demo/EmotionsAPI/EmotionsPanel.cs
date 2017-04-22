using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nox7atra.Services;
using System.Linq;
namespace Nox7atra.Demo.EmotionsAPI
{
    public class EmotionsPanel : MonoBehaviour
    {
        private const float DELAY = 2f;
        [SerializeField]
        private WebCamPanel _WebCam;
        [SerializeField]
        private Text _MaxEmotionValue;
        void Awake()
        {
            StartCoroutine(CheckEmotions());
        }

        private IEnumerator CheckEmotions()
        {
            EmotionService serv = new EmotionService(SubscriptionKeys.Instance.EmotionsApiKey);
            while (true)
            {
                yield return new WaitForEndOfFrame();
                yield return serv.GetEmoInfoCoroutine(_WebCam.Screenshot);
                var emotions = serv.LastEmotions;
                if (emotions != null)
                {
                    _MaxEmotionValue.text = GetMaxEmotionOnScreenshot(emotions);
                }
                yield return new WaitForSeconds(DELAY);
            }
        }
        
        private string GetMaxEmotionOnScreenshot(List<Emotion> emotions)
        {
            Dictionary<EmotionType, float> weights = new Dictionary<EmotionType, float>();
            for(int i = 0; i < emotions.Count; i++)
            {
                var scores = emotions[i].Scores;
                foreach(var key in scores.Keys)
                {
                    if (weights.ContainsKey(key))
                    {
                        weights[key] += scores[key];
                    }
                    else
                    {
                        weights.Add(key, scores[key]);
                    }
                }
            }
            float maxWeight = weights.Values.Max();
            return weights.First(x => x.Value == maxWeight).Key.GetEmotionString();
        }
    }
}
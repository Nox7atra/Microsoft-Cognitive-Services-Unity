using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Nox7atra
{
    public class TestUI : MonoBehaviour
    {
        [SerializeField]
        private string _EmoSubscriptionKey;
        [SerializeField]
        private RawImage _WebCamImage;
        void Awake()
        {
            var texture = new WebCamTexture();
            texture.Play();
            _WebCamImage.texture = texture;
            StartCoroutine(TestServices());
        }

        public IEnumerator TestServices()
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("Start emotions service");
            Services.EmotionService emoServ = new Services.EmotionService(_EmoSubscriptionKey);
            yield return emoServ.GetEmoInfo(_WebCamImage.texture.GetTexture2D());
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Nox7atra.Demo
{
    public class WebCamPanel : MonoBehaviour
    {
        [SerializeField]
        private RawImage _WebCamImage;

        public Texture2D Screenshot
        {
            get
            {
                return _WebCamImage.texture.GetTexture2D();
            }
        }
        void Awake()
        {
            var texture = new WebCamTexture();
            texture.Play();
            _WebCamImage.texture = texture;
        }
        public void OnSaveScreenshotButton()
        {
            StartCoroutine(SaveScreenshot());
        }
        private IEnumerator SaveScreenshot()
        {
            yield return new WaitForEndOfFrame();
            var screenshot = Screenshot;
            File.WriteAllBytes("Assets/test.png", screenshot.EncodeToPNG());
        }
    }
}

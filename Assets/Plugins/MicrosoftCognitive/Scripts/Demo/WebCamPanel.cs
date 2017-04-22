using System.Collections;
using System.Collections.Generic;
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

    }
}

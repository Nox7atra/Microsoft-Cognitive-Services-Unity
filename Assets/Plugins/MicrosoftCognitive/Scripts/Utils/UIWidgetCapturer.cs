using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Nox7atra.UI.Utils
{
    public class UIWidgetCapturer : MonoBehaviour
    {
        private static readonly Vector2 TARGET_RESOLUTION = new Vector2(1080, 1920);
        private RectTransform _Widget;
        private int width;
        private int height;

        private Texture2D _LastCapture;

        public Texture2D LastCapture
        {
            get
            {
                return _LastCapture;
            }
        }
        private void Awake()
        {
            _Widget = GetComponent<RectTransform>();
            width = (int)(System.Convert.ToInt32(_Widget.rect.width)
                * Screen.width / TARGET_RESOLUTION.x);
            height = (int)(System.Convert.ToInt32(_Widget.rect.height)
                * Screen.height / TARGET_RESOLUTION.y);
        }
        public IEnumerator Capture()
        {
            var pos = Camera.main.WorldToScreenPoint(_Widget.transform.position)
                - new Vector3(width / 2, 0);
            var texture = new Texture2D(width, height, TextureFormat.ARGB32, false);
            yield return new WaitForEndOfFrame();
            texture.ReadPixels(new Rect(pos.x, pos.y, width, height), 0, 0);
            texture.Apply();
            _LastCapture = texture;
        }
    }
}
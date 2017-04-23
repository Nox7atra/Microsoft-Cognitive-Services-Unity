using Nox7atra.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Nox7atra.Demo.FaceAPI
{
    public class FacePanel : MonoBehaviour
    {
        private const float DELAY = 2f;
        [SerializeField]
        private WebCamPanel _WebCam;


        private void Awake()
        {
            StartCoroutine(CheckFaces());
        }

        private IEnumerator CheckFaces()
        {
            FaceService serv = new FaceService(SubscriptionKeys.Instance.FaceApiKey);
            while (true)
            {
                yield return new WaitForEndOfFrame();
                var screenshot = _WebCam.Screenshot;
                yield return serv.GetDetectFaceCoroutine(screenshot, false, true);

                yield return new WaitForSeconds(DELAY);
            }
        }
    }
}
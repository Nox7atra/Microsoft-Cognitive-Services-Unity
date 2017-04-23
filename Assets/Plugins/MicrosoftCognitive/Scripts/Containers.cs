using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nox7atra
{
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
    [Serializable]
    public class Face
    {
        public string FaceID;
        public FaceRectangle FaceRectangle;
        public FaceLandmarks FaceLandmarks;
        public FaceAttributes Attributes;
    }
    [Serializable]
    public struct FaceLandmarks
    {
        public Vector2 PupilLeft;
        public Vector2 PupilRight;
        public Vector2 NoseTip;
        public Vector2 MouthLeft;
        public Vector2 MouthRight;
        public Vector2 EyebrowLeftOuter;
        public Vector2 EyebrowLeftInner;
        public Vector2 EyeLeftOuter;
        public Vector2 EyeLeftTop;
        public Vector2 EyeLeftBottom;
        public Vector2 EyeLeftInner;
        public Vector2 EyebrowRightOuter;
        public Vector2 EyebrowRightInner;
        public Vector2 EyeRightOuter;
        public Vector2 EyeRightTop;
        public Vector2 EyeRightBottom;
        public Vector2 EyeRightInner;
        public Vector2 NoseRootLeft;
        public Vector2 NoseRootRight;
        public Vector2 NoseLeftAlarTop;
        public Vector2 NoseRightAlarTop;
        public Vector2 NoseLeftAlarOutTip;
        public Vector2 NoseRightAlarOutTip;
        public Vector2 UpperLipTop;
        public Vector2 UpperLipBottom;
        public Vector2 UnderLipTop;
        public Vector2 UnderLipBottom;
    }
    [Serializable]
    public class FaceAttributes
    {
        public float Age;
        public string Gender;
        public float Smile;
        public Dictionary<FacialHairType, float> FacialHair;
        public string Glasses;
        public Dictionary<HeadPose, float> HeadPose;
        public Dictionary<EmotionType, float> Emotions;
    }
    [Serializable]
    public sealed class Emotion
    {
        public FaceRectangle FaceRectangle;
        public Dictionary<EmotionType, float> Scores;
    }
}
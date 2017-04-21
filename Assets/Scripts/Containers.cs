using System;
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
}
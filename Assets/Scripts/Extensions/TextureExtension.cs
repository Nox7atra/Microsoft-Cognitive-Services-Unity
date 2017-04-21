using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureExtension
{
    public static Texture2D GetTexture2D(this Texture texture)
    {
        Texture2D texture2D = new Texture2D(texture.width, texture.height);
        texture2D.ReadPixels(new Rect(0, 0, texture.width, texture.height), 0, 0);
        texture2D.Apply();
        return texture2D;
    }
}

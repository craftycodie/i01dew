using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace i01dew.Utility
{
    public static class Texture2D
    {
        public static UnityEngine.Texture2D MakeTexture(int width, int height, UnityEngine.Color col)
        {
            UnityEngine.Color[] pix = new UnityEngine.Color[width * height];
            for (int i = 0; i < pix.Length; ++i)
            {
                pix[i] = col;
            }
            UnityEngine.Texture2D result = new UnityEngine.Texture2D(width, height);
            result.SetPixels(pix);
            result.Apply();
            return result;
        }
    }
}

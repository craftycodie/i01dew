using System;

namespace i01dew.Utility
{
    public static class Color
    {
        public static string ToHexString(UnityEngine.Color color)
        {
            int r = (int)(color.r * 256);
            int g = (int)(color.g * 256);
            int b = (int)(color.b * 256);
            int a = (int)(color.a * 255);
            return string.Format("{0:X2}{1:X2}{2:X2}{3:X2}", r, g, b, a);
        }

        public static UnityEngine.Color FromHexString(string hexString)
        {
            if (hexString.Length != 8)
                return UnityEngine.Color.white;

            string rString = hexString.Substring(0, 2);
            string gString = hexString.Substring(2, 4);
            string bString = hexString.Substring(4, 6);
            string aString = hexString.Substring(6, 8);

            int r = Convert.ToByte(rString);
            int g = Convert.ToByte(gString);
            int b = Convert.ToByte(bString);
            int a = Convert.ToByte(aString);

            return new UnityEngine.Color(r / 255, g / 255, b / 255, a / 255);
        }
    }
}

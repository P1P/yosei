using System;

namespace UnityEngine
{
    public static class ColorHSV
    {
        public static Color FromHsv(int h, int s, int v)
        {
            while (h >= 360) h -= 360;
            while (h < 0) h += 360;
            if (s > 255) s = 255;
            if (s < 0) s = 0;
            if (v > 255) v = 255;
            if (v < 0) v = 0;

            return FromHsv((float)h / 255.0f, (float)s / 255.0f, (float)v / 255.0f);
        }

        public static Color FromHsv(float h, float s, float v)
        {
            h = Mathf.Clamp01(h);
            s = Mathf.Clamp01(s);
            v = Mathf.Clamp01(v);

            h *= 255.0f;

            Color resColor = Color.clear;

            if (s == 0.0)
            {
                int rgb = Convert.ToInt16((float)(v * 255));
                resColor = new Color(rgb, rgb, rgb);
            }
            else
            {
                int Hi = (int)(Mathf.Floor(h / 60.0f) % 6.0f);
                float f = (h / 60.0f) - Hi;

                float p = v * (1 - s);
                float q = v * (1 - f * s);
                float t = v * (1 - (1 - f) * s);

                float r = 0.0f;
                float g = 0.0f;
                float b = 0.0f;

                switch (Hi)
                {
                    case 0: r = v; g = t; b = p; break;
                    case 1: r = q; g = v; b = p; break;
                    case 2: r = p; g = v; b = t; break;
                    case 3: r = p; g = q; b = v; break;
                    case 4: r = t; g = p; b = v; break;
                    case 5: r = v; g = p; b = q; break;
                    default: break;
                }

                resColor = new Color(r, g, b);
            }

            return resColor;
        }
    }

    public static class ColorExtension
    {
        public static int h(this Color c)
        {
            float min = Mathf.Min(new float[] { c.r, c.g, c.b });
            float max = Mathf.Max(new float[] { c.r, c.g, c.b });

            if (max == 0) return 0;

            float h = 0;

            if (max == c.r) h = 60 * (c.g - c.b) / (max - min) + 0;
            else if (max == c.g) h = 60 * (c.b - c.r) / (max - min) + 120;
            else if (max == c.b) h = 60 * (c.r - c.g) / (max - min) + 240;

            if (h < 0) h += 360;

            return (int)Mathf.Round(h);
        }

        public static int s(this Color c)
        {
            float min = Mathf.Min(new float[] { c.r, c.g, c.b });
            float max = Mathf.Max(new float[] { c.r, c.g, c.b });

            if (max == 0) return 0;
            return (int)(255 * (max - min) / max);
        }

        public static int v(this Color c)
        {
            return (int)(255.0f * Mathf.Max(new float[] { c.r, c.g, c.b }));
        }

        public static Color Offset(this Color c, float offsetH, float offsetS, float offsetV)
        {
            int newH = (int)(c.h() + offsetH * 255.0f);
            int newS = (int)(c.s() + offsetS * 255.0f);
            int newV = (int)(c.v() + offsetV * 255.0f);

            return ColorHSV.FromHsv(newH, newS, newV);
        }
    }
}
using System;

namespace UnityEngine
{
    public static class ColorHSV
    {
        public static Color FromHsv(int p_hue, int p_saturation, int p_value)
        {
            while (p_hue >= 360) p_hue -= 360;
            while (p_hue < 0) p_hue += 360;
            if (p_saturation > 255) p_saturation = 255;
            if (p_saturation < 0) p_saturation = 0;
            if (p_value > 255) p_value = 255;
            if (p_value < 0) p_value = 0;

            return FromHsv((float) p_hue / 255.0f, (float) p_saturation / 255.0f, (float) p_value / 255.0f);
        }

        public static Color FromHsv(float p_hue, float p_saturation, float p_value)
        {
            p_hue = Mathf.Clamp01(p_hue);
            p_saturation = Mathf.Clamp01(p_saturation);
            p_value = Mathf.Clamp01(p_value);

            p_hue *= 255.0f;

            Color resColor = Color.clear;

            if (p_saturation == 0.0)
            {
                int rgb = Convert.ToInt16((float) (p_value * 255));
                resColor = new Color(rgb, rgb, rgb);
            }
            else
            {
                int Hi = (int) (Mathf.Floor(p_hue / 60.0f) % 6.0f);
                float f = (p_hue / 60.0f) - Hi;

                float p = p_value * (1 - p_saturation);
                float q = p_value * (1 - f * p_saturation);
                float t = p_value * (1 - (1 - f) * p_saturation);

                float r = 0.0f;
                float g = 0.0f;
                float b = 0.0f;

                switch (Hi)
                {
                    case 0: r = p_value; g = t; b = p; break;
                    case 1: r = q; g = p_value; b = p; break;
                    case 2: r = p; g = p_value; b = t; break;
                    case 3: r = p; g = q; b = p_value; break;
                    case 4: r = t; g = p; b = p_value; break;
                    case 5: r = p_value; g = p; b = q; break;
                    default: break;
                }

                resColor = new Color(r, g, b);
            }

            return resColor;
        }
    }

    public static class ColorExtension
    {
        public static int Hue(this Color p_color)
        {
            float min = Mathf.Min(new float[] { p_color.r, p_color.g, p_color.b });
            float max = Mathf.Max(new float[] { p_color.r, p_color.g, p_color.b });

            if (max == 0) return 0;

            float h = 0;

            if (max == p_color.r) h = 60 * (p_color.g - p_color.b) / (max - min) + 0;
            else if (max == p_color.g) h = 60 * (p_color.b - p_color.r) / (max - min) + 120;
            else if (max == p_color.b) h = 60 * (p_color.r - p_color.g) / (max - min) + 240;

            if (h < 0) h += 360;

            return (int) Mathf.Round(h);
        }

        public static int Saturation(this Color p_color)
        {
            float min = Mathf.Min(new float[] { p_color.r, p_color.g, p_color.b });
            float max = Mathf.Max(new float[] { p_color.r, p_color.g, p_color.b });

            if (max == 0) return 0;
            return (int) (255 * (max - min) / max);
        }

        public static int Value(this Color p_color)
        {
            return (int) (255.0f * Mathf.Max(new float[] { p_color.r, p_color.g, p_color.b }));
        }

        public static Color Offset(this Color p_color, float p_offset_h, float p_offset_s, float p_offset_v)
        {
            int new_h = (int) (p_color.Hue() + p_offset_h * 255.0f);
            int new_s = (int) (p_color.Saturation() + p_offset_s * 255.0f);
            int new_v = (int) (p_color.Value() + p_offset_v * 255.0f);

            return ColorHSV.FromHsv(new_h, new_s, new_v);
        }
    }
}
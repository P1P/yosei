using UnityEngine;
using System.Collections;

public class Textures : MonoBehaviour
{
    public Texture2D GetRandomGreyscaleTexture(int p_width, int p_height, float p_base_value = 0.5f, float p_value_variance = 0.5f)
    {
        Texture2D texture = new Texture2D(p_width, p_height, TextureFormat.ARGB32, false);

        for (int w = 0; w < p_width; ++w)
        {
            for (int h = 0; h < p_height; ++h)
            {
                texture.SetPixel(w, h, Game.Inst.m_colors.GetRandomGrey(p_base_value, p_value_variance));
            }
        }

        texture.filterMode = FilterMode.Point;
        texture.Apply();

        return texture;
    }
}

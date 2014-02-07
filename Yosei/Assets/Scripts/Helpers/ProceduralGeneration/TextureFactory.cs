using UnityEngine;
using System.Collections;

public class TextureFactory : MonoBehaviour
{
    #region SINGLETON
    private static TextureFactory _instance = null;
    public static TextureFactory Instance { get { return _instance; } }

    void Awake()
    {
        _instance = this;
    }
    #endregion

    /// <summary>
    /// Returns a random texture with gray tone textures
    /// </summary>
    /// <param name="p_width">The width of the texture in px</param>
    /// <param name="p_height">The height of the texture in px</param>
    /// <param name="p_base_value">The base value of the colors</param>
    /// <param name="p_value_variance">The random value offsets</param>
    /// <returns>The gray tone texture</returns>
    public Texture2D GetRandomGrayscaleTexture(int p_width, int p_height, float p_base_value = 0.5f, float p_value_variance = 0.5f)
    {
        Texture2D texture = new Texture2D(p_width, p_height, TextureFormat.ARGB32, false);

        for (int w = 0; w < p_width; ++w)
        {
            for (int h = 0; h < p_height; ++h)
            {
                texture.SetPixel(w, h, ColorFactory.Instance.GetRandomGrey(p_base_value, p_value_variance));
            }
        }

        texture.filterMode = FilterMode.Point;
        texture.Apply();

        return texture;
    }
}

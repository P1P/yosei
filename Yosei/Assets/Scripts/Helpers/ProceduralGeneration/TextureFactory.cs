using UnityEngine;
using System.Collections;

public class TextureFactory : MonoBehaviour
{
    #region SINGLETON
    private static TextureFactory _instance = null;
    public static TextureFactory Instance { get { return _instance; } }

    private Texture2D _default_texture;

    public void Awake()
    {
        _instance = this;
        _default_texture = Resources.Load("Tiles/Texture/Texture", typeof(Texture2D)) as Texture2D;;
    }
    #endregion

    public void Update()
    {
        //GetRandomGrayscaleTexture(4, 4, 0.5f, 0.1f);
    }

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

        return texture;
    }

    public Texture2D GetDummyTexture()
    {
        return _default_texture;
    }
}

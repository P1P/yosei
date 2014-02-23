using UnityEngine;
using System.Collections;

public class MemoryLeaker : MonoBehaviour
{
    public void Update()
    {
        for (int i = 0; i < 500; ++i)
        {
            Texture2D texture = new Texture2D(1, 1);
            /*
            Texture2D texture = new Texture2D(4, 4, TextureFormat.ARGB32, false, false);
            
            for (int w = 0; w < 4; ++w)
            {
                for (int h = 0; h < 4; ++h)
                {
                    texture.SetPixel(w, h, Color.black);
                }
            }
            */
            Destroy(texture);
            texture = null;
        }

        if (Time.timeSinceLevelLoad >= 5f)
        {
            System.GC.Collect();
            Resources.UnloadUnusedAssets();
            Destroy(this);
            Destroy(this.gameObject);
        }
    }
}

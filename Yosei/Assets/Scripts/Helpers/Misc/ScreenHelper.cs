using UnityEngine;
using System.Collections;

public class ScreenHelper
{
    public static float W_to_px(float p_perc)
    {
        return Screen.width * p_perc;
    }

    public static float H_to_px(float p_perc)
    {
        return Screen.height * p_perc;
    }
}

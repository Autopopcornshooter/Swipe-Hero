using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static Color RandomColor()
    {
        int rand_H = Random.Range(0, 310);
        float H = rand_H / 310.0f;
        float S = 0.3f;
        float V = 1.0f;
        
        //float A =0.5f;
        Color temp = Color.HSVToRGB(H, S, V);
        //temp.a = A;
        return temp;
    }
    public static Color SetHSVTORGB(float value)
    {
        float H = value;
        float S = 0.3f;
        float V = 1.0f;
        Color temp = Color.HSVToRGB(H, S, V);
        return temp;
    }
}

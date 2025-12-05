using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpFunctions : MonoBehaviour
{
    public bool EaseIn;
    public bool EaseOut;
    public bool Flip;
    public bool EaseInOut;
    public bool RoughSpike;
    public bool GradualSpike;

    public static float EaseInF(float t)
    {
        return t * t;
    }
    static float FlipF(float x)
    {
        return 1 - x;
    }
    public static float EaseInOutF(float t)
    {
        return Mathf.Lerp(EaseInF(t), EaseOutF(t), t);
    }
    public static float EaseOutF(float t)
    {
        return FlipF(FlipF(t)*FlipF(t));
    }
    public static float Spike(float t)
    {
        if (t <= 0.5f)
            return EaseInF(t / 0.5f);
    
        return EaseInF(FlipF(t)/0.5f);
    }
    public static float Spike2(float t)
    {
        if (t <= 2f)
            return EaseInF(t / 2f);
    
        return EaseInF(FlipF(t)/2f);
    }
}

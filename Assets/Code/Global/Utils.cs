using UnityEngine;

public class Utils
{

    public static bool InRange(float num, float target, float range)
    {
        return Mathf.Abs(num - target) < range;
    }

}

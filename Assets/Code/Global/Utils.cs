using UnityEngine;

public class Utils
{

    public static int ResetAngle(int angle)
    {
        if (angle == 270)
            return -90;
        else if (angle == -180)
            return 180;
        else if (angle >= 360 || angle <= -360)
            return 0;
        else
            return angle;
    }

    public static bool InRange(float num, float target, float range)
    {
        return Mathf.Abs(num - target) < range;
    }

}

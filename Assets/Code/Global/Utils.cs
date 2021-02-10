using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{

    /// <summary>
    /// Forcefully exit the game with the given error message
    /// </summary>
    public static void Crash(object error)
    {
        Debug.LogError(error);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    /// <summary>
    /// Get a random element from the given list
    /// </summary>
    public static T GetRandom<T>(T[] list)
    {
        return list[Random.Range(0, list.Length)];
    }

    /// <summary>
    /// Keep the given angle within the game's expected range.
    /// </summary>
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

}

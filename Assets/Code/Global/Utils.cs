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
    /// Get the section in which the given object exists
    /// </summary>
    public static Section GetParentSection(GameObject gameObject)
    {
        Section parent = null;
        GameObject childObject = gameObject;
        while (parent == null)
        {
            if (!childObject.transform.parent)
                Utils.Crash(string.Format("could not find parent section for object {0}", gameObject.name));

            parent = childObject.transform.parent.gameObject.GetComponent<Section>();
            childObject = childObject.transform.parent.gameObject;
        }
        return parent;
    }

    public static void DebugInGame(string msg)
    {
        PlayerHUDDebug hud = PlayerManager.Instance.Controller.gameObject.GetComponent<PlayerHUDDebug>();
        hud.PushText(msg);
    }

    public static void DebugInGame(string id, string msg)
    {
        PlayerHUDDebug hud = PlayerManager.Instance.Controller.gameObject.GetComponent<PlayerHUDDebug>();
        hud.UpdateText(id, msg);
    }

    /// <summary>
    /// Get a random element from the given list
    /// </summary>
    public static T GetRandom<T>(T[] list)
    {
        return list[Random.Range(0, list.Length)];
    }

    /// <summary>
    /// Keep the given angle within the game's expected range: {-90, 0, 90, 180}
    ///                                                        {W    N  E   S  }
    /// </summary>
    public static int ResetAngle(int angle)
    {
        if (angle == -270)
            return 90;
        else if (angle == 270)
            return -90;
        else if (angle == -180)
            return 180;
        else if (angle >= 360 || angle <= -360)
            return 0;
        else
            return angle;
    }

    /// <summary>
    /// Convert the angle into the game's cardinal system
    /// </summary>
    public static CardinalDirection GetAngleDirection(int angle)
    {
        int fixedAngle = Utils.ResetAngle(angle);
        switch (fixedAngle)
        {
            case 0:
                return CardinalDirection.NORTH;
            case 180:
                return CardinalDirection.SOUTH;
            case 90:
                return CardinalDirection.EAST;
            case -90:
                return CardinalDirection.WEST;
            default:
                Utils.Crash(string.Format("invalid angle {0} does not correspond to a cardinal direction", fixedAngle));
                break;
        }
        return CardinalDirection.NORTH;
    }

}

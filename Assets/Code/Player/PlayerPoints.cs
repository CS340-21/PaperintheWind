using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{

    private Vector3 prevLoc;

    private PlayerController controller { get { return PlayerManager.Instance.Controller; } }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentLoc = controller.transform.position;

        if (prevLoc != null)
        {
            float dist = Vector3.Distance(prevLoc, currentLoc);
            controller.PointsEarned += dist;
            // Utils.DebugInGame("points", "points: " + (int)controller.PointsEarned);
        }

        prevLoc = currentLoc;
    }
}

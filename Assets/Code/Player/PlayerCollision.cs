using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private PlayerController controller { get { return PlayerManager.Instance.Controller; } }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "right_turn":
                controller.SetTurnPossibility("right");
                return;

            case "left_turn":
                controller.SetTurnPossibility("left");
                return;

            case "section_begin":
                controller.CurrentSection.ReplaceAfterDelay();
                controller.CurrentSection = other.transform.parent.parent.GetComponent<Section>();
                return;

            case "section_end":
                return;

            default:
                Debug.Log("ran into " + other.name);
                MenuController.Instance.CollisionResultText.text = string.Format("Before running into a {0}, you earned", other.name.ToLower());
                // Utils.DebugInGame("ran into " + other.name);
                controller.Kill();
                return;
        }
    }

    void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "turn":
                controller.SetTurnPossibility(null);
                return;
        }
    }

}

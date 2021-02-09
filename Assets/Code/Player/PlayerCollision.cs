using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        PlayerController controller = PlayerManager.Instance.Controller;

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
                PlayerManager.Instance.Controller.Kill();
                return;
        }
    }

    void OnTriggerExit(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "turn":
                PlayerManager.Instance.Controller.SetTurnPossibility(null);
                return;
        }
    }

}

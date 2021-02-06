using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collide with obstacle " + other.gameObject.name);
        PlayerManager.Instance.Controller.Kill();
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController controller = PlayerManager.Instance.Controller;

        switch (other.gameObject.tag)
        {
            case "turn":
                string dir = other.gameObject.name.Split(' ')[0].ToLower();
                controller.SetTurnPossibility(dir);
                return;

            case "section_begin":
                GameObject oldSection = controller.CurrentSection.gameObject;
                controller.CurrentSection = other.transform.parent.parent.GetComponent<Section>();
                Destroy(oldSection);
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

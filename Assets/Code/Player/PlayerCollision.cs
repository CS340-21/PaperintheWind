using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "turn")
        {
            string dir = other.gameObject.name.Split(' ')[0].ToLower();
            PlayerManager.Instance.Controller.SetTurnPossibility(dir);
            return;
        }

        Debug.Log("collide with obstacle " + other.gameObject.name);
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "turn")
        {
            PlayerManager.Instance.Controller.SetTurnPossibility(null);
        }
    }

}

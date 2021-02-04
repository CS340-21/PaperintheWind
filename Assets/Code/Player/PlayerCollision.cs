using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    void OnCollisionEnter(Collision other)
    {
        Debug.Log("collided with obstacle " + other.gameObject.name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "turn")
        {
            string dir = other.gameObject.name.Split(' ')[1].ToLower();
            Debug.Log("let's turn " + dir);
            PlayerManager.Instance.Controller.SetTurnPossibility(dir);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "turn")
        {
            Debug.Log("disable turn possibilites");
            PlayerManager.Instance.Controller.SetTurnPossibility(null);
        }
    }

}

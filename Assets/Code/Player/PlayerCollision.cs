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
            string dir = other.gameObject.name.Split(' ')[0].ToLower();
            PlayerManager.Instance.Controller.SetTurnPossibility(dir);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "turn")
        {
            PlayerManager.Instance.Controller.SetTurnPossibility(null);
        }
    }

}

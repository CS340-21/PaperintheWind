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

    }

    void OnTriggerExit(Collider other)
    {

    }

}

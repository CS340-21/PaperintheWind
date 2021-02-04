using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerMovement MovementController;

    public void SetTurnPossibility(string direction)
    {
        MovementController.turnPossibility = direction;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

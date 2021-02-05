using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : InputSource
{

    public override void ProcessInput()
    {
        PlayerMovement movement = PlayerManager.Instance.Controller.MovementController;

        if (Input.GetKeyDown("up") || Input.GetKeyDown("w")) movement.MoveDirection("up");
        if (Input.GetKeyDown("left") || Input.GetKeyDown("a")) movement.MoveDirection("left");
        if (Input.GetKeyDown("down") || Input.GetKeyDown("s")) movement.MoveDirection("down");
        if (Input.GetKeyDown("right") || Input.GetKeyDown("d")) movement.MoveDirection("right");
    }

}

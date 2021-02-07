using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : InputSource
{

    private float SwipeThreshold = 20f;

    private float SwipeCooldown = 0.15f;

    private float LastSwipeTime = 0f;

    private Vector2 FingerDown;

    private Vector2 FingerUp;

    private float VerticleMovement { get { return Mathf.Abs(FingerDown.y - FingerUp.y); } }

    private float HorizontalMovement { get { return Mathf.Abs(FingerDown.x - FingerUp.x); } }

    public override void ProcessInputType()
    {
        if (Time.time - LastSwipeTime < SwipeCooldown) return;

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                FingerUp = touch.position;
                FingerDown = touch.position;
            }

            // Detects swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Ended)
            {
                FingerDown = touch.position;
                CheckSwipeDirection();
            }
        }
    }

    private void CheckSwipeDirection()
    {
        PlayerMovement movement = PlayerManager.Instance.Controller.MovementController;

        //Check if Vertical swipe
        if (VerticleMovement > SwipeThreshold && VerticleMovement > HorizontalMovement)
        {
            if (FingerDown.y - FingerUp.y > 0)
            {
                movement.MoveDirection("up");
                LastSwipeTime = Time.time;
            }
            else if (FingerDown.y - FingerUp.y < 0)
            {
                movement.MoveDirection("down");
                LastSwipeTime = Time.time;
            }
            FingerUp = FingerDown;
        }

        //Check if Horizontal swipe
        else if (HorizontalMovement > SwipeThreshold && HorizontalMovement > VerticleMovement)
        {
            if (FingerDown.x - FingerUp.x > 0)
            {
                movement.MoveDirection("right");
                LastSwipeTime = Time.time;
            }
            else if (FingerDown.x - FingerUp.x < 0)
            {
                movement.MoveDirection("left");
                LastSwipeTime = Time.time;
            }
            FingerUp = FingerDown;
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : InputSource
{

    // How many pixels the finger must move to be a swipe
    private float SwipeThreshold = 25f;

    // How long to wait (in seconds) between detecting swipes
    private float SwipeCooldown = 0.15f;

    // Time of the last swipe
    private float LastSwipeTime = 0f;

    // Location on screen where the player has started a touch
    private Vector2 FingerDown;

    // Location on screen where the player has ended a touch
    private Vector2 FingerUp;

    public override void ProcessInputType()
    {
        // Wait for the specified amount of time between swipes
        if (Time.time - LastSwipeTime < SwipeCooldown) return;

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                FingerUp = touch.position;
                FingerDown = touch.position;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                FingerDown = touch.position;
                CheckSwipeDirection();
            }
        }
    }

    private void CheckSwipeDirection()
    {
        float verticalMove = Mathf.Abs(FingerDown.y - FingerUp.y);
        float horizontalMove = Mathf.Abs(FingerDown.x - FingerUp.x);

        // Check if vertical swipe
        if (verticalMove > SwipeThreshold && verticalMove > horizontalMove)
        {
            if (FingerDown.y - FingerUp.y > 0)
            {
                this.PerformSwipe("up");
            }
            else if (FingerDown.y - FingerUp.y < 0)
            {
                this.PerformSwipe("down");
            }
            FingerUp = FingerDown;
        }

        // Check if horizontal swipe
        else if (horizontalMove > SwipeThreshold && horizontalMove > verticalMove)
        {
            if (FingerDown.x - FingerUp.x > 0)
            {
                this.PerformSwipe("right");
            }
            else if (FingerDown.x - FingerUp.x < 0)
            {
                this.PerformSwipe("left");
            }
            FingerUp = FingerDown;
        }
    }

    private void PerformSwipe(string dir)
    {
        controller.MovementController.MoveDirection(dir);
        LastSwipeTime = Time.time;
        FingerUp = FingerDown;
    }

}

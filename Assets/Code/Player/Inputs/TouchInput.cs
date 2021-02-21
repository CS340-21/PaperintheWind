using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : InputSource
{

    // How many pixels the finger must move to be a swipe
    private float SwipeThreshold = 45f;

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
        Vector2 SwipeDelta = FingerUp - FingerDown;

        // Left/right
        if (SwipeDelta.x > SwipeThreshold)
        {
            this.PerformSwipe("left");
        }
        else if (SwipeDelta.x < -SwipeThreshold)
        {
            this.PerformSwipe("right");
        }

        // Up/down
        if (SwipeDelta.y > SwipeThreshold)
        {
            this.PerformSwipe("down");
        }
        else if (SwipeDelta.y < -SwipeThreshold)
        {
            this.PerformSwipe("up");
        }
    }

    private void PerformSwipe(string dir)
    {
        PlayerManager.Instance.Controller.MovementController.MoveDirection(dir);
        LastSwipeTime = Time.time;
        FingerUp = FingerDown;
    }

}

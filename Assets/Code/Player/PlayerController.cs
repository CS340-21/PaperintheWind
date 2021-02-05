using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerMovement MovementController;
    public Animator AnimationController;

    public void SetTurnPossibility(string dir)
    {
        MovementController.turnPossibility = dir;
    }

    public void PlayAnimation(string state)
    {
        AnimationController.Play(state);
    }

    public void TriggerAnimation(string triggerName)
    {
        AnimationController.SetTrigger(triggerName);
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

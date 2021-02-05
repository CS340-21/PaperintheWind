using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerMovement MovementController;
    public Animator PaperAnimation;
    public Animator PlayerAnimation;

    public void Teleport(Vector3 pos)
    {
        transform.position = pos;
        MovementController.Rotation = 0;
    }

    public void SetTurnPossibility(string dir)
    {
        MovementController.TurnPossibility = dir;
    }

    public void PlayAnimation(string state)
    {
        PaperAnimation.Play(state);
    }

    public void PlayParentAnimation(string state)
    {
        PlayerAnimation.Play(state);
    }

    public void TriggerAnimation(string triggerName)
    {
        PaperAnimation.SetTrigger(triggerName);
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

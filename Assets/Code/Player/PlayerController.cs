using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerMovement MovementController;
    public Animator PaperAnimation;
    public Animator PlayerAnimation;
    public Animator CameraAnimation;

    public Section CurrentSection;

    public void Kill()
    {
        CameraAnimation.SetTrigger("Death");
        Time.timeScale = 0f;
        MovementController.Speed = 0f;
        MovementController.FlipSpeed = 0f;
    }

    public void Teleport(Transform newTransform)
    {
        transform.position = newTransform.position;
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

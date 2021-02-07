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

    public float DeathTime = 0f;

    public void Kill()
    {
        Time.timeScale = 0f;
        this.DeathTime = Time.unscaledTime;
        CameraAnimation.SetBool("Dead", true);
        PaperAnimation.SetBool("Dead", true);
    }

    public void Revive()
    {
        CameraAnimation.SetBool("Dead", false);
        PaperAnimation.SetBool("Dead", false);
        StartCoroutine(ResetRotationAnimation());

        MovementController.Position = (1, 1);
        MovementController.Rotation = 0;
        Time.timeScale = 1f;
        this.DeathTime = 0f;
    }

    private IEnumerator ResetRotationAnimation()
    {
        PlayerAnimation.SetTrigger("ResetRotation");
        yield return new WaitForSeconds(1);
        PlayerAnimation.ResetTrigger("ResetRotation");
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
}

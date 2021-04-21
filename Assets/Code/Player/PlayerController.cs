using System.Collections;
using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerMovement MovementController;
    public Animator PaperAnimation;
    public Animator PlayerAnimation;
    public Animator CameraAnimation;

    public Section CurrentSection;

    public float ReviveTime = 0f;

    public float DeathTime = 0f;

    public float PointsEarned = 0;

    private void Awake()
    {
        if (MovementController == null)
            Utils.Crash("player controller missing MovementController");

        if (PaperAnimation == null)
            Utils.Crash("player controller missing PaperAnimation");

        if (PlayerAnimation == null)
            Utils.Crash("player controller missing PlayerAnimation");

        if (CameraAnimation == null)
            Utils.Crash("player controller missing CameraAnimation");
    }

    /// <summary>
    /// Begin the death process
    /// </summary>
    public void Kill()
    {
        Time.timeScale = 0f;
        this.ReviveTime = 0f;
        this.DeathTime = Time.unscaledTime;
        CameraAnimation.SetBool("Dead", true);
        StartCoroutine(ShowMenu());
    }

    /// <summary>
    /// Reset the player's postion and rotation
    /// </summary>
    public void Revive()
    {
        CameraAnimation.SetBool("Dead", false);
        StartCoroutine(ResetAnimations());

        MovementController.TurnPossibility = null;
        MovementController.Position = (1, 1);
        MovementController.Rotation = 0;
        Time.timeScale = 1f;
        this.ReviveTime = Time.unscaledTime;
        this.DeathTime = 0f;
        this.PointsEarned = 0f;
    }

    /// <summary>
    /// Reset the player's animations to the default state
    /// </summary>
    private IEnumerator ResetAnimations()
    {
        PaperAnimation.SetTrigger("Died");
        PlayerAnimation.SetTrigger("ResetRotation");
        yield return new WaitForSeconds(1);
        PaperAnimation.ResetTrigger("Died");
        PlayerAnimation.ResetTrigger("ResetRotation");
    }

    /// <summary>
    /// Reset the player's animations to the default state
    /// </summary>
    private IEnumerator ShowMenu()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        MenuController.Instance.ShowDeathScreen((int)PointsEarned);
    }

    /// <summary>
    /// Move the player to the given 3D vector
    /// </summary>
    public void Teleport(Vector3 loc)
    {
        transform.position = loc;
    }

    /// <summary>
    /// Set the direction in which the player can turn on the next input
    /// </summary>
    public void SetTurnPossibility(string dir)
    {
        MovementController.TurnPossibility = dir;
    }

    /// <summary>
    /// Directly play the given animation on the parent player object
    /// </summary>
    public void PlayParentAnimation(string state)
    {
        PlayerAnimation.Play(state);
    }

    /// <summary>
    /// Set the given animation trigger in the paper's animator
    /// </summary>
    public void TriggerPaperAnimation(string triggerName)
    {
        PaperAnimation.SetTrigger(triggerName);
    }
}

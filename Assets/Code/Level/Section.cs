using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{

    [HideInInspector]
    public int ID = 0;

    /// <summary>
    /// This section's rotation at its beginning.
    /// Used to calculate rotation angles for infinite levels.
    /// </summary>
    public int BeginRotation;

    /// <summary>
    /// This section's rotation at its ending. 
    /// Used to calculate rotation angles for infinite levels.
    /// </summary>
    public int EndRotation;

    public GameObject Spawn { get { return this.transform.Find("Spawn").gameObject; } }

    public GameObject Beginning { get { return this.transform.Find("Signals/Begin").gameObject; } }

    public GameObject Ending { get { return this.transform.Find("Signals/End").gameObject; } }

    public GameObject PreTurn
    {
        get
        {
            if (this.transform.Find("MoveGrid/PreTurn") == null) return null;
            return this.transform.Find("MoveGrid/PreTurn").gameObject;
        }
    }

    public GameObject PostTurn
    {
        get
        {
            if (this.transform.Find("MoveGrid/PostTurn") == null) return null;
            return this.transform.Find("MoveGrid/PostTurn").gameObject;
        }
    }

    /// <summary>
    /// Move the beginning of this section to the ending of the given section
    /// </summary>
    public void AlignWithSection(Section section)
    {
        // If the section rotations don't match, rotate this section to match the given section
        if (section.EndRotation != this.BeginRotation)
        {
            // Rotate this section
            this.transform.Rotate(0f, section.EndRotation, 0f);

            // Does this section have a turn? 90 deg for right turn, -90 for left turn
            int diff = this.EndRotation - this.BeginRotation;

            // Set the section rotation values to match its real-world rotation
            this.BeginRotation = section.EndRotation;
            this.EndRotation = this.BeginRotation + diff;
        }

        // Move this section's beginning to the given section's ending
        Vector3 absoluteMovement = section.Ending.transform.position - this.Beginning.transform.position;
        this.transform.position += absoluteMovement;
    }

    /// <summary>
    /// Destroy this section and replace it with a random one after a predefined delay
    /// </summary>
    private IEnumerator ReplaceAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(2);

        // The player may have died and quit the level in the last 2 seconds
        if (LevelManager.Instance.CurrentLevel != null)
        {
            LevelManager.Instance.CurrentLevel.DestroySection(this);
            LevelManager.Instance.CurrentLevel.GenerateNewSection();
        }
    }

    /// <summary>
    /// Start replacement coroutine
    /// </summary>
    public void ReplaceAfterDelay()
    {
        StartCoroutine(ReplaceAfterDelayCoroutine());
    }

    private void Awake()
    {
        if (this.transform.Find("Spawn") == null)
            Utils.Crash(transform.name + " has no Spawn object");
        else if (this.transform.Find("Signals/Begin") == null)
            Utils.Crash(transform.name + " has no Signals/Begin object");
        else if (this.transform.Find("Signals/End") == null)
            Utils.Crash(transform.name + " has no Signals/End object");
        else if (this.transform.Find("MoveGrid/PreTurn") == null)
            Utils.Crash(transform.name + " has no MoveGrid/PreTurn object");
    }

}

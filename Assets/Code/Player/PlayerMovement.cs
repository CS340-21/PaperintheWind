using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Process the player's inputs and enable movement around the level
/// </summary>
public class PlayerMovement : MonoBehaviour
{

    // The parent player object
    private GameObject Player { get { return gameObject; } }

    public float Speed = 5f;

    public float FlipSpeed = 5f;

    /**
     * Default (1,1):       Right (1, 2):       Upper Right (0, 2):
     * |0|0|0|              |0|0|0|             |0|0|x|
     * |0|x|0|              |0|0|x|             |0|0|0|
     * |0|0|0|              |0|0|0|             |0|0|0|
     */

    /// <summary>
    /// A tuple of the player's position in the predefined grid of movements
    /// </summary>
    private (int, int) Position = (1, 1);

    /// <summary>
    /// The player's directional heading (0 is forward, 90 is right, -90 is left)
    /// </summary>
    [HideInInspector]
    public int Rotation = 0;

    /// <summary>
    /// The direction a player could possibly turn. Determined by a trigger collider.
    /// </summary>
    [HideInInspector]
    public string TurnPossibility = null;

    /// <summary>
    /// Return the 3D position of the cell using the player's position tuple
    /// </summary>
    private Vector3 GetChosen3DVector()
    {
        string cellTag = string.Format("({0},{1})", Position.Item1, Position.Item2);

        GameObject parent = GameObject.FindGameObjectWithTag("preturn");
        if (parent == null) parent = GameObject.FindGameObjectWithTag("postturn");

        return parent.transform.Find(cellTag).transform.position;
    }

    /// <summary>
    /// Translate the player's simplified coordinates into the 3D space
    /// </summary>
    private Vector3 GetChosen2DVector(Vector3 cell)
    {
        float yAngle = Player.transform.rotation.eulerAngles.y;
        int rotation = Utils.ResetAngle(Rotation);

        if (rotation == 0 || rotation == 180)
        {
            return new Vector3(cell.x, cell.y, Player.transform.position.z);
        }
        else if (rotation == 90 || rotation == -90)
        {
            return new Vector3(Player.transform.position.x, cell.y, cell.z);
        }
        else
        {
            Debug.Log("player moving in invalid direction?");
            return new Vector3(cell.x, cell.y, Player.transform.position.z);
        }
    }

    /// <summary>
    /// Listen for the currently selected inputs and move accordingly
    /// </summary>
    private void MoveWithInputs()
    {
        if (Input.GetKeyDown("right")) MoveDirection("right");
        if (Input.GetKeyDown("left")) MoveDirection("left");
        if (Input.GetKeyDown("down")) MoveDirection("down");
        if (Input.GetKeyDown("up")) MoveDirection("up");
    }

    /// <summary>
    /// Update the player's position grid based on the given direction
    /// </summary>
    private void MoveDirection(string dir)
    {
        switch (dir)
        {
            case "down":
                if (Position.Item1 < 2)
                {
                    Position.Item1++;
                    PlayerManager.Instance.Controller.TriggerAnimation("Flip Down");
                }
                break;
            case "up":
                if (Position.Item1 > 0)
                {
                    Position.Item1--;
                    PlayerManager.Instance.Controller.TriggerAnimation("Flip Up");
                }
                break;
            case "right":
                if (TurnPossibility == "right")
                {
                    TurnDirection("right");
                    break;
                }

                if (Position.Item2 < 2)
                {
                    Position.Item2++;
                    PlayerManager.Instance.Controller.TriggerAnimation("Flip Right");
                }

                break;
            case "left":
                if (TurnPossibility == "left")
                {
                    TurnDirection("left");
                    break;
                }

                if (Position.Item2 > 0)
                {
                    Position.Item2--;
                    PlayerManager.Instance.Controller.TriggerAnimation("Flip Left");
                }

                break;
            default:
                Debug.Log("called Move() with invalid direction");
                break;
        }
    }

    /// <summary>
    /// Update the player's rotation to move in the new direction
    /// </summary>
    private void TurnDirection(string dir)
    {
        if (GameObject.FindGameObjectWithTag("preturn") == null) return;

        Destroy(GameObject.FindGameObjectWithTag("preturn"));
        TurnPossibility = null;

        if (dir == "right")
        {
            Rotation = Utils.ResetAngle(Rotation + 90);
            PlayerManager.Instance.Controller.TriggerAnimation("Flip Right");
        }
        else if (dir == "left")
        {
            Rotation = Utils.ResetAngle(Rotation - 90);
            PlayerManager.Instance.Controller.TriggerAnimation("Flip Left");
        }

        this.PlayRotateAnimation(dir);

        // Reset movement grid cell to center
        Position = (1, 1);
    }

    /// <summary>
    /// Play the appropriate rotation animation based on the old and new directions
    /// </summary>
    private void PlayRotateAnimation(string dir)
    {
        switch (Rotation)
        {
            case 0:
                if (dir == "right")
                    PlayerManager.Instance.Controller.PlayParentAnimation("Rotate Left-Forward");
                else
                    PlayerManager.Instance.Controller.PlayParentAnimation("Rotate Right-Forward");
                break;
            case 90:
                if (dir == "right")
                    PlayerManager.Instance.Controller.PlayParentAnimation("Rotate Forward-Right");
                else
                    PlayerManager.Instance.Controller.PlayParentAnimation("Rotate Back-Right");
                break;
            case 180:
                if (dir == "right")
                    PlayerManager.Instance.Controller.PlayParentAnimation("Rotate Right-Back");
                else
                    PlayerManager.Instance.Controller.PlayParentAnimation("Rotate Left-Back");
                break;
            case -90:
                if (dir == "right")
                    PlayerManager.Instance.Controller.PlayParentAnimation("Rotate Back-Left");
                else
                    PlayerManager.Instance.Controller.PlayParentAnimation("Rotate Forward-Left");
                break;
        }
    }

    void Update()
    {
        // Check for new inputs every frame
        MoveWithInputs();

        Vector3 movementGridCell = this.GetChosen3DVector();

        // Constantly move forward to the chosen movement grid cell in the 3D world
        Player.transform.position = Vector3.MoveTowards(Player.transform.position, movementGridCell, Time.deltaTime * Speed);

        // Linearly interpolate (lerp) from player's current position to the chosen movement grid cell on the 2D plane
        if (Player.transform.rotation.eulerAngles.y % 90 == 0)
            Player.transform.position = Vector3.Lerp(Player.transform.position, this.GetChosen2DVector(movementGridCell), Time.deltaTime * FlipSpeed);
    }

}

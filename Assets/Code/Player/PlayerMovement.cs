using System;
using UnityEngine;

/// <summary>
/// Process the player's inputs and enable movement around the level
/// </summary>
public class PlayerMovement : MonoBehaviour
{

    private PlayerController controller { get { return PlayerManager.Instance.Controller; } }

    private InputSource Inputs;

    // The player's forward movement speed
    public float Speed = 5f;

    // The player's left and right movement speed
    public float FlipSpeed = 5f;

    /// <summary>
    /// A tuple of the player's 2D position in the predefined grid of movements
    /// </summary>
    public (int, int) Position = (1, 1);

    /**
     * Default (1,1):       Right (1, 2):       Upper Right (0, 2):
     * |0|0|0|              |0|0|0|             |0|0|x|
     * |0|x|0|              |0|0|x|             |0|0|0|
     * |0|0|0|              |0|0|0|             |0|0|0|
     */

    /// <summary>
    /// The player's heading (0 is forward, 90 is right, -90 is left)
    /// </summary>
    public int Rotation = 0;

    /// <summary>
    /// The direction a player could possibly turn. Determined by a trigger collider.
    /// </summary>
    [HideInInspector]
    public string TurnPossibility = null;

    /// <summary>
    /// Return the full location of the 2D grid cell in the 3D space
    /// </summary>
    public Vector3 GetChosen3DVector()
    {
        string cellTag = string.Format("({0},{1})", Position.Item1, Position.Item2);

        GameObject parent = controller.CurrentSection.PreTurn;
        if (parent == null) parent = controller.CurrentSection.PostTurn;

        return parent.transform.Find(cellTag).transform.position;
    }

    /// <summary>
    /// Translate the player's 2D grid coordinates into the local 3D space
    /// </summary>
    public Vector3 GetChosen2DVector(Vector3 cell)
    {
        int rotation = Utils.ResetAngle(Rotation);

        if (rotation == 0 || rotation == 180)
        {
            return new Vector3(cell.x, cell.y, transform.position.z);
        }
        else if (rotation == 90 || rotation == -90)
        {
            return new Vector3(transform.position.x, cell.y, cell.z);
        }
        else
        {
            throw new Exception("player moving in invalid direction " + rotation);
        }
    }

    /// <summary>
    /// Update the player's position grid based on the given direction and play the appropriate animation
    /// </summary>
    public void MoveDirection(string dir)
    {
        switch (dir)
        {
            case "down":
                if (Position.Item1 >= 2) break;

                Position.Item1++;
                controller.TriggerPaperAnimation("Flip Down");
                break;

            case "up":
                if (Position.Item1 <= 0) break;

                Position.Item1--;
                controller.TriggerPaperAnimation("Flip Up");
                break;

            case "right":
                if (TurnPossibility == "right")
                {
                    TurnDirection("right");
                    break;
                }
                if (Position.Item2 >= 2) break;

                Position.Item2++;
                controller.TriggerPaperAnimation("Flip Right");
                break;

            case "left":
                if (TurnPossibility == "left")
                {
                    TurnDirection("left");
                    break;
                }
                if (Position.Item2 <= 0) break;

                Position.Item2--;
                controller.TriggerPaperAnimation("Flip Left");
                break;

            default:
                throw new Exception("invalid direction given: " + dir);
        }
    }

    /// <summary>
    /// Update the player's rotation to move in the new direction
    /// </summary>
    public void TurnDirection(string dir)
    {
        if (controller.CurrentSection.PreTurn == null) return;
        Destroy(controller.CurrentSection.PreTurn);

        TurnPossibility = null;

        if (dir == "right")
        {
            Rotation = Utils.ResetAngle(Rotation + 90);
            controller.TriggerPaperAnimation("Flip Right");
        }
        else if (dir == "left")
        {
            Rotation = Utils.ResetAngle(Rotation - 90);
            controller.TriggerPaperAnimation("Flip Left");
        }

        this.PlayRotateAnimation(dir);

        // Reset movement grid cell to center
        Position = (1, 1);
    }

    /// <summary>
    /// Play the appropriate rotation animation based on the old and new directions
    /// </summary>
    public void PlayRotateAnimation(string dir)
    {
        switch (Rotation)
        {
            case 0:
                if (dir == "right")
                    controller.PlayParentAnimation("Rotate Left-Forward");
                else
                    controller.PlayParentAnimation("Rotate Right-Forward");
                break;
            case -90:
                if (dir == "right")
                    controller.PlayParentAnimation("Rotate Back-Left");
                else
                    controller.PlayParentAnimation("Rotate Forward-Left");
                break;
            case 90:
                if (dir == "right")
                    controller.PlayParentAnimation("Rotate Forward-Right");
                else
                    controller.PlayParentAnimation("Rotate Back-Right");
                break;
            case 180:
                if (dir == "right")
                    controller.PlayParentAnimation("Rotate Right-Back");
                else
                    controller.PlayParentAnimation("Rotate Left-Back");
                break;
            default:
                throw new Exception("player rotation is invalid value: " + Rotation);
        }
    }

    void Awake()
    {
        // Detect platform and change input source accordingly
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
            case RuntimePlatform.IPhonePlayer:
                Inputs = new TouchInput();
                break;
            default:
                Inputs = new KeyboardInput();
                break;
        }
    }

    void Update()
    {
        // Check for new inputs every frame
        Inputs.Process();

        Vector3 movementGridCell = this.GetChosen3DVector();

        // Constantly move forward to the chosen movement grid cell in the 3D world
        transform.position = Vector3.MoveTowards(transform.position, movementGridCell, Time.deltaTime * Speed);

        // Linearly interpolate (lerp) from player's current position to the chosen movement grid cell on the 2D plane
        if (transform.rotation.eulerAngles.y % 90 == 0)
            transform.position = Vector3.Lerp(transform.position, this.GetChosen2DVector(movementGridCell), Time.deltaTime * FlipSpeed);
    }

}

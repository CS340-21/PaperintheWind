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

    private InputSource Inputs;

    // The player's forward movement speed
    public float Speed = 5f;

    // The player's left and right movement speed
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
    public int Rotation = 90;

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

        GameObject parent = PlayerManager.Instance.Controller.CurrentSection.PreTurn;
        if (parent == null) parent = PlayerManager.Instance.Controller.CurrentSection.PostTurn;

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
    /// Update the player's position grid based on the given direction
    /// </summary>
    public void MoveDirection(string dir)
    {
        PlayerController controller = PlayerManager.Instance.Controller;

        switch (dir)
        {
            case "down":
                if (Position.Item1 >= 2) break;

                Position.Item1++;
                controller.TriggerAnimation("Flip Down");
                break;

            case "up":
                if (Position.Item1 <= 0) break;

                Position.Item1--;
                controller.TriggerAnimation("Flip Up");
                break;

            case "right":
                if (TurnPossibility == "right")
                {
                    TurnDirection("right");
                    break;
                }
                if (Position.Item2 >= 2) break;

                Position.Item2++;
                controller.TriggerAnimation("Flip Right");
                break;

            case "left":
                if (TurnPossibility == "left")
                {
                    TurnDirection("left");
                    break;
                }
                if (Position.Item2 <= 0) break;

                Position.Item2--;
                controller.TriggerAnimation("Flip Left");
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
        if (PlayerManager.Instance.Controller.CurrentSection.PreTurn == null) return;
        Destroy(PlayerManager.Instance.Controller.CurrentSection.PreTurn);

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
        PlayerController controller = PlayerManager.Instance.Controller;

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
        Inputs.ProcessInput();

        Vector3 movementGridCell = this.GetChosen3DVector();

        // Constantly move forward to the chosen movement grid cell in the 3D world
        Player.transform.position = Vector3.MoveTowards(Player.transform.position, movementGridCell, Time.deltaTime * Speed);

        // Linearly interpolate (lerp) from player's current position to the chosen movement grid cell on the 2D plane
        if (Player.transform.rotation.eulerAngles.y % 90 == 0)
            Player.transform.position = Vector3.Lerp(Player.transform.position, this.GetChosen2DVector(movementGridCell), Time.deltaTime * FlipSpeed);
    }

}

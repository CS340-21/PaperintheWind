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

    public GameObject PaperObject;

    /**
     * Default (1,1):       Right (1, 2):       Upper Right (0, 2):
     * |0|0|0|              |0|0|0|             |0|0|x|
     * |0|x|0|              |0|0|x|             |0|0|0|
     * |0|0|0|              |0|0|0|             |0|0|0|
     */

    /// <summary>
    /// A tuple of the player's position in the predefined grid of movements
    /// </summary>
    private (int, int) position = (1, 1);

    /// <summary>
    /// The player's directional heading (0 is forward, 90 is right, -90 is left)
    /// </summary>
    private int rotation = 0;

    /// <summary>
    /// The direction a player could possibly turn. Determined by a trigger collider.
    /// </summary>
    [HideInInspector]
    public string turnPossibility = null;

    private Vector3 GetCellVector(string cellTag)
    {
        GameObject parent = GameObject.FindGameObjectWithTag("preturn");
        if (parent == null) parent = GameObject.FindGameObjectWithTag("postturn");

        return parent.transform.Find(cellTag).transform.position;
    }

    private Vector3 GetChosen3DVector()
    {
        string cellTag = string.Format("({0},{1})", position.Item1, position.Item2);
        return GetCellVector(cellTag);
    }

    /// <summary>
    /// Translate the player's simplified coordinates into the 3D space
    /// </summary>
    private Vector3 GetChosen2DVector(Vector3 cell)
    {
        float yAngle = Player.transform.rotation.eulerAngles.y;
        if (rotation == 0)
        {
            return new Vector3(cell.x, cell.y, Player.transform.position.z);
        }
        else if (rotation == 90)
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
                if (position.Item1 < 2)
                {
                    position.Item1++;
                    PlayerManager.Instance.Controller.TriggerAnimation("Flip Down");
                }
                break;
            case "up":
                if (position.Item1 > 0)
                {
                    position.Item1--;
                    PlayerManager.Instance.Controller.TriggerAnimation("Flip Up");
                }
                break;
            case "right":
                if (turnPossibility == "right")
                {
                    TurnDirection("right");
                    break;
                }

                if (position.Item2 < 2)
                {
                    position.Item2++;
                    PlayerManager.Instance.Controller.TriggerAnimation("Flip Right");
                }

                break;
            case "left":
                if (turnPossibility == "left")
                {
                    TurnDirection("left");
                    break;
                }

                if (position.Item2 > 0)
                {
                    position.Item2--;
                    PlayerManager.Instance.Controller.TriggerAnimation("Flip Left");
                }

                break;
            default:
                Debug.Log("called Move() with invalid direction");
                break;
        }
    }

    private void TurnDirection(string dir)
    {
        if (GameObject.FindGameObjectWithTag("preturn") == null) return;

        Destroy(GameObject.FindGameObjectWithTag("preturn"));
        turnPossibility = null;

        if (dir == "right")
        {
            rotation = 90;
            PlayerManager.Instance.Controller.TriggerAnimation("Turn Right");
        }
        if (dir == "left")
        {
            rotation = -90;
            PlayerManager.Instance.Controller.TriggerAnimation("Turn Left");
        }

        // Reset movement grid cell to center
        position = (1, 1);
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
            Player.transform.position = Vector3.Lerp(Player.transform.position, this.GetChosen2DVector(movementGridCell), Time.deltaTime * 5);

        Player.transform.rotation = Quaternion.RotateTowards(Player.transform.rotation, Quaternion.Euler(0f, rotation, 0f), Time.deltaTime * 500);
    }

}

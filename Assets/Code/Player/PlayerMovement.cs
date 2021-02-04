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

    /// <summary>
    /// Provide the translation table for converting the player's simplified position coordinates into
    /// real 2D coordinates for the level.
    /// TODO: move this 2d array to the current level's object
    /// </summary>
    private (float, float)[,] LOCATIONS = new (float, float)[3, 3] {
        { (-17.69f, 2.77f), (-15.33f, 2.77f), (-12.96f, 2.77f) },
        { (-17.69f, 2.1695f), (-15.33f, 2.1695f), (-12.96f, 2.1695f) },
        { (-17.69f, 1.44f), (-15.33f, 1.44f), (-12.96f, 1.44f) }
    };

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
    /// Translate the player's simplified coordinates into the 3D space
    /// </summary>
    private Vector3 GetChosen2DVector()
    {
        (float, float) face = LOCATIONS[position.Item1, position.Item2];
        return new Vector3(face.Item1, face.Item2, Player.transform.position.z);
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
                if (position.Item1 < 2) position.Item1++;
                break;
            case "up":
                if (position.Item1 > 0) position.Item1--;
                break;
            case "right":
                if (position.Item2 < 2) position.Item2++;
                break;
            case "left":
                if (position.Item2 > 0) position.Item2--;
                break;
            default:
                Debug.Log("called Move() with invalid direction");
                break;
        }
    }

    void Update()
    {
        // Check for new inputs every frame
        MoveWithInputs();

        // Move forward infinitely
        Vector3 forwardVector = Player.transform.rotation * Vector3.forward;
        Player.transform.position += forwardVector * Time.deltaTime * 2;

        // Linearly interpolate (lerp) from player's current position to the chosen position based on player's input
        Player.transform.position = Vector3.Lerp(Player.transform.position, this.GetChosen2DVector(), Time.deltaTime * 5);
    }

}

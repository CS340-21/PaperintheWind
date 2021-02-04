using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    /**************************************
     * Set up the PlayerManager singleton *
     *************************************/

    private static PlayerManager _instance;

    public static PlayerManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }



    /*************************
     * GameObject References *
     *************************/

    public GameObject Player { get { return gameObject; } }

    public GameObject Paper;



    /*******************
     * Local Variables *
     *******************/

    /// <summary>
    /// Provide the translation table for converting the player's simplified position coordinates into
    /// real 2D coordinates for the level.
    /// TODO: move this 2d array to the current level's object
    /// </summary>
    private Vector2[,] LOCATIONS = new Vector2[3, 3] {
        { new Vector2(-10.3f, 2.35f), new Vector2(-8f, 2.35f), new Vector2(-5.5f, 2.35f) },
        { new Vector2(-10.3f, 1.7f), new Vector2(-8f, 1.7f), new Vector2(-5.5f, 1.7f) },
        { new Vector2(-10.3f, 0.8f), new Vector2(-8f, 0.8f), new Vector2(-5.5f, 0.8f) }
    };

    /**
     * Default (1,1):       Right (1, 2):       Upper Right (0, 2):
     * |0|0|0|              |0|0|0|             |0|0|x|
     * |0|x|0|              |0|0|x|             |0|0|0|
     * |0|0|0|              |0|0|0|             |0|0|0|
     */

    /// <summary>
    /// The player's position in the predefined grid of movements
    /// </summary>
    private (int, int) position = (1, 1);



    /********************
     * Member Functions *
     ********************/

    /// <summary>
    /// Translate the player's simplified coordinates into the 3D space
    /// </summary>
    private Vector3 GetChosenPositionVector()
    {
        Vector2 face = LOCATIONS[position.Item1, position.Item2];
        return new Vector3(face.x, face.y, Player.transform.position.z);
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
        Player.transform.position += Vector3.forward * Time.deltaTime * 2;

        // Linearly interpolate (lerp) from player's current position to the chosen position based on player's input
        Player.transform.position = Vector3.Lerp(Player.transform.position, this.GetChosenPositionVector(), Time.deltaTime * 5);
    }
}

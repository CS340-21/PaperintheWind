using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTransform : MonoBehaviour
{

    public string TransformProperty = "rotation_y";
    public float MinimumValue = 0f;
    public float MaximumValue = -135f;

    // Start is called before the first frame update
    void Start()
    {
        float randomValue = Random.Range(MinimumValue, MaximumValue);
        Vector3 pos = transform.position;
        CardinalDirection sectionDirection = Utils.GetAngleDirection(Utils.GetParentSection(gameObject).BeginRotation);
        switch (TransformProperty)
        {
            case "rotation_x":
                transform.Rotate(randomValue, 0f, 0f);
                break;
            case "rotation_y":
                transform.Rotate(0f, randomValue, 0f);
                break;
            case "rotation_z":
                transform.Rotate(0f, 0f, randomValue);
                break;
            case "position_x":
                // We know the script is added when the object is facing north (0 degrees) as the default,
                // so let's change how it moves when the parent section is rotated away from north
                switch (sectionDirection)
                {
                    case CardinalDirection.NORTH:
                        transform.position = new Vector3(pos.x + randomValue, pos.y, pos.z);
                        break;
                    case CardinalDirection.SOUTH:
                        transform.position = new Vector3(pos.x - randomValue, pos.y, pos.z);
                        break;
                    case CardinalDirection.EAST:
                        transform.position = new Vector3(pos.x, pos.y, pos.z + randomValue);
                        break;
                    case CardinalDirection.WEST:
                        transform.position = new Vector3(pos.x, pos.y, pos.z - randomValue);
                        break;
                }
                break;
            case "position_y":
                // Moving the object up or down doesn't depend on the rotation to do it correctly,
                // so we don't need to change the behavior based on the rotation
                transform.position = new Vector3(pos.x, pos.y + randomValue, pos.z);
                break;
            case "position_z":
                // We know the script is added when the object is facing north (0 degrees) as the default,
                // so let's change how it moves when the parent section is rotated away from north
                switch (sectionDirection)
                {
                    case CardinalDirection.NORTH:
                        transform.position = new Vector3(pos.x, pos.y, pos.z + randomValue);
                        break;
                    case CardinalDirection.SOUTH:
                        transform.position = new Vector3(pos.x, pos.y, pos.z - randomValue);
                        break;
                    case CardinalDirection.EAST:
                        transform.position = new Vector3(pos.x + randomValue, pos.y, pos.z);
                        break;
                    case CardinalDirection.WEST:
                        transform.position = new Vector3(pos.x - randomValue, pos.y, pos.z);
                        break;
                }
                break;
            default:
                Utils.Crash("RandomTransform script given an invalid transform property: " + TransformProperty);
                break;
        }
    }
}

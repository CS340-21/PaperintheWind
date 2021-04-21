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
                transform.position = new Vector3(transform.position.x + randomValue, transform.position.y, transform.position.z);
                break;
            case "position_y":
                transform.position = new Vector3(transform.position.x, transform.position.y + randomValue, transform.position.z);
                break;
            case "position_z":
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + randomValue);
                break;
            default:
                Utils.Crash("RandomTransform script given an invalid transform property: " + TransformProperty);
                break;
        }
    }
}

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
                gameObject.transform.Rotate(randomValue, 0f, 0f);
                break;
            case "rotation_y":
                gameObject.transform.Rotate(0f, randomValue, 0f);
                break;
            case "rotation_z":
                gameObject.transform.Rotate(0f, 0f, randomValue);
                break;
            default:
                Utils.Crash("RandomTransform script given an invalid transform property: " + TransformProperty);
                break;
        }
    }
}

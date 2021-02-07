using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Add this script to a parent object containing a row of unpassable obstacles.
/// This script will randomly delete one of the obstacles to create a hole.
/// </summary>
public class ObstacleRow : MonoBehaviour
{

    public Section ParentSection;

    private GameObject[] GetChildren()
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }
        return children.ToArray();
    }

    void Start()
    {
        if (ParentSection == null)
            throw new Exception("obstacle row has no parent section");

        if (ParentSection.ID > 0)
            Destroy(Utils.GetRandom(this.GetChildren()));
    }

}

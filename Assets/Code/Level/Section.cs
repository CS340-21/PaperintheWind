using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section : MonoBehaviour
{

    // TODO:    When generating next section, the begin rotation must be last section's end rotation.
    //          So if Section 1 EndRotation=90 but Section 2 BeginRotation=0, rotate Section 2 by 90 degrees.
    //
    //          Maybe transform.SetParent(BeginSignalObject, true) on the Section 2 parent, move BeginSignalObject
    //          to Section 1 EndSignalObject, then reset with BeginSignalObject.SetParent(transform.Transform, true)

    public int BeginRotation;
    public int EndRotation;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

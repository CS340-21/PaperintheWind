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

    public GameObject Spawn { get { return this.transform.Find("Spawn").gameObject; } }

    public GameObject Beginning { get { return this.transform.Find("Signals/Begin").gameObject; } }

    public GameObject Ending { get { return this.transform.Find("Signals/End").gameObject; } }

    public GameObject PreTurn
    {
        get
        {
            if (this.transform.Find("MoveGrid/PreTurn") == null) return null;
            return this.transform.Find("MoveGrid/PreTurn").gameObject;
        }
    }

    public GameObject PostTurn
    {
        get
        {
            if (this.transform.Find("MoveGrid/PostTurn") == null) return null;
            return this.transform.Find("MoveGrid/PostTurn").gameObject;
        }
    }

    // mvoe beginning object to given position, bringing all objects with
    public void AlignWithSection(Section section)
    {
        Vector3 absoluteMovement = section.Ending.transform.position - this.Beginning.transform.position;
        this.transform.position += absoluteMovement;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

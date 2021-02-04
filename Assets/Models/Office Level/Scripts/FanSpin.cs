using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpin : MonoBehaviour
{

    public float xAxisSpeed = 0.0f;
    public float yAxisSpeed = 0.0f;
    public float zAxisSpeed = 14.0f;
    private float xAxis;
    private float yAxis;
    private float zAxis;


    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        // alters the angle of the object over time.
        xAxis += xAxisSpeed ;
        yAxis += yAxisSpeed ;
        zAxis += zAxisSpeed ;

         Vector3 target = new Vector3(xAxis, yAxis, zAxis);
   
        transform.localEulerAngles = target;
    }

}

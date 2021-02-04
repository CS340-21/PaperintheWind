using System;
using UnityEngine;



public class ClockHands : MonoBehaviour {

    private const float
      hoursToDegrees = 360f / 12f,
      minutesToDegrees = 360f / 60f;
     // secondsToDegrees = 360f / 60f;

    public Transform hours, minutes;//, seconds;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      
        // sets the angle of the hands based on the system time.

        DateTime time = DateTime.Now;
        hours.localRotation = Quaternion.Euler(0f, 0f, ((time.Hour ) * hoursToDegrees)+ ((time.Minute * minutesToDegrees) / 12.0f));
        minutes.localRotation = Quaternion.Euler(0f, 0f, (time.Minute * minutesToDegrees));
        
    }
}

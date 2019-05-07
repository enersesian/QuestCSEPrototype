using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public float movementSpeed = 0.01f;

    private Vector2 rightThumbAxis;
    private float heightChange = 0f;
	
	// Update is called once per frame
	void Update ()
    {
        rightThumbAxis = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick) * movementSpeed;

        if (OVRInput.Get(OVRInput.Button.One)) //Right-hand A button
        {
            heightChange = -1f * movementSpeed;
        }

        else if (OVRInput.Get(OVRInput.Button.Two)) //Right-hand B button
        {
            heightChange = 1f * movementSpeed;
        }

        else heightChange = 0f;

        transform.position = new Vector3(transform.position.x + rightThumbAxis.x, transform.position.y + heightChange, transform.position.z + rightThumbAxis.y);
    }
}

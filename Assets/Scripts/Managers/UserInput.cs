﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public LevelManager levelManager;

    public float movementSpeed = 0.01f;

    private Vector2 rightThumbAxis;
    private float heightChange = 0f;
    private bool isNear;

    // Update is called once per frame
    //Possible feature: disable hand colliders when moving avatar around level 
    //to remove chance of superuser accidently interacting with a station's controls while moving
    private void Start()
    {
        //levelManager.transform.position = new Vector3(levelManager.transform.position.x, levelManager.transform.position.y + 0.02f, levelManager.transform.position.z);
    }
    void Update ()
    {
        
        rightThumbAxis = OVRInput.Get(OVRInput.RawAxis2D.RThumbstick) * movementSpeed; //position control
        /*
        if (OVRInput.Get(OVRInput.Button.One)) //Right-hand A button
        {
            heightChange = -1f * movementSpeed;
        }

        else if (OVRInput.Get(OVRInput.Button.Two)) //Right-hand B button
        {
            heightChange = 1f * movementSpeed;
        }

        else heightChange = 0f;
        */
        levelManager.transform.position = new Vector3(levelManager.transform.position.x + rightThumbAxis.x, levelManager.transform.position.y + heightChange, levelManager.transform.position.z + rightThumbAxis.y);
        //levelManager.transform.position = new Vector3(levelManager.transform.position.x, levelManager.transform.position.y + heightChange, levelManager.transform.position.z);

        if (OVRInput.Get(OVRInput.Button.Three)) //Left-hand X button //Reset level
        {
            levelManager.SetTask("start", 1); //task one
            levelManager.outputStation.GetTaskLeverPulled(1); //need to resetCubeGrabbable if manually resetting level
        }
        
        if (OVRInput.Get(OVRInput.Button.One) && OVRInput.Get(OVRInput.Button.Two) && OVRInput.GetDown(OVRInput.Button.Three))
        {
            levelManager.SetLevelDistance(isNear);
            isNear = !isNear;
        }

    }
}
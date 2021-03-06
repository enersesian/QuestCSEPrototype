﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceOutput : MonoBehaviour
{
    public Text taskText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10) //layer OutputSent, collider on cubeGrabbable
        {
            LevelManager.instance.PlacedOutput();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 10) //layer OutputSent, collider on cubeGrabbable
        {
            LevelManager.instance.RemovedOutput();
        }
    }
}

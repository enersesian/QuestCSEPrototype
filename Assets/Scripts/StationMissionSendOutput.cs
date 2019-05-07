using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationMissionSendOutput : MonoBehaviour
{
    public Text taskText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10) //layer SendOutput, collider on cubeGrabbable
        {
            transform.GetChild(0).GetComponent<RandomMovement>().isSent = true;
            taskText.text = "You performed the task perfectly! Congratulations!";
        }
    }
}

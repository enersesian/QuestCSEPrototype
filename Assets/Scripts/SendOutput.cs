using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendOutput : MonoBehaviour
{
    public Text taskText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10) //layer SendOutput, collider on cubeGrabbable
        {
            transform.parent.parent.parent.GetComponent<StationManager>().SendOutput();
        }
    }
}

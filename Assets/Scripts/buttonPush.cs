using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonPush : MonoBehaviour
{
    public Animator buttonAnim;
    public Text TaskText;
    public stationNumber stationNumberVariable;
    public OutputStationManager outputMng;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            buttonAnim.SetBool("isPushed", true);
            TaskText.text = "Please bring back one blue cube. \nStart at the number station";
            stationNumberVariable.ActivateStation();
            outputMng.ActivateStation();
        }
        

        //Debug.Log("hand in position");
        //if user clicks
        /*
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) || OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
        {
            //push button in, hold push button out
            buttonAnim.SetTrigger("pushed");
            //update UI
        }
        */
    }

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            buttonAnim.SetBool("isPushed", false);

        }
    }
    */
}

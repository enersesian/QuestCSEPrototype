using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendOutput : MonoBehaviour
{
    public Text taskText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            transform.GetChild(0).GetComponent<RandomMovement>().isSent = true;
            taskText.text = "You performed the task perfectly! Congratulations!";
        }
    }
}

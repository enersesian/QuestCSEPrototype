using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buttonPushOutput : MonoBehaviour
{
    public Animator buttonAnim;
    public Text TaskText;
    public GameObject cubeCollectable;
    public stationNumber stationNumMng;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 9)
        {
            buttonAnim.SetBool("isPushed", true);
            TaskText.text = "Output generated\nPlease take output to task station";
            if(stationNumMng.GetTotalCount() == 1)
            {
                cubeCollectable.SetActive(true);
            }
        }
    }
}

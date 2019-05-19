using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputSent : MonoBehaviour
{
    public Text taskText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10) //layer OutputSent, collider on cubeGrabbable
        {
            transform.root.GetComponent<LevelManager>().OutputSent();
        }
    }
}

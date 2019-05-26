using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbed : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        transform.root.GetComponent<LevelManager>().OutputContainerGrabbed();
    }
}

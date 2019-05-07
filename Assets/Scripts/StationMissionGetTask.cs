using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationMissionGetTask : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) //Hand sphere
        {
            transform.root.GetComponent<StationManager>().GetTask();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggyTouched : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        transform.parent.parent.parent.GetComponent<TutorialStation>().isEggyTouched = true;
    }
}

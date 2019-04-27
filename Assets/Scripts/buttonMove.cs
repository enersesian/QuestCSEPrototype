using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonMove : MonoBehaviour
{
    public GameObject button;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        if (transform.localPosition.z < 0f) transform.localPosition = Vector3.zero;
        if (transform.localPosition.z > 0.5f)
        {
            transform.localPosition = Vector3.zero;
            //update task window
        }
        button.transform.localPosition = transform.localPosition;
    }
}

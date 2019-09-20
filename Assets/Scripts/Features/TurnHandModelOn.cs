using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnHandModelOn : MonoBehaviour {

	//Oculus app update now runs unity editor play mode in steamVR and hands renderers are turned off for some reason
	void Start ()
    {
        Invoke("TurnSkinnedMeshRendererOn", 0.5f);
	}
	
	private void TurnSkinnedMeshRendererOn ()
    {
        GetComponent<SkinnedMeshRenderer>().enabled = true;
    }
}

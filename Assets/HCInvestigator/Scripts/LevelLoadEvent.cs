using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class LevelLoadEvent : HCInvestigatorEvent
{


    private UnityAction listner;
    private int nextLevelIndex;
    private AsyncOperation asyncLoad;
	// Use this for initialization
	void Start () {

        listner = new UnityAction(LoadNextLevel);

        nextLevelIndex = SceneManager.GetActiveScene().buildIndex + 1;
        StartCoroutine(LoadSceneAsy());
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void LoadNextLevel()
    {
        HCInvestigatorManager.instance.WriteTextData(0, SceneManager.GetActiveScene().name + " completed" + " at" + " " + DateTime.Now.ToString("MM-dd-yy hh_mm_ss"));
        asyncLoad.allowSceneActivation = false;
        
    }

    IEnumerator LoadSceneAsy()
    {
        asyncLoad = SceneManager.LoadSceneAsync(nextLevelIndex);
        asyncLoad.allowSceneActivation = false;
        while(asyncLoad.progress < 0.9f)
        {
            yield return null;
        }
    }

}

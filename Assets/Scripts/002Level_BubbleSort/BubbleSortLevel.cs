using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSortLevel : MonoBehaviour {

    private List<Listener> listeners = new List<Listener>();

    public BubbleSortState currentState;

    // Use this for initialization
    void Start()
    {
        SetAppState(BubbleSortState.IntroductionToNextButton);
    }

    public void RegisterListener(Listener newListener)
    {
        listeners.Add(newListener);
        Debug.Log("Registered " + newListener.gameObject.name);
    }

    private void SetAppState(BubbleSortState tempState)
    {
        currentState = tempState;
        Debug.Log(currentState.ToString());
        foreach (Listener listenerObj in listeners) listenerObj.SetListenerState(tempState);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SetAppState(BubbleSortState.IntroductionToNextButton);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SetAppState(BubbleSortState.BeginnerBubbleSortTask01);
    }

    public void ButtonPushed(string buttonName)
    {
        if (currentState == BubbleSortState.IntroductionToNextButton && buttonName == "ButtonNext") SetAppState(BubbleSortState.IntroductionToSwapButton);
    }
}

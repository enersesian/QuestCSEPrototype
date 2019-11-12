using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSortLevel : MonoBehaviour {

    private List<Listener> listeners = new List<Listener>();

    public BubbleSortState currentState;

    // Use this for initialization
    void Start()
    {
        //SetAppState(BubbleSortState.IntroductionToNextButton);
        SetAppState(currentState);
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

    private void SetAppStateToIntroductionToCyclingThroughList()
    {
        SetAppState(BubbleSortState.IntroductionToCyclingThroughList);
    }

    private void SetAppStateToIntroductionToThreeElementList03()
    {
        SetAppState(BubbleSortState.IntroductionToThreeElementList03);
    }

    private void SetAppStateToIntroductionToThreeElementList05()
    {
        SetAppState(BubbleSortState.IntroductionToThreeElementList05);
    }

    public void ButtonPushed(string buttonName)
    {
        
        switch (currentState)
        {
            case BubbleSortState.IntroductionToNextButton:
                if (buttonName == "ButtonNext") SetAppState(BubbleSortState.IntroductionToSwapButton);
                break;

            case BubbleSortState.IntroductionToSwapButton:
                if (buttonName == "ButtonSwap")
                {
                    foreach (Listener listenerObj in listeners) listenerObj.ButtonPushed(buttonName);
                    Invoke("SetAppStateToIntroductionToCyclingThroughList", 4f);
                }
                break;

            case BubbleSortState.IntroductionToCyclingThroughList:
                if (buttonName == "ButtonNext") SetAppState(BubbleSortState.IntroductionToFinishingBubbleSort);
                break;

            case BubbleSortState.IntroductionToFinishingBubbleSort:
                if (buttonName == "ButtonNext") SetAppState(BubbleSortState.IntroductionToThreeElementList01);
                break;

            case BubbleSortState.IntroductionToThreeElementList01:
                if (buttonName == "ButtonNext")
                {
                    foreach (Listener listenerObj in listeners) listenerObj.ButtonPushed(buttonName);
                    SetAppState(BubbleSortState.IntroductionToThreeElementList02);
                }
                break;

            case BubbleSortState.IntroductionToThreeElementList02:
                if (buttonName == "ButtonSwap")
                {
                    foreach (Listener listenerObj in listeners) listenerObj.ButtonPushed(buttonName);
                    Invoke("SetAppStateToIntroductionToThreeElementList03", 4f);
                }
                break;

            case BubbleSortState.IntroductionToThreeElementList03:
                if (buttonName == "ButtonNext")
                {
                    foreach (Listener listenerObj in listeners) listenerObj.ButtonPushed(buttonName);
                    SetAppState(BubbleSortState.IntroductionToThreeElementList04);
                }
                break;

            case BubbleSortState.IntroductionToThreeElementList04:
                if (buttonName == "ButtonSwap")
                {
                    foreach (Listener listenerObj in listeners) listenerObj.ButtonPushed(buttonName);
                    Invoke("SetAppStateToIntroductionToThreeElementList05", 4f);
                }
                break;

            case BubbleSortState.IntroductionToThreeElementList05:
                if (buttonName == "ButtonNext")
                {
                    foreach (Listener listenerObj in listeners) listenerObj.ButtonPushed(buttonName);
                    SetAppState(BubbleSortState.IntroductionToThreeElementList06);
                }
                break;

            case BubbleSortState.IntroductionToThreeElementList06:
                if (buttonName == "ButtonNext") SetAppState(BubbleSortState.IntroductionToThreeElementList07);
                break;

            case BubbleSortState.IntroductionToThreeElementList07:
                if (buttonName == "ButtonNext") SetAppState(BubbleSortState.IntroductionToThreeElementList08);
                break;

            case BubbleSortState.IntroductionToThreeElementList08:
                if (buttonName == "ButtonNext") SetAppState(BubbleSortState.IntroductionToThreeElementList09);
                break;

            case BubbleSortState.IntroductionToThreeElementList09:
                if (buttonName == "ButtonNext") SetAppState(BubbleSortState.BeginnerBubbleSortTask01);
                break;

            default:
                foreach (Listener listenerObj in listeners) listenerObj.ButtonPushed(buttonName);
                break;
        }
        
    }
}

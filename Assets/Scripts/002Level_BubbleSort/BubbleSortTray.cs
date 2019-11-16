﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSortTray : Listener
{
    public Text passiveInstructionalText, activeInstructionalText;
    private Color blankColor = new Color(0f, 0f, 0f, 0f);
    private float start, end, time;
    [SerializeField]
    private ButtonPushable buttonSwap, buttonNext;
    private IEnumerator coroutine;
    private string passiveInstruction, activeInstruction;
    private Vector3 trayEndPosition;
    public UIControls uiControls;

    private void Awake()
    {
        base.Awake();
    }

    private IEnumerator TrayTransition(Vector3 startPosition, Vector3 endPosition, float moveTime = 4f, float waitTime = 0f)
    {
        yield return new WaitForSeconds(waitTime);
        float elapsedTime = 0;
        while (elapsedTime < moveTime)
        {
            transform.localPosition = Vector3.Lerp(startPosition, endPosition, (elapsedTime / moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = endPosition;
        yield return null;
    }

    private IEnumerator TextTransition(Text textElement, Color startColor, Color endColor, string endText, float moveTime = 4f, float waitTime = 0f)
    {
        yield return new WaitForSeconds(waitTime);
        if (endColor.a == 1) textElement.text = endText;
        float elapsedTime = 0;
        while (elapsedTime < moveTime)
        {
            textElement.color = Color.Lerp(startColor, endColor, (elapsedTime / moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        textElement.color = endColor;
        yield return null;
    }

    private void ResetTray(float transitionTime = 0)
    {
        coroutine = TextTransition(passiveInstructionalText, passiveInstructionalText.color, blankColor, "", transitionTime, 0f);
        StartCoroutine(coroutine);
        coroutine = TextTransition(activeInstructionalText, passiveInstructionalText.color, blankColor, "", transitionTime, 0f);
        StartCoroutine(coroutine);

        buttonSwap.ResetButton(false);
        buttonNext.ResetButton(false);
    }

    private void TurnOnButtonNext()
    {
        buttonNext.ResetButton(true);
        buttonNext.StartTutorialBlink();
    }

    private void TurnOffButtonNext()
    {
        buttonNext.ResetButton(false);
    }

    private void TurnOnButtonSwap()
    {
        buttonSwap.ResetButton(true);
        buttonSwap.StartTutorialBlink();
    }

    private void TurnOffButtonSwap()
    {
        buttonSwap.ResetButton(false);
    }

    private void TurnOnButtons()
    {
        buttonNext.ResetButton(true);
        buttonSwap.ResetButton(true);
    }

    public override void TaskUnsuccessful(int hint)
    {
        coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
        StartCoroutine(coroutine);
        passiveInstruction = "List is not in order. You might want to check around container " + hint.ToString() + ".";
        coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
        StartCoroutine(coroutine);
    }

    public void UpdateTray(int containerPosition, int uiSize)
    {
        if(containerPosition > 0) trayEndPosition = new Vector3(transform.localPosition.x + 0.5f, transform.localPosition.y, transform.localPosition.z);
        else trayEndPosition = new Vector3(transform.localPosition.x - (uiSize * 0.25f) + 0.25f, transform.localPosition.y, transform.localPosition.z);
        coroutine = TrayTransition(transform.localPosition, trayEndPosition, 4f, 0f);
        StartCoroutine(coroutine);
    }

    public override void TaskSuccessful()
    {
        coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 2f, 0f);
        StartCoroutine(coroutine);
        coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 2f, 0f);
        StartCoroutine(coroutine);
        passiveInstruction = "Great job! You successfully sorted this list.";
        activeInstruction = "Press next to proceed for next task.";
        coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 2f, 3f);
        StartCoroutine(coroutine);
        coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 2f, 5f);
        StartCoroutine(coroutine);
        TurnOffButtonSwap();
    }

    public override void SetListenerState(BubbleSortState currentState)
    {
        switch (currentState)
        {
            case BubbleSortState.IntroductionToNextButton:
                //ResetTray();
                uiControls.SetUISize(0);
                passiveInstructionalText.text = "";
                activeInstructionalText.text = "";
                buttonSwap.ResetButton(false);
                buttonNext.ResetButton(false);
                passiveInstruction = "Welcome to the Bubble Sort trainer. Let's learn to use the controls.";
                activeInstruction = "Find the blinking button. Its the next button. Press it to continue.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 4f);
                StartCoroutine(coroutine);
                Invoke("TurnOnButtonNext", 8f);
                Debug.Log(gameObject.name + " set to " + currentState.ToString());
                break;

            case BubbleSortState.IntroductionToSwapButton:
                //ResetTray();
                uiControls.SetUISize(2);
                coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                passiveInstruction = "In front of you are two containers that each contain an amount of objects.";
                activeInstruction = "We need to place them into ascending order. Press the blinking button to swap them.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 9f);
                StartCoroutine(coroutine);
                Invoke("TurnOffButtonNext", 4f);
                Invoke("TurnOnButtonSwap", 14f);
                break;

            case BubbleSortState.IntroductionToCyclingThroughList:
                coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                passiveInstruction = "Notice the numbers inbetween the buttons help you keep track of the order.";
                activeInstruction = "These two containers are now in correct ascending order. Press next button to finish the sort.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 9f);
                StartCoroutine(coroutine);
                Invoke("TurnOffButtonSwap", 4f);
                Invoke("TurnOnButtonNext", 14f);
                break;

            case BubbleSortState.IntroductionToFinishingBubbleSort:
                uiControls.SetUISize(0);
                coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                passiveInstruction = "Once we cycle through the list without having to swap any containers, the sort is complete.";
                activeInstruction = "Press the next button to learn bubble sort on three containers.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 9f);
                StartCoroutine(coroutine);
                Invoke("TurnOffButtonNext", 4f);
                Invoke("TurnOnButtonNext", 14f);
                break;

            case BubbleSortState.IntroductionToThreeElementList01:
                uiControls.SetUISize(3);
                trayEndPosition = new Vector3(-0.25f, transform.localPosition.y, transform.localPosition.z);
                coroutine = TrayTransition(transform.localPosition, trayEndPosition, 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                passiveInstruction = "In front of you are now three containers. Lets learn to sort a list of three containers.";
                activeInstruction = "Focus on two containers at a time. These two are in order and dont need to swap, press next.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 9f);
                StartCoroutine(coroutine);
                Invoke("TurnOffButtonNext", 4f);
                Invoke("TurnOnButtonNext", 14f);
                break;

            case BubbleSortState.IntroductionToThreeElementList02:
                trayEndPosition = new Vector3(0.25f, transform.localPosition.y, transform.localPosition.z);
                coroutine = TrayTransition(transform.localPosition, trayEndPosition, 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                passiveInstruction = "Notice we moved only one container to the right, and now we examine these two containers.";
                activeInstruction = "These two containers are not in ascending order. Press swap to place them in correct order.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 9f);
                StartCoroutine(coroutine);
                Invoke("TurnOffButtonNext", 4f);
                Invoke("TurnOnButtonSwap", 14f);
                break;

            case BubbleSortState.IntroductionToThreeElementList03:
                coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                passiveInstruction = "Notice that these two are now in order but the previous two are now not in order.";
                activeInstruction = "We need to return to the beginning of the list to swap them. Press next to move to beginning.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 9f);
                StartCoroutine(coroutine);
                Invoke("TurnOffButtonSwap", 4f);
                Invoke("TurnOnButtonNext", 14f);
                break;

            case BubbleSortState.IntroductionToThreeElementList04:
                trayEndPosition = new Vector3(-0.25f, transform.localPosition.y, transform.localPosition.z);
                coroutine = TrayTransition(transform.localPosition, trayEndPosition, 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                passiveInstruction = "Notice that we may need to cycle through the list to continue swapping to get all in order.";
                activeInstruction = "Let's swap these two containers to place them into ascending order. Press swap.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 9f);
                StartCoroutine(coroutine);
                Invoke("TurnOffButtonNext", 4f);
                Invoke("TurnOnButtonSwap", 14f);
                break;

            case BubbleSortState.IntroductionToThreeElementList05:
                coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                passiveInstruction = "These two containers are now in order. Notice the entire list is now in ascending order.";
                activeInstruction = "But we only focus on two containers at a time. Let's press next to continue checking the list.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 9f);
                StartCoroutine(coroutine);
                Invoke("TurnOffButtonSwap", 4f);
                Invoke("TurnOnButtonNext", 14f);
                break;
                
            case BubbleSortState.IntroductionToThreeElementList06:
                trayEndPosition = new Vector3(0.25f, transform.localPosition.y, transform.localPosition.z);
                coroutine = TrayTransition(transform.localPosition, trayEndPosition, 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                passiveInstruction = "These two containers are also in ascending order. There is no need to swap them.";
                activeInstruction = "Since there is no need to swap, let's press the next button to cycle back to beginning of list.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 9f);
                StartCoroutine(coroutine);
                Invoke("TurnOffButtonNext", 4f);
                Invoke("TurnOnButtonNext", 14f);
                break;

            case BubbleSortState.IntroductionToThreeElementList07:
                trayEndPosition = new Vector3(-0.25f, transform.localPosition.y, transform.localPosition.z);
                coroutine = TrayTransition(transform.localPosition, trayEndPosition, 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                passiveInstruction = "The bubble sort algorithm does not finish until we go through the entire list without swapping.";
                activeInstruction = "That way we know for sure that the list is sorted. Press next since these two are in order.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 9f);
                StartCoroutine(coroutine);
                Invoke("TurnOffButtonNext", 4f);
                Invoke("TurnOnButtonNext", 14f);
                break;

            case BubbleSortState.IntroductionToThreeElementList08:
                trayEndPosition = new Vector3(0.25f, transform.localPosition.y, transform.localPosition.z);
                coroutine = TrayTransition(transform.localPosition, trayEndPosition, 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                passiveInstruction = "We are at the end of the list and have not swapped previous elements this cycle through.";
                activeInstruction = "These two are also in ascending order. Press next to complete the final cycle through the list.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 9f);
                StartCoroutine(coroutine);
                Invoke("TurnOffButtonNext", 4f);
                Invoke("TurnOnButtonNext", 14f);
                break;

            case BubbleSortState.IntroductionToThreeElementList09:
                uiControls.SetUISize(0);
                coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                passiveInstruction = "Congratulations on learning the basics of a bubble sort. Now let's do a series of exercises.";
                activeInstruction = "Once you complete these exercises, we will move to freeform bubble sort. Press next to begin.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 9f);
                StartCoroutine(coroutine);
                Invoke("TurnOffButtonNext", 4f);
                Invoke("TurnOnButtonNext", 14f);
                break;

            case BubbleSortState.BeginnerBubbleSortTask01:
                uiControls.SetUISize(3);
                trayEndPosition = new Vector3(-0.25f, transform.localPosition.y, transform.localPosition.z);
                coroutine = TrayTransition(transform.localPosition, trayEndPosition, 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                passiveInstruction = "Here is a list of 3 containers. Short them in ascending order. Focus on two containers at a time.";
                activeInstruction = "Remember you must cycle through the list without a swap to complete the bubble sort correctly.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 9f);
                StartCoroutine(coroutine);
                Invoke("TurnOffButtonNext", 4f);
                Invoke("TurnOnButtons", 14f);
                break;

            case BubbleSortState.BeginnerBubbleSortTask02:
                uiControls.SetUISize(4);
                trayEndPosition = new Vector3(-0.5f, transform.localPosition.y, transform.localPosition.z);
                coroutine = TrayTransition(transform.localPosition, trayEndPosition, 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                passiveInstruction = "Here is a list of 4 containers. Short them in ascending order. Focus on two containers at a time.";
                activeInstruction = "Remember you must cycle through the list without a swap to complete the bubble sort correctly.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 9f);
                StartCoroutine(coroutine);
                Invoke("TurnOffButtonNext", 4f);
                Invoke("TurnOnButtons", 14f);
                break;

            case BubbleSortState.BeginnerBubbleSortTask03:
                uiControls.SetUISize(5);
                trayEndPosition = new Vector3(-0.75f, transform.localPosition.y, transform.localPosition.z);
                coroutine = TrayTransition(transform.localPosition, trayEndPosition, 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                passiveInstruction = "Here is a list of 5 containers. Short them in ascending order. Focus on two containers at a time.";
                activeInstruction = "Remember you must cycle through the list without a swap to complete the bubble sort correctly.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 9f);
                StartCoroutine(coroutine);
                Invoke("TurnOffButtonNext", 4f);
                Invoke("TurnOnButtons", 14f);
                break;

            case BubbleSortState.BeginnerBubbleSortTask04:
                uiControls.SetUISize(6);
                trayEndPosition = new Vector3(-1f, transform.localPosition.y, transform.localPosition.z);
                coroutine = TrayTransition(transform.localPosition, trayEndPosition, 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                passiveInstruction = "Here is a list of 6 containers. Short them in ascending order. Focus on two containers at a time.";
                activeInstruction = "Remember you must cycle through the list without a swap to complete the bubble sort correctly.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 9f);
                StartCoroutine(coroutine);
                Invoke("TurnOffButtonNext", 4f);
                Invoke("TurnOnButtons", 14f);
                break;

            case BubbleSortState.BeginnerBubbleSortTask05:
                uiControls.SetUISize(7);
                trayEndPosition = new Vector3(-1.25f, transform.localPosition.y, transform.localPosition.z);
                coroutine = TrayTransition(transform.localPosition, trayEndPosition, 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                passiveInstruction = "Here is a list of 7 containers. Short them in ascending order. Focus on two containers at a time.";
                activeInstruction = "Remember you must cycle through the list without a swap to complete the bubble sort correctly.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 9f);
                StartCoroutine(coroutine);
                Invoke("TurnOffButtonNext", 4f);
                Invoke("TurnOnButtons", 14f);
                break;

            case BubbleSortState.BeginnerBubbleSortTask06:
                uiControls.SetUISize(8);
                trayEndPosition = new Vector3(-1.5f, transform.localPosition.y, transform.localPosition.z);
                coroutine = TrayTransition(transform.localPosition, trayEndPosition, 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(passiveInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, Color.black, blankColor, "", 4f, 0f);
                StartCoroutine(coroutine);
                passiveInstruction = "Here is a list of 8 containers. Short them in ascending order. Focus on two containers at a time.";
                activeInstruction = "Remember you must cycle through the list without a swap to complete the bubble sort correctly.";
                coroutine = TextTransition(passiveInstructionalText, blankColor, Color.black, passiveInstruction, 4f, 5f);
                StartCoroutine(coroutine);
                coroutine = TextTransition(activeInstructionalText, blankColor, Color.black, activeInstruction, 4f, 9f);
                StartCoroutine(coroutine);
                Invoke("TurnOffButtonNext", 4f);
                Invoke("TurnOnButtons", 14f);
                break;

            default:
                ResetTray();
                break;
        }
    }

    private void TurnOnActiveInstruction()
    {
        activeInstructionalText.color = Color.black;
        //later on fade up and down text
        //StartCoroutine("TransitionEffect");
    }

    private IEnumerator TransitionEffect()
    {
        return null;
    }
}

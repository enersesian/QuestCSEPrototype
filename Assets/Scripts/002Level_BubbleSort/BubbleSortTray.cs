using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSortTray : Listener
{
    public Text passiveInstructionalText, activeInstructionalText;
    private Color blankColor = new Color(0f, 0f, 0f, 0f);
    private float start, end, time;
    [SerializeField]
    private ButtonPushable buttonSwap, buttonSubmit, buttonNext;

    private void Awake()
    {
        base.Awake();
    }

    private void ResetTray()
    {
        passiveInstructionalText.text = "";
        passiveInstructionalText.color = Color.black;
        activeInstructionalText.text = "";
        activeInstructionalText.color = Color.black;
        buttonSwap.ResetButton(false);
        buttonSubmit.ResetButton(false);
        buttonNext.ResetButton(false);
    }

    private void TurnOnButtonNext()
    {
        buttonNext.ResetButton(true);
        buttonNext.StartTutorialBlink();
    }

    public override void SetListenerState(BubbleSortState currentState)
    {
        switch (currentState)
        {
            case BubbleSortState.IntroductionToNextButton:
                ResetTray();
                passiveInstructionalText.text = "Welcome to the Bubble Sort trainer. Let's learn to use the controls.";
                activeInstructionalText.color = blankColor;
                activeInstructionalText.text = "Find the blinking button. Its the next button. Press it to continue.";
                Invoke("TurnOnActiveInstruction", 4f);
                Invoke("TurnOnButtonNext", 4f);
                Debug.Log(gameObject.name + " set to " + currentState.ToString());
                break;

            case BubbleSortState.IntroductionToSwapButton:
                ResetTray();
                passiveInstructionalText.text = "In front of you are two containers that each contain an amount of objects.";
                activeInstructionalText.color = blankColor;
                activeInstructionalText.text = "We need to place them into ascending order. Press the blinking button to swap them.";
                Invoke("TurnOnActiveInstruction", 4f);
                break;

            case BubbleSortState.IntroductionToSubmitButton:
                passiveInstructionalText.text = "Notice the numbers inbetween the buttons help you keep track of the order.";
                activeInstructionalText.color = blankColor;
                activeInstructionalText.text = "The list is now in ascending order. Press the blinking button to finish the sort.";
                Invoke("TurnOnActiveInstruction", 4f);
                break;

            case BubbleSortState.IntroductionToThreeElementList01:
                ResetTray();
                passiveInstructionalText.text = "In front of you are now three containers. Lets learn to sort a list of three containers.";
                activeInstructionalText.color = blankColor;
                activeInstructionalText.text = "Focus on two containers at a time. These two are in order and dont need to swap, press next.";
                Invoke("TurnOnActiveInstruction", 4f);
                break;

            case BubbleSortState.IntroductionToThreeElementList02:
                passiveInstructionalText.text = "Notice we moved only one container to the right, and now we examine these two containers.";
                activeInstructionalText.color = blankColor;
                activeInstructionalText.text = "These two containers are not in ascending order. Press swap to place them in correct order.";
                Invoke("TurnOnActiveInstruction", 4f);
                break;

            case BubbleSortState.IntroductionToThreeElementList03:
                passiveInstructionalText.text = "Notice that these two are now in order but the previous two are now not in order.";
                activeInstructionalText.color = blankColor;
                activeInstructionalText.text = "We need to return to the beginning of the list to swap them. Press next to move to beginning.";
                Invoke("TurnOnActiveInstruction", 4f);
                break;

            case BubbleSortState.IntroductionToThreeElementList04:
                passiveInstructionalText.text = "Notice that we may need to cycle through the list to continue swapping to get all in order.";
                activeInstructionalText.color = blankColor;
                activeInstructionalText.text = "Let's swap these two containers to place them into ascending order. Press swap.";
                Invoke("TurnOnActiveInstruction", 4f);
                break;

            case BubbleSortState.IntroductionToThreeElementList05:
                passiveInstructionalText.text = "These two containers are now in order. Notice the entire list is now in ascending order.";
                activeInstructionalText.color = blankColor;
                activeInstructionalText.text = "But we only focus on two containers at a time. Let's press next to continue checking the list.";
                Invoke("TurnOnActiveInstruction", 4f);
                break;
                
            case BubbleSortState.IntroductionToThreeElementList06:
                passiveInstructionalText.text = "These two containers are also in ascending order. There is no need to swap them.";
                activeInstructionalText.color = blankColor;
                activeInstructionalText.text = "Since there is no need to swap, let's press the next button to cycle back to beginning of list.";
                Invoke("TurnOnActiveInstruction", 4f);
                break;

            case BubbleSortState.IntroductionToThreeElementList07:
                passiveInstructionalText.text = "The bubble sort algorithm does not finish until we go through the entire list without swapping.";
                activeInstructionalText.color = blankColor;
                activeInstructionalText.text = "That way we know for sure that the list is sorted. Press next since these two are in order.";
                Invoke("TurnOnActiveInstruction", 4f);
                break;

            case BubbleSortState.IntroductionToThreeElementList08:
                passiveInstructionalText.text = "We are at the end of the list and have not swapped previous elements this cycle through.";
                activeInstructionalText.color = blankColor;
                activeInstructionalText.text = "These two are also in ascending order. Press next to complete the final cycle through the list.";
                Invoke("TurnOnActiveInstruction", 4f);
                break;

            case BubbleSortState.IntroductionToThreeElementList09:
                passiveInstructionalText.text = "Congratulations on learning the basics of a bubble sort. Now let's do a series of exercises.";
                activeInstructionalText.color = blankColor;
                activeInstructionalText.text = "Once you successfully complete these exercises, we will let you perform freeform bubble sort.";
                Invoke("TurnOnActiveInstruction", 4f);
                break;

            case BubbleSortState.BeginnerBubbleSortTask01:
                passiveInstructionalText.text = "Here is a list of 3 containers. Short them in ascending order. Focus on two containers at a time.";
                activeInstructionalText.color = blankColor;
                activeInstructionalText.text = "Remember you must cycle through the list without a swap to complete the bubble sort correctly.";
                Invoke("TurnOnActiveInstruction", 4f);
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

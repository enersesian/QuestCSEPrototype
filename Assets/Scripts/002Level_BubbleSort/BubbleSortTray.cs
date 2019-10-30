using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleSortTray : Listener
{

    public Text passiveInstructionalText, activeInstructionalText;
    private Color blankColor = new Color(0f, 0f, 0f, 0f);

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
    }

    public override void SetListenerState(BubbleSortState currentState)
    {

        switch (currentState)
        {
            case BubbleSortState.Welcome:
                ResetTray();
                passiveInstructionalText.text = "Welcome to the Bubble Sort trainer. Let's learn to use the controls.";
                activeInstructionalText.color = blankColor;
                activeInstructionalText.text = "Find the blinking button. Its the next button. Press it to continue.";
                Invoke("TurnOnActiveInstruction", 4f);
                break;

            default:
                ResetTray();
                break;
        }
    }

    private void TurnOnActiveInstruction()
    {
        StartCoroutine("TransitionEffect");
    }

    private IEnumerator TransitionEffect(float start, float end, float time)
    {
        return null;
    }
}

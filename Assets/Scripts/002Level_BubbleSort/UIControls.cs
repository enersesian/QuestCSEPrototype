using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControls : Listener {

    public Transform buttonSwap, buttonNext;
    public RectTransform rectTranBkgrd, rectTranHighlightLeft, rectTranHighlightRight;
    public Text[] textContainerAmount;
    private IEnumerator coroutine;
    private const float textOffset = 26f, highlightWidth = 50f, 
        textBkgrdWidth = 70f, textBkgrdWidthOffset = 50f, textBkgrdHeight = 70f,
        buttonLocalPositionXOffset = 80f, buttonLocalPositionY = -206f, buttonLocalPositionZ = -12f;

    public override void PopulateContainer(int containerPosition, int containerContents)
    {
        coroutine = TextTransition(textContainerAmount[containerPosition], Color.clear, Color.black, containerContents.ToString(), 6f, 0f);
        StartCoroutine(coroutine);
    }

    public override void DepopulateContainer(int containerPosition)
    {
        coroutine = TextTransition(textContainerAmount[containerPosition], Color.black, Color.clear, "", 4f, 0f);
        StartCoroutine(coroutine);
    }

    //pick up here
    public override void ButtonPushed(string buttonName)
    {
        if (buttonName == "ButtonSwap")
        {
            
        }

        if (buttonName == "ButtonNext")
        {
            
        }
    }

    public void SetUISize(int size)
    {
        switch(size)
        {
            case 0:
            case 1:
            case 2:
            default:
                rectTranBkgrd.sizeDelta = new Vector2(textBkgrdWidth * 2f + textBkgrdWidthOffset, textBkgrdHeight);
                rectTranHighlightLeft.sizeDelta = new Vector2(highlightWidth, textBkgrdHeight);
                rectTranHighlightLeft.localPosition = new Vector3(-textOffset, 0f, 0f);
                rectTranHighlightRight.sizeDelta = new Vector2(highlightWidth, textBkgrdHeight);
                rectTranHighlightRight.localPosition = new Vector3(textOffset, 0f, 0f);
                textContainerAmount[0].rectTransform.localPosition = new Vector3(-textOffset, 0f, 0f);
                textContainerAmount[1].rectTransform.localPosition = new Vector3(textOffset, 0f, 0f);
                buttonSwap.localPosition = new Vector3(-buttonLocalPositionXOffset * 1.5f, buttonLocalPositionY, buttonLocalPositionZ);
                buttonNext.localPosition = new Vector3(buttonLocalPositionXOffset * 1.5f, buttonLocalPositionY, buttonLocalPositionZ);
                foreach (Text textComponent in textContainerAmount) textComponent.text = "";
                break;

        }
    }

    public void SetUITextElement()
    {

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
}

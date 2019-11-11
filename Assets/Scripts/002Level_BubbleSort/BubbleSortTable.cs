using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSortTable : Listener
{
    public GameObject tableModel;
    private IEnumerator coroutine;
    private Vector3 trayEndScale;

    public override void SetListenerState(BubbleSortState currentState)
    {
        switch (currentState)
        {
            case BubbleSortState.IntroductionToNextButton: //size for 2 containers
                trayEndScale = new Vector3(1.3f, tableModel.transform.localScale.y, tableModel.transform.localScale.z);
                coroutine = TableTransition(tableModel.transform.localScale, trayEndScale, 4f, 0f);
                StartCoroutine(coroutine);
                Debug.Log(gameObject.name + " set to " + currentState.ToString());
                break;

            case BubbleSortState.IntroductionToThreeElementList01: //size for 3 containers
                trayEndScale = new Vector3(1.7f, tableModel.transform.localScale.y, tableModel.transform.localScale.z);
                coroutine = TableTransition(tableModel.transform.localScale, trayEndScale, 4f, 0f);
                StartCoroutine(coroutine);
                Debug.Log(gameObject.name + " set to " + currentState.ToString());
                break;

            case BubbleSortState.BeginnerBubbleSortTask01: //size for 3 containers
                trayEndScale = new Vector3(1.7f, tableModel.transform.localScale.y, tableModel.transform.localScale.z);
                coroutine = TableTransition(tableModel.transform.localScale, trayEndScale, 4f, 0f);
                StartCoroutine(coroutine);
                Debug.Log(gameObject.name + " set to " + currentState.ToString());
                break;

            default:
                break;
        }
    }

    private IEnumerator TableTransition(Vector3 startScale, Vector3 endScale, float moveTime = 4f, float waitTime = 0f)
    {
        yield return new WaitForSeconds(waitTime);
        float elapsedTime = 0;
        while (elapsedTime < moveTime)
        {
            tableModel.transform.localScale = Vector3.Lerp(startScale, endScale, (elapsedTime / moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        tableModel.transform.localScale = endScale;
        yield return null;
    }
}

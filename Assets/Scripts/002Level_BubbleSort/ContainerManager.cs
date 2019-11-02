using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : Listener
{
    private GameObject[] containers = new GameObject[8];
    private IEnumerator coroutine;
    private int containerOnLeftSideOfTray;

    private void Awake()
    {
        base.Awake();
        for(int i = 0; i < containers.Length; i++)
        {
            containers[i] = transform.GetChild(i).gameObject;
        }
    }

    public override void ButtonPushed(string buttonName)
    {
        if(buttonName == "ButtonSwap")
        {
            if (containerOnLeftSideOfTray >= 0) StartCoroutine("SwapContainer");
        }
    }

    private void ResetContainers(int neededAmount = 0)
    {
        float yHeightLow = -0.35f, moveTime = 4f;
        

        switch(neededAmount)
        {
            case 0:
                for (int i = 0; i < containers.Length; i++)
                {
                    containers[i].transform.localPosition = Vector3.zero;
                    containers[i].SetActive(false);
                }
                containerOnLeftSideOfTray = -1;
                break;

            case 2:
                containers[0].SetActive(true);
                coroutine = MoveContainer(containers[0], new Vector3(-0.25f, yHeightLow, 0f), new Vector3(-0.25f, 0f, 0f), moveTime);
                StartCoroutine(coroutine);
                containers[1].SetActive(true);
                coroutine = MoveContainer(containers[1], new Vector3(0.25f, yHeightLow, 0f), new Vector3(0.25f, 0f, 0f), moveTime);
                StartCoroutine(coroutine);
                containerOnLeftSideOfTray = 0;
                break;

            case -2:
                coroutine = MoveContainer(containers[0], new Vector3(-0.25f, 0f, 0f), new Vector3(-0.25f, yHeightLow, 0f), moveTime);
                StartCoroutine(coroutine);
                coroutine = MoveContainer(containers[1], new Vector3(0.25f, 0f, 0f), new Vector3(0.25f, yHeightLow, 0f), moveTime);
                StartCoroutine(coroutine);
                Invoke("ResetContainers", moveTime);
                break;

            case 3:
                containers[0].SetActive(true);
                coroutine = MoveContainer(containers[0], new Vector3(-0.5f, yHeightLow, 0f), new Vector3(-0.5f, 0f, 0f), moveTime);
                StartCoroutine(coroutine);
                containers[1].SetActive(true);
                coroutine = MoveContainer(containers[1], new Vector3(0f, yHeightLow, 0f), new Vector3(0f, 0f, 0f), moveTime);
                StartCoroutine(coroutine);
                containers[2].SetActive(true);
                coroutine = MoveContainer(containers[2], new Vector3(0.5f, yHeightLow, 0f), new Vector3(0.5f, 0f, 0f), moveTime);
                StartCoroutine(coroutine);
                containerOnLeftSideOfTray = 0;
                break;

            case -3:
                coroutine = MoveContainer(containers[0], new Vector3(-0.5f, 0f, 0f), new Vector3(-0.5f, yHeightLow, 0f), moveTime);
                StartCoroutine(coroutine);
                coroutine = MoveContainer(containers[1], new Vector3(0f, 0f, 0f), new Vector3(0f, yHeightLow, 0f), moveTime);
                StartCoroutine(coroutine);
                coroutine = MoveContainer(containers[2], new Vector3(0.5f, 0f, 0f), new Vector3(0.5f, yHeightLow, 0f), moveTime);
                StartCoroutine(coroutine);
                Invoke("ResetContainers", moveTime);
                break;

            default:
                break;
        }
    }

    private IEnumerator SwapContainer()
    {
        float elapsedTime = 0, moveTime = 4f;
        GameObject tempGO;
        Debug.Log(containerOnLeftSideOfTray);
        Vector3 leftPosition = containers[containerOnLeftSideOfTray].transform.localPosition;
        Vector3 rightPosition = containers[containerOnLeftSideOfTray + 1].transform.localPosition;
        while (elapsedTime < moveTime)
        {
            
            containers[containerOnLeftSideOfTray].transform.localPosition = Vector3.Slerp(leftPosition, rightPosition, (elapsedTime / moveTime));
            containers[containerOnLeftSideOfTray + 1].transform.localPosition = Vector3.Slerp(rightPosition, leftPosition, (elapsedTime / moveTime));
            containers[containerOnLeftSideOfTray + 1].transform.localPosition = new Vector3(containers[containerOnLeftSideOfTray + 1].transform.localPosition.x, containers[containerOnLeftSideOfTray + 1].transform.localPosition.y, -containers[containerOnLeftSideOfTray + 1].transform.localPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        tempGO = containers[containerOnLeftSideOfTray];
        containers[containerOnLeftSideOfTray] = containers[containerOnLeftSideOfTray + 1];
        containers[containerOnLeftSideOfTray + 1] = tempGO;
        yield return null;
    }

    private IEnumerator MoveContainer(GameObject container, Vector3 startPosition, Vector3 endPosition, float moveTime)
    {
        float elapsedTime = 0;
        while (elapsedTime < moveTime)
        {
            container.transform.localPosition = Vector3.Lerp(startPosition, endPosition, (elapsedTime / moveTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    public override void SetListenerState(BubbleSortState currentState)
    {

        switch (currentState)
        {
            case BubbleSortState.IntroductionToNextButton:
                ResetContainers(0);
                Debug.Log(gameObject.name + " set to " + currentState.ToString());
                break;

            case BubbleSortState.IntroductionToSwapButton:
                ResetContainers(2);
                break;

            case BubbleSortState.IntroductionToFinishingBubbleSort:
                ResetContainers(-2);
                break;

            case BubbleSortState.IntroductionToThreeElementList01:
                ResetContainers(3);
                break;

            case BubbleSortState.IntroductionToThreeElementList09:
                ResetContainers(-3);
                break;

            case BubbleSortState.BeginnerBubbleSortTask01:
                ResetContainers(3);
                break;

            default:
                break;
        }
    }
}

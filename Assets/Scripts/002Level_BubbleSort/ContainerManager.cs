﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : Listener
{
    private GameObject[] containers = new GameObject[8];
    private int[] containerContents = new int[8];
    private IEnumerator coroutine;
    private int containerOnLeftSideOfTray;
    private Vector3 sphereLocation;
    public GameObject sphere;
    private GameObject primitive;
    public Transform rotationCenter;

    private void Awake()
    {
        base.Awake();
        for(int i = 0; i < containers.Length; i++)
        {
            containers[i] = transform.GetChild(i).gameObject;
            containerContents[i] = 0;
        }
    }

    public override void ButtonPushed(string buttonName)
    {
        if(buttonName == "ButtonSwap")
        {
            if (containerOnLeftSideOfTray >= 0) StartCoroutine("SwapContainer");
        }

        if (buttonName == "ButtonNext")
        {
            containerOnLeftSideOfTray++;
            if (containers[containerOnLeftSideOfTray + 1].activeSelf == false) containerOnLeftSideOfTray = 0;
        }
    }

    private void CleanupContainers()
    {
        ResetContainers(0);
    }

    public override void PopulateContainer(int containerPosition, int content) //double data recorded issue
    {
        for (int i = 0; i < containerContents[containerPosition]; i++)
        {
            sphereLocation = new Vector3(Random.Range(containers[containerPosition].transform.position.x - 0.05f, containers[containerPosition].transform.position.x + 0.05f),
                Random.Range(containers[containerPosition].transform.position.y - 0.1f, containers[containerPosition].transform.position.y + 0.1f),
                Random.Range(containers[containerPosition].transform.position.z - 0.05f, containers[containerPosition].transform.position.z + 0.05f));
            primitive = Instantiate(sphere);
            primitive.transform.position = sphereLocation;
            primitive.transform.parent = containers[containerPosition].transform;
        }
    }

    public override void DepopulateContainer(int containerPosition)
    {
        foreach (Transform child in containers[containerPosition].transform) Destroy(child.gameObject);
        containerContents[containerPosition] = 0;
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
                    level.DepopulateContainer(i);
                    containers[i].SetActive(false);
                }
                containerOnLeftSideOfTray = -1;
                break;

            case 2:
                containers[0].SetActive(true);
                level.PopulateContainer(0, containerContents[0]);
                coroutine = MoveContainer(containers[0], new Vector3(-0.25f, yHeightLow, 0f), new Vector3(-0.25f, 0f, 0f), moveTime);
                StartCoroutine(coroutine);
                containers[1].SetActive(true);
                level.PopulateContainer(1, containerContents[1]);
                coroutine = MoveContainer(containers[1], new Vector3(0.25f, yHeightLow, 0f), new Vector3(0.25f, 0f, 0f), moveTime);
                StartCoroutine(coroutine);
                containerOnLeftSideOfTray = 0;
                break;

            case -2:
                coroutine = MoveContainer(containers[0], new Vector3(-0.25f, 0f, 0f), new Vector3(-0.25f, yHeightLow, 0f), moveTime);
                StartCoroutine(coroutine);
                coroutine = MoveContainer(containers[1], new Vector3(0.25f, 0f, 0f), new Vector3(0.25f, yHeightLow, 0f), moveTime);
                StartCoroutine(coroutine);
                Invoke("CleanupContainers", moveTime);
                break;

            case 3:
                containers[0].SetActive(true);
                level.PopulateContainer(0, containerContents[0]);
                coroutine = MoveContainer(containers[0], new Vector3(-0.5f, yHeightLow, 0f), new Vector3(-0.5f, 0f, 0f), moveTime);
                StartCoroutine(coroutine);
                containers[1].SetActive(true);
                level.PopulateContainer(1, containerContents[1]);
                coroutine = MoveContainer(containers[1], new Vector3(0f, yHeightLow, 0f), new Vector3(0f, 0f, 0f), moveTime);
                StartCoroutine(coroutine);
                containers[2].SetActive(true);
                level.PopulateContainer(2, containerContents[2]);
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
                Invoke("CleanupContainers", moveTime);
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
        Vector3 leftPosition = containers[containerOnLeftSideOfTray].transform.position;
        Vector3 rightPosition = containers[containerOnLeftSideOfTray + 1].transform.position;
        //trying to control the normal of the arc that right container slerps across
        rotationCenter.position = (rightPosition + leftPosition) * 0.5f;
        Quaternion startRotation = Quaternion.identity;
        Quaternion endRotation = Quaternion.Euler(0f, -179.9f, 0f);
        containers[containerOnLeftSideOfTray + 1].transform.parent = rotationCenter;

        while (elapsedTime < moveTime)
        {
            containers[containerOnLeftSideOfTray].transform.position = Vector3.Lerp(leftPosition, rightPosition, (elapsedTime / moveTime));
            rotationCenter.rotation = Quaternion.Slerp(startRotation, endRotation, (elapsedTime / moveTime));
            //containers[containerOnLeftSideOfTray + 1].transform.position = Vector3.Slerp(rightRelCenter, leftRelCenter, (elapsedTime / moveTime));
            //containers[containerOnLeftSideOfTray + 1].transform.position += centerPosition;
            //what the hell is this line?
            //containers[containerOnLeftSideOfTray + 1].transform.localPosition = new Vector3(containers[containerOnLeftSideOfTray + 1].transform.localPosition.x, containers[containerOnLeftSideOfTray + 1].transform.localPosition.y, -containers[containerOnLeftSideOfTray + 1].transform.localPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        containers[containerOnLeftSideOfTray].transform.position = rightPosition;
        containers[containerOnLeftSideOfTray + 1].transform.position = leftPosition;
        containers[containerOnLeftSideOfTray + 1].transform.parent = this.transform;
        tempGO = containers[containerOnLeftSideOfTray];
        containers[containerOnLeftSideOfTray] = containers[containerOnLeftSideOfTray + 1];
        containers[containerOnLeftSideOfTray + 1] = tempGO;
        rotationCenter.rotation = Quaternion.identity;
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
                containerContents[0] = (int)Random.Range(5, 9);
                containerContents[1] = (int)Random.Range(1, 4);
                ResetContainers(2);
                break;

            case BubbleSortState.IntroductionToFinishingBubbleSort:
                ResetContainers(-2);
                break;

            case BubbleSortState.IntroductionToThreeElementList01:
                containerContents[0] = (int)Random.Range(4, 6);
                containerContents[1] = (int)Random.Range(7, 9);
                containerContents[2] = (int)Random.Range(1, 3);
                ResetContainers(3);
                break;

            case BubbleSortState.IntroductionToThreeElementList09:
                ResetContainers(-3);
                break;

            case BubbleSortState.BeginnerBubbleSortTask01:
                containerContents[0] = (int)Random.Range(7, 9);
                containerContents[1] = (int)Random.Range(4, 6);
                containerContents[2] = (int)Random.Range(1, 3);
                ResetContainers(3);
                break;

            default:
                break;
        }
    }
}

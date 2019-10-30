using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerManager : Listener
{
    private GameObject[] containers = new GameObject[8];

    private void Awake()
    {
        base.Awake();
        for(int i = 0; i < containers.Length; i++)
        {
            containers[i] = transform.GetChild(i).gameObject;
        }
    }

    private void ResetContainers()
    {
        for (int i = 0; i < containers.Length; i++)
        {
            containers[i].transform.localPosition = Vector3.zero;
            containers[i].SetActive(false);
        }
    }

    public override void SetListenerState(BubbleSortState currentState)
    {

        switch (currentState)
        {
            case BubbleSortState.Welcome:
                ResetContainers();
                break;

            case BubbleSortState.Task01:
                ResetContainers();
                containers[0].transform.localPosition = new Vector3(-0.25f, 0f, 0f);
                containers[0].SetActive(true);
                containers[1].transform.localPosition = new Vector3(0.25f, 0f, 0f);
                containers[1].SetActive(true);
                break;

            default:
                ResetContainers();
                break;
        }
    }
}

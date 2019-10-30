﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSortTable : Listener
{

    public GameObject tableModel;

    public override void SetListenerState(BubbleSortState currentState)
    {
        switch (currentState)
        {
            case BubbleSortState.Welcome:
                tableModel.transform.localScale = new Vector3(1.3f, tableModel.transform.localScale.y, tableModel.transform.localScale.y);
                break;

            default:
                break;
        }
    }
}
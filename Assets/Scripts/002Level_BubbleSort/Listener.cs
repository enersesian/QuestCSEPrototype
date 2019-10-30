﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Listener : MonoBehaviour
{
    static protected BubbleSortLevel level;

    public virtual void SetListenerState(BubbleSortState currentState) { }

    protected void Awake()
    {
        if(level == null) level = GameObject.FindGameObjectWithTag("GameController").GetComponent<BubbleSortLevel>();
        level.RegisterListener(this);
    }

}

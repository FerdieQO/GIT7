﻿using UnityEngine;
using System.Collections;
using System;

public class ActionCard : CardController
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void NotifyDropped()
    {
        base.NotifyDropped();
    }

    public void SetCard(Sprite action)
    {
        base.SetCard(action, CardType.Action);
    }
}

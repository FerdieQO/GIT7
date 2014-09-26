using UnityEngine;
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
        GameObject grid = GameObject.FindGameObjectWithTag("Grid");
        _spriteRenderer.enabled = false;
        transform.SetParent(grid.transform);

        // If first
        //transform.SetAsFirstSibling();
        // If last
        transform.SetAsLastSibling();
        // Otherwise
        //int index = 1;
        //grid.transform.SetSiblingIndex(index);
        _canDrag = false;
    }

    public override CardType GetCardType()
    {
        return CardType.Action;
    }
}

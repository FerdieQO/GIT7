using UnityEngine;
using System.Collections;
using System;

public class EmotionCard : CardController
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
        // Link this emotion to an Action.
        // Or destroy if not above an Action.
        Destroy(gameObject);
    }

    public void SetCard(Sprite emotion)
    {
        base.SetCard(emotion, CardType.Emotion);
    }
}

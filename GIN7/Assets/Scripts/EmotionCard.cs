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

    public void SetCard(Sprite emotion)
    {
        base.SetCard(emotion, CardType.Emotion);
    }
}

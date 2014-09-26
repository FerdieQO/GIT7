using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class TimelineController : MonoBehaviour
{
    private List<GameObject> _cards;
    private Vector3 _position;
    public const int OFFSET_INCREMENT = 10;

    // Use this for initialization
    void Start()
    {
        _cards = new List<GameObject>();
        _position = this.transform.position;

        //PlaceCards();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp("space"))
        {
            FindCards();
            PlaceCards();
        }
    }

    // Finds all GameObjects in the scene with the tag "Card" and store them in a List
    void FindCards()
    {
        _cards.Clear();

        foreach (GameObject card in GameObject.FindGameObjectsWithTag("ToBeSorted"))
        {
            _cards.Add(card);
        }
    }

    // Places all cards from the card list in a horizontal timeline
    void PlaceCards()
    {
        int offset = 0;

        foreach (GameObject card in _cards)
        {
            //card.transform.position = Vector3.MoveTowards(card.transform.position,
            //    new Vector3(_transform.position.x + offset, 0, 0), Time.deltaTime*CARD_MOVEMENT_SPEED);
            var target = new Vector3(_position.x + offset, _position.y, _position.z);
            CardController cc = card.GetComponent<CardController>();
            cc.SetMovement(target);
            card.transform.parent = this.gameObject.transform;
            offset += OFFSET_INCREMENT;
        }
    }
}

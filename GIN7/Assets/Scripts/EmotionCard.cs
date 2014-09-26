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
        // Get the position to check if it's colliding,
        // if the position of the mouse has passed the borders of the screen the coördinates get clamped to the edges.
        Vector3 mousePos = Input.mousePosition;
        Vector3 clampedPos = new Vector3(
            Mathf.Clamp(mousePos.x, 0, Screen.width),
            Mathf.Clamp(mousePos.y, 0, Screen.height),
            mousePos.z);
        mousePos.z = -10;
        Ray mouseRay = Camera.main.ScreenPointToRay(clampedPos);
        RaycastHit2D[] hits = Physics2D.GetRayIntersectionAll(mouseRay, Camera.main.farClipPlane);
        if (hits.Length > 0)
        {
            foreach (var hit in hits)
            {
                print(hit.collider.gameObject.name);
                var go = hit.collider.gameObject;

                if (!go.CompareTag("Card"))
                {
                    continue;
                }
                var cc = go.GetComponent<CardController>();
                if (cc.GetCardType() != CardType.Action || cc.IsMenuItem())
                {
                    continue;
                }
                //YES!
                // Add to action :D
                transform.SetParent(cc.transform);
                print("Nope: Still needs to be done");
                return;
            }
        }
        // Or destroy.
        Destroy(gameObject);
    }

    public override CardType GetCardType()
    {
        return CardType.Emotion;
    }
}

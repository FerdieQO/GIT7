using System;
using UnityEngine;

public enum CardType
{
    Action,
    Emotion,
    None
}

public class CardController : MonoBehaviour
{

    private SpriteRenderer _spriteRenderer;

    private Sprite _sprite;
    private CardType _cardType;

    private bool isMoving = false;
    private Vector3 target;
    private Vector3 cardVelocity = Vector3.zero;

    void Awake()
    {
        SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
        foreach (var rend in renderers)
        {
            if (rend.gameObject.name.Equals("Front"))
            {
                _spriteRenderer = rend;
            }
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (isMoving)
        {
            this.transform.position = Vector3.SmoothDamp(this.transform.position, target, ref cardVelocity, .1f);
            if (Vector3.Distance(this.transform.position, target) < .25f)
            {
                this.transform.position = target;
                isMoving = false;
            }
        }
    }

    public void SetMovement(Vector3 target)
    {
        this.target = target;
        isMoving = true;
    }

    public virtual void SetCard(Sprite front, CardType type)
    {
        _cardType = type;
        _spriteRenderer.sprite = _sprite = front;
    }

    public Sprite GetCard()
    {
        return _sprite;
    }

    public CardType GetCardType()
    {
        return _cardType;
    }
}

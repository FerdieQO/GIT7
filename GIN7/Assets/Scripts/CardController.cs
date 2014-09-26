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

    private bool _canDrag = true;
    private bool _isDragging = false;
    private const float _maxSpeed = .5f;

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
        if (_isDragging && !isMoving)
        {
            Drag(Time.deltaTime);
        }
        if (isMoving && !_isDragging)
        {
            Move();
        }
    }

    private void OnMouseDown()
    {
        if (!_canDrag)
        {
            return;
        }
        if (gameObject.layer != LayerMask.NameToLayer("UI"))
        {
            StartDrag();
        }
        else if (GameController.GetGameController().ChangeButtonCard(this))
        {
            StartDrag();
        }
    }

    private void OnMouseUp()
    {
        if (_isDragging)
        {
            StopDrag();
        }
    }

    private float _dragZ;
    void StartDrag()
    {
        if (!_isDragging)
        {
            _dragZ = transform.position.z;
            Vector3 pos = transform.position;
            pos.z = -1;
            transform.position = pos;
            _isDragging = true;
        }
    }

    void StopDrag()
    {
        if (_isDragging)
        {
            _isDragging = false;
            Vector3 pos = transform.position;
            pos.z = _dragZ;
            transform.position = pos;
            NotifyDropped();
        }
    }

    protected virtual void NotifyDropped()
    {
        
    }

    void Drag(float deltaTime)
    {
        // Get the object's currentMat position and the destination.
        Vector3 currPos = transform.position;   // WorldPoint
        Vector3 mousePos = Input.mousePosition; // ScreenPoint
        // If the mouse position has passed the borders of the screen the coördinates get clamped to the edges.
        var clampedPos = new Vector3(
            Mathf.Clamp(mousePos.x, 0, Screen.width),
            Mathf.Clamp(mousePos.y, 0, Screen.height),
            mousePos.z);
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(clampedPos);
        targetPos.z = -1;
        float step = _maxSpeed * 1000 * deltaTime;
        transform.position = Vector3.MoveTowards(currPos, targetPos, step);
    }

    void Move()
    {
        this.transform.position = Vector3.SmoothDamp(this.transform.position, target, ref cardVelocity, .1f);
        if (Vector3.Distance(this.transform.position, target) < .25f)
        {
            this.transform.position = target;
            isMoving = false;
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

using System;
using UnityEngine;
using UnityEngine.UI;

public enum CardType
{
    Action,
    Emotion,
    None
}

public class CardController : MonoBehaviour
{
    private Image _image;
    protected SpriteRenderer _spriteRenderer;

    private Sprite _sprite;

    private bool isMoving = false;
    private Vector3 target;
    private Vector3 cardVelocity = Vector3.zero;

    protected bool _canDrag = true;
    private bool _isDragging = false;
    private const float _maxSpeed = .5f;
    protected const float _timeToHold = 2f;
    protected float _currHoldTime = 0f;

    private bool _menuItem;

    void Awake()
    {
        SpriteRenderer[] renderers = gameObject.GetComponentsInChildren<SpriteRenderer>(true);
        _image = gameObject.GetComponent<Image>();
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
        if (!_menuItem)
        {
            StartDrag();
        }
        else if (GameController.GetGameController().ChangeButtonCard(this))
        {
            StartDrag();
        }
    }

    private void OnMouseDrag()
    {
        if (!_canDrag)
        {
            if (_currHoldTime + Time.deltaTime >= _timeToHold)
            {
                _canDrag = true;
                print("Draggable");
            }
            else
            {
                _currHoldTime += Time.deltaTime;
            }
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
            if (transform.IsChildOf(GameObject.FindGameObjectWithTag("Grid").transform))
            {
                transform.parent = null;
            }

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
            NotifyDropped();
            transform.position = pos;
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

    public void SetMenuItem(bool isMenuItem)
    {
        _menuItem = isMenuItem;
    }

    public bool IsMenuItem()
    {
        return _menuItem;
    }

    public virtual void SetCard(Sprite front)
    {
         
        _spriteRenderer.sprite = _image.sprite = _sprite = front;
    }

    public Sprite GetCard()
    {
        return _sprite;
    }

    public virtual CardType GetCardType()
    {
        return CardType.None;
    }
}

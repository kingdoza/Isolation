using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using static ControllerUtils;

public class DraggableItem : Item
{
    [SerializeField] protected Transform leftEnd;
    [SerializeField] protected Transform rightEnd;

    public static bool isDragging = false;
    private float offsetX;
    private float fixedY;
    private Camera mainCamera;

    private bool hasReachedLeft = false;
    private bool hasReachedRight = false;
    private Collider2D[] otherColliders;

    private bool isMouseOn = false;




    protected override void Start()
    {
        base.Start();
        Transform currentView = GameManager.Instance.RoomController.CurrentView.transform;
        if (leftEnd == null)
        {
            leftEnd = new GameObject(ItemName + "LeftEnd").transform;
            leftEnd.SetParent(currentView);
            leftEnd.position = transform.position;
        }
        if (rightEnd == null)
        {
            rightEnd = new GameObject(ItemName + "RightEnd").transform;
            rightEnd.SetParent(currentView);
            rightEnd.position = transform.position;
        }

        mouseHoverComp = GetComponent<MouseHover>();
        RegisterDragScrollCondition(() => !isMouseOn);
        RegisterDragScrollCondition(() => !isDragging);
        mainCamera = Camera.main;
        fixedY = transform.position.y;

        otherColliders = GameManager.Instance.RoomController.CurrentView
            .GetComponentsInChildren<Collider2D>(true) // true면 비활성화 포함
            .Where(c => !GetComponents<Collider2D>().Contains(c))
            .ToArray();
    }



    private void OnMouseDown()
    {
        isDragging = true;
        mouseHoverComp.enabled = false;
        SetCursorTexture(CursorTextures.Click);
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        offsetX = transform.position.x - mouseWorldPos.x;
    }



    private void OnMouseUp()
    {
        isDragging = false;
        Array.ForEach(otherColliders, col => col.enabled = true);
        mouseHoverComp.enabled = true;
        if (isMouseOn == false)
        {
            SetCursorTexture(CursorTextures.Normal);
            ChangeAllSubSpritesColor(originalColor);
        }
    }



    protected override void Update()
    {
        if (isDragging)
        {
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            float targetX = mouseWorldPos.x + offsetX;

            // Clamp within range
            float clampedX = Mathf.Clamp(targetX, leftEnd.position.x, rightEnd.position.x);
            transform.position = new Vector3(clampedX, fixedY, transform.position.z);

            // 도달 체크
            bool isAtLeft = Mathf.Approximately(clampedX, leftEnd.position.x);
            bool isAtRight = Mathf.Approximately(clampedX, rightEnd.position.x);

            if (isAtLeft && !hasReachedLeft)
            {
                hasReachedLeft = true;
                hasReachedRight = false;
                ReachedLeftEnd();
            }
            else if (isAtRight && !hasReachedRight)
            {
                hasReachedRight = true;
                hasReachedLeft = false;
                ReachedRightEnd();
            }
            else if (!isAtLeft && !isAtRight)
            {
                hasReachedLeft = false;
                hasReachedRight = false;
            }
        }
    }



    protected virtual void ReachedRightEnd()
    {
        
    }



    protected virtual void ReachedLeftEnd()
    {
        
    }



    public override void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOn = true;
        if (CanInteract)
        {
            Array.ForEach(otherColliders, col => col.enabled = false);
            ChangeAllSubSpritesColor(Color.gray);
        }
    }



    public override void OnPointerExit(PointerEventData eventData)
    {
        isMouseOn = false;
        if (CanInteract && !isDragging)
        {
            Array.ForEach(otherColliders, col => col.enabled = true);
            ChangeAllSubSpritesColor(originalColor);
        }
    }
}

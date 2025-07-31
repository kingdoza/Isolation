using UnityEngine;
using UnityEngine.EventSystems;
using static ControllerUtils;

public class DraggableItem : Item
{
    [SerializeField] protected Transform leftEnd;
    [SerializeField] protected Transform rightEnd;

    private bool isDragging = false;
    private float offsetX;
    private float fixedY;
    private Camera mainCamera;

    private bool hasReachedLeft = false;
    private bool hasReachedRight = false;

    private bool isMouseOn = false;




    protected override void Start()
    {
        base.Start();
        if(leftEnd == null)
        {
            leftEnd = new GameObject(ItemName + "LeftEnd").transform;
            leftEnd.position = transform.position;
        }
        if (rightEnd == null)
        {
            rightEnd = new GameObject(ItemName + "RightEnd").transform;
            rightEnd.position = transform.position;
        }

        RegisterDragScrollCondition(() => !isDragging);
        mainCamera = Camera.main;
        fixedY = transform.position.y;
    }



    private void OnMouseDown()
    {
        isDragging = true;
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        offsetX = transform.position.x - mouseWorldPos.x;
    }



    private void OnMouseUp()
    {
        isDragging = false;
        if (isMouseOn == false)
            ChangeAllSubSpritesColor(originalColor);
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
            ChangeAllSubSpritesColor(Color.gray);
        }
    }



    public override void OnPointerExit(PointerEventData eventData)
    {
        isMouseOn = false;
        if (CanInteract && !isDragging)
        {
            ChangeAllSubSpritesColor(originalColor);
        }
    }
}

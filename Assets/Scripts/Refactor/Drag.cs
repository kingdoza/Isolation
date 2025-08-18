using UnityEngine;
using UnityEngine.Events;
using static EtcUtils;

public class Drag : MouseInteraction
{
    [SerializeField] protected DragDirection dragDirection;
    protected override string InputLayerName => "Draggable";
    public override bool IsPassDown => false;
    protected Vector2 offset;
    protected float dragDistance = 0;
    [HideInInspector] public UnityEvent DragEndEvent = new();
    [HideInInspector] public UnityEvent DragStartEvent = new();
    [HideInInspector] public UnityEvent DragStopEvent = new();
    [HideInInspector] public UnityEvent DragResumeEvent = new();
    protected bool isStop = true;



    protected override void Awake()
    {
        base.Awake();
    }



    protected virtual Vector3 CalculateNewPosition()
    {
        Vector3 newPosition = (Vector2)GetMouseWorldPosition() + offset;
        if (dragDirection == DragDirection.Vertical)
        {
            newPosition.x = transform.position.x;
        }
        else if (dragDirection == DragDirection.Horizontal)
        {
            newPosition.y = transform.position.y;
        }
        return newPosition;
    }



    public override void OnInteractStart()
    {
        offset = transform.position - GetMouseWorldPosition();
        dragDistance = 0;
        DragStartEvent?.Invoke();
    }



    public override void OnInteracting()
    {
        Vector3 newPosition = CalculateNewPosition();
        float moveDistance = Vector2.Distance(transform.position, newPosition);
        //WatchDragStopResume(newPosition);
        dragDistance += moveDistance;
        transform.position = newPosition;
    }



    protected virtual void WatchDragStopResume(Vector3 targetPosition)
    {
        //float moveDistance = Vector2.Distance(targetPosition, transform.position);
        //Debug.Log(moveDistance);
        //if (moveDistance <= 0f)
        if (targetPosition == transform.position)
        {
            if (isStop == false)
            {
                isStop = true;
                Debug.Log("DragStopEvent");
                DragStopEvent?.Invoke();
            }
        }
        else
        {
            if (isStop)
            {
                isStop = false;
                Debug.Log("DragResumeEvent");
                DragResumeEvent?.Invoke();
            }
        }
    }



    public override void OnInteractEnd()
    {
        dragDistance = 0;
        DragEndEvent?.Invoke();
    }



    public override bool HasInteracted()
    {
        return dragDistance > Mathf.Epsilon;
    }
}



public enum DragDirection
{
    Vertical, Horizontal, Plane
}

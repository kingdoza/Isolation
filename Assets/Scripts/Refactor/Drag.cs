using UnityEngine;
using UnityEngine.Events;
using static EtcUtils;

public class Drag : MouseInteraction
{
    [SerializeField] private DragDirection dragDirection;
    protected override string InputLayerName => "Draggable";
    public override bool IsPassDown => false;
    protected Vector2 offset;
    protected float dragDistance = 0;



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
    }



    public override void OnInteracting()
    {
        Vector3 newPosition = CalculateNewPosition();
        dragDistance += Vector2.Distance(transform.position, newPosition);
        transform.position = newPosition;
    }



    public override void OnInteractEnd()
    {
        dragDistance = 0;
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

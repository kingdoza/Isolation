using UnityEngine;
using static EtcUtils;

public class Drag : MouseInteraction
{
    protected override string InputLayerName => "Draggable";
    public override bool IsPassDown => false;
    private Vector2 offset;
    private float dragDistance = 0;



    protected override void Awake()
    {
        base.Awake();
    }



    public override void OnInteractStart()
    {
        offset = transform.position - GetMouseWorldPosition();
        dragDistance = 0;
    }



    public override void OnInteracting()
    {
        Vector3 newPosition = (Vector2)GetMouseWorldPosition() + offset;
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

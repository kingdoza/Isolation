using UnityEngine;
using static EtcUtils;

public class LimitedLineDrag : LimitedDrag
{
    [SerializeField] private DragDirection dragDir;



    enum DragDirection
    {
        Vertical, Horizontal
    }



    public override void OnInteracting()
    {
        Vector3 newPosition = (Vector2)GetMouseWorldPosition() + offset;

        float clampedX = transform.position.x;
        float clampedY = transform.position.y;
        if (dragDir == DragDirection.Vertical)
        {
            clampedY = Mathf.Clamp(newPosition.y, lowerLimitPos.y, higherLimitPos.y);
        }
        else if (dragDir == DragDirection.Horizontal)
        {
            clampedX = Mathf.Clamp(newPosition.x, lowerLimitPos.x, higherLimitPos.x);
        }

        Vector3 clampedPosition = new Vector3(clampedX, clampedY, transform.position.z);
        dragDistance += Vector2.Distance(transform.position, clampedPosition);
        transform.position = clampedPosition;
    }
}
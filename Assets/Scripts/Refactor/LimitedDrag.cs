using DG.Tweening;
using UnityEngine;
using static EtcUtils;

public class LimitedDrag : Drag
{
    [SerializeField] private Transform limitMarker1;
    [SerializeField] private Transform limitMarker2;
    protected Vector2 lowerLimitPos;
    protected Vector2 higherLimitPos;



    protected override void Awake()
    {
        base.Awake();

        if (limitMarker1 == null || limitMarker2 == null)
        {
            Debug.LogError(name + "'s limit marker is invalid");
        }

        float lowerX = Mathf.Min(limitMarker1.position.x, limitMarker2.position.x);
        float lowerY = Mathf.Min(limitMarker1.position.y, limitMarker2.position.y);
        float higherX = Mathf.Max(limitMarker1.position.x, limitMarker2.position.x);
        float higherY = Mathf.Max(limitMarker1.position.y, limitMarker2.position.y);

        if (dragDirection == DragDirection.Vertical)
        {
            lowerX = transform.position.x;
            higherX = transform.position.x;
        }
        else if (dragDirection == DragDirection.Horizontal)
        {
            lowerY = transform.position.y;
            higherY = transform.position.y;
        }

        lowerLimitPos = new Vector2(lowerX, lowerY);
        higherLimitPos = new Vector2(higherX, higherY);

        if (lowerLimitPos.x > higherLimitPos.x ||
            lowerLimitPos.y > higherLimitPos.y)
        {
            Debug.LogError(name + "'s limit range is invalid");
        }
    }



    public override void OnInteracting()
    {
        Vector3 newPosition = CalculateNewPosition();
        float clampedX = Mathf.Clamp(newPosition.x, lowerLimitPos.x, higherLimitPos.x);
        float clampedY = Mathf.Clamp(newPosition.y, lowerLimitPos.y, higherLimitPos.y);
        Vector3 clampedPosition = new Vector3(clampedX, clampedY, transform.position.z);
        dragDistance += Vector2.Distance(transform.position, clampedPosition);
        //WatchDragStopResume(clampedPosition);
        transform.position = clampedPosition;
    }
}

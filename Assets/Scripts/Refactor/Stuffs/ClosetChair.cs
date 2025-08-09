using System;
using UnityEngine;
using static EtcUtils;
using static Extensions;

[RequireComponent(typeof(LimitedDrag))]
[RequireComponent(typeof(CursorHover))]
public class ClosetChair : DraggableStuff, ITriggerEventSendable
{
    private ColliderDetector colliderDectectComp;
    private Collider2D detectorColliderComp;
    public event Action TriggerChangeAction;
    private bool isReachedRight = false;



    protected override void Awake()
    {
        base.Awake();
        colliderDectectComp = GetComponentInChildren<ColliderDetector>();
        detectorColliderComp = GetComponentInChildren<Collider2D>();
        TriggerEventController.Instance.ClosetChairTrigger = this;
        if (colliderDectectComp == null || detectorColliderComp == null)
        {
            PrintErrorLog(gameObject, Error.NullComp);
        }
        colliderDectectComp.TriggerEnterEvent.AddListener(Triggered);
        colliderDectectComp.TriggerExitEvent.AddListener(Untriggered);
    }



    public bool GetTriggerValue()
    {
        return isReachedRight;
    }



    private void Triggered(Collider2D collision)
    {
        isReachedRight = true;
        TriggerChangeAction?.Invoke();
        Debug.Log("Triggered");
    }


    private void Untriggered(Collider2D collision)
    {
        isReachedRight = false;
        TriggerChangeAction?.Invoke();
        Debug.Log("Untriggered");
    }



    public class Info
    {
        public bool isReachedRight = false;
    }
}

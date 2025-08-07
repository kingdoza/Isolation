using UnityEngine;
using static EtcUtils;

[RequireComponent(typeof(LimitedDrag))]
[RequireComponent(typeof(CursorHover))]
public class TriggerDragStuff : DraggableStuff
{
    private ColliderDetector triggerDectectComp;
    private Collider2D triggerColliderComp;



    protected override void Awake()
    {
        base.Awake();
        triggerDectectComp = GetComponentInChildren<ColliderDetector>();
        triggerColliderComp = GetComponentInChildren<Collider2D>();
        if (triggerDectectComp == null || triggerColliderComp == null)
        {
            PrintErrorLog(gameObject, Error.NullComp);
        }

        triggerDectectComp.TriggerEnterEvent.AddListener(Triggered);
        triggerDectectComp.TriggerExitEvent.AddListener(Untriggered);
    }



    private void Triggered(Collider2D collision)
    {
        Debug.Log("Triggered");
    }


    private void Untriggered(Collider2D collision)
    {
        Debug.Log("Untriggered");
    }
}

using UnityEngine;

[RequireComponent(typeof(InactiveStuff))]
[RequireComponent(typeof(ColliderDetector))]
public class Hidden : ConditionalActivator
{
    private InactiveStuff inactiveStuff;
    private ClickableStuff clickableStuff;
    private ColliderDetector ColliderDetectorComp;
    private int overlayCount = 0;



    protected virtual void Awake()
    {
        ColliderDetectorComp = GetComponent<ColliderDetector>();
        clickableStuff = GetComponent<ClickableStuff>();
        inactiveStuff = GetComponent<InactiveStuff>();
        ColliderDetectorComp.TriggerEnterEvent.AddListener(Triggered);
        ColliderDetectorComp.TriggerExitEvent.AddListener(Untriggered);
    }



    private void Triggered(Collider2D collision)
    {
        ++overlayCount;
        SetConditionalComponent();
    }


    private void Untriggered(Collider2D collision)
    {
        --overlayCount;
        SetConditionalComponent();
    }



    protected override void SetConditionalComponent()
    {
        if (overlayCount > 0)
        {
            clickableStuff.enabled = false;
            inactiveStuff.enabled = true;
        }
        else
        {
            inactiveStuff.enabled = false;
            clickableStuff.enabled = true;
        }
    }
}

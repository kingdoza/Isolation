using UnityEngine;

public class ClosetTop : ConditionalActivator
{
    private TriggerEvent playerWakeTrigger = new();
    private TriggerEvent closetChairTrigger = new();
    [SerializeField] private ZoomStuff zoomStuff;
    [SerializeField] private FocusStuff focusStuff;
    [SerializeField] private InactiveStuff inactiveStuff;



    protected void Awake()
    {
        playerWakeTrigger.SetTargetSender(GlobalTriggerEvent.PlayerWake);
        closetChairTrigger.SetTargetSender(GlobalTriggerEvent.ClosetChair);
        playerWakeTrigger.TargetSender.TriggerChangeAction += OnTriggerStatusChanged;
        closetChairTrigger.TargetSender.TriggerChangeAction += OnTriggerStatusChanged;
    }



    protected override void SetConditionalComponent()
    {
        if (playerWakeTrigger.TargetSender.GetTriggerValue())
        {
            if (closetChairTrigger.TargetSender.GetTriggerValue())
            {
                zoomStuff.enabled = true;
                focusStuff.enabled = false;
                inactiveStuff.enabled = false;
            }
            else
            {
                zoomStuff.enabled = false;
                focusStuff.enabled = false;
                inactiveStuff.enabled = true;
            }
        }
        else
        {
            zoomStuff.enabled = false;
            focusStuff.enabled = true;
            inactiveStuff.enabled = false;
        }
    }
}

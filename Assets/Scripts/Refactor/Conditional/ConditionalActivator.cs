using NUnit.Framework.Constraints;
using System;
using UnityEngine;

public abstract class ConditionalActivator : MonoBehaviour
{
    private void OnEnable()
    {
        SetConditionalComponent();
    }



    protected virtual void OnTriggerStatusChanged()
    {
        SetConditionalComponent();
    }



    protected abstract void SetConditionalComponent();
}



[Serializable]
public class TriggerEvent
{
    [SerializeField] private GlobalTriggerEvent globalType;
    [SerializeField] private MonoBehaviour targetComponent;
    private ITriggerEventSendable targetSender;

    public ITriggerEventSendable TargetSender => targetSender;



    public void SetTargetSender()
    {
        switch (globalType)
        {
            case GlobalTriggerEvent.None:
                targetSender = targetComponent as ITriggerEventSendable;
                break;
            case GlobalTriggerEvent.PlayerWake:
                targetSender = TriggerEventController.Instance.PlayerWakeTrigger;
                break;
            case GlobalTriggerEvent.ClosetChair:
                targetSender = TriggerEventController.Instance.ClosetChairTrigger;
                break;
            default:
                break;
        }
    }



    public void SetTargetSender(GlobalTriggerEvent globalType)
    {
        this.globalType = globalType;
        SetTargetSender();
    }
}

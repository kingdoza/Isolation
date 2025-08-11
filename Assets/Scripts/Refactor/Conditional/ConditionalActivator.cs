using NUnit.Framework;
using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.EventSystems.EventTrigger;

public abstract class ConditionalActivator : MonoBehaviour
{
    private UnityEvent DestroyEvent = new();

    private void OnEnable()
    {
        SetConditionalComponent();
    }



    private void OnDestroy()
    {
        DestroyEvent?.Invoke();
        DestroyEvent?.RemoveAllListeners();
    }



    protected virtual void OnTriggerStatusChanged()
    {
        SetConditionalComponent();
    }



    protected virtual void InitTrigger(TriggerEvent triggerEvent, GlobalTriggerEvent globalType)
    {
        //triggerEvent = new(globalType);
        triggerEvent.SetTargetSender(globalType);
        triggerEvent.TargetSender.TriggerChangeAction += OnTriggerStatusChanged;
        DestroyEvent.AddListener(() => triggerEvent.TargetSender.TriggerChangeAction -= OnTriggerStatusChanged);
    }



    protected virtual void InitTrigger(TriggerEvent triggerEvent)
    {
        triggerEvent.SetTargetSender();
        triggerEvent.TargetSender.TriggerChangeAction += OnTriggerStatusChanged;
        DestroyEvent.AddListener(() => triggerEvent.TargetSender.TriggerChangeAction -= OnTriggerStatusChanged);
    }



    protected virtual void InitTrigger(ITriggerEventSendable triggerEvent)
    {
        triggerEvent.TriggerChangeAction += OnTriggerStatusChanged;
        DestroyEvent.AddListener(() => triggerEvent.TriggerChangeAction -= OnTriggerStatusChanged);
    }



    protected abstract void SetConditionalComponent();
}



[Serializable]
public class TriggerEvent
{
    [SerializeField] protected GlobalTriggerEvent globalType;
    [SerializeField] protected MonoBehaviour targetComponent;
    protected ITriggerEventSendable targetSender;

    public ITriggerEventSendable TargetSender => targetSender;



    public TriggerEvent(GlobalTriggerEvent globalType)
    {
        SetTargetSender(globalType);
    }



    public void SetTargetSender()
    {
        switch (globalType)
        {
            case GlobalTriggerEvent.None:
                targetSender = targetComponent as ITriggerEventSendable;
                break;
            case GlobalTriggerEvent.PlayerWakeup:
                targetSender = TriggerEventController.Instance.PlayerWakeup;
                break;
            case GlobalTriggerEvent.ChairRightReach:
                targetSender = TriggerEventController.Instance.ChairRightReach;
                break;
            case GlobalTriggerEvent.FirstScrewsLoose:
                targetSender = TriggerEventController.Instance.FirstScrewsLoose;
                break;
            case GlobalTriggerEvent.FramePhotoFlip:
                targetSender = TriggerEventController.Instance.FramePhotoFlip;
                break;
            case GlobalTriggerEvent.DrawerStick:
                targetSender = TriggerEventController.Instance.DrawerStick;
                break;
            default:
                Debug.LogError("Invalid GlobalType");
                break;
        }
    }



    public void SetTargetSender(GlobalTriggerEvent globalType)
    {
        this.globalType = globalType;
        SetTargetSender();
    }



    public bool GetValue()
    {
        if (targetSender == null)
            SetTargetSender();
        return targetSender.GetTriggerValue();
    }
}



public static class TriggerEventExtension
{
    public static bool GetValue(this TriggerEvent[] triggerEvents, EvaluateType evaluateType)
    {
        switch (evaluateType)
        {
            case EvaluateType.OR:
                return triggerEvents.EvaluateOr();
            case EvaluateType.AND:
                return triggerEvents.EvaluateAnd();
        }
        return false;
    }



    public static bool GetValue(this List<TriggerEvent> triggerEvents, EvaluateType evaluateType)
    {
        switch (evaluateType)
        {
            case EvaluateType.OR:
                return triggerEvents.ToArray().EvaluateOr();
            case EvaluateType.AND:
                return triggerEvents.ToArray().EvaluateAnd();
        }
        return false;
    }



    private static bool EvaluateOr(this TriggerEvent[] triggerEvents)
    {
        bool evaluation = false;
        foreach (TriggerEvent triggerEvent in triggerEvents)
        {
            evaluation = evaluation || triggerEvent.GetValue();
        }
        return evaluation;
    }



    private static bool EvaluateAnd(this TriggerEvent[] triggerEvents)
    {
        bool evaluation = true;
        foreach (TriggerEvent triggerEvent in triggerEvents)
        {
            evaluation = evaluation && triggerEvent.GetValue();
        }
        return evaluation;
    }
}



public enum EvaluateType
{
    OR, AND
}
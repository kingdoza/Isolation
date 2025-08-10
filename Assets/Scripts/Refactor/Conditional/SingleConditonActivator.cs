using UnityEditor.Tilemaps;
using UnityEngine;

public abstract class SingleConditonActivator : ConditionalActivator
{
    [SerializeField] private TriggerEvent triggerEvent;



    protected virtual void Awake()
    {
        InitTrigger(triggerEvent);
    }



    protected override void SetConditionalComponent()
    {
        if (triggerEvent.TargetSender.GetTriggerValue())
        {
            SetTrueComponent();
        }
        else
        {
            SetFalseComponent();
        }
    }



    protected abstract void SetTrueComponent();
    protected abstract void SetFalseComponent();
}

using UnityEngine;

public abstract class SingleConditonActivator : ConditionalActivator
{
    [SerializeField] protected TriggerEvent triggerEvent;



    protected virtual void Awake()
    {
        InitTrigger(triggerEvent);
    }



    protected override void SetConditionalComponent()
    {
        if (name.Equals("KakaoTalk"))
            Debug.Log("KakaoTalk : " + triggerEvent.GetValue());
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

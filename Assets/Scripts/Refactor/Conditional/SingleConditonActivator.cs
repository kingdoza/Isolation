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
        //if (name.Equals("KakaoTalk"))
        //    Debug.Log("KakaoTalk : " + triggerEvent.GetValue());
        if (triggerEvent.TargetSender == null)
        {
            if (TriggerEventController.Instance.GetChair() != null)
                InitTrigger(TriggerEventController.Instance.GetChair() as ITriggerEventSendable);
            if (triggerEvent.TargetSender == null)
            {
                SetFalseComponent();
                return;
            }
        }
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

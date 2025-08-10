using System;
using UnityEngine;

[RequireComponent(typeof(FlipStuff))]
[RequireComponent(typeof(InactiveStuff))]
public class FramePhoto : ConditionalActivator, ITriggerEventSendable
{
    [SerializeField] private bool isOriginalUnder;
    [SerializeField] private bool isUnder;
    [SerializeField] private TriggerEvent photoFlipTrigger;
    [SerializeField] private TriggerEvent plateCloseTrigger;
    public event Action TriggerChangeAction;
    private FlipStuff flipStuff;
    private InactiveStuff inactiveStuff;



    private void Awake()
    {
        InitTrigger(plateCloseTrigger);
        InitTrigger(photoFlipTrigger);
        flipStuff = GetComponent<FlipStuff>();
        inactiveStuff = GetComponent<InactiveStuff>();
        GetComponent<FlipStuff>().ClickEvent.AddListener(OnPhotoFliped);

        bool isFliped = TriggerEventController.Instance.FramePhotoFlip.GetTriggerValue();
        isUnder = isFliped ? !isOriginalUnder : isOriginalUnder;
        if (isFliped && isOriginalUnder)
        {
            flipStuff.SwapZPosition();
        }
    }



    public void OnPhotoFliped()
    {
        isUnder = !isUnder;
        (photoFlipTrigger.TargetSender as FramePhoto).FlipIsUnder();
        TriggerChangeAction?.Invoke();
        SetConditionalComponent();
    }



    public void FlipIsUnder()
    {
        isUnder = !isUnder;
    }



    public bool GetTriggerValue()
    {
        return isUnder;
    }



    protected override void SetConditionalComponent()
    {
        if (!plateCloseTrigger.GetValue() && isUnder)
        {
            inactiveStuff.enabled = false;
            flipStuff.enabled = true;
        }
        else
        {
            flipStuff.enabled = false;
            inactiveStuff.enabled = true;
        }
    }
}

using System;
using UnityEngine;

[RequireComponent (typeof(ToggleStuff))]
[RequireComponent(typeof(InactiveStuff))]
public class BackPlateClose : ConditionalActivator, ITriggerEventSendable
{
    private TriggerEvent playerWakeTrigger;
    [SerializeField] private TriggerEvent screwLooseTrigger;
    private ToggleStuff toggleStuff;
    private InactiveStuff inactiveStuff;
    public event Action TriggerChangeAction;
    private bool isClose = true;



    public bool GetTriggerValue()
    {
        return isClose;
    }

    

    protected virtual void Awake()
    {
        InitTrigger(playerWakeTrigger, GlobalTriggerEvent.PlayerWakeup);
        InitTrigger(screwLooseTrigger);
        toggleStuff = GetComponent<ToggleStuff>();
        inactiveStuff = GetComponent<InactiveStuff>();
    }



    private void OnEnable()
    {
        isClose = true;
    }



    private void OnDisable()
    {
        isClose = false;
        TriggerChangeAction?.Invoke();
    }



    protected override void SetConditionalComponent()
    {
        //if (!playerWakeTrigger.GetValue() && screwLooseTriggers.GetValue(EvaluateType.AND))
        if (screwLooseTrigger.GetValue())
        {
            inactiveStuff.enabled = false;
            toggleStuff.enabled = true;
        }
        else
        {
            toggleStuff.enabled = false;
            inactiveStuff.enabled = true;
        }
    }
}

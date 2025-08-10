using System;
using UnityEngine;

[RequireComponent(typeof(InactiveStuff))]
[RequireComponent(typeof(ToggleStuff))]
[RequireComponent(typeof(ItemSelectTrigger))]
public class ScrewClose : ConditionalActivator, ITriggerEventSendable
{
    [SerializeField] protected TriggerEvent playerWakeTrigger;
    [SerializeField] protected TriggerEvent driverSelectTrigger;
    protected InactiveStuff inactiveStuff;
    protected ToggleStuff toggleStuff;
    public event Action TriggerChangeAction;
    [SerializeField] private bool isLoose = false;




    public virtual bool GetTriggerValue()
    {
        return isLoose;
    }




    protected virtual void Awake()
    {
        inactiveStuff = GetComponent<InactiveStuff>();
        inactiveStuff.enabled = false;
        toggleStuff = GetComponent<ToggleStuff>();
        toggleStuff.enabled = false;
        InitTrigger(playerWakeTrigger);
        InitTrigger(driverSelectTrigger);
        GetComponent<ToggleStuff>().ClickEvent.AddListener(OnStuffClicked);
    }



    private void OnEnable()
    {
        isLoose = false;
        TriggerChangeAction?.Invoke();
    }



    private void OnDisable()
    {
        isLoose = true;
        TriggerChangeAction?.Invoke();
    }



    protected virtual void OnStuffClicked()
    {
        isLoose = true;
        TriggerChangeAction?.Invoke();
    }



    protected override void SetConditionalComponent()
    {
        //if (!playerWakeTrigger.GetValue() && driverSelectTrigger.GetValue())
        if(driverSelectTrigger.GetValue())
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

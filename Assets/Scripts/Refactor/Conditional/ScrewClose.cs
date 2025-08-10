using System;
using UnityEngine;

[RequireComponent(typeof(InactiveStuff))]
[RequireComponent(typeof(ToggleStuff))]
public class ScrewClose : ConditionalActivator, ITriggerEventSendable
{
    [SerializeField] protected TriggerEvent playerWakeTrigger;
    protected InactiveStuff inactiveStuff;
    protected ToggleStuff toggleStuff;
    public event Action TriggerChangeAction;
    [SerializeField] protected bool isLoose = false;
    public bool IsLoose { set
        {
            if (isLoose != value)
            {
                TriggerChangeAction?.Invoke();
            }
            isLoose = value;
        }
    }




    public virtual bool GetTriggerValue()
    {
        return isLoose;
    }




    protected virtual void Awake()
    {
        inactiveStuff = GetComponent<InactiveStuff>();
        inactiveStuff.enabled = false;
        toggleStuff = GetComponent<ToggleStuff>();
        //toggleStuff.enabled = false;
        InitTrigger(playerWakeTrigger);
        GetComponent<ToggleStuff>().ToggleEvent.AddListener(OnStuffClicked);
    }



    protected virtual void OnStuffClicked(GameObject toggledObject)
    {
        isLoose = true;
        toggledObject.GetComponent<ScrewOpen>().IsLoose = true;
        TriggerChangeAction?.Invoke();
    }



    protected override void SetConditionalComponent()
    {
        //if (!playerWakeTrigger.GetValue() && driverSelectTrigger.GetValue())
        //if(driverSelectTrigger.GetValue())
        //{
        //    inactiveStuff.enabled = false;
        //    toggleStuff.enabled = true;
        //}
        //else
        //{
        //    toggleStuff.enabled = false;
        //    inactiveStuff.enabled = true;
        //}
    }
}

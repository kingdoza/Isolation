using System;
using UnityEngine;

public class ScrewOpen : ScrewClose
{
    [SerializeField] private TriggerEvent screwsLooseTrigger;
    [SerializeField] private TriggerEvent plateCloseTrigger;
    public new event Action TriggerChangeAction;
    [SerializeField] private bool isTight = true;




    public override bool GetTriggerValue()
    {
        return isTight;
    }



    private void OnEnable()
    {
        isTight = false;
        TriggerChangeAction?.Invoke();
    }



    private void OnDisable()
    {
        isTight = true;
        TriggerChangeAction?.Invoke();
    }



    protected override void Awake()
    {
        base.Awake();
        InitTrigger(plateCloseTrigger);
        InitTrigger(screwsLooseTrigger);
    }



    protected override void OnStuffClicked()
    {
        isTight = true;
        TriggerChangeAction?.Invoke();
    }



    protected override void SetConditionalComponent()
    {
        //if (screwsLooseTrigger.GetValue() && GetComponent<ItemStuff>())
        //{
        //    ItemStuff itemStuff = GetComponent<ItemStuff>();
        //    itemStuff.Acquire();
        //    return;
        //}
        //if (!playerWakeTrigger.GetValue() && driverSelectTrigger.GetValue() && plateCloseTrigger.GetValue())
        if (driverSelectTrigger.GetValue() && plateCloseTrigger.GetValue())
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

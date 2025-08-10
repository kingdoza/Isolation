using System;
using UnityEngine;

public class ScrewOpen : ScrewClose
{
    [SerializeField] private TriggerEvent screwsLooseTrigger;
    [SerializeField] private TriggerEvent plateCloseTrigger;
    public new event Action TriggerChangeAction;
    //[SerializeField] private bool isTight = true;




    public override bool GetTriggerValue()
    {
        return !isLoose;
    }



    protected override void OnStuffClicked(GameObject toggledObject)
    {
        isLoose = false;
        toggledObject.GetComponent<ScrewClose>().IsLoose = false;
        TriggerChangeAction?.Invoke();
    }



    protected override void Awake()
    {
        base.Awake();
        toggleStuff.enabled = false;
        InitTrigger(plateCloseTrigger);
        InitTrigger(screwsLooseTrigger);
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
        if (plateCloseTrigger.GetValue())
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

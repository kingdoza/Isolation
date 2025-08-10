using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ScrewsTightTrigger : ConditionalActivator, ITriggerEventSendable
{
    [SerializeField] private List<TriggerEvent> screwsOpen;
    public event Action TriggerChangeAction;
    private ITriggerEventSendable firstScrewsLooseTrigger;



    protected void Awake()
    {
        firstScrewsLooseTrigger = TriggerEventController.Instance.FirstScrewsLoose;
        InitTrigger(TriggerEventController.Instance.FirstScrewsLoose);
        foreach (TriggerEvent screw in screwsOpen)
        {
            InitTrigger(screw);
        }
    }



    public bool GetTriggerValue()
    {
        foreach (TriggerEvent screw in screwsOpen)
        {
            if (screw.TargetSender == null)
                InitTrigger(screw);
        }
        return screwsOpen.GetValue(EvaluateType.AND);
    }




    protected override void SetConditionalComponent() { }



    protected override void OnTriggerStatusChanged()
    {
        //Debug.Log("ScrewsTightTrigger : OnTriggerStatusChanged");
        if (firstScrewsLooseTrigger.GetTriggerValue() && screwsOpen.Count == 4)
        {
            GameObject screwObject = (screwsOpen[3].TargetSender as MonoBehaviour).gameObject;
            screwsOpen.RemoveAt(3);
            Destroy(screwObject);
            screwObject = (screwsOpen[0].TargetSender as MonoBehaviour).gameObject;
            screwsOpen.RemoveAt(0);
            Destroy(screwObject);
        }
        TriggerChangeAction?.Invoke();
    }
}

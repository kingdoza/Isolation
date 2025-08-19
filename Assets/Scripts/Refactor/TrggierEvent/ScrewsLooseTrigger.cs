using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScrewsLooseTrigger : ConditionalActivator, ITriggerEventSendable
{
    [SerializeField] private List<TriggerEvent> screwsClose;
    [SerializeField] private ItemData screwItemData;
    [SerializeField] private int[] screwsIdxToAquire;
    public event Action TriggerChangeAction;
    private ITriggerEventSendable firstScrewsLooseTrigger;



    protected void Awake()
    {
        firstScrewsLooseTrigger = TriggerEventController.Instance.FirstScrewsLoose;
        InitTrigger(TriggerEventController.Instance.FirstScrewsLoose);
        foreach (TriggerEvent screw in screwsClose)
        {
            InitTrigger(screw);
        }
        OnTriggerStatusChanged();
    }



    public bool GetTriggerValue()
    {
        return screwsClose.GetValue(EvaluateType.AND);
    }



    protected override void SetConditionalComponent() { }



    protected override void OnTriggerStatusChanged()
    {
        //Debug.Log("ScrewsLooseTrigger : OnTriggerStatusChanged");
        if (firstScrewsLooseTrigger.GetTriggerValue() && screwsClose.Count == 4)
        {
            GameObject screwObject = (screwsClose[3].TargetSender as MonoBehaviour).gameObject;
            screwsClose.RemoveAt(3);
            Destroy(screwObject);
            screwObject = (screwsClose[0].TargetSender as MonoBehaviour).gameObject;
            screwsClose.RemoveAt(0);
            Destroy(screwObject);
        }
        TriggerChangeAction?.Invoke(); 
    }
}

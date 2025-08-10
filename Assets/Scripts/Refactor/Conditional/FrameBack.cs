using System;
using UnityEngine;

[RequireComponent(typeof(InactiveStuff))]
[RequireComponent(typeof(ClickableStuff))]
public class FrameBack : ConditionalActivator
{
    [SerializeField] private TriggerEvent itemSelectTrigger;
    [SerializeField] private TriggerEvent screwsTightTrigger;
    [SerializeField] private TriggerEvent screwsLooseTrigger;
    [SerializeField] private TriggerEvent photoFlipTrigger;
    [SerializeField] private ItemData screwItem;
    private InactiveStuff inactiveStuff;
    private ClickableStuff clickableStuff;

    private TriggerWrapper firstScrewsLooseStatus;
    private TriggerWrapper framePhotoFlipStatus;



    private void Awake()
    {
        InitTrigger(itemSelectTrigger);
        InitTrigger(screwsTightTrigger);
        InitTrigger(screwsLooseTrigger);
        InitTrigger(photoFlipTrigger);
        inactiveStuff = GetComponent<InactiveStuff>();
        clickableStuff = GetComponent<ClickableStuff>();
        inactiveStuff.enabled = false;
        clickableStuff.enabled = false;
        firstScrewsLooseStatus = TriggerEventController.Instance.FirstScrewsLoose as TriggerWrapper;
        framePhotoFlipStatus = TriggerEventController.Instance.FramePhotoFlip as TriggerWrapper;
    }



    protected override void SetConditionalComponent()
    {
        if (itemSelectTrigger.GetValue() && screwsTightTrigger.GetValue())
        {
            inactiveStuff.enabled = false;
            clickableStuff.enabled = true;
        }
        else
        {
            clickableStuff.enabled = false;
            inactiveStuff.enabled = true;
        }

        if (firstScrewsLooseStatus.GetTriggerValue() == false)
        {
            firstScrewsLooseStatus.TriggerValue = screwsLooseTrigger.GetValue();
            if (firstScrewsLooseStatus.GetTriggerValue())
            {
                AquireScrews();
            }
        }
        if (screwsTightTrigger.GetValue())
        {
            framePhotoFlipStatus.TriggerValue = photoFlipTrigger.GetValue();
        }
    }



    private void AquireScrews()
    {
        InventoryUI.Instance.AddItem(screwItem);
        InventoryUI.Instance.AddItem(screwItem);
    }
}

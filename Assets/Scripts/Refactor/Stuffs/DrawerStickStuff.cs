using System;
using UnityEditor.Tilemaps;
using UnityEngine;

public class DrawerStickStuff : ClickableStuff
{
    private bool isSticked = false;



    protected override void Awake()
    {
        base.Awake();
    }



    protected override void OnClicked()
    {
        if (!enabled) return;
        if (InventoryUI.Instance.HasTwoScrews() == false) return;
        base.OnClicked();
        InventoryUI.Instance.DeleteTwoScrews();
        InventoryUI.Instance.DeleteSelectedSlot();
        (TriggerEventController.Instance.DrawerStick as TriggerWrapper).TriggerValue = true;
        TimeController.Instance.CheckTimeChanged();
    }
}

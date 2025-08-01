using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class DrawerHandler : ItemUsePoint
{



    public override void Interact()
    {
        if (InventoryUI.Instance.HasTwoScrews() == false)
            return;
        base.Interact();
    }



    //protected override void OnPlayerItemSelected(UsableItem selectedItem)
    //{
    //    CanInteract = selectedItem == usableItem;
    //}
}
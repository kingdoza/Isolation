using UnityEngine;
using UnityEngine.EventSystems;

public class CollectibleItem : Item
{
    public override void Interact()
    {
        //webhook test
        base.Interact();
        InventoryUI.Instance.AddItem(itemicon);
        Destroy(gameObject);
    }
}

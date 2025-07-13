using UnityEngine;
using UnityEngine.EventSystems;

public class CollectibleItem : Item
{
    public override void Interact()
    {
        base.Interact();
        InventoryUI.Instance.AddItem(itemicon);
        Destroy(gameObject);
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class CollectibleItem : Item
{
    [SerializeField] private Sprite inventorySprite;



    public override void Interact()
    {
        //webhook test
        base.Interact();
        InventoryUI.Instance.AddItem(itemicon);
        Destroy(gameObject);
    }
}

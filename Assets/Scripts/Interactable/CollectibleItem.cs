using UnityEngine;
using UnityEngine.EventSystems;

public class CollectibleItem : Item
{
    [SerializeField] private Sprite inventorySprite;
    public Sprite InventorySprite => inventorySprite;



    public override void Interact()
    {
        base.Interact();
        InventoryUI.Instance.AddItem(this);
        Destroy(gameObject);
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class CollectibleItem : Item
{
    [SerializeField] private Sprite inventorySprite;
    [SerializeField] private UsableItem useType;
    public Sprite InventorySprite => inventorySprite;
    public UsableItem UseType => useType;



    public override void Interact()
    {
        base.Interact();
        InventoryUI.Instance.AddItem(this);
        Destroy(gameObject);
    }
}

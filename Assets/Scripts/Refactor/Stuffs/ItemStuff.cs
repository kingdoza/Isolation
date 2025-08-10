using UnityEngine;

[RequireComponent(typeof(Click))]
[RequireComponent(typeof(CursorHover))]
public class ItemStuff : ClickableStuff
{
    [SerializeField] private ItemData itemData;
    protected override StuffTypeData StuffData => GameData.ItemStuffData;



    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        Acquire();
        //GameManager.Instance.RoomController.CollectedItemNames.Add(ItemName);
    }



    public void Acquire()
    {
        InventoryUI.Instance.AddItem(itemData);
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }
}

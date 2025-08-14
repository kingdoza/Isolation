using UnityEngine;

[RequireComponent(typeof(Click))]
[RequireComponent(typeof(CursorHover))]
public class ItemStuff : ClickableStuff
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private ItemData itemData2;
    protected override StuffTypeData StuffData => GameData.ItemStuffData;



    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        Acquire();
        TimeController.Instance.CheckTimeChanged();
        //GameManager.Instance.RoomController.CollectedItemNames.Add(ItemName);
    }



    public void Acquire()
    {
        InventoryUI.Instance.AddItem(itemData);
        if (itemData2 != null )
            InventoryUI.Instance.AddItem(itemData2);
        Destroy(gameObject);
        //gameObject.SetActive(false);
    }
}

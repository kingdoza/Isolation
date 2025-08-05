using UnityEngine;

[RequireComponent(typeof(Click))]
[RequireComponent(typeof(CursorHover))]
public class ItemStuff : ClickableStuff
{
    [SerializeField] private ItemData itemData;
    protected override StuffTypeData StuffData => GameData.ItemStuffData;



    protected override void OnClicked()
    {
        base.OnClicked();
        InventoryUI.Instance.AddItem(itemData);
        gameObject.SetActive(false);
        //GameManager.Instance.RoomController.CollectedItemNames.Add(ItemName);
    }
}

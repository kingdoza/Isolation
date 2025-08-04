using UnityEngine;

[RequireComponent(typeof(Click))]
[RequireComponent(typeof(CursorHover))]
public class ItemStuff : ClickableStuff
{
    [SerializeField] private ItemData itemData;



    protected override void OnClicked()
    {
        InventoryUI.Instance.AddItem(itemData);
        gameObject.SetActive(false);
        //GameManager.Instance.RoomController.CollectedItemNames.Add(ItemName);
    }
}

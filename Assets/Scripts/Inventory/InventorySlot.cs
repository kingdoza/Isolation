using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ControllerUtils;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CollectibleItem slotItem;
    private Texture2D itemTexture;
    [HideInInspector] public UnityEvent<InventorySlot> OnClicked = new UnityEvent<InventorySlot>();
    public CollectibleItem SlotItem => slotItem;



    public void OnPointerClick(PointerEventData eventData)
    {
        OnClicked?.Invoke(this);
    }



    public void SetItem(CollectibleItem item)
    {
        slotItem = item;
        transform.Find("Image").GetComponent<Image>().sprite = item.InventorySprite;

        Rect spriteRect = slotItem.InventorySprite.rect;
        Texture2D spriteTexture = slotItem.InventorySprite.texture;
        itemTexture = new Texture2D((int)spriteRect.width, (int)spriteRect.height);
        Color[] pixels = spriteTexture.GetPixels(
            (int)spriteRect.x,
            (int)spriteRect.y,
            (int)spriteRect.width,
            (int)spriteRect.height
        );
        itemTexture.SetPixels(pixels);
        itemTexture.Apply();
    }



    public void Select()
    {
        SetItemCursor();
        GetComponent<Image>().color = Color.gray;
        GameManager.Instance.Player.SelectUsableItem(slotItem.UseType);
    }



    public void Unselect()
    {
        SetCursorTexture(CursorTextures.Normal);
        GetComponent<Image>().color = Color.white;
        GameManager.Instance.Player.SelectUsableItem(UsableItem.None);
    }



    public void SetItemCursor()
    {
        Vector2 hotspot = new Vector2(itemTexture.width / 2, itemTexture.height / 2);
        SetCursorTexture(itemTexture, hotspot);
    }
}

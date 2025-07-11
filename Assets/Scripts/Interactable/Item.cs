using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IInteractable, IPointerClickHandler
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemIcon;

    public string ItemName => itemName;
    private Color originalColor;
    //
    private SpriteRenderer spriteRenderer;
    //



    private void Start()
    {
        //
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemIcon;
        //
        gameObject.AddComponent<MouseHover>();
        originalColor = GetComponent<SpriteRenderer>().color;
    }



    public void Interact()
    {
        Debug.Log(itemName + " is clicked!!!");
        GameManager.Instance.TimeController.ProgressMinutes(ProgressTimeType.ItemInteract);
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        //if(!DragScroller.IsDragging)
        //    Interact();
    }



    private void OnMouseUp()
    {
        if (!DragScroller.IsDragging)
            Interact();
    }



    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = Color.gray;
    }



    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = originalColor;
    }
    //
    public Sprite GetItemIcon()
    {
        return itemIcon;
    }
    //
}

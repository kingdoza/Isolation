using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IInteractable, IPointerClickHandler
{
    [SerializeField] private string itemName;
    [SerializeField] private Sprite itemicon;//
    public string ItemName => itemName;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;//



    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();//
        gameObject.AddComponent<MouseHover>();
        originalColor = GetComponent<SpriteRenderer>().color;
    }



    public void Interact()
    {
        Debug.Log(itemName + " is clicked!!!");
        GameManager.Instance.TimeController.ProgressMinutes(ProgressTimeType.ItemInteract);
        Sprite icon = GetComponent<SpriteRenderer>().sprite;
        InventoryUI.Instance.AddItem(itemicon);
        Destroy(gameObject);
    } 



    public void OnPointerClick(PointerEventData eventData)
    {
        if(!DragScroller.IsDragging)
            Interact();
    }



    private void OnMouseUp()
    {
        if(!DragScroller.IsDragging)
            Interact();
    }



    private void OnMouseEnter()
    {
        spriteRenderer.color = Color.gray;
    }



    private void OnMouseExit()
    {
        spriteRenderer.color = originalColor;
    }
}

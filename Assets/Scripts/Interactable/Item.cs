using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IInteractable
{
    [SerializeField] private string itemName;

    public string ItemName => itemName;
    protected Sprite itemicon;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;



    private void Start()
    {
        itemicon = GetComponent<SpriteRenderer>().sprite;
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.AddComponent<MouseHover>();
        originalColor = GetComponent<SpriteRenderer>().color;
    }



    public virtual void Interact()
    {
        Debug.Log(itemName + " is clicked!!!");
        GameManager.Instance.TimeController.ProgressMinutes(ProgressTimeType.ItemInteract);
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

using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IInteractable, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string itemName;
    private bool canInteract = true;
    protected Sprite itemicon;
    private Color originalColor;
    private SpriteRenderer spriteRenderer;

    public string ItemName => itemName;
    public bool CanInteract
    {
        get => canInteract; set
        {
            MouseHover mouseHoverComp = GetComponent<MouseHover>();
            canInteract = value;
            if (mouseHoverComp == null)
                return;
            if (canInteract)
            {
                mouseHoverComp.enabled = true;
            }
            else
            {
                mouseHoverComp.enabled = false;
            }
        }
    }



    private void Awake()
    {
        itemicon = GetComponent<SpriteRenderer>().sprite;
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameObject.AddComponent<MouseHover>();
        originalColor = GetComponent<SpriteRenderer>().color;

        if (gameObject.GetComponent<MouseHover>() == null)
        {
            gameObject.AddComponent<MouseHover>();
        }
    }



    public virtual void Interact()
    {
        Debug.Log(itemName + " is clicked!!!");
        GameManager.Instance.TimeController.ProgressMinutes(ProgressTimeType.ItemInteract);
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        if (!DragScroller.IsDragging && CanInteract)
            Interact();
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        if(CanInteract)
            spriteRenderer.color = Color.gray;
    }



    public void OnPointerExit(PointerEventData eventData)
    {
        if (CanInteract)
            spriteRenderer.color = originalColor;
    }
}

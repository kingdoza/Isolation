using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IInteractable, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string itemName;
    private bool canInteract = true;
    protected Sprite itemicon;
    private Color originalColor;
    protected SpriteRenderer spriteRenderer;

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
                spriteRenderer.color = originalColor;
                mouseHoverComp.enabled = false;
            }
        }
    }



    protected virtual void Awake()
    {
        itemicon = GetComponent<SpriteRenderer>().sprite;
        spriteRenderer = GetComponent<SpriteRenderer>();
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
        if (CanInteract)
            ChangeAllSubSpritesColor(Color.gray);
    }



    private void ChangeAllSubSpritesColor(Color color)
    {
        foreach(SpriteRenderer childSr in GetComponentsInChildren<SpriteRenderer>())
        {
            childSr.color = color;
        }
    }



    public void OnPointerExit(PointerEventData eventData)
    {
        if (CanInteract)
            ChangeAllSubSpritesColor(originalColor);
    }
}

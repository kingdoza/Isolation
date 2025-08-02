using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IInteractable, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private string itemName;
    protected Sprite itemicon;
    protected Color originalColor;
    protected SpriteRenderer spriteRenderer;
    protected virtual UsableItem InteractItemStatus => UsableItem.None;
    protected MouseHover mouseHoverComp;

    public string ItemName => itemName;



    public bool CanInteract { get => interactConditions.All(cond => cond()); }

    private List<Func<bool>> interactConditions = new List<Func<bool>>();



    protected virtual void Awake()
    {
        mouseHoverComp = GetComponent<MouseHover>();
        itemicon = GetComponent<SpriteRenderer>().sprite;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = GetComponent<SpriteRenderer>().color;

        if (gameObject.GetComponent<MouseHover>() == null)
        {
            gameObject.AddComponent<MouseHover>();
        }
    }



    protected virtual void Start()
    {
        //GameManager.Instance.Player.OnInventoryItemSelect.AddListener(OnPlayerItemSelected);
        Player player = GameManager.Instance.Player;
        RegisterInteractCondition(() => player.UsingItemType == InteractItemStatus);
    }



    protected virtual void Update()
    {
        if (mouseHoverComp == null)
            return;

        if (CanInteract)
        {
            mouseHoverComp.enabled = true;
        }
        else
        {
            //spriteRenderer.color = originalColor;
            mouseHoverComp.enabled = false;
        }
    }



    public void RegisterInteractCondition(Func<bool> condition)
    {
        interactConditions.Add(condition);
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



    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (CanInteract)
            ChangeAllSubSpritesColor(Color.gray);
    }



    protected void ChangeAllSubSpritesColor(Color color)
    {
        foreach(SpriteRenderer childSr in GetComponentsInChildren<SpriteRenderer>())
        {
            childSr.color = color;
        }
    }



    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (CanInteract)
            ChangeAllSubSpritesColor(originalColor);
    }



    //protected virtual void OnPlayerItemSelected(UsableItem selectedItem)
    //{
    //    CanInteract = selectedItem == UsableItem.None;
    //}
}

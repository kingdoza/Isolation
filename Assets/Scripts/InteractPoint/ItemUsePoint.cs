using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemUsePoint : Item
{
    [SerializeField] private UsableItem usableItem;
    [HideInInspector] public UnityEvent<ItemUsePoint> OnItemUse = new UnityEvent<ItemUsePoint>();
    protected override UsableItem InteractItemStatus => usableItem;



    public override void Interact()
    {
        base.Interact();
        OnItemUse?.Invoke(this);
    }



    //protected override void OnPlayerItemSelected(UsableItem selectedItem)
    //{
    //    CanInteract = selectedItem == usableItem;
    //}
}
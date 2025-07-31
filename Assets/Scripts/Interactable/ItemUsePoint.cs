using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemUsePoint : Item
{
    [SerializeField] private UsableItem usableItem;
    [HideInInspector] public UnityEvent OnItemUse = new UnityEvent();



    public override void Interact()
    {
        base.Interact();
        OnItemUse?.Invoke();
    }



    protected override void OnPlayerItemSelected(UsableItem selectedItem)
    {
        CanInteract = selectedItem == usableItem;
    }
}
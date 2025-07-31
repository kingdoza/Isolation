using UnityEngine;

public class SortSwitcher : ItemUsePoint
{
    [SerializeField] private SpriteRenderer renderder1;
    [SerializeField] private SpriteRenderer renderder2;



    public override void Interact()
    {
        base.Interact();
        int sortingOrder1 = renderder1.sortingOrder;
        renderder1.sortingOrder = renderder2.sortingOrder;
        renderder2.sortingOrder = sortingOrder1;
    }
}

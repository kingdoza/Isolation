using UnityEngine;

public class LayerTopDragStuff : DraggableStuff
{
    [SerializeField] private HighestSortLayer highestSortLayer;
    private SpriteRenderer spriteRenderer;



    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<Drag>().DragStartEvent.AddListener(OnDragStart);
    }



    private void OnDragStart()
    {
        spriteRenderer.sortingOrder = highestSortLayer.GetHightestSortOrder() + 1;
    }
}

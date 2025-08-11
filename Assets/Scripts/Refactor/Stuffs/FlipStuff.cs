using UnityEngine;

public class FlipStuff : ClickableStuff
{
    protected override StuffTypeData StuffData => GameData.FocusStuffData;
    [SerializeField] private Transform otherTransform;
    [SerializeField] private SpriteRenderer otherRenderer;
    private SpriteRenderer myRenderer;



    protected override void Awake()
    {
        base.Awake();
        myRenderer = GetComponent<SpriteRenderer>();
    }



    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        SwapZPosition();
        TimeController.Instance.CheckTimeChanged();
    }



    public void SwapZPosition()
    {
        float tempZ = transform.position.z;
        Vector3 myPos = transform.position;
        Vector3 otherPos = otherTransform.position;

        transform.position = new Vector3(myPos.x, myPos.y, otherPos.z);
        otherTransform.position = new Vector3(otherPos.x, otherPos.y, tempZ);
    }



    private void SwapOrderInLayer()
    {
        int tempOrder = myRenderer.sortingOrder;
        myRenderer.sortingOrder = otherRenderer.sortingOrder;
        otherRenderer.sortingOrder = tempOrder;
    }
}

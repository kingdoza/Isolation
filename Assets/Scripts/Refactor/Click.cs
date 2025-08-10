using UnityEngine;
using UnityEngine.Events;

public class Click : MouseInteraction
{
    protected override string InputLayerName => "Clickable";
    public override bool IsPassDown => false;
    [HideInInspector] public UnityEvent ClickEvent = new UnityEvent();



    public override void OnInteractStart()
    {

    }



    public override void OnInteracting()
    {

    }



    public override void OnInteractEnd()
    {
        OnClick();
        ClickEvent?.Invoke();
    }



    public override bool HasInteracted()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return GetComponent<Collider2D>().OverlapPoint(mousePos);
    }



    private void OnClick()
    {
        //Debug.Log("Clicked");
    }
}

using UnityEngine;

public class Click : MouseInteraction
{
    protected override string InputLayerName => "Clickable";
    public override bool IsPassDown => false;



    public override void OnInteractStart()
    {

    }



    public override void OnInteracting()
    {

    }



    public override void OnInteractEnd()
    {
        OnClick();
    }



    public override bool HasInteracted()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return GetComponent<Collider2D>().OverlapPoint(mousePos);
    }



    private void OnClick()
    {
        Debug.Log("Clicked");
    }
}

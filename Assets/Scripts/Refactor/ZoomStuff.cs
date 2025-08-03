using UnityEngine;

[RequireComponent(typeof(Click))]
[RequireComponent(typeof(CursorHover))]
public class ZoomStuff : ClickableStuff
{
    [SerializeField] private GameObject zoomPrefab;



    protected override void Awake()
    {
        base.Awake();
    }



    protected override void OnClicked()
    {
        Debug.Log("Zoom");
    }
}

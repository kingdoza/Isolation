using UnityEngine;

[RequireComponent(typeof(Click))]
[RequireComponent(typeof(CursorHover))]
public class ZoomStuff : ClickableStuff
{
    [SerializeField] private GameObject zoomPrefab;
    private GameObject zoomView;



    protected override void Awake()
    {
        base.Awake();
        //zoomPrefab.SetActive(false);
        zoomView = Instantiate(zoomPrefab);
        zoomView.SetActive(false);
    }



    protected override void OnClicked()
    {
        GameManager.Instance.RoomController.ZoomInView(zoomView);
    }
}

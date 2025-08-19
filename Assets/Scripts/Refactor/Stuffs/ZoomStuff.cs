using UnityEngine;

[RequireComponent(typeof(Click))]
[RequireComponent(typeof(CursorHover))]
public class ZoomStuff : ClickableStuff
{
    [SerializeField] private GameObject zoomPrefab;
    [SerializeField] private AudioClip outClip;
    private GameObject zoomView;
    protected override StuffTypeData StuffData => GameData.ZoomStuffData;

    protected override void Awake()
    {
        base.Awake();
        if (sfxClip == null)
            sfxClip = ControllerUtils.SFXClips.click1;
        if (outClip == null)
            outClip = sfxClip;
        //zoomPrefab.SetActive(false);
        zoomView = Instantiate(zoomPrefab);
        zoomView.SetActive(false);
    }



    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        GameManager.Instance.RoomController.ZoomInView(zoomView, outClip);
    }
}

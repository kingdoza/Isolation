using UnityEngine;
using UnityEngine.EventSystems;
using static ControllerUtils;

public class ZoomItem : Item
{
    [SerializeField] private GameObject zoomViewPrefab;



    protected override void Awake()
    {
        base.Awake();
        if (gameObject.GetComponent<MouseHover>() == null)
        {
            gameObject.AddComponent<MouseHover>();
        }
    }



    protected override void Start()
    {
        base.Start();
    }



    public override void Interact()
    {
        base.Interact();
        //GameManager.Instance.RoomController.ZoomInView(zoomViewPrefab);

        SoundController soundPlayer = GameManager.Instance.SoundController;
        //PlaySFX(SFXClips.click);
    }
}

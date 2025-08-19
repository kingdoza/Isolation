using UnityEngine;
using UnityEngine.EventSystems;
using static ControllerUtils;

public class ZoomArea : MonoBehaviour, IInteractable, IPointerClickHandler
{
    [SerializeField] private GameObject zoomViewPrefab;
    private bool canInteract = true;

    public bool CanInteract
    {
        get => canInteract; set
        {
            MouseHover mouseHoverComp = GetComponent<MouseHover>();
            canInteract = value;
            if (mouseHoverComp == null)
                return;
            if (canInteract)
            {
                mouseHoverComp.enabled = true;
            }
            else
            {
                mouseHoverComp.enabled = false;
            }
        }
    }

    private void Awake()
    {
        if(gameObject.GetComponent<MouseHover>() == null)
        {
            gameObject.AddComponent<MouseHover>();
        }
    }



    private void Start()
    {
        GameManager.Instance.Player.OnInventoryItemSelect.AddListener(OnPlayerItemSelected);
    }



    public void Interact()
    {
        //GameManager.Instance.RoomController.ZoomInView(zoomViewPrefab);

        SoundController soundPlayer = GameManager.Instance.SoundController;
        //PlaySFX(SFXClips.click);
    }



    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (!DragScroller.IsDragging && CanInteract)
            Interact();
    }



    private void OnPlayerItemSelected(UsableItem selectedItem)
    {
        CanInteract = selectedItem == UsableItem.None;
    }
}

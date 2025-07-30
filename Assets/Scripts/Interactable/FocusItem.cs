using UnityEngine;
using UnityEngine.EventSystems;
using static ControllerUtils;

public class FocusItem : MonoBehaviour, IInteractable, IPointerClickHandler
{
    [SerializeField] private GameObject focusViewPrefab;
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



    public void Interact()
    {
        GameManager.Instance.RoomController.FocusItem(focusViewPrefab);

        SoundController soundPlayer = GameManager.Instance.SoundController;
        PlaySFX(SFXClips.Click);
    }



    public virtual void OnPointerClick(PointerEventData eventData)
    {
        if (!DragScroller.IsDragging && CanInteract)
            Interact();
    }
}

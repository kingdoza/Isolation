using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

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



    public void Interact()
    {
        GameManager.Instance.RoomController.ZoomInView(zoomViewPrefab);
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        if (!DragScroller.IsDragging)
            Interact();
    }
}

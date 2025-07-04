using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ZoomArea : MonoBehaviour, IInteractable, IPointerClickHandler
{
    [SerializeField] private GameObject zoomViewPrefab;



    public void Interact()
    {
        GameManager.Instance.RoomController.ZoomInView(zoomViewPrefab);
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        Interact();
    }
}

using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ZoomArea : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject zoomViewPrefab;



    private void Start()
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



    private void OnMouseUp()
    {
        if (!DragScroller.IsDragging)
            Interact();
    }
}

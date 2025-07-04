using UnityEngine;
using UnityEngine.EventSystems;

public class ZoomArea : MonoBehaviour, IInteractable, IPointerClickHandler
{
    [SerializeField] private GameObject zoomViewPrefab;
    private RoomController roomController;



    private void Start()
    {
        roomController = GameManager.Instance.RoomController;
    }



    public void OnInteract()
    {
        roomController.ZoomInView(zoomViewPrefab);
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        OnInteract();
    }
}

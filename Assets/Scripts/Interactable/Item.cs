using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IInteractable, IPointerClickHandler
{
    [SerializeField] private string itemName;
    public string ItemName => itemName;



    public void Interact()
    {
        Debug.Log(itemName + " is clicked!!!");
        GameManager.Instance.TimeController.ProgressMinutes(ProgressTimeType.ItemInteract);
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        Interact();
    }
}

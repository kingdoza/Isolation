using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IInteractable, IPointerClickHandler
{
    [SerializeField] private string itemName;



    public void OnInteract()
    {
        Debug.Log(itemName + " is clicked!!!");
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        OnInteract();
    }
}

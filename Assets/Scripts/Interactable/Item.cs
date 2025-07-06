using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour, IInteractable, IPointerClickHandler
{
    [SerializeField] private string itemName;
    public string ItemName => itemName;
    private Color originalColor;



    private void Start()
    {
        gameObject.AddComponent<MouseHover>();
        originalColor = GetComponent<SpriteRenderer>().color;
    }



    public void Interact()
    {
        Debug.Log(itemName + " is clicked!!!");
        GameManager.Instance.TimeController.ProgressMinutes(ProgressTimeType.ItemInteract);
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        Interact();
    }



    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = Color.gray;
    }



    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = originalColor;
    }
}

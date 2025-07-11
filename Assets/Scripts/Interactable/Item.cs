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
        // 디버깅 코드 시작
        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager.Instance is NULL!");
            return;
        }

        if (GameManager.Instance.TimeController == null)
        {
            Debug.LogError("GameManager.Instance.TimeController is NULL!");
            return;
        }

        if (InventoryStorage.Instance == null)
        {
            Debug.LogError("InventoryStorage.Instance is NULL!");
            return;
        }
        // 디버깅 코드 끝
        GameManager.Instance.TimeController.ProgressMinutes(ProgressTimeType.ItemInteract);

        InventoryItem newItem = new InventoryItem(itemName, GetComponent<SpriteRenderer>().sprite);
        InventoryStorage.Instance.AddItem(newItem);

        Destroy(gameObject); 
        
        
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        //if(!DragScroller.IsDragging)
            //Interact();
    }



    private void OnMouseUp()
    {
        if(!DragScroller.IsDragging)
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

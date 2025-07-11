using UnityEngine;
using UnityEngine.UI;

public class TestInventoryUI : MonoBehaviour
{
    public TestInventory testInventory; 
    public Transform inventoryPanel;  
    public GameObject inventoryItemPrefab;  

    
    public void UpdateInventoryUI()
    {
        
        foreach (Transform child in inventoryPanel)
        {
            Destroy(child.gameObject);
        }

        
        foreach (var item in testInventory.inventoryItems)
        {
            GameObject itemUI = Instantiate(inventoryItemPrefab, inventoryPanel);
            itemUI.GetComponentInChildren<Text>().text = item.testItemName;
            itemUI.GetComponentInChildren<Image>().sprite = item.testItemIcon;
            itemUI.GetComponent<Button>().onClick.AddListener(() => UseTestItem(item)); 
        }
    }

    // 아이템 사용
    private void UseTestItem(TestItem item)
    {
        Debug.Log(item.testItemName + " used!");
        testInventory.RemoveTestItem(item);  
        UpdateInventoryUI();  
    }
}

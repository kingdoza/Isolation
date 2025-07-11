using System.Collections.Generic;
using UnityEngine;

public class TestInventory : MonoBehaviour
{
    public List<TestItem> inventoryItems = new List<TestItem>();  

    
    public void AddTestItem(TestItem newItem)
    {
        inventoryItems.Add(newItem);
        Debug.Log(newItem.testItemName );
    }

    
    public void RemoveTestItem(TestItem item)
    {
        if (inventoryItems.Contains(item))
        {
            inventoryItems.Remove(item);
            Debug.Log(item.testItemName);
        }
    }

    
    public void PrintInventory()
    {
        foreach (var item in inventoryItems)
        {
            Debug.Log("인벤내용: " + item.testItemName);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    
    private List<Item> items = new List<Item>();

    
    public void AddItem(Item newItem)
    {
        if (!items.Contains(newItem))
        {
            items.Add(newItem);
            Debug.Log(newItem.ItemName + " added to inventory");
        }
    }

    
    public void RemoveItem(Item item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            Debug.Log(item.ItemName + " removed from inventory");
        }
    }

  
    public List<Item> GetItems()
    {
        return items;
    }
}


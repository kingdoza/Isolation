using UnityEngine;
using System.Collections.Generic;

public class InventoryStorage : MonoBehaviour
{
    public static InventoryStorage Instance { get; private set; }
    private List<InventoryItem> items = new List<InventoryItem>();
    private const int MAX_SLOTS = 6;

    private void Start()
{
    if (InventoryUI.Instance != null)
    {
        InventoryUI.Instance.UpdateInventoryUI();
    }
}

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool AddItem(InventoryItem item)
    {
        if (items.Count >= MAX_SLOTS)
        {
            Debug.Log("inventory full");
            return false;
        }

        items.Add(item);
        Debug.Log($"add inventory {item.itemName} ");
        return true;
    }

    public List<InventoryItem> GetItems()
    {
        return items;
    }
}
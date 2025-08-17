using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;
using static ControllerUtils;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [Header("패널")]
    [SerializeField] private Transform slotParent;

    [Header("슬롯 프리팹")]
    [SerializeField] private GameObject slotPrefab;

    private List<InventorySlot> slots = new List<InventorySlot>();
    private InventorySlot slotSelected = null;
    private bool canSelect = true;



    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Player.Instance.OnSleep.AddListener(OpenAllSlots);
        Player.Instance.OnWakeup.AddListener(CloseAllSlots);
    }



    private void Update()
    {
        if (Time.timeScale >= 0.99 && Input.GetMouseButtonUp(1) && slotSelected)
        {
            slotSelected.Unselect();
            slotSelected = null;
        }
    }



    public void AddItem(CollectibleItem item)
    {
        GameObject slot = Instantiate(slotPrefab, slotParent);

        InventorySlot newSlot = slot.GetComponent<InventorySlot>();
        slots.Add(newSlot);
        newSlot.OnClicked.AddListener(OnSlotClicked);
        newSlot.SetItem(item);
        //Image image = slot.transform.Find("Image").GetComponent<Image>();
        //image.sprite = item.InventorySprite;
    }



    public void AddItem(ItemData itemData)
    {
        GameObject slot = Instantiate(slotPrefab, slotParent);

        InventorySlot newSlot = slot.GetComponent<InventorySlot>();
        slots.Add(newSlot);
        newSlot.OnClicked.AddListener(OnSlotClicked);
        newSlot.SetItem(itemData);
        //Image image = slot.transform.Find("Image").GetComponent<Image>();
        //image.sprite = item.InventorySprite;
    }



    public void OnSlotClicked(InventorySlot clickedSlot)
    {
        if (!canSelect) return;

        slotSelected?.Unselect();
        slotSelected = (slotSelected == clickedSlot) ? null : clickedSlot;
        slotSelected?.Select();
    }



    public bool HasTwoScrews()
    {
        return true;
        int screwCount = 0;
        foreach (InventorySlot slot in slots)
        {
            if (slot.Item.Type == ItemType.Screw)
                ++screwCount;
        }
        return screwCount >= 2;
    }



    public void DeleteTwoScrews()
    {
        return;
        foreach (InventorySlot slot in slots)
        {
            if (slot.Item.Type != ItemType.Screw)
                continue;
            DeleteSlot(slot);
        }
    }



    public void DeleteSelectedSlot()
    {
        if (slotSelected == null) 
            return;
        DeleteSlot(slotSelected);
        slotSelected = null;
    }



    public void DeleteSlot(InventorySlot targetSlot)
    {
        targetSlot.Unselect();
        slots.Remove(targetSlot);
        Destroy(targetSlot.gameObject);
    }



    private void CloseAllSlots()
    {
        canSelect = false;
        slotSelected?.Unselect();
        foreach (InventorySlot slot in slots)
        {
            slot.Close();
        }
    }



    private void OpenAllSlots()
    {
        canSelect = true;
        foreach (InventorySlot slot in slots)
        {
            slot.Open();
        }
    }



    // public void AddItem(Sprite itemIcon)
    // {
    //     GameObject slot = Instantiate(slotPrefab, slotParent);
    //     Image image = slot.transform.Find("Image").GetComponent<Image>();
    //     image.sprite = itemIcon;
    // }
}
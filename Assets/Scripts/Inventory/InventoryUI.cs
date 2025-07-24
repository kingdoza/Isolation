using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;

    [Header("패널")]
    [SerializeField] private Transform slotParent;

    [Header("슬롯 프리팹")]
    [SerializeField] private GameObject slotPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void AddItem(CollectibleItem item)
    {
        GameObject slot = Instantiate(slotPrefab, slotParent);
        Image image = slot.transform.Find("Image").GetComponent<Image>();
        image.sprite = item.InventorySprite;
    }

    // public void AddItem(Sprite itemIcon)
    // {
    //     GameObject slot = Instantiate(slotPrefab, slotParent);
    //     Image image = slot.transform.Find("Image").GetComponent<Image>();
    //     image.sprite = itemIcon;
    // }
}
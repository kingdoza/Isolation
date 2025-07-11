using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }
    [SerializeField] private GameObject[] slots; // 6개의 슬롯 (인스펙터에서 설정)
    [SerializeField] private Sprite emptySlotSprite; // 빈 슬롯의 기본 스프라이트

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

    private void Start()
    {
        InitializeSlots();
        UpdateInventoryUI();
    }

    private void InitializeSlots()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].GetComponent<Image>().sprite = emptySlotSprite;
            slots[i].GetComponentInChildren<TMP_Text>().text = "";
        }
    }

    public void UpdateInventoryUI()
    {
        var items = InventoryStorage.Instance.GetItems();
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                slots[i].GetComponent<Image>().sprite = items[i].itemSprite;
                slots[i].GetComponentInChildren<TMP_Text>().text = items[i].itemName;
            }
            else
            {
                slots[i].GetComponent<Image>().sprite = emptySlotSprite;
                slots[i].GetComponentInChildren<TMP_Text>().text = "";
            }
        }
    }
}
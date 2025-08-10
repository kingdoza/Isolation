using System;
using TMPro.Examples;
using UnityEngine;

public class ItemSelectTrigger : MonoBehaviour, ITriggerEventSendable
{
    [SerializeField] private ItemType itemType;
    public event Action TriggerChangeAction;



    private void Awake()
    {
        Player.Instance.ItemSelectEvent.AddListener(OnPlayerSelectedItem);
        Player.Instance.ItemUnselectEvent.AddListener(OnPlayerSelectedItem);
    }



    private void OnPlayerSelectedItem(ItemData itemData)
    {
        TriggerChangeAction?.Invoke();
    }



    public bool GetTriggerValue()
    {
        if (Player.Instance.ItemInUse == null) 
            return itemType == ItemType.None;
        return Player.Instance.ItemInUse.Type == itemType;
    }
}

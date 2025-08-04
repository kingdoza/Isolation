using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static ControllerUtils;

public class Player : SceneSingleton<Player>
{
    [SerializeField] private Motivation[] motivations;
    private bool isSleeping = false;
    public bool IsSleeping => isSleeping;
    [HideInInspector] public UnityEvent OnSleep;
    [HideInInspector] public UnityEvent OnWakeup;
    public Dictionary<EndingType, MotiveProgress> MotiveProgresses { get; private set; }

    [HideInInspector] public UnityEvent<MotiveProgress, string> OnItemCollect;
    [HideInInspector] public UnityEvent<UsableItem> OnInventoryItemSelect;
    [SerializeField] private UsableItem usingItemType = UsableItem.None;
    [HideInInspector] public UnityEvent<ItemData> ItemSelectEvent = new UnityEvent<ItemData>();
    [HideInInspector] public UnityEvent<ItemData> ItemUnselectEvent = new UnityEvent<ItemData>();
    private ItemData itemInUse;

    public UsableItem UsingItemType => usingItemType;

    public ItemData ItemInUse => itemInUse;



    private void Start()
    {
        RegisterDragScrollCondition(() => usingItemType == UsableItem.None);
    }



    public void InitMotives()
    {
        MotiveProgresses = new Dictionary<EndingType, MotiveProgress>();

        foreach (Motivation motive in motivations)
        {
            MotiveProgresses.Add(motive.type, new MotiveProgress(motive));
        }
    }



    public void Sleep()
    {
        if (isSleeping)
            return;
        isSleeping = true;
        GameManager.Instance.FilterController.SetSleep();
        OnSleep?.Invoke();
    }



    public void Wakeup()
    {
        isSleeping = false;
        GameManager.Instance.FilterController.SetWakeup();
        OnWakeup?.Invoke();
    }



    public CollectStatus GetCollectStatus(TriggerItem item)
    {
        return MotiveProgresses[item.EndingType].GetStatus(item);
    }



    public void CollectItem(TriggerItem item)
    {
        if (item.CollectStatus != CollectStatus.Positive)
            return;
        MotiveProgresses[item.EndingType].Collect(item);
        OnItemCollect?.Invoke(MotiveProgresses[item.EndingType], item.ItemName);
    }



    public void SelectUsableItem(UsableItem uitem)
    {
        usingItemType = uitem;
        OnInventoryItemSelect?.Invoke(uitem);
    }



    public void SelectItem(ItemData item)
    {
        itemInUse = item;
        ItemSelectEvent?.Invoke(item);
    }



    public void UnselectItem()
    {
        if (itemInUse == null)
            return;
        ItemUnselectEvent?.Invoke(itemInUse);
        itemInUse = null;
    }
}



public class MotiveProgress
{
    public Motivation Motive {  get; private set; }
    public int[] ClearedItemIdx { get; private set; }



    public MotiveProgress(Motivation motive)
    {
        Motive = motive;
        ClearedItemIdx = new int[Motive.evidences.Count];
    }



    public CollectStatus GetStatus(TriggerItem item)
    {
        int evidenceCount = Motive.evidences.Count;
        for (int i = 0; i < evidenceCount; ++i)
        {
            Evidence evidence = Motive.evidences[i];
            int itemCount = evidence.itemNames.Count;

            int targetItemIdx = 0;
            while (targetItemIdx < itemCount)
            {
                if(evidence.itemNames[targetItemIdx] == item.ItemName)
                    return JudgeCollectStatus(targetItemIdx, ClearedItemIdx[i]);
                ++targetItemIdx;
            }
        }
        return CollectStatus.Null;
    }



    private CollectStatus JudgeCollectStatus(int targetItemIdx, int collectibleItemIdx)
    {
        if (targetItemIdx == collectibleItemIdx)
            return CollectStatus.Positive;
        if (targetItemIdx < collectibleItemIdx)
            return CollectStatus.Finished;
        return CollectStatus.Negative;
    }



    public void Collect(TriggerItem item)
    {
        int evidenceCount = Motive.evidences.Count;
        for (int i = 0; i < evidenceCount; ++i)
        {
            if (IsEvidenceCleared(i))
                continue;
            string collectibleItemName = Motive.evidences[i].itemNames[ClearedItemIdx[i]];
            if (item.ItemName == collectibleItemName)
            {
                item.CollectStatus = CollectStatus.Finished;
                ++ClearedItemIdx[i];
                return;
            }
        }
        Debug.LogError(item.ItemName + " is not collectible.");
    }



    public bool IsEvidenceCleared(int evidenceIdx)
    {
        int evidenceItemCount = Motive.evidences[evidenceIdx].itemNames.Count;
        int collectedItemCount = ClearedItemIdx[evidenceIdx];
        return collectedItemCount >= evidenceItemCount;
    }
}
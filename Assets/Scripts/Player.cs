using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static ControllerUtils;

public class Player : SceneSingleton<Player>, ITriggerEventSendable
{
    [SerializeField] private Motivation[] motivations;
    private bool isSleeping = false;
    public bool IsSleeping => isSleeping;
    [HideInInspector] public UnityEvent OnSleep;
    [HideInInspector] public UnityEvent OnWakeup;
    public Dictionary<EndingType, MotiveProgress> MotiveProgresses { get; private set; }

    [HideInInspector] public UnityEvent<MotiveProgress, string> EvidenceCollectEvent;

    [HideInInspector] public UnityEvent<UsableItem> OnInventoryItemSelect;
    [SerializeField] private UsableItem usingItemType = UsableItem.None;
    [HideInInspector] public UnityEvent<ItemData> ItemSelectEvent = new UnityEvent<ItemData>();
    [HideInInspector] public UnityEvent<ItemData> ItemUnselectEvent = new UnityEvent<ItemData>();

    [SerializeField] private string[] onWakeupDialogues;
    [SerializeField] private string[] onSleepDialogues;
    private ItemData itemInUse;

    public event Action TriggerChangeAction;
    public UsableItem UsingItemType => usingItemType;

    public ItemData ItemInUse => itemInUse;



    protected override void Awake()
    {
        Debug.Log("Player Awake");
        TriggerEventController.Instance.PlayerWakeup = this;
    }



    private void Start()
    {
        RegisterDragScrollCondition(() => usingItemType == UsableItem.None);
        UnselectItem();
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
        ShowWakeSleepDialogues(onSleepDialogues);
        OnSleep?.Invoke();
        TriggerChangeAction?.Invoke();
    }



    public void Wakeup()
    {
        isSleeping = false;
        GameManager.Instance.FilterController.SetWakeup();
        ShowWakeSleepDialogues(onWakeupDialogues);
        OnWakeup?.Invoke();
        TriggerChangeAction?.Invoke();
    }



    private void ShowWakeSleepDialogues(string[] dialogues)
    {
        Debug.Log("Dialogue Start");
        GameManager.Instance.UIController.DisableMoveButtons();
        GameManager.Instance.DialogueController.DiagloueEndEvent.AddListener(OnDiaglogueClosed); 
        GameManager.Instance.DialogueController.StartDialogueSequence(dialogues);
    }



    private void OnDiaglogueClosed()
    {
        GameManager.Instance.UIController.EnableMoveButtons();
        Debug.Log("Dialogue End");
    }



    public CollectStatus GetCollectStatus(TriggerItem item)
    {
        return MotiveProgresses[item.EndingType].GetStatus(item);
    }



    public CollectStatus GetCollectStatus(EvidenceStuff evidence)
    {
        return MotiveProgresses[evidence.EndingType].GetStatus(evidence);
    }



    public void CollectItem(TriggerItem item)
    {
        if (item.CollectStatus != CollectStatus.Positive)
            return;
        MotiveProgresses[item.EndingType].Collect(item);
        EvidenceCollectEvent?.Invoke(MotiveProgresses[item.EndingType], item.ItemName);
    }



    public void CollectItem(EvidenceStuff evidence)
    {
        if (evidence.CollectStatus != CollectStatus.Positive)
            return;
        MotiveProgresses[evidence.EndingType].Collect(evidence);
        EvidenceCollectEvent?.Invoke(MotiveProgresses[evidence.EndingType], evidence.EvidenceName);
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
        itemInUse = null;
        ItemUnselectEvent?.Invoke(itemInUse);
    }



    public bool IsUsingItemTypeMatched(ItemType itemType)
    {
        if (itemInUse == null)
            return itemType == ItemType.None;
        return itemInUse.Type == itemType; 
    }



    public bool GetTriggerValue()
    {
        return !IsSleeping;
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



    public CollectStatus GetStatus(EvidenceStuff evidence)
    {
        int evidenceCount = Motive.evidences.Count;
        for (int i = 0; i < evidenceCount; ++i)
        {
            Evidence memory = Motive.evidences[i];
            int itemCount = memory.itemNames.Count;

            int targetItemIdx = 0;
            while (targetItemIdx < itemCount)
            {
                if (memory.itemNames[targetItemIdx] == evidence.EvidenceName)
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



    public void Collect(EvidenceStuff evidence)
    {
        int evidenceCount = Motive.evidences.Count;
        for (int i = 0; i < evidenceCount; ++i)
        {
            if (IsEvidenceCleared(i))
                continue;
            string collectibleItemName = Motive.evidences[i].itemNames[ClearedItemIdx[i]];
            if (evidence.EvidenceName == collectibleItemName)
            {
                evidence.CollectStatus = CollectStatus.Finished;
                ++ClearedItemIdx[i];
                return;
            }
        }
        Debug.LogError(evidence.EvidenceName + " is not collectible.");
    }



    public bool IsEvidenceCleared(int evidenceIdx)
    {
        int evidenceItemCount = Motive.evidences[evidenceIdx].itemNames.Count;
        int collectedItemCount = ClearedItemIdx[evidenceIdx];
        return collectedItemCount >= evidenceItemCount;
    }
}
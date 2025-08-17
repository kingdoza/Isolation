using UnityEngine;

public class EvidenceStuff : DialogueStuff
{
    [SerializeField] private EndingType endingType;
    [SerializeField] private string evidenceName;
    [SerializeField] private string[] collectBeforeDialogues;
    [SerializeField] private string[] playerSleepDialogues;
    public string EvidenceName => evidenceName;
    public string[] Dialogues => dialogues;
    protected override StuffTypeData StuffData => GameData.EvidenceStuffData;
    public EndingType EndingType => endingType;
    private CollectStatus collectStatus = CollectStatus.Negative;
    public CollectStatus CollectStatus
    {
        get => collectStatus; set
        {
            if (value == CollectStatus.Null)
                Debug.LogError(gameObject.name + "'s CollectStatus is Null.");
            collectStatus = value;
        }
    }



    protected override void Awake()
    {
        base.Awake();
        Player.Instance.EvidenceCollectEvent.AddListener(OnPlayerCollectedEvidence);
        CollectStatus = Player.Instance.GetCollectStatus(this);
    }



    protected override void ChangeInputStatus()
    {
        if (!enabled) return;
        isItemMatched = Player.Instance.IsUsingItemTypeMatched(interactItem);
        isValidTime = Player.Instance.IsSleeping ? sleepEnabled : wakeEnabled;
        bool isUnfinished = Player.Instance.IsSleeping || collectStatus != CollectStatus.Finished;
        inputComp.SetStatus(isItemMatched && !isCovered && isValidTime && isUnfinished);
    }



    protected override string[] GetPrintTargetDialogues()
    {
        if (Player.Instance.IsSleeping)
            return playerSleepDialogues;
        if (collectStatus == CollectStatus.Negative)
            return collectBeforeDialogues;
        if (collectStatus == CollectStatus.Positive)
            return dialogues;
        return null;
    }



    private void OnPlayerCollectedEvidence(MotiveProgress motive, string name)
    {
        CollectStatus = Player.Instance.GetCollectStatus(this);
        ChangeInputStatus();
    }



    public override void OnDialogueClosed()
    {
        //base.OnDialogueClosed();
        if (CollectStatus == CollectStatus.Positive)
        {
            Debug.Log("Collect");
            GameManager.Instance.Player.CollectItem(this);
        }
        base.OnDialogueClosed();
    }
}

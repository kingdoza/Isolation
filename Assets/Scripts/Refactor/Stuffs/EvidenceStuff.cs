using UnityEngine;

public class EvidenceStuff : DialogueStuff
{
    [SerializeField] private EndingType endingType;
    [SerializeField] private string evidenceName;
    public EndingType EndingType => endingType;
    public string EvidenceName => evidenceName;
    [SerializeField] private CollectStatus collectStatus = CollectStatus.Negative;
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



    private void OnPlayerCollectedEvidence(MotiveProgress motive, string name)
    {
        CollectStatus = Player.Instance.GetCollectStatus(this);
    }



    public override void OnDialogueClosed()
    {
        base.OnDialogueClosed();
        if (CollectStatus == CollectStatus.Positive)
        {
            Debug.Log("Collect");
            GameManager.Instance.Player.CollectItem(this);
        }
    }
}

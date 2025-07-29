using UnityEngine;

public class TriggerItem : DialogueItem
{
    [SerializeField] private EndingType endingType;
    [SerializeField] private CollectStatus collectStatus = CollectStatus.Negative;

    public EndingType EndingType => endingType;
    public CollectStatus CollectStatus { get => collectStatus; set
        {
            if (value == CollectStatus.Null)
                Debug.LogError(gameObject.name + "'s CollectStatus is Null.");
            collectStatus = value;
        } 
    }



    public override void Interact()
    {
        base.Interact();
    }



    public override void OnDiaglogueStart()
    {
        base.OnDiaglogueStart();
    }



    public override void OnDialogueEnd()
    {
        base.OnDialogueEnd();
        if(CollectStatus == CollectStatus.Positive)
        {
            GameManager.Instance.Player.CollectItem(this);
            GameManager.Instance.InteractController.SetTriggerItems();
        }
    }
}



public enum CollectStatus
{
    Null, Negative, Positive, Finished
}
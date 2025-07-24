using UnityEngine;

public class DialogueItem : Item
{
    [TextArea] [SerializeField] private string[] wakeupDialogs;
    [TextArea] [SerializeField] private string[] sleepDialogs;

    public string[] WakeupDialogs => wakeupDialogs;
    public string[] SleepDialogs => sleepDialogs;



    public override void Interact()
    {
        base.Interact();
        GameManager.Instance.DialogueController.StartItemDialogSequence(this);
    }
}

using UnityEngine;
using static UnityEditor.Progress;

public class DialogueItem : Item
{
    [TextArea] [SerializeField] private string[] wakeupDialogs;
    [TextArea] [SerializeField] private string[] sleepDialogs;
    private DialogueController dialogueController;
    private RoomController roomController;

    public string[] WakeupDialogs => wakeupDialogs;
    public string[] SleepDialogs => sleepDialogs;



    protected override void Start()
    {
        base.Start();
        dialogueController = GameManager.Instance.DialogueController;
        roomController = GameManager.Instance.RoomController;
    }



    public override void Interact()
    {
        base.Interact();
        dialogueController.StartItemDialogSequence(this);
    }



    public virtual void OnDiaglogueStart()
    {
        roomController.SetOtherRoomObjectsColor(gameObject, dialogueController.DarkenColor);
        GameManager.Instance.UIController.DisableMoveButtons();
    }



    public virtual void OnDialogueEnd()
    {
        roomController.SetOtherRoomObjectsColor(gameObject, Color.white);
        GameManager.Instance.UIController.EnableMoveButtons();
    }
}

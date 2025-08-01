using UnityEngine;

public class DialogueItem : Item
{
    [TextArea] [SerializeField] private string[] wakeupDialogs;
    [TextArea] [SerializeField] private string[] sleepDialogs;
    private DialogueController dialogueController;
    private RoomController roomController;
    private int originSortingLayer;
    private bool isDialogueProcess = false;

    public string[] WakeupDialogs => wakeupDialogs;
    public string[] SleepDialogs => sleepDialogs;



    protected override void Awake()
    {
        base.Awake();
        originSortingLayer = GetComponent<SpriteRenderer>().sortingOrder;
        dialogueController = GameManager.Instance.DialogueController;
        roomController = GameManager.Instance.RoomController;
    }



    protected override void Start()
    {
        base.Start();
        RegisterInteractCondition(() => !isDialogueProcess);
    }



    public override void Interact()
    {
        base.Interact();
        dialogueController.StartItemDialogSequence(this);
    }



    public virtual void OnDiaglogueStart()
    {
        //roomController.SetOtherRoomObjectsColor(gameObject, dialogueController.DarkenColor);
        GameManager.Instance.UIController.DisableMoveButtons();
        GetComponent<SpriteRenderer>().sortingOrder = 100;
        isDialogueProcess = true;
        //CanInteract = false;
    }



    public virtual void OnDialogueEnd()
    {
        //roomController.SetOtherRoomObjectsColor(gameObject, Color.white);
        GameManager.Instance.UIController.EnableMoveButtons();
        GetComponent<SpriteRenderer>().sortingOrder = originSortingLayer;
        isDialogueProcess = false;
        //CanInteract = true;
    }
}

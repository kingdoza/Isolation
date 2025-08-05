using UnityEngine;

[RequireComponent(typeof(Click))]
[RequireComponent(typeof(CursorHover))]
public class DialogueStuff : ClickableStuff
{
    [TextArea][SerializeField] private string[] dialogues;
    private DialogueController dialogueController;
    private int originSortingLayer;
    private bool isDialogueProcess = false;
    protected override StuffTypeData StuffData => GameData.DialogueStuffData;



    protected override void Awake()
    {
        base.Awake();
        originSortingLayer = GetComponent<SpriteRenderer>().sortingOrder;
        dialogueController = GameManager.Instance.DialogueController;
    }


    protected override void OnClicked()
    {
        base.OnClicked();
        OnDiaglogueStart();
        dialogueController.DiagloueEndEvent.AddListener(OnDialogueClosed);
        dialogueController.StartDialogueSequence(dialogues);
    }



    public virtual void OnDiaglogueStart()
    {
        //roomController.SetOtherRoomObjectsColor(gameObject, dialogueController.DarkenColor);
        GameManager.Instance.UIController.DisableMoveButtons();
        GetComponent<SpriteRenderer>().sortingOrder = 100;
        isDialogueProcess = true;
        //CanInteract = false;
    }



    public virtual void OnDialogueClosed()
    {
        //roomController.SetOtherRoomObjectsColor(gameObject, Color.white);
        GameManager.Instance.UIController.EnableMoveButtons();
        GetComponent<SpriteRenderer>().sortingOrder = originSortingLayer;
        isDialogueProcess = false;
        dialogueController.DiagloueEndEvent.RemoveListener(OnDialogueClosed);
        //CanInteract = true;
    }
}

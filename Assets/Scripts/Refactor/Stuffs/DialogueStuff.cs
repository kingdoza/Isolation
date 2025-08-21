using UnityEngine;

[RequireComponent(typeof(Click))]
[RequireComponent(typeof(CursorHover))]
public class DialogueStuff : ClickableStuff
{
    [TextArea][SerializeField] protected string[] dialogues;
    protected DialogueController dialogueController;
    private int originSortingLayer;
    private bool isDialogueProcess = false;
    protected override StuffTypeData StuffData => GameData.DialogueStuffData;



    protected override void Awake()
    {
        base.Awake();
        if (dialogues.Length <= 0)
        {
            dialogues = new string[] { "(흔적 스크립트)" };
        }
        originSortingLayer = GetComponent<SpriteRenderer>().sortingOrder;
        dialogueController = GameManager.Instance.DialogueController;
    }


    protected override void OnClicked()
    {
        if (!enabled) return;
        if (dialogues.Length <= 0) return;
        OnDiaglogueStart();
        dialogueController.StartDialogueSequence(GetPrintTargetDialogues());
        base.OnClicked();
        dialogueController.DiagloueEndEvent.AddListener(OnDialogueClosed);
    }



    protected virtual string[] GetPrintTargetDialogues()
    {
        return dialogues;
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
        TimeController.Instance.CheckTimeChanged();
        //CanInteract = true;
    }
}

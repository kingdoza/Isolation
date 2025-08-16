using UnityEngine;
using static EtcUtils;
using static UnityEngine.EventSystems.EventTrigger;

[RequireComponent(typeof(CursorHover))]
public abstract class BaseStuff : SingleConditonActivator
{
    [SerializeField] protected ItemType interactItem = ItemType.None;
    [SerializeField] protected bool wakeEnabled;
    [SerializeField] protected bool sleepEnabled;
    [SerializeField] protected AudioClip sfxClip;
    [SerializeField] protected Color hoverColor = Color.gray;
    protected abstract StuffTypeData StuffData { get; }
    protected MouseInteraction inputComp;
    protected CursorHover hoverComp;
    protected Collider2D colliderComp;
    protected Color originalColor;

    protected bool isCovered = false;
    public bool IsCovered { get => isCovered; set {
            if (isCovered != value)
            {
                isCovered = value;
                ChangeInputStatus();
            }
        }
     }

    protected bool isItemMatched = false;
    protected bool isValidTime = false;



    protected new virtual void Awake()
    {
        //base.Awake();
        triggerEvent = new(GlobalTriggerEvent.PlayerWakeup);
        InitTrigger(triggerEvent);
        inputComp = GetComponent<MouseInteraction>();
        hoverComp = GetComponent<CursorHover>();
        colliderComp = GetComponent<Collider2D>();
        if (GetComponent<SpriteRenderer>())
            originalColor = GetComponent<SpriteRenderer>().color;

        if (!inputComp || !hoverComp || !colliderComp)
        {
            PrintErrorLog(gameObject, Error.NullComp);
        }

        hoverComp.CursorEnterEvent.AddListener(OnCursorEntered);
        hoverComp.CursorExitEvent.AddListener(OnCursorExited);
        Player.Instance.ItemSelectEvent.AddListener(PlayerChangedItem);
        Player.Instance.ItemUnselectEvent.AddListener(PlayerChangedItem);
    }



    private void PlayerChangedItem(ItemData item)
    {
        ChangeInputStatus();
    }



    private void OnEnable()
    {
        ChangeInputStatus();
    }



    protected virtual void ChangeInputStatus()
    {
        if (!enabled) return;
        isItemMatched = Player.Instance.IsUsingItemTypeMatched(interactItem);
        isValidTime = Player.Instance.IsSleeping ? sleepEnabled : wakeEnabled;
        inputComp.SetStatus(isItemMatched && !isCovered && isValidTime);
    }



    protected virtual void OnCursorEntered()
    {
        if (!enabled) return;
        if (GetComponent<SpriteRenderer>())
            GetComponent<SpriteRenderer>().color = hoverColor;
        SetCursorTexture(StuffData.CursorTexture);
    }



    protected virtual void OnCursorExited()
    {
        if (!enabled) return;
        if (GetComponent<SpriteRenderer>())
            GetComponent<SpriteRenderer>().color = originalColor;
        SetCursorTexture();
    }



    protected override void SetTrueComponent()
    {
        isValidTime = wakeEnabled;
        ChangeInputStatus();
    }



    protected override void SetFalseComponent()
    {
        isValidTime = sleepEnabled;
        ChangeInputStatus();
    }
}

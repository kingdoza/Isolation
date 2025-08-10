using UnityEngine;
using static EtcUtils;

[RequireComponent(typeof(CursorHover))]
public abstract class BaseStuff : MonoBehaviour
{
    [SerializeField] protected ItemType interactItem = ItemType.None;
    protected abstract StuffTypeData StuffData { get; }
    protected MouseInteraction inputComp;
    protected CursorHover hoverComp;
    protected Collider2D colliderComp;
    protected Color originalColor;

    [SerializeField] private bool isCovered = false;
    public bool IsCovered { get => isCovered; set {
            if (isCovered != value)
            {
                isCovered = value;
                ChangeInputStatus();
            }
        }
     }

    private bool isItemMatched = false;



    protected virtual void Awake()
    {
        inputComp = GetComponent<MouseInteraction>();
        hoverComp = GetComponent<CursorHover>();
        colliderComp = GetComponent<Collider2D>();
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
        inputComp.SetStatus(isItemMatched && !isCovered);
    }



    protected virtual void OnCursorEntered()
    {
        if (!enabled) return;
        GetComponent<SpriteRenderer>().color = Color.gray;
        SetCursorTexture(StuffData.CursorTexture);
    }



    protected virtual void OnCursorExited()
    {
        if (!enabled) return;
        GetComponent<SpriteRenderer>().color = originalColor;
        SetCursorTexture();
    }
}

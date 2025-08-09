using UnityEngine;
using static EtcUtils;
using UnityEngine.Events;

[RequireComponent(typeof(CursorHover))]
public abstract class BaseStuff : MonoBehaviour
{
    protected abstract StuffTypeData StuffData { get; }
    protected MouseInteraction inputComp;
    protected CursorHover hoverComp;
    protected Collider2D colliderComp;

    protected Color originalColor;



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

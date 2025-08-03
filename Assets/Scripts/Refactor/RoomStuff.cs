using UnityEngine;
using static EtcUtils;
using UnityEngine.Events;

[RequireComponent(typeof(CursorHover))]
public class RoomStuff : MonoBehaviour
{
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
        GetComponent<SpriteRenderer>().color = Color.gray;
    }



    protected virtual void OnCursorExited()
    {
        GetComponent<SpriteRenderer>().color = originalColor;
    }
}

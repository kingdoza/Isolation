using UnityEngine;
using static EtcUtils;
using UnityEngine.Events;

[RequireComponent(typeof(CursorHover))]
public class RoomObject : MonoBehaviour
{
    private MouseInteraction inputComp;
    private CursorHover hoverComp;
    private Collider2D colliderComp;

    private Color originalColor;



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



    private void OnCursorEntered()
    {
        GetComponent<SpriteRenderer>().color = Color.gray;
    }



    private void OnCursorExited()
    {
        GetComponent<SpriteRenderer>().color = originalColor;
    }
}

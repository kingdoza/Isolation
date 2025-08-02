using UnityEngine;
using UnityEngine.Events;

public class CursorHover : MonoBehaviour
{
    public bool IsHover { get; private set; } = false;
    [HideInInspector] public UnityEvent CursorEnterEvent = new UnityEvent();
    [HideInInspector] public UnityEvent CursorExitEvent = new UnityEvent();



    public void OnCursorEnter()
    {
        IsHover = true;
        CursorEnterEvent?.Invoke();
    }



    public void OnCursorExit()
    {
        IsHover = false;
        CursorExitEvent?.Invoke();
    }
}

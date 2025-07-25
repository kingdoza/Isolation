using UnityEngine;
using UnityEngine.EventSystems;

public class RoomDragScroller : MonoBehaviour
{
    [SerializeField] private Camera targetCamera;
    private Vector2 camViewSize;
    private Vector2 scrollMinPos;
    private Vector2 scrollMaxPos;


    private void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
    }

    private void OnMouseDrag()
    {
        Debug.Log("드래그 중 on...");
    }
}

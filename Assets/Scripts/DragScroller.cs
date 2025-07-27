using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DragScroller : MonoBehaviour
{
    [SerializeField] private LayoutElement rightLayout;
    private List<Func<bool>> dragConditions = new List<Func<bool>>();
    public bool CanDrag => dragConditions.All(cond => cond());

    private Vector2 camViewSize;
    private Vector2 scrollMinPos;
    private Vector2 scrollMaxPos;

    private Vector3 dragOrigin;
    private new Camera camera;

    private static bool isDragging = false;
    public static bool IsDragging => isDragging;



    private void Awake()
    {
        camera = GetComponent<Camera>();
        camViewSize.y = camera.orthographicSize * 2f;
        camViewSize.x = camViewSize.y * camera.aspect;
    }



    private void Update()
    {
        if (CanDrag)
            HandleDrag();
        else
            isDragging = false;
    }


    
    private void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!CanDrag)
            {
                isDragging = false;
                return;
            }
            dragOrigin = camera.ScreenToWorldPoint(Input.mousePosition);
            isDragging = false;
        }

        if (Input.GetMouseButton(0))
        {
            if (!CanDrag)
            {
                isDragging = false;
                return;
            }

            Vector3 current = camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = (dragOrigin - current);

            if (difference.magnitude > 0.05f)
            {
                isDragging = true;

                camera.transform.position += difference;
                Vector3 clampedPosition = camera.transform.position;
                clampedPosition.x = Mathf.Clamp(clampedPosition.x, scrollMinPos.x, scrollMaxPos.x);
                clampedPosition.y = Mathf.Clamp(clampedPosition.y, scrollMinPos.y, scrollMaxPos.y);
                camera.transform.position = clampedPosition;

                dragOrigin = camera.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }



    public void InitPosAndSetView(GameObject viewObject)
    {
        transform.position = new Vector3(0, 0, -10);

        Bounds viewBounds = viewObject.GetComponent<SpriteRenderer>().bounds;
        scrollMinPos = (Vector2)viewBounds.min + camViewSize / 2;
        scrollMaxPos = (Vector2)viewBounds.max - camViewSize / 2;
        float rightWorldOffset = (rightLayout.preferredWidth / Screen.width) * camViewSize.x;
        scrollMaxPos.x += rightWorldOffset;
        scrollMinPos += Vector2.one * 0.05f;
        scrollMaxPos -= Vector2.one * 0.05f;
    }



    public void RegisterCondition(Func<bool> condition)
    {
        if (!dragConditions.Contains(condition))
            dragConditions.Add(condition);
    }



    public void UnregisterCondition(Func<bool> condition)
    {
        dragConditions.Remove(condition);
    }
}

using System;
using UnityEngine;
using static ControllerUtils;

[RequireComponent(typeof(CursorHover))]
public class DraggableStuff : BaseStuff
{
    protected override StuffTypeData StuffData => GameData.DragStuffData;
    private bool isDragging = false;
    private bool isDragStop = true;
    private Vector2 lastPosition;



    protected override void Awake()
    {
        base.Awake();
        lastPosition = transform.position;
        (inputComp as Drag).DragEndEvent.AddListener(OnDragCompleted);
        (inputComp as Drag).DragStartEvent.AddListener(() => 
        {
            isDragging = true;
            isDragStop = true;
        });
        (inputComp as Drag).DragEndEvent.AddListener(() =>
        {
            isDragging = false;
            OnDragStop();
        });
    }



    private void FixedUpdate()
    {
        if (isDragging)
        {
            //Debug.Log(Vector2.Distance(lastPosition, transform.position));
            if (Vector2.Distance(lastPosition, transform.position) <= 0.001f)
            {
                if (isDragStop == false)
                {
                    isDragStop = true;
                    Debug.Log("DragStop");
                    OnDragStop();
                }
            }
            else
            {
                if (isDragStop)
                {
                    isDragStop = false;
                    Debug.Log("DragResume");
                    OnDragResume();
                }
            }
            lastPosition = transform.position;
        }
    }



    private void OnDragStop()
    {
        PlaySFX(null);
    }



    private void OnDragResume()
    {
        PlaySFX(sfxClip);
    }



    private void OnDragCompleted()
    {
        TimeController.Instance.ProgressMinutes(StuffData.MinuteWaste);
        TimeController.Instance.CheckTimeChanged();
    }
}

using System;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using static ControllerUtils;

[RequireComponent(typeof(CursorHover))]
public class DraggableStuff : BaseStuff
{
    [SerializeField] private AudioClip pickClip;
    [SerializeField] private AudioClip dragClip;
    [SerializeField] private AudioClip putClip;
    private AudioSource draggingSource;
    protected override StuffTypeData StuffData => GameData.DragStuffData;
    private bool isDragging = false;
    private bool isDragStop = true;
    private Vector2 lastPosition;



    protected override void Awake()
    {
        base.Awake();
        draggingSource = gameObject.AddComponent<AudioSource>();
        SetLoopSFXAudioSource(ref draggingSource, dragClip);
        lastPosition = transform.position;
        (inputComp as Drag).DragEndEvent.AddListener(OnDragEnd);
        (inputComp as Drag).DragStartEvent.AddListener(OnDragStart);
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
        draggingSource.Stop();
    }



    private void OnDragResume()
    {
        draggingSource.Play();
    }



    private void OnDragStart()
    {
        isDragging = true;
        isDragStop = true;
        PlaySFX(pickClip);
    }



    private void OnDragEnd()
    {
        isDragging = false;
        OnDragStop();
        PlaySFX(putClip);
        TimeController.Instance.ProgressMinutes(StuffData.MinuteWaste);
        TimeController.Instance.CheckTimeChanged();
    }
}

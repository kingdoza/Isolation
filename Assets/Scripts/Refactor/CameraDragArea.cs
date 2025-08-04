using TMPro;
using UnityEngine;
using static EtcUtils;

public class CameraDragArea : MouseInteraction
{
    private static DragData _camDragData;
    protected override string InputLayerName => "CameraMovable";
    public override bool IsPassDown => true;
    [SerializeField] private float sensitivity;
    [SerializeField] private float smoothTime;

    private Vector2 camViewSize;
    private Vector2 scrollMinPos;
    private Vector2 scrollMaxPos;

    private Vector3 dragOrigin;
    private Vector3 targetPosition;
    private Vector3 smoothVel;
    private Camera mainCamera;

    private float dragDistance;



    protected override void Awake()
    {
        base.Awake();
        if (_camDragData == null)
            _camDragData = Camera.main.GetComponent<DragData>();

        mainCamera = Camera.main;
        SetCamereViewSize();
        SetMovementLimit();
        OnInteractStart();
    }



    private void Update()
    {
        mainCamera.transform.position = Vector3.SmoothDamp(
            mainCamera.transform.position,
            targetPosition,
            ref smoothVel,
            _camDragData.SmoothTime
        );
    }



    private void SetCamereViewSize()
    {
        camViewSize.y = mainCamera.orthographicSize * 2f;
        camViewSize.x = camViewSize.y * mainCamera.aspect;
    }



    private void SetMovementLimit()
    {
        Bounds viewBounds = GetComponent<SpriteRenderer>().bounds;
        scrollMinPos = (Vector2)viewBounds.min + camViewSize / 2;
        scrollMaxPos = (Vector2)viewBounds.max - camViewSize / 2;
        scrollMinPos += Vector2.one * 0.01f;
        scrollMaxPos -= Vector2.one * 0.01f;
    }



    public override void OnInteractStart()
    {
        targetPosition = mainCamera.transform.position;
        dragOrigin = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        dragDistance = 0;
    }



    public override void OnInteracting()
    {
        Vector3 current = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 difference = dragOrigin - current;
        if (difference.magnitude >= Mathf.Epsilon)
        {
            targetPosition = mainCamera.transform.position + difference * _camDragData.Sensitivity;
            targetPosition.x = Mathf.Clamp(targetPosition.x, scrollMinPos.x, scrollMaxPos.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, scrollMinPos.y, scrollMaxPos.y);
            dragDistance += difference.magnitude;
        }
        dragOrigin = current;
    }



    public override void OnInteractEnd()
    {
        
    }



    public override bool HasInteracted()
    {
        return dragDistance >= 0.02f;
    }
}

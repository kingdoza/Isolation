using UnityEngine;

[RequireComponent(typeof(CameraDragArea))]
public class CameraEdgeScroll : MonoBehaviour
{
    [Header("모서리 스크롤 설정")]
    [SerializeField]
    [Tooltip("가장자리 픽셀")]
    private float edgeThreshold = 50f;

    [SerializeField]
    [Tooltip("카메라 스크롤 속도")]
    private float scrollSpeed = 10f;
    private CameraDragArea cameraDragArea;
    private bool isMouseDragging = false;

    private void Awake()
    {
        cameraDragArea = GetComponent<CameraDragArea>();
    }

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            isMouseDragging = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isMouseDragging = false;
        }

        
        if (isMouseDragging)
        {
            return;
        }

        HandleEdgeScrolling();
    }

    private void HandleEdgeScrolling()
    {
        Vector2 mousePosition = Input.mousePosition;
        Vector3 moveDirection = Vector3.zero;

        
        if (mousePosition.x > Screen.width - edgeThreshold)
        {
            moveDirection.x += 1;
        }
        
        if (mousePosition.x < edgeThreshold)
        {
            moveDirection.x -= 1;
        }
      
        if (mousePosition.y > Screen.height - edgeThreshold)
        {
            moveDirection.y += 1;
        }
        
        if (mousePosition.y < edgeThreshold)
        {
            moveDirection.y -= 1;
        }

        
        // moveDirection.normalized
        
        Vector3 delta = moveDirection.normalized * scrollSpeed * Time.deltaTime;

        // 이동할 방향 존재
        if (delta != Vector3.zero)
        {
            cameraDragArea.MoveCamera(delta);
        }
    }
}
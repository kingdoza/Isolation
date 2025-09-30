using UnityEngine;

[RequireComponent(typeof(CameraDragArea))]
public class CameraEdgeScroll : MonoBehaviour
{
    [Header("모서리 스크롤 설정")]
    [SerializeField]
    [Tooltip("픽셀 단위")]
    private float edgeThickness = 30f; // 픽셀 단위 경계

    [SerializeField]
    [Tooltip("카메라 스크롤 최대 속도")]
    private float scrollSpeed = 15f;

    private CameraDragArea cameraDragArea;
    private bool isMouseDragging = false;

    private Tutorial tutorial;

    private void Awake()
    {
        tutorial = FindAnyObjectByType<Tutorial>();
        cameraDragArea = GetComponent<CameraDragArea>();
    }

    private void Update()
    {
        if (tutorial.IsProgessing)
            return;
        if (Input.GetMouseButtonDown(0)) isMouseDragging = true;
        if (Input.GetMouseButtonUp(0)) isMouseDragging = false;
        if (isMouseDragging) return;

        HandleEdgeScroll();
    }

    private void HandleEdgeScroll()
    {
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 mouseOffset = new Vector2(Input.mousePosition.x - screenCenter.x,
                                          Input.mousePosition.y - screenCenter.y);

        float halfWidth = Screen.width / 2f;
        float halfHeight = Screen.height / 2f;

        
        float xStrength = Mathf.Max(Mathf.Abs(mouseOffset.x) - (halfWidth - edgeThickness), 0f) / edgeThickness;
        float yStrength = Mathf.Max(Mathf.Abs(mouseOffset.y) - (halfHeight - edgeThickness), 0f) / edgeThickness;
        float strength = Mathf.Clamp01(Mathf.Max(xStrength, yStrength));

        if (strength > 0f)
        {
            
            Vector2 moveDir = mouseOffset.normalized;

            // 최종
            Vector3 delta = new Vector3(moveDir.x, moveDir.y, 0f) * strength * scrollSpeed * Time.deltaTime;
            cameraDragArea.MoveCamera(delta);
        }
    }
}

using UnityEngine;

public class EgdeScroller : MonoBehaviour
{
    public float edgeSize = 50f;         // 엣지 감지 범위 (픽셀)
    public float maxSpeed = 10f;         // 엣지에 완전히 붙었을 때의 최대 속도

    private Vector2 camViewSize;
    private Vector2 scrollMinPos;
    private Vector2 scrollMaxPos;



    void Start()
    {
        Camera camera = GetComponent<Camera>();
        camViewSize.y = camera.orthographicSize * 2f;
        camViewSize.x = camViewSize.y * camera.aspect;
    }



    public void InitPosAndSetView(GameObject viewObject)
    {
        transform.position = Vector3.zero;

        Bounds viewBounds = viewObject.GetComponent<SpriteRenderer>().bounds;
        scrollMinPos = (Vector2)viewBounds.min + camViewSize / 2;
        scrollMaxPos = (Vector2)viewBounds.max - camViewSize / 2;
        scrollMinPos += new Vector2(5, 5);
        scrollMaxPos -= new Vector2(5, 5);
    }



    void Update()
    {
        Vector3 movement = Vector3.zero;
        Vector3 mousePos = Input.mousePosition;
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // 왼쪽
        if (mousePos.x < edgeSize)
        {
            float percent = 1f - (mousePos.x / edgeSize);
            movement.x -= percent * maxSpeed * Time.deltaTime;
        }

        // 오른쪽
        if (mousePos.x > screenWidth - edgeSize)
        {
            float percent = (mousePos.x - (screenWidth - edgeSize)) / edgeSize;
            movement.x += percent * maxSpeed * Time.deltaTime;
        }

        // 아래쪽
        if (mousePos.y < edgeSize)
        {
            float percent = 1f - (mousePos.y / edgeSize);
            movement.y -= percent * maxSpeed * Time.deltaTime;
        }

        // 위쪽
        if (mousePos.y > screenHeight - edgeSize)
        {
            float percent = (mousePos.y - (screenHeight - edgeSize)) / edgeSize;
            movement.y += percent * maxSpeed * Time.deltaTime;
        }

        // 카메라 위치 이동
        Vector3 newPosition = transform.position + movement;

        // 이동 범위 제한 (선택)
        newPosition.x = Mathf.Clamp(newPosition.x, scrollMinPos.x, scrollMaxPos.x);
        newPosition.y = Mathf.Clamp(newPosition.y, scrollMinPos.y, scrollMaxPos.y);
        newPosition.z = -10;

        transform.position = newPosition;
    }
}

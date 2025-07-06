using UnityEngine;

public class EgdeScroller : MonoBehaviour
{
    public float scrollSpeed = 10f;
    public float borderThickness = 10f;
    public float minX = -50f, maxX = 50f;
    public float minZ = -50f, maxZ = 50f;

    [SerializeField] private float edgeThreshold = 50f;
    [SerializeField] private float maxScrollSpeed = 10f;



    void Update()
    {
        Vector3 pos = transform.position;
        Vector3 mousePos = Input.mousePosition;

        if (mousePos.x >= Screen.width - edgeThreshold)
        {
            pos.x += scrollSpeed * Time.deltaTime;
        }
        if (mousePos.x <= edgeThreshold)
        {
            pos.x -= scrollSpeed * Time.deltaTime;
        }
        if (mousePos.y >= Screen.height - edgeThreshold)
        {
            pos.z += scrollSpeed * Time.deltaTime;
        }
        if (mousePos.y <= edgeThreshold)
        {
            pos.z -= scrollSpeed * Time.deltaTime;
        }

        // Clamp to prevent camera from going out of bounds
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        transform.position = pos;
    }
}

using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenTarget : MonoBehaviour
{
    [Header("���� UI")]
    [SerializeField] private Canvas uiCanvas;        // Ŭ�� �̺�Ʈ ���� UI Canvas
    [SerializeField] private Camera uiCamera;        // UI Canvas ī�޶�
    [Header("����Ÿ�� ��� ������Ʈ")]
    [SerializeField] private MeshCollider targetMesh;  // RenderTexture�� �ѷ��� MeshCollider ������Ʈ
    [SerializeField] private Camera mainCamera;        // Ŭ�� �Է��� �޴� ���� ī�޶�

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (targetMesh.Raycast(ray, out RaycastHit hit, 100f))
            {
                // Ray�� MeshCollider�� �����
                Vector3 localHit = targetMesh.transform.InverseTransformPoint(hit.point);
                Bounds bounds = targetMesh.bounds;

                Vector2 uv;
                uv.x = Mathf.InverseLerp(bounds.min.x, bounds.max.x, hit.point.x);
                uv.y = Mathf.InverseLerp(bounds.min.y, bounds.max.y, hit.point.y);

                // UI Canvas Screen ��ǥ�� ��ȯ
                RectTransform canvasRect = uiCanvas.GetComponent<RectTransform>();
                Vector2 screenPos = new Vector2(
                    uv.x * uiCanvas.pixelRect.width,
                    uv.y * uiCanvas.pixelRect.height
                );

                // PointerEventData ����
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = screenPos;

                // GraphicRaycaster�� UI Ŭ�� ����
                List<RaycastResult> results = new List<RaycastResult>();
                GraphicRaycaster raycaster = uiCanvas.GetComponent<GraphicRaycaster>();
                raycaster.Raycast(eventData, results);

                foreach (var result in results)
                {
                    Debug.Log("UI Click: " + result.gameObject.name);
                    ExecuteEvents.Execute(result.gameObject, eventData, ExecuteEvents.pointerClickHandler);
                }
            }
        }
    }
}

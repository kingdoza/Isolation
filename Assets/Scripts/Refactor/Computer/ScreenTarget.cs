using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenTarget : MonoBehaviour
{
    [Header("원본 UI")]
    [SerializeField] private Canvas uiCanvas;        // 클릭 이벤트 받을 UI Canvas
    [SerializeField] private Camera uiCamera;        // UI Canvas 카메라
    [Header("렌더타켓 출력 오브젝트")]
    [SerializeField] private MeshCollider targetMesh;  // RenderTexture가 뿌려진 MeshCollider 오브젝트
    [SerializeField] private Camera mainCamera;        // 클릭 입력을 받는 메인 카메라

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (targetMesh.Raycast(ray, out RaycastHit hit, 100f))
            {
                // Ray가 MeshCollider에 닿았음
                Vector3 localHit = targetMesh.transform.InverseTransformPoint(hit.point);
                Bounds bounds = targetMesh.bounds;

                Vector2 uv;
                uv.x = Mathf.InverseLerp(bounds.min.x, bounds.max.x, hit.point.x);
                uv.y = Mathf.InverseLerp(bounds.min.y, bounds.max.y, hit.point.y);

                // UI Canvas Screen 좌표로 변환
                RectTransform canvasRect = uiCanvas.GetComponent<RectTransform>();
                Vector2 screenPos = new Vector2(
                    uv.x * uiCanvas.pixelRect.width,
                    uv.y * uiCanvas.pixelRect.height
                );

                // PointerEventData 생성
                PointerEventData eventData = new PointerEventData(EventSystem.current);
                eventData.position = screenPos;

                // GraphicRaycaster로 UI 클릭 전달
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

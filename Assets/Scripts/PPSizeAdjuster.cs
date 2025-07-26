#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Rendering;

[ExecuteAlways]
[RequireComponent(typeof(Volume))]
[RequireComponent(typeof(BoxCollider))]
public class PPSizeAdjuster : MonoBehaviour
{
    [SerializeField] private RectTransform uiReferenceArea; // 기준이 되는 UI
    [SerializeField] private Camera targetCamera; // Overlay일 땐 주 카메라 (필수)
    private BoxCollider boxCollider;
    private Volume volume;



    private void OnValidate()
    {
        if (Application.isPlaying)
            return;
#if UNITY_EDITOR
        EditorApplication.delayCall += ApplySizing;
#endif
    }



    private void ApplySizing()
    {
        if (this == null) // 또는: if (this == null || gameObject == null) 
        {
#if UNITY_EDITOR
            EditorApplication.delayCall -= ApplySizing; // 안전하게 콜백 해제
#endif
            return;
        }

        if (volume == null)
            volume = GetComponent<Volume>();

        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider>();

        if (volume == null || boxCollider == null || uiReferenceArea == null || targetCamera == null)
            return;

        // UI의 화면 좌표 (픽셀) 얻기
        Vector3[] corners = new Vector3[4];
        uiReferenceArea.GetWorldCorners(corners);

        // Overlay는 월드 좌표계가 아니므로 직접 변환 필요
        for (int i = 0; i < 4; i++)
            corners[i] = targetCamera.ScreenToWorldPoint(new Vector3(corners[i].x, corners[i].y, targetCamera.nearClipPlane + 1f)); // 약간의 깊이 보정

        Vector3 worldCorner0 = corners[0];
        Vector3 worldCorner2 = corners[2];

        // 중심 및 크기 계산
        Vector3 center = (worldCorner0 + worldCorner2) / 2f;
        Vector3 size = new Vector3(
            Mathf.Abs(worldCorner2.x - worldCorner0.x),
            Mathf.Abs(worldCorner2.y - worldCorner0.y),
            50f // Volume 영역의 깊이
        );

        boxCollider.center = transform.InverseTransformPoint(center);
        boxCollider.size = size;

        volume.isGlobal = false;
        volume.blendDistance = 0f;

#if UNITY_EDITOR
        EditorApplication.delayCall -= ApplySizing;
#endif
    }
}

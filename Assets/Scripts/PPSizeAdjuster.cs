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
    [SerializeField] private RectTransform uiReferenceArea; // ������ �Ǵ� UI
    [SerializeField] private Camera targetCamera; // Overlay�� �� �� ī�޶� (�ʼ�)
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
        if (this == null) // �Ǵ�: if (this == null || gameObject == null) 
        {
#if UNITY_EDITOR
            EditorApplication.delayCall -= ApplySizing; // �����ϰ� �ݹ� ����
#endif
            return;
        }

        if (volume == null)
            volume = GetComponent<Volume>();

        if (boxCollider == null)
            boxCollider = GetComponent<BoxCollider>();

        if (volume == null || boxCollider == null || uiReferenceArea == null || targetCamera == null)
            return;

        // UI�� ȭ�� ��ǥ (�ȼ�) ���
        Vector3[] corners = new Vector3[4];
        uiReferenceArea.GetWorldCorners(corners);

        // Overlay�� ���� ��ǥ�谡 �ƴϹǷ� ���� ��ȯ �ʿ�
        for (int i = 0; i < 4; i++)
            corners[i] = targetCamera.ScreenToWorldPoint(new Vector3(corners[i].x, corners[i].y, targetCamera.nearClipPlane + 1f)); // �ణ�� ���� ����

        Vector3 worldCorner0 = corners[0];
        Vector3 worldCorner2 = corners[2];

        // �߽� �� ũ�� ���
        Vector3 center = (worldCorner0 + worldCorner2) / 2f;
        Vector3 size = new Vector3(
            Mathf.Abs(worldCorner2.x - worldCorner0.x),
            Mathf.Abs(worldCorner2.y - worldCorner0.y),
            50f // Volume ������ ����
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

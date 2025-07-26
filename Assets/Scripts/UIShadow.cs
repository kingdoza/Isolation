using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways]
public class UIShadow : MonoBehaviour
{
    [SerializeField] private float shadowDistance;
    [SerializeField, Range(0f, 1f)] private float startAlpha;



#if UNITY_EDITOR
    private bool isUpdateScheduled = false;
#endif

    private void OnValidate()
    {
        if (Application.isPlaying)
            return;

#if UNITY_EDITOR
        if (!isUpdateScheduled)
        {
            isUpdateScheduled = true;
            EditorApplication.delayCall += DelayedApplyShadowSettings;
        }
#endif
    }

#if UNITY_EDITOR
    private void DelayedApplyShadowSettings()
    {
        isUpdateScheduled = false;

        if (this == null) return;  // 파괴되었으면 실행 안함
        ApplyShadowSettings();
    }
#endif



    private void ApplyShadowSettings()
    {
        Shadow[] shadowComps = GetComponents<Shadow>();
        int shadowCount = Mathf.Max(1, Mathf.RoundToInt(shadowDistance));

        if (shadowComps.Length < shadowCount)
        {
            int compsToAdd = shadowCount - shadowComps.Length;
            for (int i = 0; i < compsToAdd; ++i)
            {
                gameObject.AddComponent<Shadow>();
            }
        }
        else if(shadowComps.Length > shadowCount)
        {
            int compsToRemove = shadowComps.Length - shadowCount;
            for (int i = 0; i < compsToRemove; ++i)
            {
                RemoveComponent(shadowComps[i]);
            }
        }

        float distancePerComp = shadowDistance / shadowCount;

        Color baseColor = GetComponent<Image>().color;
        shadowComps = GetComponents<Shadow>();
        for(int i = 0; i < shadowCount; ++i)
        {
            shadowComps[i].effectDistance = new Vector2(-distancePerComp * (i + 1), 0);
            Color shadowColor = baseColor;
            shadowColor.a = Mathf.Lerp(startAlpha, 0, (float)i / shadowCount);
            shadowComps[i].effectColor = shadowColor;
        }
    }



    private void RemoveComponent(Component comp)
    {
        if (Application.isPlaying)
            Destroy(comp);
        else
            DestroyImmediate(comp);
    }
}

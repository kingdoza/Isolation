using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SceneController : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    private const float DadeDuration = 0.4f;



    private void Awake()
    {
        GameObject blackImageObj = new GameObject("SceneFadePanel");
        blackImageObj.transform.SetParent(canvas.transform, false);

        Image img = blackImageObj.AddComponent<Image>();
        img.color = Color.black;

        RectTransform rt = blackImageObj.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        img.DOFade(0f, DadeDuration) // 1.5초 동안 페이드아웃
            .SetEase(Ease.InOutQuad) // 부드럽게
            .OnComplete(() =>
            {
                blackImageObj.SetActive(false);
            });
    }
}

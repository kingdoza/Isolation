using DG.Tweening;
using UnityEngine;

public class TutorialArrow : MonoBehaviour
{
    public RectTransform rect;
    public float amplitude = 30f;   // 위아래 이동 거리 (유닛: 픽셀)
    public float duration = 0.8f;   // 한 방향으로 가는 시간
    private float startY;
    private Tween tween;

    void Awake()
    {
        startY = rect.anchoredPosition.y;
        //if (tween == null)
        //{
        //    tween = rect.DOAnchorPosY(startY + amplitude, duration)
        //        .SetLoops(-1, LoopType.Yoyo)
        //        .SetEase(Ease.InOutSine)
        //        .SetId(this)
        //        .SetUpdate(true)
        //        .Pause(); // 처음엔 멈춰둠
        //}
    }

    void OnEnable()
    {
        rect.anchoredPosition = new Vector2(rect.anchoredPosition.x, startY);
        rect.DOAnchorPosY(startY + amplitude, duration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .SetId(this)
            .SetUpdate(true);
    }

    private void OnDisable()
    {
        DOTween.Kill(this);
    }
}

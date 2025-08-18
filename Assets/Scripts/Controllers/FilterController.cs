using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FilterController : MonoBehaviour
{
    [SerializeField] private Volume filter;
    private ColorAdjustments colorAdjustments;



    private void Awake()
    {
        if (filter.profile.TryGet(out ColorAdjustments ca))
            colorAdjustments = ca;
        Player.Instance.EvidenceCollectEvent.AddListener(SetMotiveFilter);
    }



    private void SetMotiveFilter(MotiveProgress motive, string name)
    {
        float happyProgress = Player.Instance.GetMotivePercentage(EndingType.Happy);
        float badProgress = Player.Instance.GetMotivePercentage(EndingType.Bad);
        float targetSaturation = 0f;

        if (happyProgress >= badProgress)
        {
            targetSaturation = Mathf.Lerp(-50f, 0f, happyProgress);
        }
        else
        {
            targetSaturation = Mathf.Lerp(-50f, -100f, badProgress);
        }

        DOTween.To(
            () => colorAdjustments.saturation.value,
            x => colorAdjustments.saturation.value = x,
            targetSaturation,
            1f
        ).SetEase(Ease.Linear);
    }



    public void SetWakeup()
    {
        colorAdjustments.active = false;
    }



    public void SetSleep()
    {
        colorAdjustments.active = true;
    }
}

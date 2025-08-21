using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class FilterController : MonoBehaviour
{
    [SerializeField] private Volume filter;
    private ColorAdjustments colorAdjustments;
    private Vignette vignette;
    private float originalIntensity;
    private float originalSmoothness;



    private void Awake()
    {
        if (filter.profile.TryGet(out ColorAdjustments ca))
            colorAdjustments = ca;
        if (filter.profile.TryGet(out Vignette vn))
            vignette = vn;
        originalIntensity = vignette.intensity.value;
        originalSmoothness = vignette.smoothness.value;
        Player.Instance.EvidenceCollectEvent.AddListener(SetMotiveFilter);
    }



    public void SetMonitorNightFilter()
    {
        if (Player.Instance.IsSleeping == false)
            return;
        colorAdjustments.active = false;
        vignette.intensity.value = 0.5f;
        vignette.smoothness.value = 0.5f;
    }



    public void UnsetMonitorNightFilter()
    {
        colorAdjustments.active = true;
        vignette.intensity.value = originalIntensity;
        vignette.smoothness.value = originalSmoothness;
    }



    public void SetMotiveFilter(MotiveProgress motive, string name)
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
        vignette.intensity.value = originalIntensity;
        vignette.smoothness.value = originalSmoothness;
    }



    public void SetSleep()
    {
        colorAdjustments.active = true;
        vignette.intensity.value = originalIntensity;
        vignette.smoothness.value = originalSmoothness;
    }
}

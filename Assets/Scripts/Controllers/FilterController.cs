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
        float maxProgress = Mathf.Max(happyProgress, badProgress);
        int minus = badProgress > happyProgress ? -1 : 1;
        float saturationDiff = Mathf.Lerp(0, 50, maxProgress);

        Debug.Log(happyProgress);
        Debug.Log(badProgress);
        Debug.Log(saturationDiff);
        colorAdjustments.saturation.value += saturationDiff * minus;
        Debug.Log(colorAdjustments.saturation.value);
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

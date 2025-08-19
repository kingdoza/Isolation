using UnityEngine;
using static ControllerUtils;

[RequireComponent(typeof(Click))]
[RequireComponent(typeof(CursorHover))]
public class LightSwitchStuff : ClickableStuff
{
    protected override StuffTypeData StuffData => GameData.LightSwitchStuffData;



    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        if (Player.Instance.IsSleeping == false)
        {
            PlaySFX(SFXClips.lightSwitch_Off);
            Player.Instance.Sleep();
        }
        else
        {
            PlaySFX(SFXClips.lightSwitch_On);
            TimeController.Instance.ProgressToNextWakeup();
            TimeController.Instance.CheckTimeChanged();
        }
    }
}

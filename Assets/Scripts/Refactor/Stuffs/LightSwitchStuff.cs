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
        if (GameManager.Instance.TimeController.IsLastDay())
        {
            GameManager.Instance.UIController.ShowLightSwitchWarning();
            return;
        }

        if (Player.Instance.IsSleeping == false)
        {
            PlaySFX(SFXClips.lightSwitch_Off);
            Player.Instance.Sleep();
            TimeController.Instance.ShowWakeSleepDialogue();
        }
        else
        {
            PlaySFX(SFXClips.lightSwitch_On);
            TimeController.Instance.ProgressToNextWakeup();
            TimeController.Instance.CheckTimeChanged();
        }
    }
}

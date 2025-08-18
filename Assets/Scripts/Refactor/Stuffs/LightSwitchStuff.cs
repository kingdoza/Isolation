using UnityEngine;

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
            Player.Instance.Sleep();
        }
        else
        {
            TimeController.Instance.ProgressToNextWakeup();
            TimeController.Instance.CheckTimeChanged();
        }
    }
}

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
        Player.Instance.Sleep();
    }
}

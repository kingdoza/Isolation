using UnityEngine;

[RequireComponent(typeof(Click))]
[RequireComponent(typeof(CursorHover))]
public class LightSwitchStuff : ClickableStuff
{
    protected override StuffTypeData StuffData => GameData.LightSwitchStuffData;



    protected override void OnClicked()
    {
        base.OnClicked();
        Player.Instance.Sleep();
    }
}

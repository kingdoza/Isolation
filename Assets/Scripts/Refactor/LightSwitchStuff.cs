using UnityEngine;

[RequireComponent(typeof(Click))]
[RequireComponent(typeof(CursorHover))]
public class LightSwitchStuff : ClickableStuff
{
    protected override void OnClicked()
    {
        Player.Instance.Sleep();
    }
}

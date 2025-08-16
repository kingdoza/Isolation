using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenStuff : ClickableStuff
{
    protected override StuffTypeData StuffData => GameData.ScreenStuffData;



    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        transform.parent.GetComponentInChildren<ScreenTarget>().ClickCanvas();
    }
}

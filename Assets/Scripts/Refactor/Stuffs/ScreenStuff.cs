using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ControllerUtils;

public class ScreenStuff : ClickableStuff
{
    protected override StuffTypeData StuffData => GameData.ScreenStuffData;



    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        PlaySFX(SFXClips.computer_Mouse);
        transform.parent.GetComponentInChildren<ScreenTarget>().ClickCanvas();
    }



    private void Update()
    {
        if (inputComp.IsEnabled && Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
        {
            PlaySFX(SFXClips.computer_Keyboard);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ControllerUtils;

public class ScreenStuff : ClickableStuff
{
    protected override StuffTypeData StuffData => GameData.ScreenStuffData;
    private GameObject screenObject;



    protected override void Awake()
    {
        base.Awake();
        screenObject = GameObject.FindWithTag("ScreenCanvas");
    }



    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        PlaySFX(SFXClips.computer_Mouse);
        transform.parent.GetComponentInChildren<ScreenTarget>().ClickCanvas();
    }



    private void Update()
    {
        if (Player.Instance.IsSleeping && inputComp.IsEnabled && Input.anyKeyDown && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
        {
            PlaySFX(SFXClips.computer_Keyboard);
        }
    }



    protected override void OnEnable()
    {
        base.OnEnable();
        screenObject.SetActive(true);
    }



    protected void OnDisable()
    {
        screenObject.SetActive(false);
    }
}

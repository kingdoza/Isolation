using UnityEngine;
using UnityEngine.Events;
using static ControllerUtils;

[RequireComponent(typeof(Click))]
public class ClickableStuff : BaseStuff
{
    [HideInInspector] public UnityEvent ClickEvent = new();
    protected override StuffTypeData StuffData => GameData.ClickStuffData;



    protected override void Awake()
    {
        base.Awake();
        (inputComp as Click).ClickEvent.AddListener(OnClicked);
    }



    protected virtual void OnClicked()
    {
        GameManager.Instance.TimeController.ProgressMinutes(StuffData.MinuteWaste);
        PlaySFX(sfxClip);
        ClickEvent?.Invoke();
    }
}

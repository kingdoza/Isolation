using UnityEngine;

[RequireComponent(typeof(Click))]
public abstract class ClickableStuff : BaseStuff
{
    protected override void Awake()
    {
        base.Awake();
        (inputComp as Click).ClickEvent.AddListener(OnClicked);
    }



    protected virtual void OnClicked()
    {
        GameManager.Instance.TimeController.ProgressMinutes(StuffData.MinuteWaste);
    }
}

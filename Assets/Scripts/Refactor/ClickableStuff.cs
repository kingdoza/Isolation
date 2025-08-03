using UnityEngine;

[RequireComponent(typeof(Click))]
public abstract class ClickableStuff : RoomStuff
{
    protected override void Awake()
    {
        base.Awake();
        (inputComp as Click).ClickEvent.AddListener(OnClicked);
    }



    protected abstract void OnClicked();
}

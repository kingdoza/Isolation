using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Click))]
public class ClickableStuff : BaseStuff
{
    [HideInInspector] public UnityEvent ClickEvent = new();
    protected override StuffTypeData StuffData => GameData.ZoomStuffData;



    protected override void Awake()
    {
        base.Awake();
        (inputComp as Click).ClickEvent.AddListener(OnClicked);
    }



    protected virtual void OnClicked()
    {
        if (GameManager.Instance.RoomController.IsFocusIn == false)
        {
            GameManager.Instance.TimeController.ProgressMinutes(StuffData.MinuteWaste);
        }
        ClickEvent?.Invoke();
    }
}

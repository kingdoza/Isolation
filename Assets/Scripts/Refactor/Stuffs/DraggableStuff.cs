using UnityEngine;

[RequireComponent(typeof(CursorHover))]
[RequireComponent(typeof(LimitedDrag))]
public class DraggableStuff : BaseStuff
{
    protected override StuffTypeData StuffData => GameData.DragStuffData;



    protected override void Awake()
    {
        base.Awake();
        (inputComp as Drag).DragEndEvent.AddListener(OnDragCompleted);
    }



    private void OnDragCompleted()
    {
        TimeController.Instance.ProgressMinutes(StuffData.MinuteWaste);
        TimeController.Instance.CheckTimeChanged();
    }
}

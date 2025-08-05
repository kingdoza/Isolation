using UnityEngine;

[RequireComponent(typeof(CursorHover))]
[RequireComponent(typeof(LimitedDrag))]
public class DraggableStuff : BaseStuff
{
    protected override StuffTypeData StuffData => GameData.DragStuffData;
}

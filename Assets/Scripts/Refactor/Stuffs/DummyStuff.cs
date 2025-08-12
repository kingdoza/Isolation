using UnityEngine;

public class DummyStuff : BaseStuff
{
    protected override StuffTypeData StuffData => GameData.NoneStuffData;


    protected new void Awake()
    {
        hoverComp = GetComponent<CursorHover>();
        hoverComp.CursorEnterEvent.AddListener(OnCursorEntered);
        hoverComp.CursorExitEvent.AddListener(OnCursorExited);
    }



    protected override void ChangeInputStatus() { }
}

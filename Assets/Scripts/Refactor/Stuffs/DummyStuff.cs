using UnityEngine;

public class DummyStuff : BaseStuff
{
    protected override StuffTypeData StuffData => GameData.NoneStuffData;


    protected new void Awake() { }

    protected override void ChangeInputStatus() { }
}

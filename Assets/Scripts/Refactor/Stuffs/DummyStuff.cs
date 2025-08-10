using UnityEngine;

public class DummyStuff : BaseStuff
{
    protected override StuffTypeData StuffData => GameData.NoneStuffData;



    protected override void ChangeInputStatus() { }
}

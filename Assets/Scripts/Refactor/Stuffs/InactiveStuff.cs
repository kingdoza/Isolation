using UnityEngine;

public class InactiveStuff : BaseStuff
{
    protected override StuffTypeData StuffData => GameData.NoneStuffData;
    private MouseInteraction mouseInteraction;



    protected override void Awake()
    {
        base.Awake();
        mouseInteraction = GetComponent<MouseInteraction>();
    }



    private void OnEnable()
    {
        mouseInteraction.DisableInput();
    }



    private void OnDisable()
    {
        mouseInteraction.EnableInput();
    }
}

using UnityEngine;

public class ConditionalInput : SingleConditonActivator
{
    [SerializeField] private bool trueConditonStatus;
    [SerializeField] private bool falseConditonStatus;
    private MouseInteraction inputComp;



    protected override void Awake()
    {
        base.Awake();
        inputComp = GetComponent<MouseInteraction>();
    }

    

    protected override void SetFalseComponent()
    {
        inputComp.SetStatus(falseConditonStatus);
    }



    protected override void SetTrueComponent()
    {
        inputComp.SetStatus(trueConditonStatus);
    }
}

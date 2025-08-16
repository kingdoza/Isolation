using UnityEngine;

public class ConditionalStuff : SingleConditonActivator
{
    [SerializeField] private BaseStuff trueConditonStuff;
    [SerializeField] private BaseStuff falseConditonStuff;



    protected override void SetFalseComponent()
    {
        trueConditonStuff.enabled = false;
        falseConditonStuff.enabled = true;
    }



    protected override void SetTrueComponent()
    {
        falseConditonStuff.enabled = false;
        trueConditonStuff.enabled = true;
    }
}

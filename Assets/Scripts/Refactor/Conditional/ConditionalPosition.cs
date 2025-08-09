using UnityEngine;

public class ConditionalPosition : SingleConditonActivator
{
    [SerializeField] private Transform trueConditonMarker;
    [SerializeField] private Transform falseConditonMarker;
    private Vector3 trueConditonPos;
    private Vector3 falseConditonPos;



    protected override void Awake()
    {
        base.Awake();
        trueConditonPos = trueConditonMarker.position;
        falseConditonPos = falseConditonMarker.position;
    }



    protected override void SetFalseComponent()
    {
        transform.position = falseConditonPos;
    }



    protected override void SetTrueComponent()
    {
        transform.position = trueConditonPos;
    }
}

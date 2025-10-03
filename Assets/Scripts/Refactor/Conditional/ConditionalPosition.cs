using UnityEngine;

public class ConditionalPosition : SingleConditonActivator
{
    [SerializeField] private Transform trueConditonMarker;
    [SerializeField] private Transform falseConditonMarker;
    [SerializeField] private bool isStart = false;
    private Vector3 trueConditonPos;
    private Vector3 falseConditonPos;



    protected override void Awake()
    {
        if (isStart == false)
            base.Awake();
        trueConditonPos = trueConditonMarker.position;
        falseConditonPos = falseConditonMarker.position;
    }



    private void Start()
    {
        if (isStart)
        {
            InitTrigger(triggerEvent);
        }
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

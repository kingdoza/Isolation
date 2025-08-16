using UnityEngine;

public class ScreenEvidence : ConditionalStuff
{



    protected override void Awake()
    {
        //triggerEvent = new TriggerEvent(GameObject.FindWithTag(triggerTag).GetComponent<MonoBehaviour>());
        InitTrigger(triggerEvent);
    }
}

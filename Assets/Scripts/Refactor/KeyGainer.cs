using UnityEngine;

public class KeyGainer : MonoBehaviour
{
    private TriggerWrapper keyGainTrigger;

    private void Awake()
    {
        keyGainTrigger = TriggerEventController.Instance.KeyGain as TriggerWrapper;
        GetComponent<ClickableStuff>().ClickEvent.AddListener(OnGained);
    }



    private void OnGained()
    {
        keyGainTrigger.TriggerValue = true;
    }
}

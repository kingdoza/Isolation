using UnityEngine;

public class DiaryKeyLocker : MonoBehaviour
{
    private TriggerWrapper unlockTrigger;



    private void Awake()
    {
        unlockTrigger = TriggerEventController.Instance.DiaryUnlock as TriggerWrapper;
        GetComponent<ClickableStuff>().ClickEvent.AddListener(OnUnlocked);
    }



    private void OnUnlocked()
    {
        unlockTrigger.TriggerValue = true;
    }
}

using UnityEngine;

public class DiaryRelocator : MonoBehaviour
{
    private TriggerWrapper diaryRelocateTrigger;

    private void Awake()
    {
        diaryRelocateTrigger = TriggerEventController.Instance.DiaryRelocate as TriggerWrapper;
        GetComponent<ClickableStuff>().ClickEvent.AddListener(OnRelocated);
    }



    private void OnRelocated()
    {
        diaryRelocateTrigger.TriggerValue = true;
    }
}

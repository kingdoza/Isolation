using System;
using System.Net.WebSockets;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TriggerEventController : SceneSingleton<TriggerEventController>
{
    public ITriggerEventSendable PlayerWakeup { get; set; }
    public ITriggerEventSendable ChairRightReach { get; set; }
    public ITriggerEventSendable FirstScrewsLoose { get; set; } = new TriggerWrapper();
    public ITriggerEventSendable FramePhotoFlip { get; set; } = new TriggerWrapper();
    public ITriggerEventSendable DrawerStick { get; set; } = new TriggerWrapper();



    private void Update()
    {
        //Debug.Log("PlayerWakeup : " + PlayerWakeup);
        //Debug.Log("FirstScrewsLoose : " + FirstScrewsLoose.GetTriggerValue());
        //Debug.Log("FramePhotoFlip : " + FramePhotoFlip.GetTriggerValue());
    }
}



public class TriggerWrapper : ITriggerEventSendable
{
    public event Action TriggerChangeAction;
    private bool triggerValue = false;
    public bool TriggerValue
    {
        set
        {
            if (value != triggerValue)
            {
                triggerValue = value;
                TriggerChangeAction?.Invoke();
            }
        }
    }



    public bool GetTriggerValue()
    {
        return triggerValue;
    }
}



public enum GlobalTriggerEvent
{
    None, PlayerWakeup, ChairRightReach, FirstScrewsLoose, FramePhotoFlip, DrawerStick
}

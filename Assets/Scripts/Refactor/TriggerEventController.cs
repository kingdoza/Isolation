using System;
using UnityEngine;

public class TriggerEventController : SceneSingleton<TriggerEventController>
{
    public ITriggerEventSendable PlayerWakeTrigger { get; set; }
    public ITriggerEventSendable ClosetChairTrigger { get; set; }
}



public enum GlobalTriggerEvent
{
    None, PlayerWake, ClosetChair
}

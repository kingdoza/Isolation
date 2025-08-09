using System;

public interface ITriggerEventSendable
{
    event Action TriggerChangeAction;
    bool GetTriggerValue();
}

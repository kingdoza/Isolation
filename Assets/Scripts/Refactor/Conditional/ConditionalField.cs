using TMPro;
using UnityEngine;

[RequireComponent (typeof(TMP_InputField))]
public class ConditionalField : ConditionalActivator
{
    [SerializeField] private TriggerEvent triggerEvent;
    private TMP_InputField inputField;



    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        InitTrigger(triggerEvent);
    }



    protected override void SetConditionalComponent()
    {
        inputField.enabled = !triggerEvent.GetValue();
    }
}

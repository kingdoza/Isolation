using UnityEngine;

public class Itemizer : ConditionalActivator
{
    [SerializeField] private TriggerEvent itemizationTrigger;



    private void Awake()
    {
        InitTrigger(itemizationTrigger);
    }



    protected override void SetConditionalComponent()
    {
        if (itemizationTrigger.GetValue())
        {

        }
    }



    private void Itemization()
    {
        BaseStuff[] stuffs = GetComponents<BaseStuff>();
            
    }
}

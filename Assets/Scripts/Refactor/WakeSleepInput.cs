using UnityEngine;

[RequireComponent(typeof(MouseInteraction))]
public class WakeSleepInput : WakeSleepBranch
{
    [SerializeField] private bool wakeStatus;
    [SerializeField] private bool sleepStatus;
    private MouseInteraction inputComp;



    private void Awake()
    {
        inputComp = GetComponent<MouseInteraction>();
    }



    protected override void SetSleepComponent()
    {
        if (sleepStatus)
        {
            inputComp.EnableInput();
        }
        else
        {
            inputComp.DisableInput();
        }
    }



    protected override void SetWakeupComponent()
    {
        if (wakeStatus)
        {
            inputComp.EnableInput();
        }
        else
        {
            inputComp.DisableInput();
        }
    }
}

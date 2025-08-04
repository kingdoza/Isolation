using UnityEngine;

public abstract class WakeSleepBranch : MonoBehaviour
{
    protected Player player;



    private void Start()
    {
        Player.Instance.OnWakeup.AddListener(OnPlayerAwaked);
        Player.Instance.OnSleep.AddListener(OnPlayerAsleeped);
    }



    private void OnEnable()
    {
        if (Player.Instance.IsSleeping)
        {
            SetSleepComponent();
        }
        else
        {
            SetWakeupComponent();
        }
    }



    protected virtual void OnPlayerAwaked()
    {
        SetWakeupComponent();
    }



    protected virtual void OnPlayerAsleeped()
    {
        SetSleepComponent();
    }



    protected abstract void SetWakeupComponent();
    protected abstract void SetSleepComponent();
}

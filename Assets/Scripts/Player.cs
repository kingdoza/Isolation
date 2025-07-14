using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private bool isSleeping = false;
    public bool IsSleeping => isSleeping;
    [HideInInspector] public UnityEvent OnSleep;
    [HideInInspector] public UnityEvent OnWakeup;



    public void Sleep()
    {
        if (isSleeping)
            return;
        isSleeping = true;
        GameManager.Instance.FilterController.SetSleep();
        OnSleep?.Invoke();
        //GameManager.Instance.InteractController.DisableLightSwitch();
    }



    public void Wakeup()
    {
        isSleeping = false;
        GameManager.Instance.FilterController.SetWakeup();
        OnWakeup?.Invoke();
        //if (GameManager.Instance.TimeController.IsLastDay())
        //    GameManager.Instance.InteractController.DisableLightSwitch();
        //else
        //    GameManager.Instance.InteractController.EnableLightSwitch();
    }
}

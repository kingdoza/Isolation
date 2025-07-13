using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isSleeping = false;
    public bool IsSleeping => isSleeping;



    public void Sleep()
    {
        if (isSleeping)
            return;
        isSleeping = true;
        GameManager.Instance.FilterController.SetSleep();
        GameManager.Instance.InteractController.DisableLightSwitch();
    }



    public void Wakeup()
    {
        isSleeping = false;
        GameManager.Instance.FilterController.SetWakeup();

        if (GameManager.Instance.TimeController.IsLastDay())
            GameManager.Instance.InteractController.DisableLightSwitch();
        else
            GameManager.Instance.InteractController.EnableLightSwitch();
    }
}

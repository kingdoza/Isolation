using UnityEngine;

public class InteractSelector : MonoBehaviour
{
    [SerializeField] private Item wakeupInteract;
    [SerializeField] private Item sleepInteract;
    private IInteractable currentInteract;



    private void Start()
    {
        GameManager.Instance.Player.OnSleep.AddListener(SelectInteraction);
        GameManager.Instance.Player.OnWakeup.AddListener(SelectInteraction);
        SelectInteraction();
    }



    private void OnEnable()
    {
        SelectInteraction();
    }



    private void SelectInteraction()
    {
        bool isPlayerWakeup = !GameManager.Instance.Player.IsSleeping;
        if (isPlayerWakeup)
        {
            currentInteract = wakeupInteract;
            ((MonoBehaviour)wakeupInteract).enabled = true;
            ((MonoBehaviour)sleepInteract).enabled = false;
        }
        else
        {
            currentInteract = sleepInteract;
            ((MonoBehaviour)wakeupInteract).enabled = false;
            ((MonoBehaviour)sleepInteract).enabled = true;
        }
    }
}

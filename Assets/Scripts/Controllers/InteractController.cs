using UnityEngine;

public class InteractController : MonoBehaviour
{
    private UIController uiController;
    private TimeController timeController;



    public void Init()
    {
        uiController = GameManager.Instance.UIController;
        timeController = GameManager.Instance.TimeController;
    }



    private void HandleZoomAreaInteract(IInteractable sender)
    {

    }


    private void HandleItemInteract(IInteractable sender)
    {

    }
}

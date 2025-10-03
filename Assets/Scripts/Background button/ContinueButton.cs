using UnityEngine;
using UnityEngine.InputSystem.XR;
using static ControllerUtils;

public class ContinueButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public GameObject Background;
    public GameObject UIBlocker;
    public RoomController RC;

    public void ClickContinue()
    {
        PlaySFX(SFXClips.click2);
        Background.SetActive(false);
        UIBlocker.SetActive(false);
        if (GameManager.Instance.UIController.IsHelp == false && GameManager.Instance.UIController.IsMind == false)
            Time.timeScale = 1f;
        RC.enabled = true;
        //DragScroller.CanDrag = true;
    }


    
    
}

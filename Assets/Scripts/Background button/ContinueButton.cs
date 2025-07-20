using UnityEngine;

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
        Background.SetActive(false);
        UIBlocker.SetActive(false);
        Time.timeScale = 1f;
        RC.enabled = true;
        DragScroller.CanDrag = true;
    }


    
    
}

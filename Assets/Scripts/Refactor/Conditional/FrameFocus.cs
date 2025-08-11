using UnityEngine;

public class FrameFocus : MonoBehaviour
{
    [SerializeField] private GameObject frameFrontPrefab;
    [SerializeField] private GameObject frameBackPrefab;
    private GameObject frameFrontInstance;
    private GameObject frameBackInstance;



    private void Awake()
    {
    }



    private void OnEnable()
    {
        ShowFront();
    }



    private void OnDisable()
    {
        if (frameFrontInstance != null)
            Destroy(frameFrontInstance);
        if (frameBackInstance != null)
            Destroy(frameBackInstance);
    }



    public void Turn()
    {
        if (frameFrontInstance)
        {
            ShowBack();
        }
        else if (frameBackInstance) {
            ShowFront();
        }
        TimeController.Instance.ProgressMinutes(GameData.FocusStuffData.MinuteWaste);
        TimeController.Instance.CheckTimeChanged();
    }



    private void ShowFront()
    {
        Destroy(frameBackInstance);
        frameFrontInstance = Instantiate(frameFrontPrefab, transform);
        frameFrontInstance.GetComponent<ClickableStuff>().ClickEvent.AddListener(Turn);
    }



    private void ShowBack()
    {
        Destroy(frameFrontInstance);
        frameBackInstance = Instantiate(frameBackPrefab, transform);
        frameBackInstance.GetComponent<ClickableStuff>().ClickEvent.AddListener(Turn);
    }
}

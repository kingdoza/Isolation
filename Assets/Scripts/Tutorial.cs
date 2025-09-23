using UnityEngine;
using UnityEngine.Events;
using static ControllerUtils;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private InfoEntry[] infoEntries;
    //[SerializeField] private GameObject[] infoPanels;
    //[SerializeField] private GameObject background;
    private Transform uiOriginParent;
    private int currentIdx = 0;
    public bool IsProgessing { get; private set; } = false;
    [HideInInspector] public UnityEvent TutorialStartEvent;
    [HideInInspector] public UnityEvent TutorialEndEvent;



    private void Start()
    {
        if (GameManager.Instance.isTutorial == false)
        {
            gameObject.SetActive(false);
            //background.SetActive(false);
            return;
        }

        GameManager.Instance.UIController.FadeCompleteEvent.AddListener(OnFadeComplete);
        ShowTutorial();
    }



    private void ShowTutorial()
    {
        TutorialStartEvent?.Invoke();
        IsProgessing = true;
        //background.SetActive(true);
        GameManager.Instance.UIController.DisableMoveButtons();
        GameManager.Instance.UIController.DeactiveMindTree();
        foreach (InfoEntry infoEntry in infoEntries)
        {
            infoEntry.infoPanel.SetActive(false);
        }
        currentIdx = 0;
        gameObject.SetActive(true);
        //infoEntries[0].infoPanel.SetActive(true);
        ActiveInfoAt(0);
    }



    private void OnFadeComplete()
    {
        Debug.Log("OnFadeComplete : " + IsProgessing);
        if (IsProgessing == false)
            return;
        if (GameManager.Instance.RoomController.IsZoomIn)
            return;
        gameObject.SetActive(true);
        transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = true;
        SkipNextPanel(false);
    }



    private void ActiveInfoAt(int idx)
    {
        infoEntries[idx].infoPanel.SetActive(true);
        if (infoEntries[idx].topLayerObject == null)
            return;

        infoEntries[idx].topLayerObject.SetActive(true);
        if (infoEntries[idx].isObject == false)
        {
            uiOriginParent = infoEntries[idx].topLayerObject.transform.parent;
            infoEntries[idx].topLayerObject.transform.SetParent(transform);
        }

        if (infoEntries[idx].isSwitch)
        {
            EnableSwitchZoom();
        }
        if (infoEntries[idx].isMindTree)
        {
            GameManager.Instance.UIController.ActiveMindTree();
        }
    }



    private void EnableSwitchZoom()
    {
        //transform.GetChild(0).GetComponent<CanvasGroup>().interactable = false;
        transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;
    }



    private void DeactiveInfoAt(int idx)
    {
        infoEntries[idx].infoPanel.SetActive(false);
        if (infoEntries[idx].topLayerObject == null)
            return;

        infoEntries[idx].topLayerObject.SetActive(false);
        if (infoEntries[idx].isObject == false)
        {
            Debug.Log("DeactiveInfoAt : " + infoEntries[idx].topLayerObject + ", " + uiOriginParent);
            infoEntries[idx].topLayerObject.SetActive(true);
            infoEntries[idx].topLayerObject.transform.SetParent(uiOriginParent);
        }
    }



    public void ShowTutorial_Button()
    {
        if (IsProgessing)
            return;
        PlaySFX(SFXClips.click1);
        ShowTutorial();
    }



    public void SkipNextPanel_Button()
    {
        SkipNextPanel(true);
    }



    public void SkipNextPanel(bool checkSwitch = true)
    {
        Debug.Log("SkipNextPanel");
        if (checkSwitch && (infoEntries[currentIdx].isSwitch || infoEntries[currentIdx].isMindTree))
            return;

        PlaySFX(SFXClips.tutorial);
        DeactiveInfoAt(currentIdx++);
        //infoEntries[currentIdx++].infoPanel.SetActive(false);
        if (currentIdx >= infoEntries.Length)
        {
            gameObject.SetActive(false);
            //background.SetActive(false);
            GameManager.Instance.isTutorial = false;
            IsProgessing = false;
            TutorialEndEvent?.Invoke();
            GameManager.Instance.UIController.EnableMoveButtons();
            return;
        }
        //infoEntries[currentIdx].infoPanel.SetActive(true);
        ActiveInfoAt(currentIdx);
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SkipNextPanel();
        }
    }
}



[System.Serializable]
public class InfoEntry
{
    public GameObject infoPanel;
    public GameObject topLayerObject;
    public bool isObject;
    public bool isSwitch;
    public bool isMindTree;
}

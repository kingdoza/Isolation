using UnityEngine;
using UnityEngine.Events;
using static ControllerUtils;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private InfoEntry[] infoEntries;
    //[SerializeField] private GameObject[] infoPanels;
    //[SerializeField] private GameObject background;
    private Transform uiOriginParent;
    private int uiOriginIndex;
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
        GameManager.Instance.UIController.DeactiveRoomTree();
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

        if (infoEntries[idx].topLayerObject)
        {
            infoEntries[idx].topLayerObject.SetActive(true);
            if (infoEntries[idx].isObject == false)
            {
                uiOriginParent = infoEntries[idx].topLayerObject.transform.parent;
                uiOriginIndex = infoEntries[idx].topLayerObject.transform.GetSiblingIndex();
                infoEntries[idx].topLayerObject.transform.SetParent(transform);
            }
        }

        if (infoEntries[idx].isSwitch)
        {
            EnableSwitchZoom();
        }
        if (infoEntries[idx].isMindTree)
        {
            GameManager.Instance.UIController.ActiveMindTree();
        }
        if (infoEntries[idx].isRoom)
        {
            transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;
            //transform.GetChild(0).GetComponent<CanvasGroup>().interactable = false;
            GameManager.Instance.UIController.ActiveRoonTree();
        }
        if (infoEntries[idx].isMemory)
        {
            GameManager.Instance.UIController.MindTreeUI.SetRouthUI(EndingType.Bad);
        }
    }



    private void EnableSwitchZoom()
    {
        //transform.GetChild(0).GetComponent<CanvasGroup>().interactable = false;
        transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = false;
        FindAnyObjectByType<CameraDragArea>().enabled = false;
    }



    private void DeactiveInfoAt(int idx)
    {
        infoEntries[idx].infoPanel.SetActive(false);
        if (infoEntries[idx].topLayerObject == null)
            return;

        infoEntries[idx].topLayerObject.SetActive(false);
        if (infoEntries[idx].isObject == false)
        {
            //Debug.Log("DeactiveInfoAt : " + infoEntries[idx].topLayerObject + ", " + uiOriginParent);
            infoEntries[idx].topLayerObject.SetActive(true);
            infoEntries[idx].topLayerObject.transform.SetParent(uiOriginParent);
            infoEntries[idx].topLayerObject.transform.SetSiblingIndex(uiOriginIndex);
        }
        if (infoEntries[idx].isArrow)
        {
            transform.GetChild(0).GetComponent<CanvasGroup>().interactable = true;
            transform.GetChild(0).GetComponent<CanvasGroup>().blocksRaycasts = true;
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



    public void SkipNextPanel(bool blockArrow = true)
    {
        Debug.Log("SkipNextPanel");

        if (blockArrow && infoEntries[currentIdx].isArrow)
            return;

        PlaySFX(SFXClips.tutorial);
        DeactiveInfoAt(currentIdx++);
        //infoEntries[currentIdx++].infoPanel.SetActive(false);
        if (currentIdx >= infoEntries.Length)
        {
            gameObject.SetActive(false);
            FindAnyObjectByType<CameraDragArea>().enabled = true;
            //background.SetActive(false);
            GameManager.Instance.isTutorial = false;
            IsProgessing = false;
            TutorialEndEvent?.Invoke();
            GameManager.Instance.UIController.EnableMoveButtons();
            return;
        }
        //infoEntries[currentIdx].infoPanel.SetActive(true);
        if (infoEntries[currentIdx].isArrow)
        {
            transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
        }
        else
        {
            transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 1;
        }
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
    public bool isRoom;
    public bool isMindTree;
    public bool isArrow;
    public bool isMemory;
}

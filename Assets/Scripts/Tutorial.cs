using UnityEngine;
using static ControllerUtils;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private InfoEntry[] infoEntries;
    //[SerializeField] private GameObject[] infoPanels;
    //[SerializeField] private GameObject background;
    private Transform uiOriginParent;
    private int currentIdx = 0;



    private void Start()
    {
        if (GameManager.Instance.isTutorial == false)
        {
            gameObject.SetActive(false);
            //background.SetActive(false);
            return;
        }

        ShowTutorial();
    }



    private void ShowTutorial()
    {
        //background.SetActive(true);
        GameManager.Instance.UIController.DisableMoveButtons();
        foreach (InfoEntry infoEntry in infoEntries)
        {
            infoEntry.infoPanel.SetActive(false);
        }
        currentIdx = 0;
        gameObject.SetActive(true);
        //infoEntries[0].infoPanel.SetActive(true);
        ActiveInfoAt(0);
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
    }



    private void DeactiveInfoAt(int idx)
    {
        infoEntries[idx].infoPanel.SetActive(false);
        if (infoEntries[idx].topLayerObject == null)
            return;

        infoEntries[idx].topLayerObject.SetActive(false);
        if (infoEntries[idx].isObject == false)
        {
            infoEntries[idx].topLayerObject.SetActive(true);
            infoEntries[idx].topLayerObject.transform.SetParent(uiOriginParent);
        }
    }



    public void ShowTutorial_Button()
    {
        PlaySFX(SFXClips.click1);
        ShowTutorial();
    }



    public void SkipNextPanel()
    {
        PlaySFX(SFXClips.tutorial);
        DeactiveInfoAt(currentIdx++);
        //infoEntries[currentIdx++].infoPanel.SetActive(false);
        if (currentIdx >= infoEntries.Length)
        {
            GameManager.Instance.UIController.EnableMoveButtons();
            gameObject.SetActive(false);
            //background.SetActive(false);
            GameManager.Instance.isTutorial = false;
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
}

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EvidenceWindow : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI memory;
    [SerializeField] private Image icon;
    [SerializeField] private GameObject sentenceParent;
    [SerializeField] private TextMeshProUGUI textArea;



    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
    }



    private void InsertBasicInfo(EvidenceInfo evidenceInfo)
    {
        title.text = evidenceInfo.name;
        memory.text = evidenceInfo.description;
        icon.sprite = evidenceInfo.sprite;
        textArea.text = "  ";

        foreach (string text in evidenceInfo.texts)
        {
            textArea.text += text + " ";
        }

        //int dialogueIdx = 0;
        //foreach (Transform child in sentenceParent.transform) 
        //{
        //    TextMeshProUGUI sentenceBox = child.GetComponent<TextMeshProUGUI>();
        //    sentenceBox.text = dialogueIdx >= evidenceInfo.texts.Length ? "" : evidenceInfo.texts[dialogueIdx++];
        //}
    }



    public void ShowSimple(EvidenceInfo evidenceInfo)
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        InsertBasicInfo(evidenceInfo);
        //sentenceParent.SetActive(false);
        textArea.gameObject.SetActive(false);
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 120f);
        canvasGroup.alpha = 1;
    }



    public void ShowDetail(EvidenceInfo evidenceInfo)
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        InsertBasicInfo(evidenceInfo);
        //sentenceParent.SetActive(true);
        textArea.gameObject.SetActive(true);
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, 700f);
        canvasGroup.alpha = 1;
    }



    public void Hide()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }
}




public class EvidenceInfo
{
    public string name;
    public string description;
    public Sprite sprite;
    public string[] texts;
}
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using System.Linq;
using static UnityEngine.GraphicsBuffer;
using System.Collections.Generic;


public class CollectionSlot : MotiveSlot, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Sprite sprite;
    private bool isCollected = false;
    private EvidenceInfo evidenceInfo = new();
    private MindTreeUI mindTreeUI;

    public bool IsCollected => isCollected;
    public EvidenceInfo EvidenceInfo => evidenceInfo;



    private void Start()
    {
        mindTreeUI = GetComponentInParent<MindTreeUI>();

        evidenceInfo.name = name.Substring(name.LastIndexOf('_') + 1);
        evidenceInfo.sprite = sprite;

        int memoryNum = transform.parent.name[transform.parent.name.Length - 1] - '0';
        string memoryName = "";
        foreach (Transform sibling in transform.parent)
        {
            if (sibling.name.StartsWith("Evidence"))
            {
                string siblingName = sibling.name;
                memoryName = siblingName.Substring(siblingName.LastIndexOf('_') + 1);
                break;
            }
        }
        evidenceInfo.description = string.Format("È¸°í{0}. {1}", memoryNum, memoryName);

        List<EvidenceStuff> evidenceStuffs = FindObjectsOfType<EvidenceStuff>(true).ToList();
        foreach (GameObject evidencePrefab in mindTreeUI.AdditionalEvidences)
        {
            EvidenceStuff[] prefabEvidences = evidencePrefab.GetComponentsInChildren<EvidenceStuff>(true);
            foreach (EvidenceStuff prefabEvidence in prefabEvidences)
            {
                evidenceStuffs.Add(prefabEvidence);
            }
        }
        foreach (EvidenceStuff stuff in evidenceStuffs)
        {
            if (stuff.EvidenceName.Equals(evidenceInfo.name))
            {
                evidenceInfo.texts = stuff.Dialogues;
            }
        }
    }



    public override void Collected(MindTreeUI mindTreeUI)
    {
        isCollected = true;
        transform.Find("Fill").GetComponent<Image>().color = mindTreeUI.ItemSlotColor;
        if (NotificationManager.Instance != null)
        {
            NotificationManager.Instance.ShowNotification();
        }
    }



    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.yellow;
        mindTreeUI.ShowEvidenceInfo(this);
    }



    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<Image>().color = Color.white;
        mindTreeUI.HideEvidenceInfo();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class MindTreeUI : MonoBehaviour
{
    [Header("슬롯 채움 색깔")] [Space]
    [SerializeField] private Color itemSlotColor;
    [SerializeField] private Color clueSlotColor;
    [SerializeField] private Color finalSlotColor;
    [Header("UI 오브젝트")] [Space]
    [SerializeField] private EndingRouteUI[] routhUIs;
    [SerializeField] private TextMeshProUGUI subtitle;
    [SerializeField] private EvidenceWindow evidenceWindow;
    [Header("UI 오브젝트")] [Space]
    [SerializeField] private GameObject[] additionalEvidences;
    public GameObject[] AdditionalEvidences => additionalEvidences;


    private EndingType currentType;

    public List<CollectionSlot> ItemSlots { get; set; }
    public List<EvidenceSlot> EvidenceSlots { get; set; }
    public List<EndingSlot> EndingSlots { get; set; }
    public Color ItemSlotColor => itemSlotColor;
    public Color ClueSlotColor => clueSlotColor;
    public Color FinalSlotColor => finalSlotColor;



    public void SetRouthUI_Bad() => SetRouthUI(EndingType.Bad);
    public void SetRouthUI_Happy() => SetRouthUI(EndingType.Happy);



    public void Init()
    {
        ItemSlots = GetComponentsInChildren<CollectionSlot>(true).ToList();
        EvidenceSlots = GetComponentsInChildren<EvidenceSlot>(true).ToList();
        EndingSlots = GetComponentsInChildren<EndingSlot>(true).ToList();
        GameManager.Instance.Player.EvidenceCollectEvent.AddListener(OnPlayerItemCollected);
        SetRouthUI(EndingType.Bad, false);
    }



    private void SetRouthUI(EndingType endingType, bool checkType = true)
    {
        if (checkType && currentType == endingType)
            return;
        HideEvidenceInfo();
        currentType = endingType;
        ShowSlotPanel();
        ShowSubtitle();
    }



    private void OnEnable()
    {
        HideEvidenceInfo();
    }



    private void ShowSlotPanel()
    {
        foreach (EndingRouteUI routhUI in routhUIs)
        {
            if (routhUI.type == currentType)
            {
                routhUI.slotPanel.SetActive(true);
                continue;
            }
            routhUI.slotPanel.SetActive(false);
        }
    }



    private void ShowSubtitle()
    {
        Motivation motivation = GameManager.Instance.Player.MotiveProgresses[currentType].Motive;
        subtitle.text = motivation.subtitle;
    }



    private void OnPlayerItemCollected(MotiveProgress motiveProgress, string itemName)
    {
        foreach (MotiveSlot itemSlot in ItemSlots)
        {
            if(itemSlot.name.Contains(itemName))
            {
                itemSlot.Collected(this);
                break;
            }
        }

        int clearedEvidenceCount = 0;
        int evidenceCount = motiveProgress.Motive.evidences.Count;
        for (int i = 0; i < evidenceCount; ++i)
        {
            if (motiveProgress.IsEvidenceCleared(i))
            {
                ++clearedEvidenceCount;
                string evidenceName = motiveProgress.Motive.evidences[i].title;
                foreach (EvidenceSlot evidenceSlot in EvidenceSlots)
                {
                    if(evidenceSlot.name.Contains(evidenceName))
                    {
                        evidenceSlot.Collected(this);
                        break;
                    }
                }
            }
        }

        if (clearedEvidenceCount >= evidenceCount)
        {
            foreach (EndingSlot endingSlot in EndingSlots)
            {
                string typeString = motiveProgress.Motive.type.ToString();
                if (endingSlot.name.Contains(typeString))
                {
                    endingSlot.Collected(this);
                    OnFirstEndingCompleted(motiveProgress.Motive.type);
                    break;
                }
            }
        }
    }



    private void OnFirstEndingCompleted(EndingType endingType)
    {
        GameManager.Instance.EndingType = endingType;
        GameManager.Instance.RoomController.ReturnToRoomView();
        GameManager.Instance.RoomController.DisableRoomViews();
    }



    public void ShowEvidenceInfo(CollectionSlot collectionSlot)
    {
        if (collectionSlot.IsCollected)
        {
            evidenceWindow.ShowDetail(collectionSlot.EvidenceInfo);
        }
        else
        {
            evidenceWindow.ShowSimple(collectionSlot.EvidenceInfo);
        }

        //evidenceWindow.ShowDetail(collectionSlot.EvidenceInfo);
    }



    public void HideEvidenceInfo()
    {
        evidenceWindow.Hide();
    }
}



[Serializable]
public class EndingRouteUI
{
    public EndingType type;
    public GameObject slotPanel;
}
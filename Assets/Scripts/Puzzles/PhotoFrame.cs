using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ControllerUtils;

public class PhotoFrame : MonoBehaviour
{
    [SerializeField] private List<CollectibleItem> screwsToCollect;
    [SerializeField] private FramePlate backPlateOpen;
    [SerializeField] private FramePlate backPlateClose;
    [SerializeField] private SortSwitcher photoSwitcher;
    private Info info;
    private int jointScrewCount = 4;
    private int screwToJoint = 4;

    private Item item;



    private void Start()
    {
        info = GameManager.Instance.PuzzleController.PhotoFrameInfo;
        LoadByInfo();

        item = GetComponent<Item>();
        item.RegisterInteractCondition(() => jointScrewCount >= screwToJoint);
        backPlateClose.RegisterInteractCondition(() => jointScrewCount <= 0);
        photoSwitcher.OnItemUse.AddListener(OnPhothSwitched);

        foreach (Screw screw in GetComponentsInChildren<Screw>(true)) 
        {
            if(screw.IsJoint == false)
                screw.RegisterInteractCondition(() => backPlateClose.gameObject.activeSelf);
            screw.OnItemUse.AddListener(OnScrewInteracted);
        }
    }



    private void LoadByInfo()
    {
        //photoRenderer.sprite = info.isConverted ? oldFamilyPhoto : graduationPhoto;
        if(info.isConverted)
        {
            photoSwitcher.Interact();
        }
        if(info.isFirstOpened)
        {
            screwToJoint = 2;
            jointScrewCount = 2;
            foreach (CollectibleItem screw in screwsToCollect)
            {
                screw.GetComponent<Screw>().OtherStatus.gameObject.SetActive(false);
            }
        }
    }



    private void OnPhothSwitched(ItemUsePoint interaction)
    {
        //PlaySFX(SFXClips.familiyPhoto_FrameTurn);
        info.isConverted = !info.isConverted;
    }



    private void OnScrewInteracted(ItemUsePoint interaction)
    {
        Screw screw = interaction as Screw;
        if (screw == null) return;

        if (screw.IsJoint)
        {
            LooseScrew();
        }
        else
        {
            JointScrew();
        }
    }



    private void JointScrew()
    {
        ++jointScrewCount;
    }



    private void LooseScrew()
    {
        --jointScrewCount;
        if (jointScrewCount <= 0)
        {
            if (info.isFirstOpened)
                return;
            foreach (CollectibleItem screwToCollect in screwsToCollect)
            {
                screwToCollect.Interact();
            }
            screwToJoint = 2;
            info.isFirstOpened = true;
        }
    }



    public class Info
    {
        public bool isConverted = false;
        public bool isFirstOpened = false;
    }
}
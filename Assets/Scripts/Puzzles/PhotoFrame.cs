using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static ControllerUtils;

public class PhotoFrame : MonoBehaviour
{
    [SerializeField] private List<CollectibleItem> screwsToCollect;
    [SerializeField] private FramePlate backPlateOpen;
    [SerializeField] private FramePlate backPlateClose;
    [SerializeField] private SortSwitcher photoSwitcher1;
    [SerializeField] private SortSwitcher photoSwitcher2;
    private bool convertStatus = false;
    private Info info;
    private int jointScrewCount = 4;
    private int screwToJoint = 4;

    private Item item;



    private void Start()
    {
        info = GameManager.Instance.PuzzleController.PhotoFrameInfo;
        convertStatus = info.isConverted;
        photoSwitcher1.GetComponent<Collider2D>().enabled = true;
        photoSwitcher2.GetComponent<Collider2D>().enabled = false;
        LoadByInfo();

        item = GetComponent<Item>();
        item.RegisterInteractCondition(() => jointScrewCount >= screwToJoint);
        backPlateClose.RegisterInteractCondition(() => jointScrewCount <= 0);
        photoSwitcher1.OnItemUse.AddListener(OnPhothSwitched);
        photoSwitcher2.OnItemUse.AddListener(OnPhothSwitched);
        photoSwitcher1.RegisterInteractCondition(() => !convertStatus);
        photoSwitcher2.RegisterInteractCondition(() => convertStatus);

        foreach (OldScrew screw in GetComponentsInChildren<OldScrew>(true)) 
        {
            if(screw.IsJoint == false)
                screw.RegisterInteractCondition(() => backPlateClose.gameObject.activeSelf);
            screw.OnItemUse.AddListener(OnScrewInteracted);
        }
    }



    private void LoadByInfo()
    {
        //photoRenderer.sprite = info.isConverted ? oldFamilyPhoto : graduationPhoto;
        if(convertStatus)
        {
            photoSwitcher1.Interact();
            SwitchPhotoCollider();
        }
        if(info.isFirstOpened)
        {
            screwToJoint = 2;
            jointScrewCount = 2;
            foreach (CollectibleItem screw in screwsToCollect)
            {
                screw.GetComponent<OldScrew>().OtherStatus.gameObject.SetActive(false);
            }
        }
    }



    private void OnPhothSwitched(ItemUsePoint interaction)
    {
        //PlaySFX(SFXClips.familiyPhoto_FrameTurn);
        convertStatus = !convertStatus;
        SwitchPhotoCollider();
    }



    private void SwitchPhotoCollider()
    {
        Collider2D col1 = photoSwitcher1.GetComponent<Collider2D>();
        Collider2D col2 = photoSwitcher2.GetComponent<Collider2D>();

        bool temp = col1.enabled;
        col1.enabled = col2.enabled;
        col2.enabled = temp;
    }



    private void OnScrewInteracted(ItemUsePoint interaction)
    {
        OldScrew screw = interaction as OldScrew;
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
        if (jointScrewCount >= screwToJoint)
        {
            info.isConverted = convertStatus;
        }
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
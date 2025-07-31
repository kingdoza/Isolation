using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PhotoFrame : MonoBehaviour
{
    [SerializeField] private List<CollectibleItem> screwsToCollect;
    [SerializeField] private FramePlate backPlateOpen;
    [SerializeField] private FramePlate backPlateClose;
    private int jointScrewCount = 4;
    private int screwToJoint = 4;

    private Item item;



    private void Start()
    {
        item = GetComponent<Item>();
        item.RegisterInteractCondition(() => jointScrewCount >= screwToJoint);
        backPlateClose.RegisterInteractCondition(() => jointScrewCount <= 0);

        foreach (Screw screw in GetComponentsInChildren<Screw>(true)) 
        {
            if(screw.IsJoint == false)
                screw.RegisterInteractCondition(() => backPlateClose.gameObject.activeSelf);
            screw.OnItemUse.AddListener(OnScrewInteracted);
        }
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
            if (screwToJoint != 4)
                return;
            foreach (CollectibleItem screwToCollect in screwsToCollect)
            {
                screwToCollect.Interact();
            }
            screwToJoint = 2;
        }
    }
}

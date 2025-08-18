using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EvidenceSlot : MotiveSlot
{


    public override void Collected(MindTreeUI mindTreeUI)
    {
        transform.Find("Fill").GetComponent<Image>().color = mindTreeUI.ClueSlotColor;
        
        
    }
    
}

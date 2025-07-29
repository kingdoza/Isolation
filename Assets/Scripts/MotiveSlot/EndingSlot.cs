using UnityEngine;
using UnityEngine.UI;

public class EndingSlot : MotiveSlot
{
    public override void Collected(MindTreeUI mindTreeUI)
    {
        transform.Find("Fill").GetComponent<Image>().color = mindTreeUI.FinalSlotColor;
    }
}

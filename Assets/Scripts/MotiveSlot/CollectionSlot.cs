using UnityEngine;
using UnityEngine.UI;


public class CollectionSlot : MotiveSlot
{
    public override void Collected(MindTreeUI mindTreeUI)
    {
        transform.Find("Fill").GetComponent<Image>().color = mindTreeUI.ItemSlotColor;

        if (NotificationManager.Instance != null)
        {
            NotificationManager.Instance.ShowNotification();
        }
    }
}

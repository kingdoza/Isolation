using UnityEngine;
using UnityEngine.UI;

public class TriggerSlot : MonoBehaviour
{
    //[SerializeField] private TriggerSlot nextSlot;
    //[SerializeField] private Item trigger;
    //private MindTreeUI mindTree;
    //private RectTransform slotLine;



    //private void Awake()
    //{
    //    mindTree = GetComponentInParent<MindTreeUI>(true);
    //    DrawLineToNextSlot();
    //}



    //private void DrawLineToNextSlot()
    //{
    //    if (nextSlot == null)
    //        return;

    //    Vector3 start = GetComponent<RectTransform>().position;
    //    Vector3 end = nextSlot.GetComponent<RectTransform>().position;

    //    Vector3 dir = end - start;
    //    float length = dir.magnitude;

    //    RectTransform line = CreateSlotLine();
    //    line.position = (start + end) / 2;
    //    line.sizeDelta = new Vector2(length, mindTree.LineThickness);
    //    line.rotation = Quaternion.FromToRotation(Vector3.right, dir);
    //}



    //private RectTransform CreateSlotLine()
    //{
    //    GameObject lineObj = new GameObject("SlotLine", typeof(RectTransform), typeof(Image));

    //    Transform lineParent = transform.parent.Find("Lines");
    //    lineObj.transform.SetParent(lineParent, false);

    //    slotLine = lineObj.GetComponent<RectTransform>();
    //    Image image = lineObj.GetComponent<Image>();
    //    image.sprite = mindTree.LineSprite;
    //    image.type = Image.Type.Sliced;
    //    image.color = Color.white;

    //    return slotLine;
    //}
}

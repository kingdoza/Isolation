using UnityEngine;
using UnityEngine.UI;

public abstract class MotiveSlot : MonoBehaviour
{
    public MotiveSlot NextSlot { get; set; }
    private RectTransform slotLine;



    public virtual void ApplySize(float size)
    {
        GetComponent<RectTransform>().sizeDelta = Vector2.one * size;
    }



    public void DrawLineTo(MotiveSlot nextSlot, MotiveUIData generationFormat)
    {
        if(slotLine != null)
            DestroyImmediate(slotLine.gameObject);
        Vector3 start = GetComponent<RectTransform>().position;
        Vector3 end = nextSlot.GetComponent<RectTransform>().position;

        Vector3 dir = end - start;
        float length = dir.magnitude;

        RectTransform line = CreateSlotLine(generationFormat);
        line.position = (start + end) / 2;
        line.sizeDelta = new Vector2(length, generationFormat.lineThickness);
        line.rotation = Quaternion.FromToRotation(Vector3.right, dir);
    }



    private RectTransform CreateSlotLine(MotiveUIData generationFormat)
    {
        GameObject lineObj = new GameObject("SlotLine", typeof(RectTransform), typeof(Image));
        lineObj.transform.SetParent(transform, false);
        lineObj.transform.SetAsFirstSibling();

        slotLine = lineObj.GetComponent<RectTransform>();
        Image image = lineObj.GetComponent<Image>();
        image.sprite = generationFormat.lineSprite;
        image.type = Image.Type.Sliced;
        image.color = Color.white;

        return slotLine;
    }



    public abstract void Collected(MindTreeUI mindTreeUI);
}

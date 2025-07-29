using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Experimental.GlobalIllumination;

public class LineGenerator : MonoBehaviour
{
    [SerializeField] private RectTransform[] destinations;
    private RectTransform rectTransform;
    [SerializeField] private Sprite lineSprite;
    [SerializeField] private float lineThickness;



    private void Start()
    {
        DrawUILines();
    }



    private void OnEnable()
    {
        DrawUILines();
    }



    private void DrawUILines()
    {
        if(rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        foreach (var dest in destinations)
        {
            DrawLineTo(dest);
        }
    }



    private RectTransform CreateUILine()
    {
        GameObject lineObj = new GameObject("UILine", typeof(RectTransform), typeof(Image));

        Transform lineParent = transform.parent.Find("Lines");
        lineObj.transform.SetParent(lineParent, false);

        RectTransform lineRect = lineObj.GetComponent<RectTransform>();
        Image image = lineObj.GetComponent<Image>();
        image.sprite = lineSprite;
        image.type = Image.Type.Sliced;
        image.color = Color.white;

        return lineRect;
    }



    private void DrawLineTo(RectTransform destination)
    {
        Vector3 start = rectTransform.position;
        Vector3 end = destination.position;

        Vector3 dir = end - start;
        float length = dir.magnitude;

        RectTransform line = CreateUILine();
        line.position = (start + end) / 2;
        line.sizeDelta = new Vector2(length, lineThickness);
        line.rotation = Quaternion.FromToRotation(Vector3.right, dir);
    }
}

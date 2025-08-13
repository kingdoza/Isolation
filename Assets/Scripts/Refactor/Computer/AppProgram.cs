using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;

public class AppProgram : MonoBehaviour, IPointerClickHandler
{
    private readonly Color HighlightColor = new Color(1, 1, 1, 0.5f);
    private readonly float DoubleClickTime = 0.2f;
    private List<AppProgram> otherApps;
    private float lastClickTime = 0f;
    private Image image;



    private void Awake()
    {
        image = GetComponent<Image>();
        otherApps = transform.parent.GetComponentsInChildren<AppProgram>().ToList();
        otherApps.Remove(this);
        Unhighlight();
    }



    public void OnPointerClick(PointerEventData eventData)
    {
        foreach (var appProgram in otherApps)
        {
            appProgram.Unhighlight();
        }
        Highlight();
        float timeSinceLastClick = Time.time - lastClickTime;
        if (timeSinceLastClick <= DoubleClickTime)
        {
            OnDoubleClick();
        }
        lastClickTime = Time.time;
    }



    private void Highlight()
    {
        image.color = HighlightColor;
    }



    private void Unhighlight()
    {
        lastClickTime = 0;
        image.color = Color.clear;
    }



    private void OnDoubleClick()
    {
        Debug.Log("더블클릭 감지!");
    }
}

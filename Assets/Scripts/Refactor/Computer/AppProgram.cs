using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Linq;
using System;

public class AppProgram : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject iconPanelObject;
    [SerializeField] private GameObject appPanelObject;
    private readonly Color HighlightColor = new Color(1, 1, 1, 0.5f);
    private readonly float DoubleClickTime = 0.3f;
    private List<AppProgram> otherApps;
    private float lastClickTime = 0f;
    private Image image;
    private bool isAppOpen;
    private TriggerWrapper appOpenTrigger;



    private void Awake()
    {
        if (name.Equals("Expulsion"))
            appOpenTrigger = TriggerEventController.Instance.ExpulsionOpen as TriggerWrapper;
        if (appPanelObject != null)
            appPanelObject.SetActive(false);
        image = GetComponent<Image>();
        otherApps = transform.parent.GetComponentsInChildren<AppProgram>().ToList();
        otherApps.Remove(this);
        Unhighlight();
    }



    private void OnEnable()
    {
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
        if (isAppOpen) return;
        Open();
    }



    public void Open()
    {
        if (appPanelObject == null) return;
        isAppOpen = true;
        appPanelObject.SetActive(true);
        DisableMainPanel();
        TimeController.Instance.ProgressMinutes(GameData.AppOpenCloseMinutes);
        TimeController.Instance.CheckTimeChanged();
        if (appOpenTrigger != null)
            appOpenTrigger.TriggerValue = true;
    }



    public void Close()
    {
        if (appPanelObject == null) return;
        isAppOpen = false;
        appPanelObject.SetActive(false);
        EnableMainPanel();
        TimeController.Instance.ProgressMinutes(GameData.AppOpenCloseMinutes);
        TimeController.Instance.CheckTimeChanged();
        if (appOpenTrigger != null)
            appOpenTrigger.TriggerValue = false;
    }



    private void EnableMainPanel()
    {
        if (iconPanelObject == null) return;
        iconPanelObject.SetActive(true);
    }



    private void DisableMainPanel()
    {
        if (iconPanelObject == null) return;
        iconPanelObject.SetActive(false);
    }
}

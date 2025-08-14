using UnityEngine;
using UnityEngine.Events;

public class ToggleStuff : ClickableStuff
{
    protected override StuffTypeData StuffData => GameData.FocusStuffData;
    [SerializeField] private GameObject toggleObject;
    [SerializeField] private bool initialEnable;
    [HideInInspector] public UnityEvent<GameObject> ToggleEvent = new();



    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(initialEnable);
    }



    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        if (toggleObject != null)
            toggleObject.SetActive(true);
        gameObject.SetActive(false);
        ToggleEvent?.Invoke(toggleObject);
        TimeController.Instance.CheckTimeChanged();
    }
}

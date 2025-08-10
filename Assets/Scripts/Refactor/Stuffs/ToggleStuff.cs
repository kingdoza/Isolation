using UnityEngine;

public class ToggleStuff : ClickableStuff
{
    protected override StuffTypeData StuffData => GameData.FocusStuffData;
    [SerializeField] private GameObject toggleObject;
    [SerializeField] private bool initialEnable;



    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(initialEnable);
    }



    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        toggleObject.SetActive(true);
        gameObject.SetActive(false);
    }
}

using UnityEngine;
using UnityEngine.Events;
using static ControllerUtils;

[RequireComponent(typeof(Click))]
public class ClickableStuff : BaseStuff
{
    [HideInInspector] public UnityEvent ClickEvent = new();
    [SerializeField] private bool isItemOnce;
    protected override StuffTypeData StuffData => GameData.ClickStuffData;



    protected override void Awake()
    {
        base.Awake();
        (inputComp as Click).ClickEvent.AddListener(OnClicked);
    }



    protected virtual void OnClicked()
    {
        if (isItemOnce && interactItem != ItemType.None)
            InventoryUI.Instance.DeleteSelectedSlot();
        GameManager.Instance.TimeController.ProgressMinutes(StuffData.MinuteWaste);
        PlaySFX(sfxClip);
        ClickEvent?.Invoke();
    }
}

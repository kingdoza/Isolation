using DG.Tweening.Core.Easing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class LightSwitchZoom : ZoomArea
{
    [SerializeField] private Sprite switchOffSprite;
    [SerializeField] private Sprite switchOnSprite;
    private SpriteRenderer spriteRenderer;
    private Player player;



    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameManager.Instance.Player;
        player.OnWakeup.AddListener(OnPlayerWakeup);
        player.OnSleep.AddListener(OnPlayerSleep);

        if (player.IsSleeping)
            OnPlayerSleep();
        else
            OnPlayerWakeup();

        GameManager.Instance.RoomController.OnFocusOut.AddListener(OnFocusOut);
    }



    private void OnFocusOut()
    {
        if (player.IsSleeping)
            OnPlayerSleep();
        else
            OnPlayerWakeup();
    }



    private void OnPlayerWakeup()
    {
        CanInteract = !GameManager.Instance.TimeController.IsLastDay();
        spriteRenderer.sprite = switchOnSprite;
    }



    private void OnPlayerSleep()
    {
        CanInteract = false;
        spriteRenderer.sprite = switchOffSprite;
    }



    public override void OnPointerClick(PointerEventData eventData)
    {
        bool isLastDay = GameManager.Instance.TimeController.IsLastDay();
        bool isPlayerWake = !GameManager.Instance.Player.IsSleeping;
        if (isLastDay && isPlayerWake)
        {
            Debug.Log("오늘은 잠이 오지않는다.");
            return;
        }
        base.OnPointerClick(eventData);
    }
}

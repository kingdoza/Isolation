using UnityEngine;
using UnityEngine.EventSystems;
using static ControllerUtils;

public class LightSwitch : Item
{
    [SerializeField] private Sprite switchOffSprite;
    [SerializeField] private Sprite switchOnSprite;


    protected override void Awake()
    {
        base.Awake();
        spriteRenderer.sprite = switchOnSprite;
    }



    public override void Interact()
    {
        base.Interact();
        GameManager.Instance.Player.Sleep();
        CanInteract = false;
        spriteRenderer.sprite = switchOffSprite;
        spriteRenderer.color = Color.white;

        PlaySFX(SFXClips.lightSwitch);
    }
}

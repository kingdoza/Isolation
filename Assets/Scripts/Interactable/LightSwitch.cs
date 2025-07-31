using UnityEngine;
using UnityEngine.EventSystems;
using static ControllerUtils;

public class LightSwitch : Item
{
    [SerializeField] private Sprite switchOffSprite;
    [SerializeField] private Sprite switchOnSprite;
    private bool isSwitched = false;


    protected override void Awake()
    {
        base.Awake();
        spriteRenderer.sprite = switchOnSprite;
    }



    protected override void Start()
    {
        base.Start();
        RegisterInteractCondition(() => !isSwitched);
    }



    public override void Interact()
    {
        base.Interact();
        GameManager.Instance.Player.Sleep();
        isSwitched = true;
        //CanInteract = false;
        spriteRenderer.sprite = switchOffSprite;
        spriteRenderer.color = Color.white;

        PlaySFX(SFXClips.lightSwitch);
    }
}

using UnityEngine;
using UnityEngine.EventSystems;

public class LightSwitch : Item
{
    [SerializeField] private Sprite switchOffSprite;
    [SerializeField] private Sprite switchOnSprite;


    protected override void Start()
    {
        base.Start();
        spriteRenderer.sprite = switchOnSprite;
    }



    public override void Interact()
    {
        base.Interact();
        GameManager.Instance.Player.Sleep();
        CanInteract = false;
        spriteRenderer.sprite = switchOffSprite;
        spriteRenderer.color = Color.white;
    }
}

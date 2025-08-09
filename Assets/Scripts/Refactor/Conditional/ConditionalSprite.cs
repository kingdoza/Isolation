using UnityEngine;
using static EtcUtils;

[RequireComponent(typeof(SpriteRenderer))]
public class ConditionalSprite : SingleConditonActivator
{
    [SerializeField] private Sprite trueConditonSprite;
    [SerializeField] private Sprite falseConditonSprite;
    private SpriteRenderer spriteRenderer;



    protected override void Awake()
    {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }



    protected override void SetFalseComponent()
    {
        spriteRenderer.sprite = falseConditonSprite;
    }



    protected override void SetTrueComponent()
    {
        spriteRenderer.sprite = trueConditonSprite;
    }
}

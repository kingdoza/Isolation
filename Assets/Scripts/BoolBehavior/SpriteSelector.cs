using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteSelector : MonoBehaviour, IConditionalSelector
{
    [SerializeField] private Sprite wakeStatusSprite;
    [SerializeField] private Sprite sleepStatusSprite;

    public bool Condition => GameManager.Instance.Player.IsSleeping;
    public Object TrueObject => sleepStatusSprite;
    public Object FalseObject => wakeStatusSprite;



    private void Awake()
    {
        //GetComponent<SpriteRenderer>().sprite = (Sprite)(this as IConditionalSelector).GetConditionalObject(Condition);
    }



    private void OnEnable()
    {
        //GetComponent<SpriteRenderer>().sprite = (Sprite)(this as IConditionalSelector).GetConditionalObject(Condition);
    }



    public void SetConditionalSprite()
    {
        bool isSleeping = GameManager.Instance.TimeController.IsSleepingTime();
        Sprite newSprite = (Sprite)(this as IConditionalSelector).GetConditionalObject(isSleeping);
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }
}

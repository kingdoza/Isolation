using UnityEngine;
using UnityEngine.Events;

public class StatusSwitcher : ItemUsePoint
{
    [SerializeField] private StatusSwitcher otherStatus;
    public StatusSwitcher OtherStatus => otherStatus;



    private void OnEnable()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }



    public override void Interact()
    {
        otherStatus.gameObject.SetActive(true);
        gameObject.SetActive(false);
        base.Interact();
    }
}

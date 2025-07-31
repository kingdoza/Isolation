using UnityEngine;
using UnityEngine.Events;

public class StatusSwitcher : ItemUsePoint
{
    [SerializeField] private StatusSwitcher otherStatus;



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

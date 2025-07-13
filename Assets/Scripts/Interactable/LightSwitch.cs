using UnityEngine;
using UnityEngine.EventSystems;

public class LightSwitch : Item
{
    public override void Interact()
    {
        base.Interact();
        GameManager.Instance.Player.Sleep();
    }
}

using UnityEngine;

public class DisabledItem : Item
{
    private void OnEnable()
    {
        CanInteract = false;
    }



    private void OnDisable()
    {
        CanInteract = true;
    }
}

using UnityEngine;

public class DisabledItem : Item
{
    protected override void Start()
    {
        RegisterInteractCondition(() => !gameObject.activeSelf);
    }
    //private void OnEnable()
    //{
    //    CanInteract = false;
    //}



    //private void OnDisable()
    //{
    //    CanInteract = true;
    //}
}

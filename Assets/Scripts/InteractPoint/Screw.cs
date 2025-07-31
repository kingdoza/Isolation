using UnityEngine;
using static ControllerUtils;

public class Screw : StatusSwitcher
{
    [SerializeField] private bool isJoint;
    public bool IsJoint => isJoint;



    public override void Interact()
    {
        base.Interact();
        PlaySFX(SFXClips.familiyPhoto_Screwing);
    }
}

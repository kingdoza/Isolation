using UnityEngine;
using static ControllerUtils;
public class FramePlate : StatusSwitcher
{
    [SerializeField] private bool isClose;
    public bool IsClose => isClose;

    public override void Interact()
    {
        base.Interact();
        PlaySFX(SFXClips.familiyPhoto_Hover);
    }

}

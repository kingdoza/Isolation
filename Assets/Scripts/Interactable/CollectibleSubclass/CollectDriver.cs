using UnityEngine;
using static ControllerUtils;

public class CollectDriver : CollectibleItem
{
    public override void Interact()
    {
        base.Interact();
        //PlaySFX(SFXClips.familiyPhoto_DriverSave);
    }
}

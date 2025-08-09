using UnityEngine;

public class PuzzleController : MonoBehaviour
{
    public ITriggerEventSendable ClosetChair { get; set; }


    public PhotoFrame.Info PhotoFrameInfo { get; set; } = new PhotoFrame.Info();
    public Chair.Info ChairInfo { get; set; } = new Chair.Info();
    public Drawer.Info DrawerInfo { get; set; } = new Drawer.Info();
}
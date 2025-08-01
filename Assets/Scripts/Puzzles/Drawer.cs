using UnityEngine;

public class Drawer : MonoBehaviour
{
    [SerializeField] private StatusSwitcher lowerDrawerClose;
    [SerializeField] private ItemUsePoint handleAttachPoint;
    [SerializeField] private GameObject attachedHandle;
    private Info info;



    private void Start()
    {
        info = GameManager.Instance.PuzzleController.DrawerInfo;
        lowerDrawerClose.RegisterInteractCondition(() => info.isHandleAttached);
        LoadByInfo();
        handleAttachPoint.OnItemUse.AddListener(OnHandleAttached);
    }



    private void LoadByInfo()
    {
        handleAttachPoint.gameObject.SetActive(!info.isHandleAttached);
        attachedHandle.SetActive(info.isHandleAttached);
    }



    private void OnHandleAttached(ItemUsePoint point)
    {
        info.isHandleAttached = true;
        InventoryUI.Instance.DeleteTwoScrews();
        handleAttachPoint.gameObject.SetActive(!info.isHandleAttached);
        attachedHandle.SetActive(info.isHandleAttached);
    }



    public class Info
    {
        public bool isHandleAttached;
    }
}

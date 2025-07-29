using UnityEngine;

public class InteractController : MonoBehaviour
{
    [SerializeField] private TriggerItem[] triggerItems;



    public void SetTriggerItems(GameObject currentView)
    {
        Player player = GameManager.Instance.Player;
        triggerItems = currentView.GetComponentsInChildren<TriggerItem>();
        foreach (TriggerItem item in triggerItems)
        {
            item.CollectStatus = player.GetCollectStatus(item);
        }
    }



    public void SetTriggerItems()
    {
        SetTriggerItems(GameManager.Instance.RoomController.CurrentView);
    }
}

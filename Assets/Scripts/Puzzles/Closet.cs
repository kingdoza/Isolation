using Unity.VisualScripting;
using UnityEngine;

public class Closet : MonoBehaviour
{
    [SerializeField] private GameObject handleAttached;
    


    private void Start()
    {
        GameManager.Instance.RoomController.OnFocusOut.AddListener(OnClosetFrameFocusOut);
    }



    private void OnEnable()
    {
        SelectClosetHandle();
    }



    private void OnClosetFrameFocusOut()
    {
        SelectClosetHandle();
    }



    private void SelectClosetHandle()
    {
        bool isHandleAttached = GameManager.Instance.PuzzleController.DrawerInfo.isHandleAttached;
        handleAttached.SetActive(isHandleAttached);
    }
}

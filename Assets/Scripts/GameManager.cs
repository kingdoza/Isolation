using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    private RoomController roomController;
    private UIController uiController;
    public RoomController RoomController => roomController;
    public UIController UIController => uiController;



    private void Start()
    {
        StartStage();
    }



    private void StartStage()
    {
        roomController = FindAnyObjectByType<RoomController>();
        uiController = FindAnyObjectByType<UIController>();

        roomController.InitRoomAndShow();
    }
}

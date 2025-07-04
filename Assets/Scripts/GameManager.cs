using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    private RoomController roomController;
    private UIController uiController;
    private TimeController timeController;
    public RoomController RoomController => roomController;
    public UIController UIController => uiController;
    public TimeController TimeController => timeController;



    private void Start()
    {
        StartStage();
    }



    private void StartStage()
    {
        roomController = FindAnyObjectByType<RoomController>();
        uiController = FindAnyObjectByType<UIController>();
        timeController = FindAnyObjectByType<TimeController>();

        roomController.InitRoomAndShow();
        timeController.InitGameTime();
    }
}

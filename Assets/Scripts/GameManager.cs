using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    private RoomController roomController;
    private UIController uiController;
    private TimeController timeController;
    [SerializeField] private Texture2D hoverCursor;
    [SerializeField] private Vector2 viewSpriteSize;
    public RoomController RoomController => roomController;
    public UIController UIController => uiController;
    public TimeController TimeController => timeController;
    public Texture2D HoverCursor => hoverCursor;
    public Vector2 ViewSpriteSize => viewSpriteSize;



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

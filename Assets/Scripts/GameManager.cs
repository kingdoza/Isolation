using UnityEngine;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] private RoomController roomController;
    private UIController uiController;
    private TimeController timeController;
    private FilterController filterController;
    private InteractController interactController;
    private Player player;
    [SerializeField] private Texture2D hoverCursor;
    [SerializeField] private Vector2 viewSpriteSize;
    public RoomController RoomController => roomController;
    public UIController UIController => uiController;
    public TimeController TimeController => timeController;
    public FilterController FilterController => filterController;
    public InteractController InteractController => interactController;
    public Player Player => player;
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
        filterController = FindAnyObjectByType<FilterController>();
        interactController = FindAnyObjectByType<InteractController>();
        player = FindAnyObjectByType<Player>();

        roomController.InitRoomAndShow();
        timeController.InitGameTime();
    }
}

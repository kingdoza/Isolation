using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : PersistentSingleton<GameManager>
{
    [SerializeField] private RoomController roomController;
    private UIController uiController;
    private TimeController timeController;
    private FilterController filterController;
    private InteractController interactController;
    private SoundController soundController;
    private DialogueController dialogueController;
    private Player player;
    [SerializeField] private Texture2D hoverCursor;
    [SerializeField] private Vector2 viewSpriteSize;
    public RoomController RoomController => roomController;
    public UIController UIController => uiController;
    public TimeController TimeController => timeController;
    public FilterController FilterController => filterController;
    public InteractController InteractController => interactController;
    public SoundController SoundController => soundController;
    public DialogueController DialogueController => dialogueController;
    public Player Player => player;
    public Texture2D HoverCursor => hoverCursor;
    public Vector2 ViewSpriteSize => viewSpriteSize;
    //

    public GameObject BackGround;
    
    //
    public GameObject UIBlocker;
    //
    public RoomController RC;
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            bool isActive = BackGround.activeSelf;
            BackGround.SetActive(!isActive);
            UIBlocker.SetActive(!isActive);

            if (!isActive)
            {
                Time.timeScale = 0f;
                RC.enabled = false;
                DragScroller.CanDrag = false;
            }
            else
            {
                Time.timeScale = 1f;
                RC.enabled = true;
                DragScroller.CanDrag = true;
            }
        }
    }
    


    //  더 좋은 방법 있으면 대체하는게 좋을듯
    protected override void Awake()
    {
        base.Awake();

        if (Instance == this)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"[GameManager] Scene loaded: {scene.name}"); //게임 씬 재로딩할때 스타트 스테이지 로딩 
        if (scene.name == "Clock")
        {

            StartStage();
        }

    }
    // 



    private void StartStage()
    {
        //나중에 FindAnyObjectByType 는 전부 최적화 필요, 모든 컨트롤러들은 Controllers 안에 있으니
        roomController = FindAnyObjectByType<RoomController>();
        uiController = FindAnyObjectByType<UIController>();
        timeController = FindAnyObjectByType<TimeController>();
        filterController = FindAnyObjectByType<FilterController>();
        interactController = FindAnyObjectByType<InteractController>();
        soundController = FindAnyObjectByType<SoundController>();
        dialogueController = FindAnyObjectByType<DialogueController>();
        player = FindAnyObjectByType<Player>();

        BackGround = GameObject.Find("Background"); //
        BackGround.SetActive(false);  //

        roomController.InitRoomAndShow();
        timeController.InitGameTime();
        uiController.DisableMindTree();
        dialogueController.DisableDialoguePanel();
    }




}

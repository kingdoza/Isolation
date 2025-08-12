using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static EtcUtils;

public class GameManager : PersistentSingleton<GameManager>
{
    private RoomController roomController;
    private UIController uiController;
    private TimeController timeController;
    private FilterController filterController;
    private InteractController interactController;
    private SoundController soundController;
    private DialogueController dialogueController;
    private PuzzleController puzzleController;
    private TriggerEventController triggerController;
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
    public PuzzleController PuzzleController => puzzleController;
    public TriggerEventController TriggerController => triggerController;
    public Player Player => player;
    public Texture2D HoverCursor => hoverCursor;
    public Vector2 ViewSpriteSize => viewSpriteSize;
    //

    public GameObject BackGround;
    
    //
    public GameObject UIBlocker;
    //
    public RoomController RC;
    public EndingType EndingType {  get; private set; } = EndingType.None;


    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            bool isActive = BackGround.activeSelf;
            BackGround.SetActive(!isActive);
            //UIBlocker.SetActive(!isActive);

            if (!isActive)
            {
                Time.timeScale = 0f;
                roomController.enabled = false;
                if (!Player.Instance.IsUsingItemTypeMatched(ItemType.None))
                    ItemCursor.Instance.Disable();
            }
            else
            {
                Time.timeScale = 1f;
                roomController.enabled = true;
                if (!Player.Instance.IsUsingItemTypeMatched(ItemType.None))
                    ItemCursor.Instance.Enable(Player.Instance.ItemInUse);
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




    private void Start()
    {
        soundController = GetComponentInChildren<SoundController>();
        soundController.PlayBGM();
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SetCursorTexture();
        Debug.Log($"[GameManager] Scene loaded: {scene.name}"); //게임 씬 재로딩할때 스타트 스테이지 로딩 
        if (scene.name == "Clock")
        {
            //RegisterDragScrollCondition(() => !BackGround.activeSelf);
            StartStage();
        }

        if (scene.name == "Refactor")
        {
            //RegisterDragScrollCondition(() => !BackGround.activeSelf);
            StartStage();
        }

    }
    // 



    private void StartStage()
    {
        FindAnyObjectByType<MindTreeUI>().MotiveCompleteEvent.AddListener((EndingType completeType) => EndingType = completeType);
        //나중에 FindAnyObjectByType 는 전부 최적화 필요, 모든 컨트롤러들은 Controllers 안에 있으니
        roomController = FindAnyObjectByType<RoomController>();
        uiController = FindAnyObjectByType<UIController>();
        timeController = FindAnyObjectByType<TimeController>();
        filterController = FindAnyObjectByType<FilterController>();
        interactController = FindAnyObjectByType<InteractController>();
        //soundController = FindAnyObjectByType<SoundController>();
        dialogueController = FindAnyObjectByType<DialogueController>();
        puzzleController = FindAnyObjectByType<PuzzleController>();
        triggerController = FindAnyObjectByType<TriggerEventController>();
        player = FindAnyObjectByType<Player>();

        BackGround = GameObject.Find("Background"); //
        BackGround.SetActive(false);  //

        player.InitMotives();
        roomController.InitRoomAndShow();
        timeController.InitGameTime();
        uiController.InitMindTreeUI();
        dialogueController.DisableDialoguePanel();
    }
}

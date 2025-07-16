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
    //

    public GameObject BackGround;

    //

    //

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            bool isActive = BackGround.activeSelf;
            BackGround.SetActive(!isActive);


        }
    }


    
        // private void Start()
        // {
        //     BackGround.SetActive(true);
        // }

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
        Debug.Log($"[GameManager] Scene loaded: {scene.name}");
        StartStage();
    }
    // 



    private void StartStage()
    {
        roomController = FindAnyObjectByType<RoomController>();
        uiController = FindAnyObjectByType<UIController>();
        timeController = FindAnyObjectByType<TimeController>();
        filterController = FindAnyObjectByType<FilterController>();
        interactController = FindAnyObjectByType<InteractController>();
        player = FindAnyObjectByType<Player>();
        BackGround = GameObject.Find("Background"); //
        BackGround.SetActive(false);  //
        

        roomController.InitRoomAndShow();
        timeController.InitGameTime();
        
    }
   
    



}

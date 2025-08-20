using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static ControllerUtils;
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
    //[SerializeField] private Texture2D hoverCursor;
    //[SerializeField] private Vector2 viewSpriteSize;
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
    //public Texture2D HoverCursor => hoverCursor;
    //public Vector2 ViewSpriteSize => viewSpriteSize;
    //

    public GameObject BackGround;
    
    //
    public GameObject UIBlocker;
    //
    public RoomController RC;
    public EndingType EndingType {  get; set; } = EndingType.None;
    private const float FadeInDuration = 0.8f;
    private const float FadeOutDuration = 1.6f;
    private GameObject sceneFadePanel;
    [Header("테스트 전용")] [Space]
    [SerializeField] private bool isIntroStart;
    [SerializeField] public bool isTutorial;
    public bool IsIntroStart => isIntroStart;

    [SerializeField] private bool isEndingComplete;
    [SerializeField] private EndingType testEndingType;


    void Update()
    {
        if (SceneManager.GetActiveScene().name.Equals("Refactor") && Input.GetButtonDown("Cancel"))
        {
            bool isActive = BackGround.activeSelf;
            BackGround.SetActive(!isActive);
            //UIBlocker.SetActive(!isActive);
            PlaySFX(SFXClips.click2);

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
        soundController = GetComponentInChildren<SoundController>();

        if (Instance == this)
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
    }




    private void Start()
    {
        //soundController.PlayBGM();
        if (isEndingComplete)
        {
            timeController.ProgressToNextWakeup();
            EndingType = testEndingType;
            roomController.DisableRoomViews();
        }
    }



    private GameObject CreateFadePanel()
    {
        Canvas canvas = GameObject.FindWithTag("MainCanvas").GetComponent<Canvas>();
        GameObject blackImageObj = new GameObject("SceneFadePanel");
        blackImageObj.transform.SetParent(canvas.transform, false);

        Image img = blackImageObj.AddComponent<Image>();
        img.color = Color.black;

        RectTransform rt = blackImageObj.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;

        return blackImageObj;
    }



    public void LoadSceneWithFade(string sceneName, bool withBgmFade = true)
    {
        if (sceneFadePanel == null)
            sceneFadePanel = CreateFadePanel();
        sceneFadePanel.SetActive(true);
        if (withBgmFade)
            soundController.FadeOutBGM(FadeOutDuration);
        Image image = sceneFadePanel.GetComponent<Image>();
        image.color = new Color(0, 0, 0, 0);
        image.DOFade(1f, FadeOutDuration) // 1.5초 동안 페이드아웃
            .SetEase(Ease.InOutQuad) // 부드럽게
            .SetUpdate(true)
            .OnComplete(() =>
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(sceneName);
            });
    }



    private void FadeInPanel()
    {
        sceneFadePanel.SetActive(true);
        Image image = sceneFadePanel.GetComponent<Image>();
        image.DOFade(0f, FadeOutDuration) // 1.5초 동안 페이드아웃
            .SetEase(Ease.InOutQuad); // 부드럽게
    }



    private void FadeoutPanel()
    {
        Image image = sceneFadePanel.GetComponent<Image>();
        image.DOFade(0f, FadeOutDuration) // 1.5초 동안 페이드아웃
            .SetEase(Ease.InOutQuad) // 부드럽게
            .OnComplete(() =>
            {
                sceneFadePanel.SetActive(false);
            });
    }



    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!(scene.name.Equals("Intro") || (scene.name.Equals("Refactor") && isIntroStart)))
        {
            sceneFadePanel = CreateFadePanel();
            FadeoutPanel();
        }
        if (scene.name == "Intro")
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
            SetCursorTexture();
        }
        Debug.Log($"[GameManager] Scene loaded: {scene.name}"); //게임 씬 재로딩할때 스타트 스테이지 로딩 
        if (scene.name == "Intro")
        {
            isIntroStart = false;
            //isIntroStart = true;
            //PlayBGM(BGMClips.main, true);
        }
        if (scene.name == "MainScene")
        {
            //if (isIntroStart == false)
            ////PlayBGM(BGMClips.main, true);
            soundController.FadeInBGM(BGMClips.main, FadeInDuration);
        }
        if (scene.name == "Clock")
        {
            StartStage();
        }

        if (scene.name == "Refactor")
        {
            StartStage();
            ////PlayBGM(BGMClips.inGame, true);
            soundController.FadeInBGM(BGMClips.inGame, FadeInDuration);
        }

        if (scene.name == "Ending")
        {
            dialogueController = FindAnyObjectByType<DialogueController>();
            FindAnyObjectByType<EndingDialogue>().ShowEndingDialogues(EndingType);
            if (EndingType == EndingType.Bad)
            {
                ////PlayBGM(BGMClips.badEnding, true);
                soundController.FadeInBGM(BGMClips.badEnding, FadeInDuration);
            }
            else if (EndingType == EndingType.Happy)
            {
                ////PlayBGM(BGMClips.trueEnding, true);
                soundController.FadeInBGM(BGMClips.trueEnding, FadeInDuration);
            }
            else
            {
                ////PlayBGM(BGMClips.timeoutEnding, true);
                soundController.FadeInBGM(BGMClips.timeoutEnding, FadeInDuration);
            }
            //PlayBGM(null, true);
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

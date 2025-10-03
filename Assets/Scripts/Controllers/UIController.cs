using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using TMPro;
using static ControllerUtils;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    [Header("�̵� ��ư")] [Space]
    [SerializeField] private List<MoveButton> moveButtons;

    [Header("���� ��ȯ�� ���̵� ��&�ƿ�")] [Space]
    [SerializeField] private CanvasGroup fadeCanvas;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadeWait;

    [Header("��¥&�ð� UI")] [Space]
    [SerializeField] private TextMeshProUGUI ampmUI;
    [SerializeField] private TextMeshProUGUI timeUI;
    [SerializeField] private TextMeshProUGUI dateUI;
    [SerializeField] private TextMeshProUGUI daysOfWeekUI;

    [Header("�ɻ� ���� UI")] [Space]
    [SerializeField] private MindTreeUI mindTreeUI;
    [SerializeField] private GameObject toMindButton;
    [SerializeField] private GameObject toRoomButton;

    [Header("���� UI ��ȣ�ۿ�")] [Space]
    [SerializeField] private CanvasGroup[] leftUICanvases;
    [SerializeField] private GameObject lightSwitchWarning;
    [SerializeField] private GameObject helpPanel;

    [HideInInspector] public UnityEvent FadeCompleteEvent = new();

    private Tutorial tutorial;

    private bool isFading = false;
    public bool IsFading => isFading;


    private void Start()
    {
        helpPanel.SetActive(false);
        tutorial = FindAnyObjectByType<Tutorial>();
        lightSwitchWarning.SetActive(false);
        RegisterDragScrollCondition(() => !mindTreeUI.gameObject.activeSelf);
        GameManager.Instance.Player.OnInventoryItemSelect.AddListener(OnPlayerItemSelected);
        Player.Instance.ItemSelectEvent.AddListener((ItemData) => DisableLeftUI());
        Player.Instance.ItemUnselectEvent.AddListener((ItemData) => EnableLeftUI());
    }



    public void InitMindTreeUI()
    {
        mindTreeUI.Init();
        DisableMindTree();
    }



    private void SetMoveButtons(params MoveDirection[] directions)
    {
        foreach (var moveButton in moveButtons)
        {
            bool shouldEnable = Array.Exists(directions, dir => dir == moveButton.moveDir);
            moveButton.button.SetActive(shouldEnable);
        }
    }



    public void DisableMoveButtons()
    {
        SetMoveButtons();
    }



    public void EnableMoveButtons()
    {
        if (tutorial && tutorial.IsProgessing)
            return;
        MoveDirection[] zoomOutDirs = { MoveDirection.Left, MoveDirection.Right };
        MoveDirection[] zoomInDirs = { MoveDirection.Down };

        bool isZoomIn = GameManager.Instance.RoomController.IsZoomIn;
        bool isFocusIn = GameManager.Instance.RoomController.IsFocusIn;
        if (isZoomIn || isFocusIn)
            SetMoveButtons(zoomInDirs);
        else
            SetMoveButtons(zoomOutDirs);
    }



    public void FadeIn(float duration)
    {
        fadeCanvas.DOFade(1f, duration).SetEase(Ease.OutQuad);
        fadeCanvas.interactable = true;
        fadeCanvas.blocksRaycasts = true;
    }



    public void FadeOut(float duration)
    {
        fadeCanvas.DOFade(0f, duration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                fadeCanvas.interactable = false;
                fadeCanvas.blocksRaycasts = false;
            });
    }



    public void FadeOutThenIn(Action<GameObject> viewChangeAction, GameObject newView)
    {
        Sequence seq = DOTween.Sequence();
        fadeCanvas.gameObject.SetActive(true);

        isFading = true;
        seq.Append(fadeCanvas.DOFade(1f, fadeDuration))
           .AppendCallback(() => viewChangeAction?.Invoke(newView))
           .AppendInterval(fadeWait)
           .Append(fadeCanvas.DOFade(0f, fadeDuration))
           .SetEase(Ease.InOutQuad)
           .OnStart(() =>
           {
               fadeCanvas.interactable = true;
               fadeCanvas.blocksRaycasts = true;
           })
           .OnComplete(() =>
           {
               fadeCanvas.interactable = false;
               fadeCanvas.blocksRaycasts = false;
               fadeCanvas.gameObject.SetActive(false);
               isFading = false;
               FadeCompleteEvent?.Invoke();
           });
    }



    public void ShowGameDateClock(GameDate gameDate)
    {
        //timeUI.text = gameDate.TwelveClockTimeString(out string ampm);
        timeUI.text = gameDate.TwentyFourClockTimeString(out string ampm);
        ampmUI.text = ampm;
        dateUI.text = gameDate.DateString;
        daysOfWeekUI.text = gameDate.DayOfWeek.ToString();
    }



    public void EnableMindTree_Button()
    {
        if (tutorial && tutorial.IsProgessing)
        {
            tutorial.SkipNextPanel(false);
        }
        PlaySFX(SFXClips.click2);
        EnableMindTree();
    }



    public void EnableMindTree()
    {
        toMindButton.SetActive(false);
        toRoomButton.SetActive(true);
        //DragScroller.CanDrag = false;
        mindTreeUI.gameObject.SetActive(true);
        //EtcUtils.SetCursorTexture(GameManager.Instance.DefaultCursor);
        Time.timeScale = 0;
    }



    public void DisableMindTree_Button()
    {
        if (tutorial && tutorial.IsProgessing)
        {
            tutorial.SkipNextPanel(false);
        }
        PlaySFX(SFXClips.click2);
        DisableMindTree();
    }



    public void DisableMindTree()
    {
        toMindButton.SetActive(true);
        toRoomButton.SetActive(false);
        //DragScroller.CanDrag = true;
        mindTreeUI.gameObject.SetActive(false);
        //EtcUtils.SetCursorTextureCenter();
        Time.timeScale = 1;
    }



    private void OnPlayerItemSelected(UsableItem uitemType)
    {
        if(uitemType == UsableItem.None)
        {
            EnableLeftUI();
        }
        else
        {
            DisableLeftUI();
        }
    }



    public void DeactiveMindTree()
    {
        toMindButton.GetComponent<CanvasGroup>().alpha = 1f;
        //toMindButton.GetComponent<CanvasGroup>().interactable = false;
        toMindButton.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }



    public void ActiveMindTree()
    {
        toMindButton.GetComponent<CanvasGroup>().alpha = 1;
        toMindButton.GetComponent<CanvasGroup>().interactable = true;
        toMindButton.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }



    public void DeactiveRoomTree()
    {
        toRoomButton.GetComponent<CanvasGroup>().alpha = 1f;
        //toRoomButton.GetComponent<CanvasGroup>().interactable = false;
        toRoomButton.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }



    public void ActiveRoonTree()
    {
        toRoomButton.GetComponent<CanvasGroup>().alpha = 1;
        toRoomButton.GetComponent<CanvasGroup>().interactable = true;
        toRoomButton.GetComponent<CanvasGroup>().blocksRaycasts = true;
    }



    public void EnableLeftUI()
    {
        foreach (CanvasGroup uiCanvas in leftUICanvases)
        {
            uiCanvas.alpha = 1;
            uiCanvas.interactable = true;
            uiCanvas.blocksRaycasts = true;
        }
    }



    public void DisableLeftUI()
    {
        foreach (CanvasGroup uiCanvas in leftUICanvases)
        {
            uiCanvas.alpha = 0.5f;
            uiCanvas.interactable = false;
            uiCanvas.blocksRaycasts = false;
        }
    }



    public void ShowLightSwitchWarning()
    {
        lightSwitchWarning.SetActive(true);
    }



    public void HideLightSwitchWarning()
    {
        lightSwitchWarning.SetActive(false);
    }



    public void LightSwitch()
    {
        HideLightSwitchWarning();
        if (Player.Instance.IsSleeping == false)
        {
            PlaySFX(SFXClips.lightSwitch_Off);
        }
        else
        {
            PlaySFX(SFXClips.lightSwitch_On);
        }
        TimeController.Instance.TimeOver();
    }



    public void Help_Button()
    {
        if (tutorial && tutorial.IsProgessing)
            return;
        if (helpPanel.activeSelf)
        {
            HideHelp();
        }
        else
        {
            ShowHelp();
        }
    }



    private void ShowHelp()
    {
        DisableMoveButtons();
        PlaySFX(SFXClips.click1);
        helpPanel.SetActive(true);
        Time.timeScale = 0;
    }



    private void HideHelp()
    {
        EnableMoveButtons();
        PlaySFX(SFXClips.click2);
        helpPanel.SetActive(false);
        Time.timeScale = 1;
    }



    [System.Serializable]
    class MoveButton
    {
        public MoveDirection moveDir;
        public GameObject button;
    }
}



public enum MoveDirection
{
    Left, Right, Down
}
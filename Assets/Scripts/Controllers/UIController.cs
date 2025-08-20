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

    [HideInInspector] public UnityEvent FadeCompleteEvent = new();

    private bool isFading = false;
    public bool IsFading => isFading;


    private void Start()
    {
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
        PlaySFX(SFXClips.click2);
        EnableMindTree();
    }



    public void EnableMindTree()
    {
        toMindButton.SetActive(false);
        toRoomButton.SetActive(true);
        //DragScroller.CanDrag = false;
        mindTreeUI.gameObject.SetActive(true);
    }



    public void DisableMindTree_Button()
    {
        PlaySFX(SFXClips.click2);
        DisableMindTree();
    }



    public void DisableMindTree()
    {
        toMindButton.SetActive(true);
        toRoomButton.SetActive(false);
        //DragScroller.CanDrag = true;
        mindTreeUI.gameObject.SetActive(false);
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



    private void EnableLeftUI()
    {
        foreach (CanvasGroup uiCanvas in leftUICanvases)
        {
            uiCanvas.alpha = 1;
            uiCanvas.interactable = true;
            uiCanvas.blocksRaycasts = true;
        }
    }



    private void DisableLeftUI()
    {
        foreach (CanvasGroup uiCanvas in leftUICanvases)
        {
            uiCanvas.alpha = 0.5f;
            uiCanvas.interactable = false;
            uiCanvas.blocksRaycasts = false;
        }
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
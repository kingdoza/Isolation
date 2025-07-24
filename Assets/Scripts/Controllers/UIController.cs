using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using TMPro;
using static ControllerUtils;

public class UIController : MonoBehaviour
{
    [Header("이동 버튼")] [Space]
    [SerializeField] private List<MoveButton> moveButtons;

    [Header("시점 전환의 페이드 인&아웃")] [Space]
    [SerializeField] private CanvasGroup fadeCanvas;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float fadeWait;

    [Header("날짜&시간 UI")] [Space]
    [SerializeField] private TextMeshProUGUI ampmUI;
    [SerializeField] private TextMeshProUGUI timeUI;
    [SerializeField] private TextMeshProUGUI dateUI;
    [SerializeField] private TextMeshProUGUI daysOfWeekUI;

    [Header("심상 관련 UI")] [Space]
    [SerializeField] private GameObject mindTreePanel;
    [SerializeField] private GameObject toMindButton;
    [SerializeField] private GameObject toRoomButton;

    private bool isFading = false;
    public bool IsFading => isFading;


    private void Start()
    {
        RegisterDragScrollCondition(() => !mindTreePanel.activeSelf);
    }



    public void EnableMoveButtons(params MoveDirection[] directions)
    {
        foreach (var moveButton in moveButtons)
        {
            bool shouldEnable = Array.Exists(directions, dir => dir == moveButton.moveDir);
            moveButton.button.SetActive(shouldEnable);
        }
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
           });
    }



    public void ShowGameDateClock(GameDate gameDate)
    {
        timeUI.text = gameDate.TwelveClockTimeString(out string ampm);
        ampmUI.text = ampm;
        dateUI.text = gameDate.DateString;
        daysOfWeekUI.text = gameDate.DayOfWeek.ToString();
    }



    public void EnableMindTree()
    {
        toMindButton.SetActive(false);
        toRoomButton.SetActive(true);
        //DragScroller.CanDrag = false;
        mindTreePanel.SetActive(true);
    }



    public void DisableMindTree()
    {
        toMindButton.SetActive(true);
        toRoomButton.SetActive(false);
        //DragScroller.CanDrag = true;
        mindTreePanel.SetActive(false);
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
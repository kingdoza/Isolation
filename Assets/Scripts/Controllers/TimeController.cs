using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class TimeController : SceneSingleton<TimeController>
{
    [SerializeField] private List<MinuteProgression> minuteProgressions;
    [SerializeField] private GameDate startGameDate;
    [SerializeField] private int limitDayGap;
    [SerializeField] private int wakeupHour;
    [SerializeField] private int sleepHour;
    [SerializeField] private bool isWakeupStart;
    [SerializeField] private string[] wakeupDialogue;
    [SerializeField] private string[] sleepDialogue;
    private Dictionary<ProgressTimeType, int> progressionDict;
    private GameDate currentGameDate;
    private GameDate limitGameDate;
    private UIController uiController;
    private bool isTimeChanged = false;



    public void InitGameTime()
    {
        uiController = GameManager.Instance.UIController;
        uiController.FadeCompleteEvent.AddListener(CheckTimeChanged);

        currentGameDate = (GameDate)startGameDate.Clone();
        currentGameDate.AdvanceHours(isWakeupStart ? wakeupHour : sleepHour);
        currentGameDate.NormalizeTime();

        limitGameDate = (GameDate)startGameDate.Clone();
        limitGameDate.AdvanceDays(limitDayGap);
        limitGameDate.AdvanceHours(sleepHour);
        currentGameDate.NormalizeTime();

        uiController.ShowGameDateClock(currentGameDate);
        OnTimeStatusChanged(isWakeupStart);
        isTimeChanged = false;

        progressionDict = new Dictionary<ProgressTimeType, int>();
        foreach (var prog in minuteProgressions)
        {
            progressionDict[prog.type] = prog.minutes;
        }
    }



    public void ProgressMinutes(ProgressTimeType type)
    {
        int minutes = 0;
        if (!progressionDict.TryGetValue(type, out minutes))
            return;

        GameDate prevGameDate = (GameDate)currentGameDate.Clone();
        currentGameDate.AdvanceMinutes(minutes);

        bool isCurrentTimeSleep = IsSleepingTime();
        if(IsSleepingTime(prevGameDate) != isCurrentTimeSleep)
        {
            OnTimeStatusChanged(!isCurrentTimeSleep);
        }

        uiController.ShowGameDateClock(currentGameDate);
        if (currentGameDate >= limitGameDate)
        {
            TimeOver();
        }
    }



    public void ProgressMinutes(int minutes)
    {
        GameDate prevGameDate = (GameDate)currentGameDate.Clone();
        currentGameDate.AdvanceMinutes(minutes);

        bool isCurrentTimeSleep = IsSleepingTime();
        if (IsSleepingTime(prevGameDate) != isCurrentTimeSleep)
        {
            OnTimeStatusChanged(!isCurrentTimeSleep);
        }

        uiController.ShowGameDateClock(currentGameDate);
        if (currentGameDate >= limitGameDate)
        {
            TimeOver();
        }
    }



    private void OnTimeStatusChanged(bool isWakeup)
    {
        if (isWakeup)
        {
            OnWakeupTime();
        }
        else
        {
            OnSleepTime();
        }
        isTimeChanged = true;
    }



    public void CheckTimeChanged()
    {
        if (!isTimeChanged) return;
        isTimeChanged = false;
        ShowWakeSleepDialogue();
    }



    private void ShowWakeSleepDialogue()
    {
        GameManager.Instance.UIController.DisableMoveButtons();
        GameManager.Instance.DialogueController.DiagloueEndEvent.AddListener(OnDiaglogueClosed);
        string[] dialogues = Player.Instance.IsSleeping ? sleepDialogue : wakeupDialogue;
        GameManager.Instance.DialogueController.StartDialogueSequence(dialogues);
    }



    private void OnDiaglogueClosed()
    {
        GameManager.Instance.UIController.EnableMoveButtons();
        GameManager.Instance.RoomController.ReturnToRoomView();
        GameManager.Instance.DialogueController.DiagloueEndEvent.RemoveListener(OnDiaglogueClosed);
    }



    private void OnWakeupTime()
    {
        GameManager.Instance.Player.Wakeup();
    }



    private void OnSleepTime()
    {
        GameManager.Instance.Player.Sleep();
    }



    private void TimeOver()
    {
        Debug.Log("Time Over!!!");
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        SceneManager.LoadScene("TimeOver");
    }



    private bool IsSleepingTime(GameDate gameDate)
    {
        return (gameDate.Hours >= sleepHour && gameDate.Hours < wakeupHour);
    }



    public bool IsSleepingTime()
    {
        return IsSleepingTime(currentGameDate);
    }



    public bool IsLastDay()
    {
        return ((limitGameDate - currentGameDate) / 60) <= 12;
    }



    [System.Serializable]
    private class MinuteProgression
    {
        public ProgressTimeType type;
        public int minutes;
    }
}



public enum ProgressTimeType
{
    Move, ZoomIn, ZoomOut, ItemInteract
}
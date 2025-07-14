using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    [SerializeField] private List<MinuteProgression> minuteProgressions;
    [SerializeField] private GameDate startGameDate;
    [SerializeField] private int limitDayGap;
    [SerializeField] private int wakeupHour;
    [SerializeField] private int sleepHour;
    [SerializeField] private bool isWakeupStart;
    private Dictionary<ProgressTimeType, int> progressionDict;
    private GameDate currentGameDate;
    private GameDate limitGameDate;
    private UIController uiController;



    public void InitGameTime()
    {
        uiController = GameManager.Instance.UIController;

        currentGameDate = (GameDate)startGameDate.Clone();
        currentGameDate.AdvanceHours(isWakeupStart ? wakeupHour : sleepHour);
        currentGameDate.NormalizeTime();

        limitGameDate = (GameDate)startGameDate.Clone();
        limitGameDate.AdvanceDays(limitDayGap);
        limitGameDate.AdvanceHours(sleepHour);
        currentGameDate.NormalizeTime();

        uiController.ShowGameDateClock(currentGameDate);
        OnTimeStatusChanged(isWakeupStart);

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
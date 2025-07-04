using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] private List<MinuteProgression> minuteProgressions;
    [SerializeField] private GameDate startGameDate;
    [SerializeField] private int limitDayGap;
    private Dictionary<ProgressTimeType, int> progressionDict;
    private GameDate currentGameDate;
    private GameDate limitGameDate;
    private UIController uiController;



    public void InitGameTime()
    {
        uiController = GameManager.Instance.UIController;
        currentGameDate = (GameDate)startGameDate.Clone();        
        currentGameDate.NormalizeTime();
        limitGameDate = (GameDate)currentGameDate.Clone();
        limitGameDate.AdvanceDays(limitDayGap);
        uiController.ShowGameDateClock(currentGameDate);

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

        currentGameDate.AdvanceMinutes(minutes);
        uiController.ShowGameDateClock(currentGameDate);
        if (currentGameDate >= limitGameDate)
        {
            TimeOver();
        }
    }



    private void TimeOver()
    {
        Debug.Log("Time Over!!!");
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
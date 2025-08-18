using UnityEngine;

public class SwitchZoomStuff : ZoomStuff
{
    protected override void OnClicked()
    {
        if (!enabled) return;
        //base.OnClicked();
        if (GameManager.Instance.TimeController.IsLastDay())
        {
            GameManager.Instance.DialogueController.StartDialogueSequence(new string[]{ "오늘은 잠이 오지 않는다."});
            Debug.Log("오늘은 잠이오지 않는다.");
            return;
        }
        base.OnClicked();
    }
}

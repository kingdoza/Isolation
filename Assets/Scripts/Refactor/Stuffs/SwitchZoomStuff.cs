using UnityEngine;

public class SwitchZoomStuff : ZoomStuff
{
    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        if (GameManager.Instance.TimeController.IsLastDay())
        {
            Debug.Log("오늘은 잠이오지 않는다.");
            return;
        }
        base.OnClicked();
    }
}

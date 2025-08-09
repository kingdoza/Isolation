using UnityEngine;

public class SwitchZoomStuff : ZoomStuff
{
    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        if (GameManager.Instance.TimeController.IsLastDay())
        {
            Debug.Log("������ ���̿��� �ʴ´�.");
            return;
        }
        base.OnClicked();
    }
}

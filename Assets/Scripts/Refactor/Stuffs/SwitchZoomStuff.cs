using UnityEngine;

public class SwitchZoomStuff : ZoomStuff
{
    protected override void OnClicked()
    {
        base.OnClicked();
        if (GameManager.Instance.TimeController.IsLastDay())
        {
            Debug.Log("������ ���̿��� �ʴ´�.");
            return;
        }
        base.OnClicked();
    }
}

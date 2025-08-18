using UnityEngine;

public class SwitchZoomStuff : ZoomStuff
{
    protected override void OnClicked()
    {
        if (!enabled) return;
        //base.OnClicked();
        if (GameManager.Instance.TimeController.IsLastDay())
        {
            GameManager.Instance.DialogueController.StartDialogueSequence(new string[]{ "������ ���� ���� �ʴ´�."});
            Debug.Log("������ ���̿��� �ʴ´�.");
            return;
        }
        base.OnClicked();
    }
}

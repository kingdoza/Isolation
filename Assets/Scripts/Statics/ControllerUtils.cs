using System;
using UnityEngine;

public static class ControllerUtils
{
    public static void RegisterDragScrollCondition(Func<bool> condition)
    {
        Debug.Log(condition);
        DragScroller dragScroller = Camera.main.GetComponent<DragScroller>();
        dragScroller.RegisterCondition(condition);
    }



    public static void PlaySFX(AudioClip clip)
    {
        GameManager.Instance.SoundController.PlaySFX(clip);
    }
}

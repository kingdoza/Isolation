using System;
using UnityEngine;

public static class ControllerUtils
{
    private static SFXLibrary sfxClips;

    public static SFXLibrary SFXClips
    {
        get
        {
            if (sfxClips == null)
                sfxClips = Resources.Load<SFXLibrary>("SO_SFXClips");

            if (sfxClips == null)
                Debug.LogError("SFXLibrary asset not found in Resources!");

            return sfxClips;
        }
    }



    private static BGMLibrary bgmClips;

    public static BGMLibrary BGMClips
    {
        get
        {
            if (bgmClips == null)
                bgmClips = Resources.Load<BGMLibrary>("SO_BGMClips");

            if (bgmClips == null)
                Debug.LogError("BGMLibrary asset not found in Resources!");

            return bgmClips;
        }
    }



    public static void RegisterDragScrollCondition(Func<bool> condition)
    {
        DragScroller dragScroller = Camera.main.GetComponent<DragScroller>();
        dragScroller.RegisterCondition(condition);
    }



    public static void PlaySFX(AudioClip clip)
    {
        GameManager.Instance.SoundController.PlaySFX(clip);
    }
}

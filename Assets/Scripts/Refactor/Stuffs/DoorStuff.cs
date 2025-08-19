using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ControllerUtils;

public class DoorStuff : ClickableStuff
{
    [SerializeField] private float dialogueDelay;
    [HideInInspector] public bool canOpen = false;


    protected override void Awake()
    {
        base.Awake();
        //inputComp.DisableInput()
    }



    //protected override void ChangeInputStatus() { }



    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        if (canOpen)
        {
            PlaySFX(SFXClips.door_Handle);
            GameManager.Instance.LoadSceneWithFade("Ending");
        }
        else
        {
            PlaySFX(SFXClips.door_Lock);
            GameManager.Instance.DialogueController.StartDialogueSequence(new string[] { "문은 잠겨있다." }, dialogueDelay);
        }
        //SceneManager.LoadScene("Ending");
    }
}

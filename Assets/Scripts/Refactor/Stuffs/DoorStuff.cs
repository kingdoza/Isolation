using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ControllerUtils;
using static UnityEngine.GraphicsBuffer;

public class DoorStuff : ClickableStuff
{
    protected override StuffTypeData StuffData => GameData.DoorStuffData;
    [SerializeField] private float dialogueDelay;
    [HideInInspector] public bool canOpen = false;
    private Tutorial tutorial;


    protected override void Awake()
    {
        base.Awake();
        tutorial = FindAnyObjectByType<Tutorial>();
        //inputComp.DisableInput()
        tutorial.TutorialStartEvent.AddListener(() => inputComp.DisableInput());
        //tutorial.TutorialEndEvent.AddListener(() => inputComp.EnableInput());
    }



    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        if (canOpen)
        {
            inputComp.DisableInput();
            PlaySFX(SFXClips.door_Handle);
            DOVirtual.DelayedCall(SFXClips.door_Handle.length + 0.2f, () =>
            {
                PlaySFX(SFXClips.door_Open);
                GameManager.Instance.LoadSceneWithFade("Ending");
            });
        }
        else
        {
            PlaySFX(SFXClips.door_Lock);
            GameManager.Instance.DialogueController.StartDialogueSequence(new string[] { "문은 잠겨있다." }, dialogueDelay);
        }
        //SceneManager.LoadScene("Ending");
    }
}

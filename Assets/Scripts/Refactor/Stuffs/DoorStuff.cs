using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            GameManager.Instance.LoadSceneWithFade("Ending");
        }
        else
        {
            GameManager.Instance.DialogueController.StartDialogueSequence(new string[] { "���� ����ִ�." }, dialogueDelay);
        }
        //SceneManager.LoadScene("Ending");
    }
}

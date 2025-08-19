using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorStuff : ClickableStuff
{
    public bool canOpen = false;


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
            GameManager.Instance.DialogueController.StartDialogueSequence(new string[] { "문은 잠겨있다." });
        }
        //SceneManager.LoadScene("Ending");
    }
}

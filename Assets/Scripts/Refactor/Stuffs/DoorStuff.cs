using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorStuff : ClickableStuff
{
    protected override void Awake()
    {
        base.Awake();
        //enabled = false;
        inputComp.DisableInput();
    }



    protected override void ChangeInputStatus() { }



    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        SceneManager.LoadScene("Ending");
    }
}

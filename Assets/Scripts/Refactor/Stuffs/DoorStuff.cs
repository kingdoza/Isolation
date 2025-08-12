using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorStuff : ClickableStuff
{
    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        SceneManager.LoadScene("Ending");
    }
}

using UnityEngine;
using static ControllerUtils;

public class GameExit : MonoBehaviour
{
    public void ExitGame()
    {
        PlaySFX(SFXClips.click2);
        Application.Quit();
    }
}

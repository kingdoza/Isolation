using UnityEngine;
using UnityEngine.SceneManagement;
using static ControllerUtils;

public class ToMainScene : MonoBehaviour
{
    public void tomainscene()
    {
        PlaySFX(SFXClips.click2);
        SceneManager.LoadScene("MainScene");
   }
}

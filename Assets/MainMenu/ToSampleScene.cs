using UnityEngine;
using UnityEngine.SceneManagement;
using static ControllerUtils;

public class ToSampleScene : MonoBehaviour
{

    public void StartGame()
    {
        Time.timeScale = 1;
        PlaySFX(SFXClips.click2);
        //GameManager.Instance.LoadSceneWithFade("Refactor");
        if (GameManager.Instance.IsIntroStart)
            GameManager.Instance.LoadSceneWithFade("Intro", false);
        else
            GameManager.Instance.LoadSceneWithFade("Refactor");
        //SceneManager.LoadScene("Refactor");
    }
}

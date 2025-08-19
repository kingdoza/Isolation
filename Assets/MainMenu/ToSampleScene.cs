using UnityEngine;
using UnityEngine.SceneManagement;

public class ToSampleScene : MonoBehaviour
{

    public void StartGame()
    {
        Time.timeScale = 1;
        //GameManager.Instance.LoadSceneWithFade("Refactor");
        if (GameManager.Instance.IsIntroStart)
            GameManager.Instance.LoadSceneWithFade("Intro");
        else
            GameManager.Instance.LoadSceneWithFade("Refactor");
        //SceneManager.LoadScene("Refactor");
    }
}

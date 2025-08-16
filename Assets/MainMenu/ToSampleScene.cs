using UnityEngine;
using UnityEngine.SceneManagement;

public class ToSampleScene : MonoBehaviour
{

    public void StartGame()
    {
        Time.timeScale = 1;
        GameManager.Instance.LoadSceneWithFade("Refactor");
        //SceneManager.LoadScene("Refactor");
    }
    
    
    
}

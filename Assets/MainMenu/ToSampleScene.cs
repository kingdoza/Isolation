using UnityEngine;
using UnityEngine.SceneManagement;

public class ToSampleScene : MonoBehaviour
{

    public void StartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Clock");
    }
    
    
    
}

using UnityEngine;
using UnityEngine.SceneManagement;
using static ControllerUtils;

public class ETCSetting : MonoBehaviour
{
    [SerializeField] private GameObject BackGround;
    public RoomController RC;
    public GameObject UIBlocker;
    public void OpenBackGround()
    {
        if (SceneManager.GetActiveScene().name.Equals("Refactor"))
        {
            PlaySFX(SFXClips.click1);
        }
        else
        {
            PlaySFX(SFXClips.click2);
        }
        BackGround.SetActive(true);
        UIBlocker.SetActive(true);

        Time.timeScale = 0f;
        RC.enabled = false;
    }


    
}

using UnityEngine;

public class ETCSetting : MonoBehaviour
{
    [SerializeField] private GameObject BackGround;
    public RoomController RC;
    public GameObject UIBlocker;
    public void OpenBackGround()
    {
        BackGround.SetActive(true);
        UIBlocker.SetActive(true);

        Time.timeScale = 0f;
        RC.enabled = false;
    }


    
}

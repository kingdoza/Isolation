using UnityEngine;
using UnityEngine.SceneManagement;
using static ControllerUtils;

public class OpenSetting : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    public void OpenPanel()
    {
        if (SceneManager.GetActiveScene().name.Equals("Refactor"))
        {
            PlaySFX(SFXClips.click1);
        }
        else
        {
            PlaySFX(SFXClips.click2);
        }
        settingsPanel.SetActive(true);
    }
}

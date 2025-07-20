using UnityEngine;

public class OpenSetting : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    public void OpenPanel()
    {
        settingsPanel.SetActive(true);
    }
}

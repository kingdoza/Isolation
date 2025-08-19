using UnityEngine;
using UnityEngine.Rendering;
using static ControllerUtils;

public class CloseSetting : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    public void ClosePanel()
    {
        PlaySFX(SFXClips.click2);
        settingsPanel.SetActive(false);
    }
}

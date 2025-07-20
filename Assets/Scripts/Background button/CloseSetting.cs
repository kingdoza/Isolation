using UnityEngine;
using UnityEngine.Rendering;

public class CloseSetting : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    public void ClosePanel()
    {
        settingsPanel.SetActive(false);
    }
}

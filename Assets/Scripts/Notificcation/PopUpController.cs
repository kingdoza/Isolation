using UnityEngine;

public class PopupController : MonoBehaviour
{
    public GameObject evidencePopup;
    public void ClosePopup()
    {
        evidencePopup.SetActive(false);
    }
}
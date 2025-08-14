using UnityEngine;
using static EtcUtils;

public class DeliveryBoxStuff : ClickableStuff
{
    [SerializeField] private GameObject[] wingsClose;
    [SerializeField] private GameObject[] wingsOpen;



    private void OnEnable()
    {
        CloseWings();
        inputComp.EnableInput();
    }



    protected override void OnClicked()
    {
        if (!enabled) return;
        base.OnClicked();
        OpenWings();
        inputComp.DisableInput();
    }



    private void OpenWings()
    {
        foreach (GameObject wing in wingsClose)
        {
            wing.SetActive(false);
        }
        foreach (GameObject wing in wingsOpen)
        {
            wing.SetActive(true);
        }
    }



    private void CloseWings()
    {
        foreach (GameObject wing in wingsClose)
        {
            wing.SetActive(true);
        }
        foreach (GameObject wing in wingsOpen)
        {
            wing.SetActive(false);
        }
    }



    protected override void OnCursorEntered()
    {
        if (!enabled) return;
        foreach (GameObject wing in wingsClose)
        {
            wing.GetComponent<SpriteRenderer>().color = Color.gray;
        }
        SetCursorTexture(StuffData.CursorTexture);
    }



    protected override void OnCursorExited()
    {
        if (!enabled) return;
        foreach (GameObject wing in wingsClose)
        {
            wing.GetComponent<SpriteRenderer>().color = Color.white;
        }
        SetCursorTexture();
    }
}

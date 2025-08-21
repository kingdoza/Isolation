using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndingSlot : MotiveSlot
{
    public GameObject evidencePopup;
    public TextMeshProUGUI popupText;
    public string evidenceMessage;

    public override void Collected(MindTreeUI mindTreeUI)
    {
        transform.Find("Fill").GetComponent<Image>().color = mindTreeUI.FinalSlotColor;
        GameManager.Instance.DialogueController.StartDialogueSequence(new string[] { evidenceMessage }, 0.5f);
        //ShowPopup();
        UIColorController.Instance.ChangeImageColor();
    }

    private void ShowPopup()
    {
        if (evidencePopup != null && popupText != null)
        {
            popupText.text = evidenceMessage;
            evidencePopup.SetActive(true);
        }
        else
        {
            Debug.LogWarning("회고 팝업 연결 안됨");
        }
    }
}

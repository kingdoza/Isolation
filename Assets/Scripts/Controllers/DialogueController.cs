using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class DialogueController : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private float typeDelay = 0.1f;
    private int textIndex = 0;
    private string[] targetTexts;
    private string targetSentence;
    private bool isTyping = false;



    public void StartItemDialogSequence(DialogueItem item) 
    {
        textBox.text = "";
        EnableDialoguePanel();
        bool isPlayerSleep = GameManager.Instance.Player.IsSleeping;
        targetTexts = isPlayerSleep ? item.SleepDialogs : item.WakeupDialogs;
        targetSentence = targetTexts[0];
        StartCoroutine(TypeText_Co());
    }



    private IEnumerator TypeText_Co()
    {
        isTyping = true;
        textBox.text = "";
        foreach (char letter in targetSentence)
        {
            textBox.text += letter;
            yield return new WaitForSeconds(typeDelay);
        }
        isTyping = false;
    }



    private void CompleteDialogue()
    {
        isTyping = false;
        StopAllCoroutines();
        textBox.text = targetSentence;
    }



    public void OnDialogueBoxClicked()
    {
        if (isTyping)
        {
            CompleteDialogue();
        }
        else if(++textIndex >= targetTexts.Length)
        {
            DisableDialoguePanel();
        }
        else
        {
            targetSentence = targetTexts[textIndex];
            StartCoroutine(TypeText_Co());
        }
    }



    private void EnableDialoguePanel()
    {
        //DragScroller.CanDrag = false;
        textBox.text = "";
        dialoguePanel.SetActive(true);
    }



    public void DisableDialoguePanel()
    {
        isTyping = false;
        //DragScroller.CanDrag = true;
        dialoguePanel.SetActive(false);
        textBox.text = "";
    }
}
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ControllerUtils;


public class DialogueController : MonoBehaviour
{
    [SerializeField] private Color darkenColor;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject dialogueTextUI;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private float typeDelay = 0.1f;
    private int textIndex = 0;
    private string[] targetTexts;
    private string targetSentence;
    private bool isTyping = false;
    private UnityEvent OnDialogueClosed = new UnityEvent();
    [HideInInspector] public UnityEvent DiagloueEndEvent = new UnityEvent();

    public Color DarkenColor => darkenColor;



    private void Start()
    {
        RegisterDragScrollCondition(() => !dialoguePanel.activeSelf);
        RegisterDragScrollCondition(() => !dialogueTextUI.activeSelf);
    }



    public void StartItemDialogSequence(DialogueItem item)
    {
        bool isPlayerSleep = GameManager.Instance.Player.IsSleeping;
        string[] dialogueTexts = isPlayerSleep ? item.SleepDialogs : item.WakeupDialogs;
        if (dialogueTexts == null || dialogueTexts.Length <= 0)
            return;
        item.OnDiaglogueStart();
        OnDialogueClosed.AddListener(item.OnDialogueEnd);
        StartDialogueSequence(dialogueTexts);
    }



    public void StartDialogueSequence(string[] sentences)
    {
        textBox.text = "";
        EnableDialoguePanel();
        targetTexts = sentences;
        targetSentence = targetTexts[textIndex = 0];
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
        dialogueTextUI.SetActive(true);
    }



    public void DisableDialoguePanel()
    {
        OnDialogueClosed?.Invoke();
        OnDialogueClosed.RemoveAllListeners();
        DiagloueEndEvent?.Invoke();
        DiagloueEndEvent.RemoveAllListeners();
        isTyping = false;
        //DragScroller.CanDrag = true;
        dialoguePanel.SetActive(false);
        dialogueTextUI.SetActive(false);
        textBox.text = "";
    }
}
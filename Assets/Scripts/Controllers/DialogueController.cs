using DG.Tweening.Core.Easing;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static ControllerUtils;
using static UnityEditor.Progress;


public class DialogueController : MonoBehaviour
{
    [SerializeField] private Color darkenColor;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI textBox;
    [SerializeField] private float typeDelay = 0.1f;
    private int textIndex = 0;
    private string[] targetTexts;
    private string targetSentence;
    private bool isTyping = false;
    private UnityEvent OnDialogueClosed = new UnityEvent();



    private void Start()
    {
        RegisterDragScrollCondition(() => !dialoguePanel.activeSelf);
    }



    private void SetOtherRoomObjectsColor(GameObject targetObject, Color color)
    {
        GameObject viewObject = GameManager.Instance.RoomController.CurrentView;
        SpriteRenderer[] childsRenderers = viewObject.GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer renderer in childsRenderers)
        {
            if (targetObject.GetComponent<SpriteRenderer>() == renderer)
                continue;
            renderer.color = color;
        }
    }



    public void StartItemDialogSequence(DialogueItem item)
    {
        SetOtherRoomObjectsColor(item.gameObject, darkenColor);
        GameManager.Instance.UIController.DisableMoveButtons();

        OnDialogueClosed.AddListener(() => SetOtherRoomObjectsColor(item.gameObject, Color.white));
        OnDialogueClosed.AddListener(() => GameManager.Instance.UIController.EnableMoveButtons());

        bool isPlayerSleep = GameManager.Instance.Player.IsSleeping;
        StartDialogueSequence(isPlayerSleep ? item.SleepDialogs : item.WakeupDialogs);
    }



    private void StartDialogueSequence(string[] sentences)
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
    }



    public void DisableDialoguePanel()
    {
        OnDialogueClosed?.Invoke();
        OnDialogueClosed.RemoveAllListeners();
        isTyping = false;
        //DragScroller.CanDrag = true;
        dialoguePanel.SetActive(false);
        textBox.text = "";
    }
}
using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
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
    private AudioSource typeAudioSource;
    private bool isDelaying;

    public Color DarkenColor => darkenColor;



    private void Awake()
    {
        typeAudioSource = gameObject.AddComponent<AudioSource>();
        SetLoopSFXAudioSource(ref typeAudioSource, SFXClips.narration);
    }



    //private void Start()
    //{
    //    typeAudioSource = gameObject.AddComponent<AudioSource>();
    //    SetLoopSFXAudioSource(ref typeAudioSource, SFXClips.narration);
    //}



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
        if (targetTexts == null || targetTexts.Length <= 0)
            targetTexts = new string[1] { " " };
        targetSentence = targetTexts[textIndex = 0];
        StartCoroutine(TypeText_Co(0));
    }



    public void StartDialogueSequence(string[] sentences, float delay)
    {
        textBox.text = "";
        EnableDialoguePanel();
        targetTexts = sentences;
        if (targetTexts == null || targetTexts.Length <= 0)
            targetTexts = new string[1] { " " };
        targetSentence = targetTexts[textIndex = 0];
        StartCoroutine(TypeText_Co(delay));
    }



    private IEnumerator TypeText_Co(float delay)
    {
        isDelaying = true;
        yield return new WaitForSeconds(delay);
        isDelaying = false;
        isTyping = true;
        textBox.text = "";
        typeAudioSource.Play();
        foreach (char letter in targetSentence)
        {
            textBox.text += letter;
            yield return new WaitForSeconds(typeDelay);
        }
        typeAudioSource.Stop();
        isTyping = false;
    }



    private void CompleteDialogue()
    {
        typeAudioSource.Stop();
        isTyping = false;
        StopAllCoroutines();
        textBox.text = targetSentence;
    }



    public void OnDialogueBoxClicked()
    {
        if (SceneManager.GetActiveScene().name.Equals("Refactor"))
        {
            PlaySFX(SFXClips.click1);
        }
        if (isDelaying)
            return;
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
            StartCoroutine(TypeText_Co(0));
        }
    }



    private void EnableDialoguePanel()
    {
        //DragScroller.CanDrag = false;
        textBox.text = "";
        RoomController roomController = GameManager.Instance.RoomController;
        if (roomController)
        {
            string dialoguePanelLayer = GameManager.Instance.RoomController.IsFocusIn ? "Focus" : "Default";
            dialoguePanel.GetComponent<SpriteRenderer>().sortingLayerName = dialoguePanelLayer;
        }
        dialoguePanel.SetActive(true);
        dialogueTextUI.SetActive(true);
    }



    public void DisableDialoguePanel()
    {
        OnDialogueClosed?.Invoke();
        OnDialogueClosed.RemoveAllListeners();
        isTyping = false;
        //DragScroller.CanDrag = true;
        dialoguePanel.SetActive(false);
        dialogueTextUI.SetActive(false);
        textBox.text = "";
        DiagloueEndEvent?.Invoke();
    }
}
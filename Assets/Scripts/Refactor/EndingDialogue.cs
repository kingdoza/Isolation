using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingDialogue : MonoBehaviour
{
    [SerializeField] private DialogueEntry[] dialogueEntries;
    private Dictionary<EndingType, string[]> dialogues = new Dictionary<EndingType, string[]>();



    private void Awake()
    {
        foreach (DialogueEntry dialogueEntry in dialogueEntries)
        {
            dialogues.Add(dialogueEntry.endingType, dialogueEntry.texts);
        }
    }



    public void ShowEndingDialogues(EndingType endingType)
    {
        DialogueController dialogueController = GameManager.Instance.DialogueController;
        dialogueController.DiagloueEndEvent.AddListener(OnEndingTextEnd);
        dialogueController.StartDialogueSequence(dialogues[endingType]);
    }



    private void OnEndingTextEnd()
    {
        SceneManager.LoadScene("MainScene");
    }



    [Serializable]
    private class DialogueEntry
    {
        public EndingType endingType;
        [TextArea] public string[] texts;
    }
}
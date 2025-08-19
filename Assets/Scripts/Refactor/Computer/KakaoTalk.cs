using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class KakaoTalk : MonoBehaviour
{
    [SerializeField] private AppProgram appProgram;
    [SerializeField] private TMP_InputField pinInputField;
    [SerializeField] private string pin;
    [SerializeField] private GameObject wrongPinPhrase;
    [SerializeField] private GameObject lockCanvas;
    [SerializeField] private GameObject talkCanvas;
    private bool isLocked = true;
    private TriggerWrapper talkOpenTrigger;



    private void Awake()
    {
        talkOpenTrigger = TriggerEventController.Instance.MomTalkOpen as TriggerWrapper;
    }




    private void OnEnable()
    {
        talkCanvas.SetActive(!isLocked);
        lockCanvas.SetActive(isLocked);
        wrongPinPhrase.SetActive(false);
        talkOpenTrigger.TriggerValue = !isLocked;
    }



    private void OnDisable()
    {
        talkOpenTrigger.TriggerValue = false;
    }



    private void Update()
    {
        if (isLocked && Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckPin();
        }

    }



    public void CheckPin()
    {
        if (!isLocked) return;
        TimeController.Instance.ProgressMinutes(GameData.PasswordCheckMinutes);
        TimeController.Instance.CheckTimeChanged();
        if (!pinInputField.text.Equals(pin))
        {
            pinInputField.text = "";
            wrongPinPhrase.SetActive(true);
            return;
        }
        isLocked = false;
        talkCanvas.SetActive(true);
        lockCanvas.SetActive(false);
        talkOpenTrigger.TriggerValue = true;
    }
}

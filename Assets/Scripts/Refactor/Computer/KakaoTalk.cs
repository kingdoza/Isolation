using TMPro;
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



    private void OnEnable()
    {
        talkCanvas.SetActive(!isLocked);
        lockCanvas.SetActive(isLocked);
        wrongPinPhrase.SetActive(false);
    }



    private void Update()
    {
        if (isLocked && Input.GetKeyDown(KeyCode.Return))
        {
            CheckPin();
        }

    }



    public void CheckPin()
    {
        if (!isLocked) return;
        if (!pinInputField.text.Equals(pin))
        {
            pinInputField.text = "";
            wrongPinPhrase.SetActive(true);
            return;
        }
        isLocked = false;
        talkCanvas.SetActive(true);
        lockCanvas.SetActive(false);
    }
}
